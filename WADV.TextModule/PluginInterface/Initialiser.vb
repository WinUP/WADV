Imports WADV.Core.PluginInterface
Imports WADV.TextModule.API

Namespace PluginInterface

    Friend NotInheritable Class PluginInitialise : Implements IPluginInitialise

        Friend Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            ScriptAPI.RegisterInTableSync("text", "init", New Action(Of Integer, Integer, Boolean, Boolean)(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("text", "getWordFrame", New Func(Of Integer)(AddressOf ConfigAPI.GetWordFrame))
            ScriptAPI.RegisterInTableSync("text", "getSetenceFrame", New Func(Of Integer)(AddressOf ConfigAPI.GetSetenceFrame))
            ScriptAPI.RegisterInTableSync("text", "getIgnoreMode", New Func(Of Boolean)(AddressOf ConfigAPI.GetIgnoreMode))
            ScriptAPI.RegisterInTableSync("text", "getAutoMode", New Func(Of Boolean)(AddressOf ConfigAPI.GetAutoMode))
            ScriptAPI.RegisterInTableSync("text", "setWordFrame", New Action(Of Integer)(AddressOf ConfigAPI.SetWordFrame))
            ScriptAPI.RegisterInTableSync("text", "setSetenceFrame", New Action(Of Integer)(AddressOf ConfigAPI.SetSentenceFrame))
            ScriptAPI.RegisterInTableSync("text", "setIgnoreMode", New Action(Of Boolean)(AddressOf ConfigAPI.SetIgnoreMode))
            ScriptAPI.RegisterInTableSync("text", "setAutoMode", New Action(Of Boolean)(AddressOf ConfigAPI.SetAutoMode))
            ScriptAPI.RegisterInTableSync("text", "setTextArea", New Action(Of String)(AddressOf ConfigAPI.SetTextArea))
            ScriptAPI.RegisterInTableSync("text", "setSpeakerArea", New Action(Of String)(AddressOf ConfigAPI.SetSpeakerArea))
            ScriptAPI.RegisterInTableSync("text", "setMainArea", New Action(Of String)(AddressOf ConfigAPI.SetMainArea))
            ScriptAPI.RegisterInTableSync("text", "setVisibility", New Action(Of Boolean)(AddressOf ConfigAPI.SetVisibility))
            ScriptAPI.RegisterInTableSync("text", "registerEvent", New Action(AddressOf ConfigAPI.RegisterEvent))
            ScriptAPI.RegisterInTableSync("text", "unregisterEvent", New Action(AddressOf ConfigAPI.UnregisterEvent))
            ScriptAPI.RegisterInTableSync("text", "show", New Action(Of String, String)(AddressOf TextAPI.Show))
            ScriptAPI.RegisterInTableSync("text", "addSentences", New Action(Of String, Neo.IronLua.LuaTable)(AddressOf TextAPI.AddSentences))
            MessageAPI.SendSync("[TEXT]LOAD_FINISH")
            Return True
        End Function

    End Class

End Namespace