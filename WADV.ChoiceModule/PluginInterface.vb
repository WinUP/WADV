Imports System.Reflection
Imports System.Windows

Namespace PluginInterface

    Public Class Script : Implements AppCore.Plugin.IScriptFunction

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements AppCore.Plugin.IScriptFunction.StartRegisting
            ScriptAPI.RegisterFunction(Reflection.Assembly.GetExecutingAssembly.GetTypes, "WADV.ChoiceModule.API", "CM")
        End Sub

    End Class

    Public Class Looping : Implements AppCore.Plugin.ILooping
        Private Style As Effect.StandardEffect

        Public Sub New(style As Effect.StandardEffect)
            Me.Style = style
        End Sub

        Public Function StartLooping() As Boolean Implements AppCore.Plugin.ILooping.StartLooping
            Dim returnData = Style.GetNextUIStyle
            If Config.DataConfig.Choice <> "" Then Return False
            If Not returnData AndAlso Config.DataConfig.Choice = "" Then Return False
            Return True
        End Function

        Public Sub StartRendering(window As Window) Implements AppCore.Plugin.ILooping.StartRendering
            Style.RenderingUI()
        End Sub

    End Class

End Namespace