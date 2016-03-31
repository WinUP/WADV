Imports System.Windows.Controls
Imports Neo.IronLua
Imports WADV.ChoiceModule.API

Namespace PluginInterface
    Friend NotInheritable Class ScriptInitialise : Implements LuaSupport.IScriptInitialise
        Public Sub Register(vm As Lua, env As LuaGlobal) Implements LuaSupport.IScriptInitialise.Register
            Dim core As LuaTable = env("core")
            core("choice") = New LuaTable
            Dim table As LuaTable = core("choice")
            table("init") = New Action(Of String, String, Double, Integer)(AddressOf Ui.Init)
            table("setContent") = New Action(Of Panel)(AddressOf Ui.Content)
            table("setStyle") = New Action(Of String)(AddressOf Ui.Style)
            table("setMargin") = New Action(Of Double)(AddressOf Ui.Margin)
            table("setZIndex") = New Action(Of Integer)(AddressOf Ui.ZIndex)
            table("show") = New Func(Of Neo.IronLua.LuaTable, String, String, Integer, String, String)(AddressOf ApiForScript.Show)
            Message.Send("[CHOICE]INIT_LOAD_FINISH")
        End Sub
    End Class
End Namespace
