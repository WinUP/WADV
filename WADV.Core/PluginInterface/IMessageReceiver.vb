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

        ''' <summary>
        ''' 获取要接收的消息的Mask<br></br>
        ''' Mask是按位验证的，请将需要接收的类型的二进制位置为1，其余置为0
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property ReceiverMask As Integer
    End Interface
End Namespace
