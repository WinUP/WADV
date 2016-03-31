Imports Neo.IronLua
Imports WADV.Core.PluginInterface
Imports WADV.LuaSupport.Extension

Namespace PluginInterface
    Friend NotInheritable Class ScriptMessageReceiver : Implements IMessageReceiver
        Private ReadOnly _script As LuaChunk
        Private ReadOnly _env As LuaGlobal

        Public Sub New()
            _env = GetEnvironment()
            _script = Compile(Config.ReceiverFileName, New LuaCompileOptions)
        End Sub

        Public Sub ReceivingMessage(message As String) Implements IMessageReceiver.ReceiveMessage
            _env.DoChunk(_script)
        End Sub
    End Class
End Namespace

