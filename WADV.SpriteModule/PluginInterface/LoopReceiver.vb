Imports System.Windows
Imports WADV.Core.PluginInterface
Imports WADV.SpriteModule.Receiver

Namespace PluginInterface
    Friend NotInheritable Class LoopReceiver : Implements ILoopReceiver
        Private Shared ReadOnly List As New List(Of SpriteReceiverForLoop)
        Private ReadOnly _removeList As New List(Of SpriteReceiverForLoop)

        ''' <summary>
        ''' 添加一个精灵循环接收器
        ''' </summary>
        ''' <param name="receiver">接收器实例</param>
        ''' <remarks></remarks>
        Friend Shared Sub Add(receiver As SpriteReceiverForLoop)
            If Not ContainsSprite(receiver.GetTarget) Then
                SyncLock (List)
                    List.Add(receiver)
                End SyncLock
            End If
        End Sub

        ''' <summary>
        ''' 确定指定精灵是否已具有循环接收器
        ''' </summary>
        ''' <param name="target">要检查的精灵</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function ContainsSprite(target As FrameworkElement) As Boolean
            Return List.Any(Function(e) e.TargetEquals(target))
        End Function

        ''' <summary>
        ''' 删除指定精灵的循环接收器
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
        ''' 删除指定的循环接收器
        ''' </summary>
        ''' <param name="target">要删除的接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Remove(target As SpriteReceiverForLoop)
            If List.Contains(target) Then
                SyncLock (List)
                    List.Remove(target)
                End SyncLock
            End If
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            _removeList.ForEach(Sub(e) List.Remove(e))
            _removeList.Clear()
            SyncLock (List)
                For Each target In List.Where(Function(e) Not e.OnLogic(frame))
                    _removeList.Add(target)
                Next
            End SyncLock
            Return True
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
            SyncLock (List)
                For Each target In List.Where(Function(e) Not e.OnRender)
                    _removeList.Add(target)
                Next
            End SyncLock
        End Sub

    End Class

End Namespace
