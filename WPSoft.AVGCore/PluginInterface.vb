Imports WADV
Imports WADV.AppCore.Plugin

Namespace PluginInterface

    Public Class Script : Implements IScript

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements AppCore.Plugin.IScript.StartRegisting
            ScriptAPI.RegisterFunction(Reflection.Assembly.GetExecutingAssembly.GetTypes, "WPSoft.AVGCore.API", "AVG")
        End Sub

    End Class

End Namespace