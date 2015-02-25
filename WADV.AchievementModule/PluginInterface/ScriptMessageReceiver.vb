Imports Neo.IronLua
Imports WADV.AppCore.PluginInterface

Namespace PluginInterface

    Friend NotInheritable Class ScriptMessageReceiver : Implements IMessageReceiver
        Private ReadOnly _script As LuaChunk
        Private ReadOnly _env As LuaGlobal

        Public Sub New()
            _env = ScriptAPI.GetEnv
            _script = ScriptAPI.GetVm.CompileChunk(PathAPI.GetPath(PathType.Script, Config.ReceiverFileName), New LuaCompileOptions)
        End Sub

        Public Sub ReceivingMessage(message As String) Implements IMessageReceiver.ReceivingMessage
            _env.DoChunk(_script)
        End Sub

    End Class

End Namespace

