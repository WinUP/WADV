Namespace TextEffect

    Public Interface ITextEffect

        Structure SentenceInfo
            Public Text As String
            Public Speaker As String
        End Structure

        Function GetNext() As SentenceInfo

        Function IsRead() As Boolean

        Function IsAllOver() As Boolean

        Function IsSentenceOver() As Boolean

    End Interface

    Friend Class Initialiser
        ''' <summary>
        ''' 待实例化的文字效果列表
        ''' </summary>
        Protected Friend Shared EffectList As Dictionary(Of String, Type)

        ''' <summary>
        ''' 读取并缓存所有文字效果
        ''' </summary>
        Protected Friend Shared Sub LoadEffect()
            EffectList = New Dictionary(Of String, Type)
            Dim basePath As String = PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Resource, "TextEffect\")
            For Each tmpType In From assemble In (IO.Directory.GetFiles(basePath, "*.dll").Select(Function(file) Reflection.Assembly.LoadFrom(file)))
                                Select types = assemble.GetTypes.Where(Function(e) e.GetInterface("ITextEffect") IsNot Nothing)
                                From tmpType1 In types Select tmpType1
                EffectList.Add(tmpType.Name, tmpType)
            Next
        End Sub
    End Class

    Public MustInherit Class StandardEffect : Implements ITextEffect
        Private ReadOnly _text() As String
        Private ReadOnly _speaker() As String
        Private ReadOnly _textLength As Integer
        Private _sentence As String
        Private _sentenceSpeaker As String
        Private _sentenceLength As Integer
        Private _processLineIndex As Integer

        Public Sub New(text() As String, speaker() As String, isRead() As Boolean)
            _text = text
            _speaker = speaker
            _textLength = _text.Length
            Sentence = _text(0)
            SentenceSpeaker = _speaker(0)
            SentenceLength = Sentence.Length
            Me.IsRead = isRead
            _processLineIndex = 0
            AllOver = False
            SentenceOver = False
            MessageAPI.SendSync("TEXT_BASEEFFECT_DECLARE")
        End Sub

#Region "       表示当前句子的元素"

        ''' <summary>
        ''' 获取当前处理的句子
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Property Sentence As String
            Get
                Return _sentence
            End Get
            Private Set(value As String)
                _sentence = value
            End Set
        End Property

        ''' <summary>
        ''' 获取当前处理的句子的长度
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Property SentenceLength As Integer
            Get
                Return _sentenceLength
            End Get
            Private Set(value As Integer)
                _sentenceLength = value
            End Set
        End Property

        ''' <summary>
        ''' 获取当前句子的已读标记
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Property SentenceRead As Boolean
            Get
                Return IsRead(_processLineIndex)
            End Get
            Private Set(value As Boolean)
                IsRead(_processLineIndex) = value
            End Set
        End Property

        ''' <summary>
        ''' 获取当前处理的句子的讲话者
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Property SentenceSpeaker As String
            Get
                Return _sentenceSpeaker
            End Get
            Private Set(value As String)
                _sentenceSpeaker = value
            End Set
        End Property

#End Region

        ''' <summary>
        ''' 获取或设置整个对话的完成状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Property AllOver As Boolean

        ''' <summary>
        ''' 获取或设置当前句子的完成状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Property SentenceOver As Boolean

        ''' <summary>
        ''' 获取对话数组的内容
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected ReadOnly Property Text As String()
            Get
                Return _text
            End Get
        End Property

        ''' <summary>
        ''' 获取讲话者数组的内容
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected ReadOnly Property Speaker As String()
            Get
                Return _speaker
            End Get
        End Property

        ''' <summary>
        ''' 获取对话的已读标志
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Property IsRead As Boolean()

        ''' <summary>
        ''' 整个对话数组的长度
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected ReadOnly Property TextLength As Integer
            Get
                Return _textLength
            End Get
        End Property

        ''' <summary>
        ''' 移动到下一行
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub MoveNext()
            If _processLineIndex = TextLength Then Return
            SentenceRead = True
            _processLineIndex += 1
            If _processLineIndex = TextLength Then
                SentenceOver = True
                AllOver = True
                Return
            End If
            Sentence = Text(_processLineIndex)
            SentenceSpeaker = Speaker(_processLineIndex)
            SentenceLength = Sentence.Length
            SentenceOver = False
            MessageAPI.SendSync("TEXT_SENTENCE_OVER")
        End Sub

        Public MustOverride Function GetNext() As ITextEffect.SentenceInfo Implements ITextEffect.GetNext

        Public Function IsThisRead() As Boolean Implements ITextEffect.IsRead
            Return SentenceRead
        End Function

        Public Function IsAllOver() As Boolean Implements ITextEffect.IsAllOver
            Return AllOver
        End Function

        Public Function IsSentenceOver() As Boolean Implements ITextEffect.IsSentenceOver
            Return SentenceOver
        End Function

    End Class

End Namespace
