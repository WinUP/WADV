Imports WADV.Core.PluginInterface

Namespace PluginInterface
    Friend NotInheritable Class PluginInitialise : Implements IPluginInitialise
        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            [Loop].Listen(New LoopReceiver)
            Message.Listen(New MessageReceiver)
            Message.Send("[SPRITE]LOAD_FINISH")
            Return True
        End Function
    End Class
End Namespace