Imports WADV.Core.PluginInterface

Namespace PluginInterface
    Friend NotInheritable Class LoopReceiver : Implements ILoopReceiver
        Private ReadOnly _style As BaseProgress

        Public Sub New(style As BaseProgress)
            _style = style
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            Return _style.Logic()
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
            _style.Render()
        End Sub
    End Class
End Namespace
