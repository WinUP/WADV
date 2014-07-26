Imports WADV.AppCore
Imports System.Reflection
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows

Namespace PluginInterface

    Public Class Initialise : Implements Plugin.IInitialise

        Public Function StartInitialising() As Boolean Implements Plugin.IInitialise.StartInitialising
            Config.ModuleConfig.ReadConfigFile()
            Return True
        End Function

    End Class

    Public Class Script : Implements Plugin.IScriptFunction

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements AppCore.Plugin.IScriptFunction.StartRegisting
            ScriptAPI.RegisterFunction(Reflection.Assembly.GetExecutingAssembly.GetTypes, "WADV.ImageModule.API", "IM")
        End Sub
    End Class

    Public Class CustomizedLoop : Implements Plugin.ILooping
        Private effect As Effect.ImageEffect
        Private content As Panel
        Private brush As ImageBrush
        Private image As ImageSource

        Public Sub New(effect As Effect.ImageEffect, content As Panel)
            Me.effect = effect
            Me.content = content
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               brush = New ImageBrush
                                               content.Background = brush
                                           End Sub)

        End Sub

        Public Function StartLooping() As Boolean Implements Plugin.ILooping.StartLooping
            image = effect.GetNextImageState
            If effect.IsEffectComplete Then Return False
            Return True
        End Function

        Public Sub StartRendering(window As Window) Implements AppCore.Plugin.ILooping.StartRendering
            brush.ImageSource = image
            GC.Collect()
        End Sub

    End Class

End Namespace
