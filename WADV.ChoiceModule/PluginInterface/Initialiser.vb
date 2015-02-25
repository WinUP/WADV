Imports System.Windows.Controls
Imports WADV.AppCore.PluginInterface
Imports WADV.ChoiceModule.API

Namespace PluginInterface

    Friend NotInheritable Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
            ScriptAPI.RegisterInTableSync("api_choice", "init", New Action(Of String, String, Double)(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("api_choice", "setContent", New Action(Of Panel)(AddressOf ConfigAPI.SetContent))
            ScriptAPI.RegisterInTableSync("api_choice", "setStyle", New Action(Of String)(AddressOf ConfigAPI.SetStyle))
            ScriptAPI.RegisterInTableSync("api_choice", "setMargin", New Action(Of Double)(AddressOf ConfigAPI.SetMargin))
            ScriptAPI.RegisterInTableSync("api_choice", "show", New Action(Of Neo.IronLua.LuaTable, String, String, Integer, String)(AddressOf ChoiceAPI.ShowByLua))
            MessageAPI.SendSync("[CHOICE]INIT_LOAD_FINISH")
            Return True
        End Function

    End Class

End Namespace
