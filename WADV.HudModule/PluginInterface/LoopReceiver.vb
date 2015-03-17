Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Public Class LoopReceiver : Implements ILoopReceiver

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            For Each target As LoopHud In HudList.LoopHudList.Values
                target.Logic()
            Next
            Return True
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
            For Each target As LoopHud In HudList.LoopHudList.Values
                target.Render()
            Next
        End Sub

    End Class

End Namespace
