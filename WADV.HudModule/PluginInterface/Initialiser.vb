Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Public Class PluginInitialise : Implements IPluginInitialise

        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            LoopAPI.AddLoopSync(New LoopReceiver)
            MessageAPI.AddSync(New MessageReceiver)
            ScriptAPI.RegisterInTableSync("api_hud", "add", New Func(Of String, Hud, Boolean)(AddressOf API.HudAPI.Add), True)
            ScriptAPI.RegisterInTableSync("api_hud", "delete", New Func(Of String, Boolean, Boolean)(AddressOf API.HudAPI.Delete))
            MessageAPI.SendSync("[HUD]INIT_FINISH")
            Return True
        End Function

    End Class

End Namespace
