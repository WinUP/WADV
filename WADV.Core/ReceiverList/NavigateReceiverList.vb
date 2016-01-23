Imports WADV.Core.PluginInterface
Imports WADV.Core.RAL

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
        Friend Sub Boardcast(e As NavigationParameter)
            UpdateRemove()
            UpdateAdd()
            List.ForEach(Sub(e1) e1.ReceiveNavigate(e))
        End Sub
    End Class
End Namespace
