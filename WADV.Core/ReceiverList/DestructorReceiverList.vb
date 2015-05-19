Imports System.ComponentModel
Imports WADV.Core.PluginInterface

Namespace ReceiverList

    ''' <summary>
    ''' 游戏关闭接收器列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class DestructorReceiverList : Inherits BaseList(Of IGameDestructorReceiver)

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
