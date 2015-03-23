Public Interface IEffect

    ReadOnly Property Sentence As String

    ReadOnly Property Speaker As String

    ReadOnly Property IsRead As Boolean

    ReadOnly Property VoiceFile As String

    ReadOnly Property IsSentenceOver As Boolean

    ReadOnly Property IsAllOver As Boolean

    Sub NextState()

End Interface
