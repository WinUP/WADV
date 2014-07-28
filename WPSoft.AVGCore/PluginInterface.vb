Imports WADV
Imports System.Reflection

Namespace PluginInterface

    Public Class Script : Implements AppCore.Plugin.IScriptFunction

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements AppCore.Plugin.IScriptFunction.StartRegisting
            ScriptAPI.RegisterFunction(Reflection.Assembly.GetExecutingAssembly.GetTypes, "WPSoft.AVGCore.API", "AVG")
        End Sub

    End Class

    Public Class Initialise : Implements AppCore.Plugin.IInitialise

        Public Function StartInitialising() As Boolean Implements AppCore.Plugin.IInitialise.StartInitialising
            WindowAPI.LoadElement(WindowAPI.GetGrid, "main1.xaml")
            WindowAPI.LoadElement(WindowAPI.GetGrid, "main2.xaml")
            WindowAPI.SetResizeMode(False)
            WindowAPI.SetBackgroundByHex("#000000")
            WindowAPI.SetIcon("icon.ico")
            Return True
        End Function

    End Class

End Namespace