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