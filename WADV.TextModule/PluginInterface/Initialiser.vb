Imports WADV.AppCore.PluginInterface
Imports WADV.TextModule.API

Namespace PluginInterface

    Friend NotInheritable Class Initialiser : Implements IInitialise

        Friend Function Initialising() As Boolean Implements IInitialise.Initialising
            ScriptAPI.RegisterInTableSync("api_text", "init", New Action(Of Integer, Integer, Boolean, Boolean)(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("api_text", "getWordFrame", New Func(Of Integer)(AddressOf ConfigAPI.GetWordFrame))
            ScriptAPI.RegisterInTableSync("api_text", "getSetenceFrame", New Func(Of Integer)(AddressOf ConfigAPI.GetSetenceFrame))
            ScriptAPI.RegisterInTableSync("api_text", "getIgnoreMode", New Func(Of Boolean)(AddressOf ConfigAPI.GetIgnoreMode))
            ScriptAPI.RegisterInTableSync("api_text", "getAutoMode", New Func(Of Boolean)(AddressOf ConfigAPI.GetAutoMode))
            ScriptAPI.RegisterInTableSync("api_text", "setWordFrame", New Action(Of Integer)(AddressOf ConfigAPI.SetWordFrame))
            ScriptAPI.RegisterInTableSync("api_text", "setSetenceFrame", New Action(Of Integer)(AddressOf ConfigAPI.SetSentenceFrame))
            ScriptAPI.RegisterInTableSync("api_text", "setIgnoreMode", New Action(Of Boolean)(AddressOf ConfigAPI.SetIgnoreMode))
            ScriptAPI.RegisterInTableSync("api_text", "setAutoMode", New Action(Of Boolean)(AddressOf ConfigAPI.SetAutoMode))
            ScriptAPI.RegisterInTableSync("api_text", "setTextArea", New Action(Of String)(AddressOf ConfigAPI.SetTextArea))
            ScriptAPI.RegisterInTableSync("api_text", "setSpeakerArea", New Action(Of String)(AddressOf ConfigAPI.SetSpeakerArea))
            ScriptAPI.RegisterInTableSync("api_text", "setMainArea", New Action(Of String)(AddressOf ConfigAPI.SetMainArea))
            ScriptAPI.RegisterInTableSync("api_text", "setVisibility", New Action(Of Boolean)(AddressOf ConfigAPI.SetVisibility))
            ScriptAPI.RegisterInTableSync("api_text", "registerEvent", New Action(AddressOf ConfigAPI.RegisterEvent))
            ScriptAPI.RegisterInTableSync("api_text", "unregisterEvent", New Action(AddressOf ConfigAPI.UnregisterEvent))
            ScriptAPI.RegisterInTableSync("api_text", "show", New Func(Of Neo.IronLua.LuaTable, String, Boolean)(AddressOf TextAPI.ShowByLua))
            MessageAPI.SendSync("[TEXT]INIT_LOAD_FINISH")
            Return True
        End Function

    End Class

End Namespace