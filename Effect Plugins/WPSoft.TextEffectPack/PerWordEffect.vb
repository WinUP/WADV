Imports WADV.TextModule

Public Class PerWordEffect : Inherits BaseEffect
    Private _currentSentence As String
    Private _isSentenceOver As Boolean
    Private _currentProcessLength As Integer
    Private _content As String


    Public Sub New(name As String)
        MyBase.New(name)
        _currentSentence = ""
        _isSentenceOver = False
        _currentProcessLength = 1
        _content = CurrentSentence.Content
    End Sub

    Public Overrides ReadOnly Property IsSentenceOver As Boolean
        Get
            Return _isSentenceOver
        End Get
    End Property

    Public Overrides ReadOnly Property Sentence As String
        Get
            Return _currentSentence
        End Get
    End Property

    Public Overrides Sub NextState()
        If _currentProcessLength = _content.Length + 1 Then
            NextLine()
            _currentProcessLength = 1
            _content = CurrentSentence.Content
            _isSentenceOver = False
        End If
        If IsAllOver Then Exit Sub
        _currentSentence = _content.Substring(0, _currentProcessLength)
        _currentProcessLength += 1
        If _currentProcessLength = _content.Length + 1 Then _isSentenceOver = True
    End Sub

End Class
