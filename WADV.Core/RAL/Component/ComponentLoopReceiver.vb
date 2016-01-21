Imports WADV.Core.PluginInterface

Namespace RAL.Component
    Friend Class ComponentLoopReceiver : Implements ILoopReceiver
        Private Shared ReadOnly List As New List(Of Component)
        Private Shared ReadOnly RemoveList As New List(Of Component)

        ''' <summary>
        ''' 令目标组件响应游戏循环更新
        ''' </summary>
        ''' <param name="target">目标组件</param>
        ''' <remarks></remarks>
        Friend Shared Sub Add(target As Component)
            If Not List.Contains(target) Then
                SyncLock (List)
                    List.Add(target)
                End SyncLock
            End If
        End Sub

        ''' <summary>
        ''' 令目标组件不再相应游戏循环更新
        ''' </summary>
        ''' <param name="target">目标组件</param>
        ''' <remarks></remarks>
        Friend Shared Sub Remove(target As Component)
            If List.Contains(target) Then
                SyncLock (RemoveList)
                    If Not RemoveList.Contains(target) Then RemoveList.Add(target)
                End SyncLock
            End If
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            SyncLock (RemoveList)
                RemoveList.ForEach(Sub(e) If List.Contains(e) Then List.Remove(e))
                RemoveList.Clear()
            End SyncLock
            Dim target = List.Where(Function(e) Not e.LoopOnLogic(frame))
            SyncLock (RemoveList)
                RemoveList.AddRange(target)
            End SyncLock
            Return True
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
            List.ForEach(Sub(e) e.LoopOnRender())
        End Sub
    End Class
End Namespace
