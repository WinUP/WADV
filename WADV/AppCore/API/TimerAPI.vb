Namespace AppCore.API

    ''' <summary>
    ''' 计时器API类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class TimerAPI

        ''' <summary>
        ''' 启动计时器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StartSync()
            MainTimer.GetInstance.Start()
        End Sub

        ''' <summary>
        ''' 停止计时器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopSync()
            MainTimer.GetInstance.Abort()
        End Sub

        ''' <summary>
        ''' 设计计时器的计时间隔
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="tick">目标间隔</param>
        ''' <remarks></remarks>
        Public Shared Sub SetTickSync(tick As Integer)
            MainTimer.GetInstance.Span = tick
        End Sub

        ''' <summary>
        ''' 获取计时器的计时间隔
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetTick() As Integer
            Return MainTimer.GetInstance.Span
        End Function

        ''' <summary>
        ''' 获取计时器的状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetStatus() As Boolean
            Return MainTimer.GetInstance.Status
        End Function

    End Class

End Namespace