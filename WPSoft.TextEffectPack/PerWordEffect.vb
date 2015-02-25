Imports WADV.TextModule

Public Class PerWordEffect : Inherits StandardEffect
    Private _currentProcessLength As Integer
    Private _finalSentence As ITextEffect.SentenceInfo

    Public Sub New(text() As String, speaker() As String, isRead() As Boolean)
        MyBase.New(text, speaker, isRead)
        _currentProcessLength = 1
        _finalSentence = New ITextEffect.SentenceInfo
        _finalSentence.Speaker = SentenceSpeaker
        _finalSentence.Text = ""
    End Sub

    Public Overrides Function GetNext() As ITextEffect.SentenceInfo
        SentenceOver = False
        _finalSentence.Speaker = SentenceSpeaker
        Dim tmpText = Sentence.Substring(0, _currentProcessLength)
        _currentProcessLength += 1
        _finalSentence.Text = tmpText
        If _currentProcessLength = SentenceLength + 1 Then
            MoveNext()
            _currentProcessLength = 1
        End If
        Return _finalSentence
    End Function

End Class
