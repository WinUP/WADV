Imports System.Windows.Controls
Imports WADV.Core.PluginInterface
Imports WADV.SpriteModule.API

Namespace PluginInterface

    Friend NotInheritable Class PluginInitialise : Implements IPluginInitialise

        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            LoopAPI.AddLoopSync(New PluginInterface.LoopReceiver)
            MessageAPI.AddSync(New PluginInterface.MessageReceiver)
            ScriptAPI.RegisterInTableSync("sprite", "init", New Action(Of String)(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("sprite", "new", New Func(Of String, Canvas)(AddressOf SpriteAPI.[New]))
            ScriptAPI.RegisterInTableSync("sprite", "show", New Func(Of String, Panel, Boolean)(AddressOf SpriteAPI.Show))
            ScriptAPI.RegisterInTableSync("sprite", "register", New Func(Of String, Panel, Boolean)(AddressOf SpriteAPI.Register))
            ScriptAPI.RegisterInTableSync("sprite", "get", New Func(Of String, Panel)(AddressOf SpriteAPI.Get))
            ScriptAPI.RegisterInTableSync("sprite", "effect", New Action(Of String, String, Boolean, Object())(AddressOf SpriteAPI.Effect))
            ScriptAPI.RegisterInTableSync("sprite", "delete", New Func(Of String, Boolean)(AddressOf SpriteAPI.Delete))
            ScriptAPI.RegisterInTableSync("sprite", "deleteObject", New Func(Of Panel, Boolean)(AddressOf SpriteAPI.Delete))
            ScriptAPI.RegisterInTableSync("sprite", "setLoopReceiver", New Action(Of String, Func(Of Object, Object, Object), Func(Of Object, Object))(AddressOf SpriteAPI.SetLoopReceiver))
            ScriptAPI.RegisterInTableSync("sprite", "setMessageReceiver", New Action(Of String, Func(Of Object, Object, Object))(AddressOf SpriteAPI.SetMessageReceiver))
            MessageAPI.SendSync("[SPRITE]LOAD_FINISH")
            Return True
        End Function

    End Class

End Namespace