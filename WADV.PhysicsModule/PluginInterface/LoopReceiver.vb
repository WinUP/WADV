Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Public Class LoopReceiver : Implements ILoopReceiver
        Private _previousTime As New DateTime

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            If Config.TargetPysicsWorld IsNot Nothing AndAlso Not Config.StopSimulation Then
                Dim timeNow = DateTime.Now
                Config.TargetPysicsWorld.UpdateStatus((timeNow - _previousTime).TotalSeconds)
                _previousTime = timeNow
                Return True
            Else
                Message.Send("[PHYSICS]SIMULATION_END")
                Return False
            End If
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
            If Config.TargetPysicsWorld IsNot Nothing Then
                Config.TargetPysicsWorld.UpdateRender()
            End If
        End Sub
    End Class
End Namespace
