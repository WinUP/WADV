Imports System.Windows
Imports WADV.Core.PluginInterface
Imports WADV.SpriteModule.Receiver

Namespace PluginInterface
    Friend NotInheritable Class MessageReceiver : Implements IMessageReceiver
        Private Shared ReadOnly List As New List(Of SpriteReceiverForMessage)
        Private ReadOnly _removeList As New List(Of SpriteReceiverForMessage)

        ''' <summary>
        ''' 添加一个精灵消息接收器
        ''' </summary>
        ''' <param name="receiver">接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Add(receiver As SpriteReceiverForMessage)
            If Not ContainsSprite(receiver.GetTarget) Then
                SyncLock (List)
                    List.Add(receiver)
                End SyncLock
            End If
        End Sub

        ''' <summary>
        ''' 确定指定精灵是否已具有消息接收器
        ''' </summary>
        ''' <param name="target">要检查的精灵</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function ContainsSprite(target As FrameworkElement) As Boolean
            Return List.Any(Function(e) e.TargetEquals(target))
        End Function

        ''' <summary>
        ''' 删除指定精灵的消息接收器
        ''' </summary>
        ''' <param name="target">要检查的精灵</param>
        ''' <remarks></remarks>
        Friend Shared Sub Remove(target As FrameworkElement)
            If ContainsSprite(target) Then
                Dim index = List.FindIndex(Function(e) e.TargetEquals(target))
                SyncLock (List)
                    List.RemoveAt(index)
                End SyncLock
            End If
        End Sub

        ''' <summary>
        ''' 删除指定的消息接收器
        ''' </summary>
        ''' <param name="target">要删除的接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Remove(target As SpriteReceiverForMessage)
            If List.Contains(target) Then
                SyncLock (List)
                    List.Remove(target)
                End SyncLock
            End If
        End Sub

        Public Sub ReceivingMessage(message As String) Implements IMessageReceiver.ReceivingMessage
            _removeList.ForEach(Sub(e) List.Remove(e))
            _removeList.Clear()
            SyncLock (List)
                For Each target In List.Where(Function(e) Not e.OnMessage(message))
                    _removeList.Add(target)
                Next
            End SyncLock
        End Sub
    End Class
End Namespace
