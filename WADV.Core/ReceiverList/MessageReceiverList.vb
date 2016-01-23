Imports WADV.Core.PluginInterface

Namespace ReceiverList

    ''' <summary>
    ''' 消息接收器列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class MessageReceiverList : Inherits BaseList(Of IMessageReceiver)

        ''' <summary>
        ''' 传递消息到所有已注册的消息接收器
        ''' </summary>
        ''' <param name="message">要传递的消息</param>
        ''' <remarks></remarks>
        Friend Sub Broadcast(message As GameSystem.Message)
            UpdateRemove()
            UpdateAdd()
            For Each receiver In List.Where(Function(e) e.ReceiverMask And message.Type <> 0)
                receiver.ReceiveMessage(message.Content)
            Next
        End Sub
    End Class
End Namespace
