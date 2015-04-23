Imports System.Windows.Controls
Imports WADV.Core.PluginInterface
Imports WADV.SpriteModule.Receiver

Namespace PluginInterface

    Public Class MessageReceiver : Implements IMessageReceiver
        Private Shared ReadOnly List As New Dictionary(Of Panel, ISpriteMessageReceiver)
        Private ReadOnly _removeList As New List(Of Panel)

        ''' <summary>
        ''' 对指定精灵添加一个消息接收器
        ''' </summary>
        ''' <param name="target">目标精灵</param>
        ''' <param name="receiver">接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Add(target As Panel, receiver As ISpriteMessageReceiver)
            If Not List.ContainsKey(target) Then
                SyncLock (List)
                    List.Add(target, receiver)
                End SyncLock
            End If
        End Sub

        Public Sub ReceivingMessage(message As String) Implements IMessageReceiver.ReceivingMessage
            _removeList.ForEach(Sub(e) List.Remove(e))
            _removeList.Clear()
            SyncLock (List)
                For Each pair In List.Where(Function(e) Not e.Value.ReceivingMessage(e.Key, message))
                    _removeList.Add(pair.Key)
                Next
            End SyncLock
        End Sub
    End Class
End Namespace
