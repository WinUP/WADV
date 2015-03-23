Imports Neo.IronLua

Public Structure Sentence
    Public Content As String
    Public Speaker As String
    Public IsRead As Boolean
    Public VoiceFile As String
End Structure

Friend NotInheritable Class SentenceList
    Private Shared ReadOnly List As New Dictionary(Of String, Sentence())

    Friend Shared Sub Add(name As String, target As Sentence())
        If Not Contains(name) Then
            List.Add(name, target)
            MessageAPI.SendSync("[TEXT]SENTENCE_ADD")
        End If
    End Sub

    Friend Shared Sub DirectAdd(name As String, target As Sentence())
        List.Add(name, target)
        MessageAPI.SendSync("[TEXT]SENTENCE_ADD")
    End Sub

    Friend Shared Function Contains(name As String)
        Return List.ContainsKey(name)
    End Function

    Friend Shared Sub Delete(name As String)
        If Contains(name) Then
            List.Remove(name)
            MessageAPI.SendSync("[TEXT]SENTENCE_DELETE")
        End If
    End Sub

    Friend Shared Sub DirectDelete(name As String)
        List.Remove(name)
        MessageAPI.SendSync("[TEXT]SENTENCE_DELETE")
    End Sub


    Friend Shared Function [Get](name As String) As Sentence()
        If Contains(name) Then
            Return List.Item(name)
        Else
            Return Nothing
        End If
    End Function

    Friend Shared Function Pop(name As String) As Sentence()
        If Contains(name) Then
            Dim target = List.Item(name)
            List.Remove(name)
            Return target
        Else
            Return Nothing
        End If
    End Function

    Friend Shared Function Export(name As String) As LuaTable
        If Not Contains(name) Then Return Nothing
        Dim target As New LuaTable
        For Each tmpItem In List(name)
            Dim tmpTarget As New LuaTable
            tmpTarget("content") = tmpItem.Content
            tmpTarget("speaker") = tmpItem.Speaker
            tmpTarget("isread") = tmpItem.IsRead
            tmpTarget("voice") = tmpItem.VoiceFile
            target.ArrayList.Add(tmpTarget)
        Next
        MessageAPI.SendSync("[TEXT]SENTENCE_EXPORT")
        Return target
    End Function

End Class
