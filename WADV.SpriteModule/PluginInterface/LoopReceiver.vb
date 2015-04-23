Imports System.Windows.Controls
Imports WADV.Core.PluginInterface
Imports WADV.SpriteModule.Receiver

Namespace PluginInterface

    Public Class LoopReceiver : Implements ILoopReceiver
        Private Shared ReadOnly List As New Dictionary(Of Panel, ISpriteLoopReceiver)
        Private ReadOnly _removeList As New List(Of Panel)

        ''' <summary>
        ''' 对指定精灵添加一个循环接收器
        ''' </summary>
        ''' <param name="target">目标精灵</param>
        ''' <param name="receiver">接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Add(target As Panel, receiver As ISpriteLoopReceiver)
            If Not List.ContainsKey(target) Then
                SyncLock (List)
                    List.Add(target, receiver)
                End SyncLock
            End If
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            _removeList.ForEach(Sub(e) List.Remove(e))
            _removeList.Clear()
            SyncLock (List)
                For Each pair In List.Where(Function(e) Not e.Value.Logic(e.Key, frame))
                    _removeList.Add(pair.Key)
                Next
            End SyncLock
            Return True
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
            SyncLock (List)
                For Each pair In List.Where(Function(e) Not e.Value.Render(e.Key))
                    _removeList.Add(pair.Key)
                Next
            End SyncLock
        End Sub

    End Class

End Namespace
