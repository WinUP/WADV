Imports WADV.TextModule.TextEffect

Public Class PerWordEffect : Inherits StandardEffect
    Private _currentProcessLength As Integer

    Public Sub New(text() As String, speaker() As String)
        MyBase.New(text, speaker)
        _currentProcessLength = 1
    End Sub

    Public Overrides Function GetNext() As ITextEffect.SentenceInfo
        Dim nextSentence As New ITextEffect.SentenceInfo With {.Speaker = "", .Text = ""}
        Dim tmpText = Sentence.Substring(0, _currentProcessLength)
        _currentProcessLength += 1
        nextSentence.Speaker = SentenceSpeaker
        nextSentence.Text = tmpText
        If _currentProcessLength = SentenceLength + 1 Then
            MoveNext()
            _currentProcessLength = 1
        End If
        Return nextSentence
    End Function

End Class
