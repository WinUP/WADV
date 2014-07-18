Namespace TextEffect

    Public MustInherit Class StandardEffect

        Protected TextArray() As String
        Protected NextTextIndex As Integer = 0
        Protected ReadOver As Boolean = False
        Protected SentenceReadOver As Boolean = False

        Public ReadOnly Property IsReadOver As Boolean
            Get
                Return ReadOver
            End Get
        End Property

        Public ReadOnly Property IsSentenceReadOver As Boolean
            Get
                Return SentenceReadOver
            End Get
        End Property

        Public Sub New(text() As String)
            TextArray = text
        End Sub

        Public MustOverride Function GetNextString() As String

    End Class

    Public Class PerWordEffect : Inherits StandardEffect

        Private LastUsedText As String = ""

        Public Sub New(text() As String)
            MyBase.New(text)
        End Sub

        Public Overrides Function GetNextString() As String
            If NextTextIndex = TextArray.Length Then Return ""
            Dim tmpText = TextArray(NextTextIndex)
            If LastUsedText.Length = tmpText.Length - 1 Then
                LastUsedText = tmpText
                SentenceReadOver = True
            ElseIf LastUsedText = tmpText Then
                SentenceReadOver = False
                NextTextIndex += 1
                If NextTextIndex = TextArray.Length Then
                    ReadOver = True
                    Return ""
                End If
                LastUsedText = tmpText(0)
            Else
                LastUsedText = tmpText.Remove(LastUsedText.Length + 1)
            End If
            Return LastUsedText
        End Function

    End Class

End Namespace
