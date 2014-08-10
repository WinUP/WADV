Imports WADV
Imports System.Reflection

Namespace PluginInterface

    Public Class Script : Implements AppCore.Plugin.IScriptFunction

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements AppCore.Plugin.IScriptFunction.StartRegisting
            ScriptAPI.RegisterFunction(Reflection.Assembly.GetExecutingAssembly.GetTypes, "WPSoft.AVGCore.API", "AVG")
        End Sub

    End Class

End Namespace