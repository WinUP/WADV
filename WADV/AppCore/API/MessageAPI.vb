Imports WADV.AppCore.PluginInterface

Namespace AppCore.API

    ''' <summary>
    ''' 消息API类
    ''' </summary>
    Public NotInheritable Class MessageAPI

        ''' <summary>
        ''' 添加一个接收器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Public Shared Sub AddSync(receiver As IMessageReceiver)
            MessageService.GetInstance.AddReceiver(receiver)
        End Sub

        ''' <summary>
        ''' 删除一个接收器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Public Shared Sub DeleteSync(receiver As IMessageReceiver)
            MessageService.GetInstance.RemoveReceiver(receiver)
        End Sub

        ''' <summary>
        ''' 发送一个消息
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="message">消息内容</param>
        Public Shared Sub SendSync(message As String)
            MessageService.GetInstance.SendMessage(message)
        End Sub

        ''' <summary>
        ''' 等待下一个指定消息的出现
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="message">消息内容</param>
        Public Shared Sub WaitSync(message As String)
            Dim waiter As New WaitReceiver
            waiter.WaitMessage(message)
        End Sub

        Public Shared Function LastMessage() As String
            Return New String(MessageService.LastMessage)
        End Function

    End Class

End Namespace