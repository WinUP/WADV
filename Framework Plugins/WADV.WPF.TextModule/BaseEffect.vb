Public MustInherit Class BaseEffect
    Private _index As Integer
    Private ReadOnly _list() As Sentence
    Private ReadOnly _listLength As Integer
    Private _currentSetence As Sentence

    Public Sub New(name As String)
        _index = -1
        _list = SentenceList.Pop(name)
        _listLength = _list.Length
        NextLine()
    End Sub

    Protected ReadOnly Property CurrentSentence As Sentence
        Get
            Return _currentSetence
        End Get
    End Property

    Public ReadOnly Property IsAllOver As Boolean
        Get
            Return _index = _listLength
        End Get
    End Property

    Public ReadOnly Property IsRead As Boolean
        Get
            Return _currentSetence.IsRead
        End Get
    End Property

    Public ReadOnly Property Speaker As String
        Get
            Return _currentSetence.Speaker
        End Get
    End Property

    Public ReadOnly Property VoiceFile As String
        Get
            Return _currentSetence.VoiceFile
        End Get
    End Property

    Public MustOverride ReadOnly Property IsSentenceOver As Boolean

    Public MustOverride ReadOnly Property Sentence As String

    Protected Sub NextLine()
        _currentSetence.IsRead = True
        _index += 1
        If Not IsAllOver Then _currentSetence = _list(_index)
    End Sub

    Public MustOverride Sub NextState()
End Class
