Imports WADV.Core.PluginInterface

Namespace PluginInterface
    Friend NotInheritable Class LoopReceiver : Implements ILoopReceiver

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            PlayerList.CheckEnding()
            Return ModuleConfig.LoopOn
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
        End Sub

    End Class

End Namespace