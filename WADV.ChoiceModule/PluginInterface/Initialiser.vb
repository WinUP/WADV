Imports System.Windows.Controls
Imports WADV.Core.PluginInterface
Imports WADV.ChoiceModule.API

Namespace PluginInterface

    Friend NotInheritable Class PluginInitialise : Implements IPluginInitialise

        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            ScriptAPI.RegisterInTableSync("choice", "init", New Action(Of String, String, Double, Integer)(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("choice", "setContent", New Action(Of Panel)(AddressOf ConfigAPI.SetContent))
            ScriptAPI.RegisterInTableSync("choice", "setStyle", New Action(Of String)(AddressOf ConfigAPI.SetStyle))
            ScriptAPI.RegisterInTableSync("choice", "setMargin", New Action(Of Double)(AddressOf ConfigAPI.SetMargin))
            ScriptAPI.RegisterInTableSync("choice", "setZIndex", New Action(Of Integer)(AddressOf ConfigAPI.SetZIndex))
            ScriptAPI.RegisterInTableSync("choice", "show", New Func(Of Neo.IronLua.LuaTable, String, String, Integer, String, String)(AddressOf ChoiceAPI.ShowByLua))
            MessageAPI.SendSync("[CHOICE]INIT_LOAD_FINISH")
            Return True
        End Function

    End Class

End Namespace
