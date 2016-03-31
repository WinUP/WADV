Imports WADV.LuaSupport
Imports WADV.ParticleModule.API
Imports Neo.IronLua

Namespace PluginInterface
    Public Class ScriptInitialise : Implements IScriptInitialise
        Public Sub Register(vm As Lua, env As LuaGlobal) Implements IScriptInitialise.Register
            Dim core As LuaTable = env("core")
            core("particle") = New LuaTable
            Dim table As LuaTable = core("particle")
            table("init") = New Action(AddressOf Config.Init)
            table("new") = New Func(Of Boolean, ParticleSystem)(AddressOf Particle.[New])
        End Sub
    End Class
End Namespace