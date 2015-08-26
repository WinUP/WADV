Namespace API
    ''' <summary>
    ''' 计时器API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Timer
        ''' <summary>
        ''' 启动计时器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Start()
            Config.MainTimer.Start()
        End Sub

        ''' <summary>
        ''' 停止计时器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub [Stop]()
            Config.MainTimer.Stop()
        End Sub

        ''' <summary>
        ''' 获取或设置计时器的计时间隔
        ''' </summary>
        ''' <param name="value">目标间隔(毫秒)</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Tick(Optional value As Integer = -1) As Integer
            If value = -1 Then Return Config.MainTimer.Span
            If value < 1 Then Throw New Exception.TickOutOfRangeException
            Config.MainTimer.Span = value
            Return value
        End Function

        ''' <summary>
        ''' 获取计时器的状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Status() As Boolean
            Return Config.MainTimer.Status
        End Function
    End Module
End Namespace