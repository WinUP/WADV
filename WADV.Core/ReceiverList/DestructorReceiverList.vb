Imports System.ComponentModel
Imports WADV.Core.PluginInterface

Namespace ReceiverList

    ''' <summary>
    ''' 游戏关闭接收器列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class DestructorReceiverList
        Private Shared ReadOnly List As New List(Of IGameDestructorReceiver)

        ''' <summary>
        ''' 添加一个游戏关闭接收器
        ''' </summary>
        ''' <param name="target">要添加的游戏关闭接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Add(target As IGameDestructorReceiver)
            If Not Contains(target) Then List.Add(target)
        End Sub

        ''' <summary>
        ''' 确定指定游戏关闭接收器是否已存在
        ''' </summary>
        ''' <param name="content">要检查的游戏关闭接收器</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function Contains(content As IGameDestructorReceiver) As Boolean
            Return List.Contains(content)
        End Function

        ''' <summary>
        ''' 删除一个游戏关闭接收器
        ''' </summary>
        ''' <param name="content">目标游戏关闭接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Delete(content As IGameDestructorReceiver)
            If Contains(content) Then
                List.Remove(content)
            End If
        End Sub

        ''' <summary>
        ''' 传递事件到所有已注册的游戏关闭接收器
        ''' </summary>
        ''' <param name="e">要传递的事件</param>
        ''' <remarks></remarks>
        Friend Shared Sub DestructingGame(e As CancelEventArgs)
            For Each receiver In List
                receiver.DestructuringGame(e)
            Next
        End Sub

    End Class

End Namespace
