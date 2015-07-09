Imports System.Windows.Navigation
Imports WADV.Core.PluginInterface

Namespace ReceiverList

    ''' <summary>
    ''' 转场接收器列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class NavigateReceiverList : Inherits BaseList(Of INavigateReceiver)

        ''' <summary>
        ''' 传递事件到所有已注册的转场接收器
        ''' </summary>
        ''' <param name="e">要传递的事件</param>
        ''' <remarks></remarks>
        Friend Shared Sub Boardcast(e As NavigatingCancelEventArgs)
            List.ForEach(Sub(e1) e1.ReceiveNavigate(e))
        End Sub
    End Class
End Namespace
