Imports WADV.AchievementModule.API
Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Friend NotInheritable Class PluginInitialise : Implements IPluginInitialise

        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            ScriptAPI.RegisterInTableSync("achieve", "init", New Action(Of String, String, String)(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("achieve", "setStyle", New Action(Of String)(AddressOf ConfigAPI.SetStyle))
            ScriptAPI.RegisterInTableSync("achieve", "addAchieve", New Action(Of Achievement)(AddressOf AchieveAPI.Add))
            ScriptAPI.RegisterInTableSync("achieve", "getAchieve", New Func(Of String, Achievement)(AddressOf AchieveAPI.Get))
            ScriptAPI.RegisterInTableSync("achieve", "deleteAchieve", New Action(Of String)(AddressOf AchieveAPI.Delete))
            ScriptAPI.RegisterInTableSync("achieve", "achieveList", New Func(Of Achievement())(AddressOf AchieveAPI.List))
            ScriptAPI.RegisterInTableSync("achieve", "saveAchieve", New Action(AddressOf AchieveAPI.Save))
            ScriptAPI.RegisterInTableSync("achieve", "loadAchieve", New Action(AddressOf AchieveAPI.Load))
            ScriptAPI.RegisterInTableSync("achieve", "addProp", New Action(Of String)(AddressOf PropertyAPI.Add))
            ScriptAPI.RegisterInTableSync("achieve", "getProp", New Func(Of String, Integer)(AddressOf PropertyAPI.GetData))
            ScriptAPI.RegisterInTableSync("achieve", "setProp", New Action(Of String, Integer)(AddressOf PropertyAPI.SetData))
            ScriptAPI.RegisterInTableSync("achieve", "addProp", New Action(Of String, Integer)(AddressOf PropertyAPI.AddData))
            ScriptAPI.RegisterInTableSync("achieve", "register", New Action(Of String, Achievement)(AddressOf PropertyAPI.Register))
            ScriptAPI.RegisterInTableSync("achieve", "registerByName", New Action(Of String, String)(AddressOf PropertyAPI.RegisterByName))
            ScriptAPI.RegisterInTableSync("achieve", "unregister", New Action(Of String, Achievement)(AddressOf PropertyAPI.Unregister))
            ScriptAPI.RegisterInTableSync("achieve", "unregisterByName", New Action(Of String, String)(AddressOf PropertyAPI.UnregisterByName))
            ScriptAPI.RegisterInTableSync("achieve", "deleteProp", New Action(Of String)(AddressOf PropertyAPI.Delete))
            ScriptAPI.RegisterInTableSync("achieve", "saveProp", New Action(AddressOf PropertyAPI.Save))
            ScriptAPI.RegisterInTableSync("achieve", "loadProp", New Action(AddressOf PropertyAPI.Load))
            MessageAPI.SendSync("[ACHIEVE]LOAD_FINISH")
            Return True
        End Function

    End Class

End Namespace
