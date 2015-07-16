Imports WADV.Core.PluginInterface

Namespace Component
    Friend Class ComponentMessageReceiver : Implements IMessageReceiver
        Private Shared ReadOnly List As New List(Of Component)
        Private Shared ReadOnly RemoveList As New List(Of Component)

        ''' <summary>
        ''' 令目标组件响应消息循环更新
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
        ''' 令目标组件不再响应消息循环更新
        ''' </summary>
        ''' <param name="target">目标组件</param>
        ''' <remarks></remarks>
        Friend Shared Sub Remove(target As Component)
            If List.Contains(target) Then
                SyncLock (List)
                    List.Remove(target)
                End SyncLock
            End If
        End Sub

        Public Sub ReceiveMessage(message As String) Implements IMessageReceiver.ReceiveMessage
            SyncLock (List)
                List.ForEach(Sub(e) e.MessageOnReceiver(message))
            End SyncLock
        End Sub
    End Class
End Namespace
