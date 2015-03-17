Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Public Class PluginInitialise : Implements IPluginInitialise

        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            LoopAPI.AddLoopSync(New LoopReceiver)
            MessageAPI.AddSync(New MessageReceiver)
            Return True
        End Function

    End Class

End Namespace
