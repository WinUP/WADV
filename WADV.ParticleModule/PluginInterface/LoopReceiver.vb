Imports WADV.Core.PluginInterface

Namespace PluginInterface
    Friend NotInheritable Class LoopReceiver : Implements ILoopReceiver
        Private Shared ReadOnly List As New List(Of ParticleSystem)
        Private Shared ReadOnly RemoveList As New List(Of ParticleSystem)

        ''' <summary>
        ''' 添加一个粒子系统
        ''' </summary>
        ''' <param name="target">要添加的粒子系统</param>
        Friend Shared Sub Add(target As ParticleSystem)
            If Not List.Contains(target) Then
                SyncLock (List)
                    List.Add(target)
                End SyncLock
            End If
        End Sub

        Public Sub Render() Implements ILoopReceiver.Render
            List.ForEach(Sub(e) e.UpdateRender())
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            Dim target = List.Where(Function(e) Not e.UpdateLogic)
            SyncLock (List)
                target.ToList.ForEach(Sub(e) List.Remove(e))
            End SyncLock
            Return True
        End Function
    End Class
End Namespace