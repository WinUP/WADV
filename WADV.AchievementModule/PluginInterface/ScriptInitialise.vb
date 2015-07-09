Imports Neo.IronLua

Namespace PluginInterface
    Friend NotInheritable Class ScriptInitialise : Implements LuaSupport.IScriptInitialise
        Public Sub Register(vm As Lua, env As LuaGlobal) Implements LuaSupport.IScriptInitialise.Register
            Dim core As LuaTable = env("core")
            core("achieve") = New LuaTable
            Dim table As LuaTable = core("achieve")
            table("init") = New Action(Of String, String, String)(AddressOf API.Init)
            table("setStyle") = New Action(Of String)(AddressOf API.SetStyle)
            table("newAchieve") = New Func(Of String, String, Func(Of LuaResult), Func(Of LuaResult), Achievement)(AddressOf API.ApiForScript.newAchievement)
            table("addAchieve") = New Action(Of Achievement)(AddressOf API.Achieve.Add)
            table("getAchieve") = New Func(Of String, Achievement)(AddressOf API.Get)
            table("deleteAchieve") = New Action(Of String)(AddressOf API.Achieve.Delete)
            table("achieveList") = New Func(Of Achievement())(AddressOf API.List)
            table("saveAchieve") = New Action(AddressOf API.Achieve.Save)
            table("loadAchieve") = New Action(AddressOf API.Achieve.Load)
            table("addProp") = New Action(Of String)(AddressOf API.AchievementProperty.Add)
            table("getProp") = New Func(Of String, Integer)(AddressOf API.GetData)
            table("setProp") = New Action(Of String, Integer)(AddressOf API.SetData)
            table("addProp") = New Action(Of String, Integer)(AddressOf API.AddData)
            table("register") = New Action(Of String, Achievement)(AddressOf API.Register)
            table("registerByName") = New Action(Of String, String)(AddressOf API.Register)
            table("unregister") = New Action(Of String, Achievement)(AddressOf API.Unregister)
            table("unregisterByName") = New Action(Of String, String)(AddressOf API.Unregister)
            table("deleteProp") = New Action(Of String)(AddressOf API.AchievementProperty.Delete)
            table("saveProp") = New Action(AddressOf API.AchievementProperty.Save)
            table("loadProp") = New Action(AddressOf API.AchievementProperty.Load)
            Message.Send("[ACHIEVE]LOAD_FINISH")
        End Sub
    End Class
End Namespace
