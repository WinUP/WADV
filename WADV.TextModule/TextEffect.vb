Namespace TextEffect

    Public MustInherit Class StandardEffect

        Protected TextArray() As String
        Protected CharacterArray() As String
        Protected NextTextIndex As Integer = 0
        Protected ReadOver As Boolean = False
        Protected SentenceReadOver As Boolean = False

        Public Structure SentenceInfo
            Public Character As String
            Public Content As String
        End Structure

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

        Public Sub New(text() As String, character() As String)
            TextArray = text
            CharacterArray = character
        End Sub

        Public MustOverride Function GetNextString() As SentenceInfo

    End Class

    Public Class PerWordEffect : Inherits StandardEffect

        Private LastUsedText As String = ""

        Public Sub New(text() As String, character() As String)
            MyBase.New(text, character)
        End Sub

        Public Overrides Function GetNextString() As SentenceInfo
            If NextTextIndex = TextArray.Length Then
                ReadOver = True
                SentenceReadOver = True
                Return New SentenceInfo With {.Character = "", .Content = ""}
            End If
            Dim tmpText = TextArray(NextTextIndex)
            If LastUsedText.Length = tmpText.Length - 1 Then
                LastUsedText = tmpText
                SentenceReadOver = True
            ElseIf LastUsedText = tmpText Then
                SentenceReadOver = False
                NextTextIndex += 1
                If NextTextIndex = TextArray.Length Then
                    SentenceReadOver = True
                    ReadOver = True
                    Return New SentenceInfo With {.Character = "", .Content = ""}
                End If
                LastUsedText = tmpText(0)
            Else
                LastUsedText = tmpText.Remove(LastUsedText.Length + 1)
            End If
            Return New SentenceInfo With {.Character = CharacterArray(NextTextIndex), .Content = LastUsedText}
        End Function

    End Class

    Public Class CodeEffect : Inherits TextEffect.StandardEffect

        Private LastUsedText As String = ""
        Private charIndex As Integer
        Private nextGenerate As NextGenerateType

        Private Enum NextGenerateType
            FirstCode
            SecondCode
            TextContent
        End Enum

        Public Sub New(text() As String, character() As String)
            MyBase.New(text, character)
            charIndex = 0
            nextGenerate = NextGenerateType.FirstCode
        End Sub

        Public Overrides Function GetNextString() As SentenceInfo
            If NextTextIndex = TextArray.Length Then
                ReadOver = True
                SentenceReadOver = True
                Return New SentenceInfo With {.Character = "", .Content = ""}
            End If
            Dim tmpText = TextArray(NextTextIndex)
            If charIndex = tmpText.Length - 1 Then
                If nextGenerate = NextGenerateType.FirstCode Then
                    LastUsedText &= GenerateCode()
                    nextGenerate = NextGenerateType.SecondCode
                ElseIf nextGenerate = NextGenerateType.SecondCode Then
                    LastUsedText &= GenerateCode()
                    nextGenerate = NextGenerateType.TextContent
                Else
                    LastUsedText = tmpText
                    charIndex += 1
                    SentenceReadOver = True
                    nextGenerate = NextGenerateType.FirstCode
                End If
            ElseIf LastUsedText = tmpText Then
                SentenceReadOver = False
                NextTextIndex += 1
                If NextTextIndex = TextArray.Length Then
                    SentenceReadOver = True
                    ReadOver = True
                    Return New SentenceInfo With {.Character = "", .Content = ""}
                End If
                LastUsedText = GenerateCode()
                charIndex = 0
                nextGenerate = NextGenerateType.SecondCode
            Else
                If nextGenerate = NextGenerateType.FirstCode Then
                    LastUsedText &= GenerateCode()
                    nextGenerate = NextGenerateType.SecondCode
                ElseIf nextGenerate = NextGenerateType.SecondCode Then
                    LastUsedText &= GenerateCode()
                    nextGenerate = NextGenerateType.TextContent
                Else
                    LastUsedText = tmpText.Remove(charIndex + 1)
                    charIndex += 1
                    nextGenerate = NextGenerateType.FirstCode
                End If
            End If
            Return New SentenceInfo With {.Character = CharacterArray(NextTextIndex), .Content = LastUsedText}
        End Function

        Private Function GenerateCode() As String
            Dim tmpRandom As New Random()
            Return "&" & tmpRandom.Next(1, 99).ToString
        End Function

    End Class

End Namespace
