Imports WADV.TextModule

Public Class CodeEffect : Inherits StandardEffect
    Private _existSentencePart As String
    Private _currentProcessLength As Integer
    Private _generateType As NextGenerateType
    Private ReadOnly _randomGenerater As Random
    Private _finalSentence As ITextEffect.SentenceInfo

    ''' <summary>
    ''' 下一个要显示的文字类型
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum NextGenerateType
        Code
        Number
        Text
    End Enum

    Public Sub New(text() As String, speaker() As String, isRead() As Boolean)
        MyBase.New(text, speaker, isRead)
        _currentProcessLength = 1
        _generateType = NextGenerateType.Code
        _existSentencePart = ""
        _randomGenerater = New Random()
        _finalSentence = New ITextEffect.SentenceInfo
        _finalSentence.Speaker = SentenceSpeaker
        _finalSentence.Text = ""
    End Sub

    Public Overrides Function GetNext() As ITextEffect.SentenceInfo
        SentenceOver = False
        _finalSentence.Speaker = SentenceSpeaker
        Select Case _generateType
            Case NextGenerateType.Code
                _finalSentence.Text = _existSentencePart & "#"
                _generateType = NextGenerateType.Number
            Case NextGenerateType.Number
                _finalSentence.Text = _existSentencePart & "#" & _randomGenerater.Next(10, 99)
                _generateType = NextGenerateType.Text
            Case NextGenerateType.Text
                _existSentencePart = Sentence.Substring(0, _currentProcessLength)
                _finalSentence.Text = _existSentencePart
                _generateType = NextGenerateType.Code
                _currentProcessLength += 1
        End Select
        If _currentProcessLength = SentenceLength + 1 Then
            MoveNext()
            _currentProcessLength = 1
            _existSentencePart = ""
        End If
        Return _finalSentence
    End Function

End Class
