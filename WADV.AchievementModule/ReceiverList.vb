Imports WADV.AppCore.PluginInterface
Imports Neo.IronLua

Public Interface IMessageAchievement

    Sub Refresh(message As String)

End Interface

Friend NotInheritable Class ReceiverList
    Private Shared _receiver As New ScriptReceiver

    Friend Shared Sub RunReceiver()
        MessageAPI.AddSync(_receiver)
    End Sub

    Friend Shared Sub StopReceiver()
        MessageAPI.DeleteSync(_receiver)
    End Sub

End Class

Public NotInheritable Class ScriptReceiver : Implements IMessageReceiver
    Private ReadOnly _script As LuaChunk
    Private ReadOnly _env As LuaGlobal

    Public Sub New()
        _env = ScriptAPI.GetEnv
        _script = ScriptAPI.GetVm.CompileChunk(PathAPI.GetPath(PathType.Script, Config.FileName), New LuaCompileOptions)
    End Sub

    Public Sub ReceivingMessage(message As String) Implements IMessageReceiver.ReceivingMessage
        _env.DoChunk(_script)
    End Sub

End Class
