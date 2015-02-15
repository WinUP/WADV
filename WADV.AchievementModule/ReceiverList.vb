Public Interface IMessageAchievement

    Sub Refresh()

End Interface
Friend NotInheritable Class ReceiverList
    Private Shared _messageRevceiver As List(Of IMessageAchievement)

    Friend Shared Sub LoadReceiver()
        _messageRevceiver = New List(Of IMessageAchievement)
        If Config.ReceiverFolder = "" Then Return
        Dim basePath As String = PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Script, Config.ReceiverFolder)
        For Each tmpType In IO.Directory.GetFiles(basePath, "*.r.lua")
            ScriptMessageReceiverHost.ScriptList.Add(tmpType)
        Next
        If ScriptMessageReceiverHost.ScriptList.Count > 0 Then _messageRevceiver.Add(New ScriptMessageReceiverHost)
    End Sub

End Class

Friend NotInheritable Class ScriptMessageReceiverHost : Implements IMessageAchievement
    Friend Shared ReadOnly ScriptList As New List(Of String)

    Public Sub Refresh() Implements IMessageAchievement.Refresh
        For Each script In ScriptList
            ScriptAPI.RunFileSync(script)
        Next
    End Sub

End Class
