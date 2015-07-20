Imports Neo.IronLua

Public Class ScriptInitialise : Implements WADV.LuaSupport.IScriptInitialise
    Public Sub Register(vm As Neo.IronLua.Lua, env As Neo.IronLua.LuaGlobal) Implements LuaSupport.IScriptInitialise.Register
        Dim core As LuaTable = env("core")
        core("sprite") = New LuaTable
        Dim table As LuaTable = core("sprite")
        table("init") = New Action(AddressOf API.Init)
        table("new") = New Func(Of Sprite)(AddressOf API.[New])
        Send("[SPRITE]LOAD_FINISH")
    End Sub
End Class
