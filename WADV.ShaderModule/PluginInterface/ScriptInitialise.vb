Imports System.Windows.Media.Effects
Imports Neo.IronLua

Namespace PluginInterface
    Friend NotInheritable Class ScriptInitialise : Implements LuaSupport.IScriptInitialise
        Public Sub Register(vm As Lua, env As LuaGlobal) Implements LuaSupport.IScriptInitialise.Register
            Dim core As LuaTable = env("core")
            core("shader") = New LuaTable
            Dim table As LuaTable = core("shader")
            table("init") = New Action(AddressOf API.Init)
            table("get") = New Func(Of String, ShaderEffect)(AddressOf API.[Get])
            Message.Send("[SHADER]LOAD_FINISH")
        End Sub
    End Class
End Namespace
