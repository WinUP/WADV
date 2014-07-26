Imports System.Reflection

Namespace PluginInterface

    Public Class Initialise : Implements AppCore.Plugin.IInitialise

        Public Function StartInitialising() As Boolean Implements AppCore.Plugin.IInitialise.StartInitialising
            Config.SoundConfig.ReadConfigFile()
            Return True
        End Function

    End Class

    Public Class Script : Implements AppCore.Plugin.IScriptFunction

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements AppCore.Plugin.IScriptFunction.StartRegisting
            ScriptAPI.RegisterFunction(Reflection.Assembly.GetExecutingAssembly.GetTypes, "WADV.MediaModule.API", "MM")
        End Sub

    End Class

End Namespace