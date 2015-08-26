Imports WADV.Core.Exception
Imports WADV.Core.PluginInterface
Imports WADV.Core.ReceiverList

Namespace API
    ''' <summary>
    ''' 循环API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module [Loop]
        ''' <summary>
        ''' 获取或设置理想执行周期(毫秒)
        ''' </summary>
        ''' <param name="value">理想执行周期的目标值(毫秒)，不需要设置的话不要传递数值</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Span(Optional value As Integer = -1) As Integer
            If value = -1 Then Return Config.MainLoop.Span
            If value < 1 Then Throw New FrameOutOfRangeException
            Config.MainLoop.Span = value
            Config.MessageService.SendMessage("[SYSTEM]LOOP_SPAN_CHANGE")
            Return value
        End Function

        ''' <summary>
        ''' 添加一个循环接收器
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Function Listen(loopContent As ILoopReceiver) As Boolean
            Return LoopReceiverList.Add(loopContent)
        End Function

        ''' <summary>
        ''' 等待一个循环接收器完成并退出
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Sub WaitLoop(loopContent As ILoopReceiver)
            While True
                Message.Wait("[SYSTEM]LOOP_CONTENT_REMOVE")
                If Not LoopReceiverList.Contains(loopContent) Then Exit While
            End While
        End Sub

        ''' <summary>
        ''' 将当前线程挂起指定帧数
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="count">要挂起的帧数</param>
        ''' <remarks></remarks>
        Public Sub WaitFrame(count As Integer)
            Dim waiter As New EmptyLoop(count)
            Listen(waiter)
            WaitLoop(waiter)
        End Sub

        ''' <summary>
        ''' 启动游戏循环
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Start()
            Config.MainLoop.Start()
        End Sub

        ''' <summary>
        ''' 终止游戏循环
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub [Stop]()
            Config.MainLoop.Stop()
        End Sub

        ''' <summary>
        ''' 获取当前的帧计数
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns></returns>
        Public Function TotalFrame() As Integer
            Return Config.MainLoop.CurrentFrame
        End Function

        ''' <summary>
        ''' 获取游戏循环的状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Status() As Boolean
            Return Config.MainLoop.Status
        End Function

        ''' <summary>
        ''' 将指定帧数转换为理想运行时间
        ''' </summary>
        ''' <param name="count">要转换的帧数目</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ToTime(count As Integer) As TimeSpan
            Dim day, hour, minute, second, millionsecond As Integer
            Dim currentFps = Config.MainLoop.Span
            day = count / (216000 * currentFps)
            count -= day * 216000 * currentFps
            hour = count / (3600 * currentFps)
            count -= hour * 3600 * currentFps
            minute = count / (60 * currentFps)
            count -= minute * 60 * currentFps
            second = count / currentFps
            count -= second * currentFps
            millionsecond = count * 1000 / currentFps
            Return New TimeSpan(day, hour, minute, second, millionsecond)
        End Function

        ''' <summary>
        ''' 将指定时间长度转换为理想帧数
        ''' </summary>
        ''' <param name="time">要转换的时间长度</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ToFrame(time As TimeSpan) As Integer
            Dim currentFps = Config.MainLoop.Span
            Return (time.Days * 216000 + time.Hours * 3600 + time.Minutes * 60 + time.Seconds) * currentFps + CInt((CDbl(time.Milliseconds) / 1000.0) * currentFps)
        End Function
    End Module
End Namespace