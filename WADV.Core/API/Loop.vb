Imports WADV.Core.Exception
Imports WADV.Core.PluginInterface
Imports WADV.Core.ReceiverList

Namespace API
    ''' <summary>
    ''' 循环API
    ''' </summary>
    ''' <remarks></remarks>
    Public Class [Loop]
        ''' <summary>
        ''' 获取或设置理想执行周期(毫秒)<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL | LOOP_SPAN_CHANGE
        ''' </summary>
        ''' <param name="value">理想执行周期的目标值(毫秒)，不需要设置的话不要传递数值</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Span(Optional value As Integer = -1) As Integer
            If value = -1 Then Return Configuration.System.MainLoop.Span
            If value < 1 Then Throw New FrameOutOfRangeException
            Configuration.System.MainLoop.Span = value
            Configuration.System.MessageService.SendMessage("[SYSTEM]LOOP_SPAN_CHANGE")
            Return value
        End Function

        ''' <summary>
        ''' 添加一个循环接收器<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL | LOOP_CONTENT_ADD
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Function Listen(loopContent As ILoopReceiver) As Boolean
            Return LoopReceiverList.Add(loopContent)
        End Function

        ''' <summary>
        ''' 等待一个循环接收器完成并退出<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Sub WaitLoop(loopContent As ILoopReceiver)
            While True
                Message.Wait("[SYSTEM]LOOP_CONTENT_REMOVE")
                If Not LoopReceiverList.Contains(loopContent) Then Exit While
            End While
        End Sub

        ''' <summary>
        ''' 将当前线程挂起指定帧数<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL | LOOP_CONTENT_ADD(Loop.Listen)
        ''' </summary>
        ''' <param name="count">要挂起的帧数</param>
        ''' <remarks></remarks>
        Public Shared Sub WaitFrame(count As Integer)
            Dim waiter As New EmptyLoop(count)
            Listen(waiter)
            WaitLoop(waiter)
        End Sub

        ''' <summary>
        ''' 获取当前的帧计数<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function TotalFrame() As Integer
            Return Configuration.System.MainLoop.CurrentFrame
        End Function

        ''' <summary>
        ''' 获取或设置游戏循环的状态<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="value">是否启用游戏循环</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Status(Optional value As Object = Nothing) As Boolean
            If value Is Nothing Then Return Configuration.System.MainLoop.Status
            Dim data = DirectCast(value, Boolean)
            If data Then
                Configuration.System.MainLoop.Start()
            Else
                Configuration.System.MainLoop.Stop()
            End If
            Return data
        End Function

        ''' <summary>
        ''' 将指定帧数转换为理想运行时间<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="count">要转换的帧数目</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ToTime(count As Integer) As TimeSpan
            Dim day, hour, minute, second, millionsecond As Integer
            Dim currentFps = Configuration.System.MainLoop.Span
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
        ''' 将指定时间长度转换为理想帧数<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="time">要转换的时间长度</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ToFrame(time As TimeSpan) As Integer
            Dim currentFps = Configuration.System.MainLoop.Span
            Return (time.Days * 216000 + time.Hours * 3600 + time.Minutes * 60 + time.Seconds) * currentFps + CInt((CDbl(time.Milliseconds) / 1000.0) * currentFps)
        End Function
    End Class
End Namespace