Namespace PluginInterface
    ''' <summary>
    ''' 消息循环接收器
    ''' </summary>
    Public Interface IMessageReceiver
        ''' <summary>
        ''' 执行一次消息处理
        ''' </summary>
        ''' <param name="message">消息标识符</param>
        Sub ReceiveMessage(message As String)
    End Interface
End Namespace
