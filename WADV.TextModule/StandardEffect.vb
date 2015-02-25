Public MustInherit Class StandardEffect : Implements ITextEffect
    Private ReadOnly _text() As String
    Private ReadOnly _speaker() As String
    Private ReadOnly _isRead() As Boolean
    Private ReadOnly _textLength As Integer
    Private _sentence As String
    Private _sentenceSpeaker As String
    Private _sentenceLength As Integer
    Private _processLineIndex As Integer

    Public Sub New(text() As String, speaker() As String, isRead() As Boolean)
        _text = text
        _speaker = speaker
        _isRead = isRead
        _textLength = _text.Length
        Sentence = _text(0)
        SentenceSpeaker = _speaker(0)
        SentenceLength = Sentence.Length
        AllOver = False
        SentenceOver = False
        _processLineIndex = 0
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
            Return _isRead(_processLineIndex)
        End Get
        Private Set(value As Boolean)
            _isRead(_processLineIndex) = value
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
    Protected Property SentenceOver As Boolean

    ''' <summary>
    ''' 移动到下一行
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub MoveNext()
        SentenceRead = True
        _processLineIndex += 1
        If _processLineIndex >= _textLength Then
            SentenceOver = True
            AllOver = True
            Return
        End If
        Sentence = _text(_processLineIndex)
        SentenceSpeaker = _speaker(_processLineIndex)
        SentenceLength = Sentence.Length
        SentenceOver = True
        MessageAPI.SendSync("[TEXT]SENTENCE_OVER")
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
