Imports System.ComponentModel
Imports WADV.Core.PluginInterface

Namespace ReceiverList
    ''' <summary>
    ''' 游戏解构接收器列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class DestructReceiverList : Inherits BaseList(Of IGameDestructingReceiver)

        ''' <summary>
        ''' 传递事件到所有已注册的游戏关闭接收器
        ''' </summary>
        ''' <param name="e">要传递的事件</param>
        ''' <remarks></remarks>
        Friend Sub DestructingGame(e As CancelEventArgs)
            UpdateRemove()
            UpdateAdd()
            For Each receiver In List
                receiver.DestructGame(e)
            Next
        End Sub
    End Class
End Namespace
