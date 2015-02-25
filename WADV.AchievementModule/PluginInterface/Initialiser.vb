Imports WADV.AchievementModule.API
Imports WADV.AppCore.PluginInterface

Namespace PluginInterface

    Friend NotInheritable Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
            ScriptAPI.RegisterInTableSync("api_achieve", "init", New Action(Of String, String, String)(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("api_achieve", "setStyle", New Action(Of String)(AddressOf ConfigAPI.SetStyle))
            ScriptAPI.RegisterInTableSync("api_achieve", "addAchieve", New Action(Of Achievement)(AddressOf AchieveAPI.Add))
            ScriptAPI.RegisterInTableSync("api_achieve", "getAchieve", New Func(Of String, Achievement)(AddressOf AchieveAPI.Get))
            ScriptAPI.RegisterInTableSync("api_achieve", "deleteAchieve", New Action(Of String)(AddressOf AchieveAPI.Delete))
            ScriptAPI.RegisterInTableSync("api_achieve", "achieveList", New Func(Of Achievement())(AddressOf AchieveAPI.List))
            ScriptAPI.RegisterInTableSync("api_achieve", "saveAchieve", New Action(AddressOf AchieveAPI.Save))
            ScriptAPI.RegisterInTableSync("api_achieve", "loadAchieve", New Action(AddressOf AchieveAPI.Load))
            ScriptAPI.RegisterInTableSync("api_achieve", "addProp", New Action(Of String)(AddressOf PropertyAPI.Add))
            ScriptAPI.RegisterInTableSync("api_achieve", "getProp", New Func(Of String, Integer)(AddressOf PropertyAPI.GetData))
            ScriptAPI.RegisterInTableSync("api_achieve", "setProp", New Action(Of String, Integer)(AddressOf PropertyAPI.SetData))
            ScriptAPI.RegisterInTableSync("api_achieve", "addProp", New Action(Of String, Integer)(AddressOf PropertyAPI.AddData))
            ScriptAPI.RegisterInTableSync("api_achieve", "register", New Action(Of String, Achievement)(AddressOf PropertyAPI.Register))
            ScriptAPI.RegisterInTableSync("api_achieve", "registerByName", New Action(Of String, String)(AddressOf PropertyAPI.RegisterByName))
            ScriptAPI.RegisterInTableSync("api_achieve", "unregister", New Action(Of String, Achievement)(AddressOf PropertyAPI.Unregister))
            ScriptAPI.RegisterInTableSync("api_achieve", "unregisterByName", New Action(Of String, String)(AddressOf PropertyAPI.UnregisterByName))
            ScriptAPI.RegisterInTableSync("api_achieve", "deleteProp", New Action(Of String)(AddressOf PropertyAPI.Delete))
            ScriptAPI.RegisterInTableSync("api_achieve", "saveProp", New Action(AddressOf PropertyAPI.Save))
            ScriptAPI.RegisterInTableSync("api_achieve", "loadProp", New Action(AddressOf PropertyAPI.Load))
            MessageAPI.SendSync("[ACHIEVE]INIT_LOAD_FINISH")
            Return True
        End Function

    End Class

End Namespace
