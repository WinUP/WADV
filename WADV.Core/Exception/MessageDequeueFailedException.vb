Namespace Exception

    ''' <summary>
    ''' 表示从消息队列中获取消息失败的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class MessageDequeueFailedException : Inherits FrameworkException
        Public Sub New()
            MyBase.New("消息循环提取消息失败。", "MessageDequeueFailed")
        End Sub
    End Class
End Namespace