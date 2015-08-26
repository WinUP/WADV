Namespace Exception

    ''' <summary>
    ''' 表示从消息队列中获取消息失败的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MessageDequeueFailedException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("就在刚刚消息循环没能从消息队列获得消息。游戏引擎将被迫中止。")
        End Sub
    End Class
End Namespace