Imports System.Windows.Controls
Imports WADV.AppCore.PluginInterface
Imports WADV.SpriteModule.API

Namespace PluginInterface

    Friend NotInheritable Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
            ScriptAPI.RegisterInTableSync("api_sprite", "init", New Action(Of String)(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("api_sprite", "new", New Func(Of String, Canvas)(AddressOf SpriteAPI.[New]))
            ScriptAPI.RegisterInTableSync("api_sprite", "show", New Func(Of String, Panel, Boolean)(AddressOf SpriteAPI.Show))
            ScriptAPI.RegisterInTableSync("api_sprite", "register", New Func(Of String, Panel, Boolean)(AddressOf SpriteAPI.Register))
            ScriptAPI.RegisterInTableSync("api_sprite", "get", New Func(Of String, Panel)(AddressOf SpriteAPI.Get))
            ScriptAPI.RegisterInTableSync("api_sprite", "effect", New Action(Of String, String, Boolean, Object())(AddressOf SpriteAPI.Effect))
            ScriptAPI.RegisterInTableSync("api_sprite", "delete", New Func(Of String, Boolean)(AddressOf SpriteAPI.Delete))
            MessageAPI.SendSync("[SPRITE]INIT_LOAD_FINISH")
            Return True
        End Function

    End Class

End Namespace