Public MustInherit Class BaseEffect : Implements IEffect
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

    Public ReadOnly Property IsAllOver As Boolean Implements IEffect.IsAllOver
        Get
            Return _index = _listLength
        End Get
    End Property

    Public ReadOnly Property IsRead As Boolean Implements IEffect.IsRead
        Get
            Return _currentSetence.IsRead
        End Get
    End Property

    Public ReadOnly Property Speaker As String Implements IEffect.Speaker
        Get
            Return _currentSetence.Speaker
        End Get
    End Property

    Public ReadOnly Property VoiceFile As String Implements IEffect.VoiceFile
        Get
            Return _currentSetence.VoiceFile
        End Get
    End Property

    Public MustOverride ReadOnly Property IsSentenceOver As Boolean Implements IEffect.IsSentenceOver

    Public MustOverride ReadOnly Property Sentence As String Implements IEffect.Sentence

    Protected Sub NextLine()
        _currentSetence.IsRead = True
        _index += 1
        If Not IsAllOver Then _currentSetence = _list(_index)
    End Sub

    Public MustOverride Sub NextState() Implements IEffect.NextState

End Class
