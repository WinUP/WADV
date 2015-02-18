Public Interface IMessageAchievement

    Sub Refresh(message As String)

End Interface

Friend NotInheritable Class ReceiverList
    Private Shared _receiver As ScriptReceiver

    Friend Shared Sub LoadReceiver()
        _receiver = New ScriptReceiver
        Dim basePath As String = PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Script, Config.ReceiverFolder)
        For Each tmpType In IO.Directory.GetFiles(basePath, "*.r.lua")
            _receiver.ScriptList.Add(tmpType)
        Next
    End Sub

    Friend Shared Sub RunReceiver()
        MessageAPI.AddSync(_receiver)
    End Sub

    Friend Shared Sub StopReceiver()
        MessageAPI.DeleteSync(_receiver)
    End Sub

End Class

Friend NotInheritable Class ScriptReceiver : Implements AppCore.Plugin.IMessageReceiver
    Friend ReadOnly ScriptList As List(Of String)
    Private ReadOnly _vm As NLua.Lua

    Public Sub New()
        ScriptList = New List(Of String)
        _vm = ScriptAPI.GetVm
    End Sub

    Public Sub ReceivingMessage(message As String) Implements AppCore.Plugin.IMessageReceiver.ReceivingMessage
        _vm.DoString("lastMessage=" & message)
        For Each script In ScriptList
            _vm.DoFile(script)
        Next
    End Sub
End Class
