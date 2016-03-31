Imports System.Reflection
Imports System.Windows

Namespace PluginInterface

    Public Class Initialiser : Implements AppCore.Plugin.IInitialise

        Public Function Initialising() As Boolean Implements AppCore.Plugin.IInitialise.Initialising
            Effect.Initialiser.LoadEffect()
            MessageAPI.SendSync("CHOICE_INIT_FINISH")
            Return True
        End Function

    End Class

    Public Class Looping : Implements AppCore.Plugin.ILoopReceiver
        Private Style As Effect.IEffect

        Public Sub New(style As Effect.IEffect)
            Me.Style = style
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements AppCore.Plugin.ILoopReceiver.Logic
            Return Style.NextState()
        End Function

        Public Sub Render(window As Window) Implements AppCore.Plugin.ILoopReceiver.Render
            Style.Render()
        End Sub

    End Class

End Namespace