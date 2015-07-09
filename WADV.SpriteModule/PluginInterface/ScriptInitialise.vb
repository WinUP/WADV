Imports System.Windows
Imports System.Windows.Controls
Imports Neo.IronLua

Namespace PluginInterface
    Friend NotInheritable Class ScriptInitialise : Implements LuaSupport.IScriptInitialise
        Public Sub Register(vm As Lua, env As LuaGlobal) Implements LuaSupport.IScriptInitialise.Register
            Dim core As LuaTable = env("core")
            core("sprite") = New LuaTable
            Dim table As LuaTable = core("sprite")
            table("init") = New Action(AddressOf API.Init)
            table("new") = New Func(Of String, Canvas)(AddressOf API.[New])
            table("register") = New Func(Of String, FrameworkElement, FrameworkElement)(AddressOf API.Register)
            table("effect") = New Action(Of String, String, Boolean, Object())(AddressOf API.Effect)
            table("get") = New Func(Of String, FrameworkElement)(AddressOf API.Get)
            table("getSprite") = New Func(Of FrameworkElement, Sprite)(AddressOf API.GetSprite)
            table("delete") = New Func(Of String, Boolean)(AddressOf API.Delete)
            table("deleteObject") = New Func(Of FrameworkElement, Boolean)(AddressOf API.Delete)
        End Sub
    End Class
End Namespace
