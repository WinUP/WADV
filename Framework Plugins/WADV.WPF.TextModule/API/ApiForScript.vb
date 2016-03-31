Imports Neo.IronLua

Namespace API
    Friend NotInheritable Class ApiForScript
        Public Shared Sub Add(name As String, content As LuaTable)
            If SentenceList.Contains(name) Then Exit Sub
            Dim tmpArray As New List(Of Sentence)
            For Each record As LuaTable In content.ArrayList
                Dim target As Sentence
                target.Content = record("content")
                target.Speaker = record("speaker")
                target.IsRead = record("isread")
                target.VoiceFile = record("voice")
                tmpArray.Add(target)
            Next
            SentenceList.DirectAdd(name, tmpArray.ToArray)
        End Sub
    End Class
End Namespace
