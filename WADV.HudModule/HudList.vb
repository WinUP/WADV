Imports System.Collections.Concurrent

Friend NotInheritable Class HudList
    Friend Shared ReadOnly MessageHudList As New ConcurrentDictionary(Of String, Object)
    Friend Shared ReadOnly LoopHudList As New ConcurrentDictionary(Of String, Object)

    Friend Shared Function Add(name As String, target As Hud)
        If target.ReceiverType = ReceiverType.LoopOnly Then
            If LoopHudList.ContainsKey(name) Then Return False
            LoopHudList.TryAdd(name, target)
        ElseIf target.ReceiverType = ReceiverType.MessageOnly Then
            If MessageHudList.ContainsKey(name) Then Return False
            MessageHudList.TryAdd(name, target)
        Else
            Return False
        End If
        Return True
    End Function

    Friend Shared Function Delete(name As String, type As ReceiverType) As Boolean
        Dim target As Hud = Nothing
        If type = ReceiverType.LoopOnly AndAlso LoopHudList.ContainsKey(name) Then
            Return LoopHudList.TryRemove(name, target)
        ElseIf type = ReceiverType.MessageOnly AndAlso MessageHudList.ContainsKey(name) Then
            Return MessageHudList.TryRemove(name, target)
        End If
        Return False
    End Function

End Class
