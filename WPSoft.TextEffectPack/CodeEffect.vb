Imports WADV.TextModule.TextEffect

Public Class CodeEffect : Inherits StandardEffect
    Private _existSentencePart As String
    Private _currentProcessLength As Integer
    Private _generateType As NextGenerateType
    Private ReadOnly _randomGenerater As Random

    ''' <summary>
    ''' 下一个要显示的文字类型
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum NextGenerateType
        Code
        Number
        Text
    End Enum

    Public Sub New(text() As String, speaker() As String, isRead() As String)
        MyBase.New(text, speaker, isRead)
        _currentProcessLength = 1
        _generateType = NextGenerateType.Code
        _existSentencePart = ""
        _randomGenerater = New Random(DateTime.Now.Ticks)
    End Sub

    Public Overrides Function GetNext() As ITextEffect.SentenceInfo
        Dim nextSentence As New ITextEffect.SentenceInfo With {.Speaker = "", .Text = ""}
        Select Case _generateType
            Case NextGenerateType.Code
                nextSentence.Text = _existSentencePart & "#"
                _generateType = NextGenerateType.Number
            Case NextGenerateType.Number
                nextSentence.Text = _existSentencePart & "#" & _randomGenerater.Next(100, 999)
                _generateType = NextGenerateType.Text
            Case NextGenerateType.Text
                _existSentencePart = Sentence.Substring(0, _currentProcessLength)
                _generateType = NextGenerateType.Code
                _currentProcessLength += 1
        End Select
        If _currentProcessLength = SentenceLength + 1 Then
            MoveNext()
            _currentProcessLength = 1
            _existSentencePart = ""
        End If
        Return nextSentence
    End Function

End Class
