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
        Friend Shared Sub Broadcast(ByRef message As String)
            For Each receiver In List
                receiver.ReceivingMessage(message)
            Next
        End Sub
    End Class
End Namespace
