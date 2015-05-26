Imports Neo.IronLua
Imports WADV.Core.PluginInterface

Namespace PluginInterface
    Friend NotInheritable Class PluginInitialise : Implements IPluginInitialise
        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            ScriptAPI.RegisterInTableSync("achieve", "init", New Action(Of String, String, String)(AddressOf API.Init), True)
            ScriptAPI.RegisterInTableSync("achieve", "setStyle", New Action(Of String)(AddressOf API.SetStyle))
            ScriptAPI.RegisterInTableSync("achieve", "newAchieve", New Func(Of String, String, Func(Of LuaResult), Func(Of LuaResult), Achievement)(AddressOf API.ApiForScript.newAchievement))
            ScriptAPI.RegisterInTableSync("achieve", "addAchieve", New Action(Of Achievement)(AddressOf API.Achieve.Add))
            ScriptAPI.RegisterInTableSync("achieve", "getAchieve", New Func(Of String, Achievement)(AddressOf API.Get))
            ScriptAPI.RegisterInTableSync("achieve", "deleteAchieve", New Action(Of String)(AddressOf API.Achieve.Delete))
            ScriptAPI.RegisterInTableSync("achieve", "achieveList", New Func(Of Achievement())(AddressOf API.List))
            ScriptAPI.RegisterInTableSync("achieve", "saveAchieve", New Action(AddressOf API.Achieve.Save))
            ScriptAPI.RegisterInTableSync("achieve", "loadAchieve", New Action(AddressOf API.Achieve.Load))
            ScriptAPI.RegisterInTableSync("achieve", "addProp", New Action(Of String)(AddressOf API.AchievementProperty.Add))
            ScriptAPI.RegisterInTableSync("achieve", "getProp", New Func(Of String, Integer)(AddressOf API.GetData))
            ScriptAPI.RegisterInTableSync("achieve", "setProp", New Action(Of String, Integer)(AddressOf API.SetData))
            ScriptAPI.RegisterInTableSync("achieve", "addProp", New Action(Of String, Integer)(AddressOf API.AddData))
            ScriptAPI.RegisterInTableSync("achieve", "register", New Action(Of String, Achievement)(AddressOf API.Register))
            ScriptAPI.RegisterInTableSync("achieve", "registerByName", New Action(Of String, String)(AddressOf API.Register))
            ScriptAPI.RegisterInTableSync("achieve", "unregister", New Action(Of String, Achievement)(AddressOf API.Unregister))
            ScriptAPI.RegisterInTableSync("achieve", "unregisterByName", New Action(Of String, String)(AddressOf API.Unregister))
            ScriptAPI.RegisterInTableSync("achieve", "deleteProp", New Action(Of String)(AddressOf API.AchievementProperty.Delete))
            ScriptAPI.RegisterInTableSync("achieve", "saveProp", New Action(AddressOf API.AchievementProperty.Save))
            ScriptAPI.RegisterInTableSync("achieve", "loadProp", New Action(AddressOf API.AchievementProperty.Load))
            MessageAPI.SendSync("[ACHIEVE]LOAD_FINISH")
            Return True
        End Function
    End Class
End Namespace
