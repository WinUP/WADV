Imports System.Windows.Data
Imports WADV.Core.PluginInterface
Imports WADV.Core.ReceiverList

Namespace API

    ''' <summary>
    ''' 循环API类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class LoopAPI

        ''' <summary>
        ''' 设置理想执行周期
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="frame">执行周期(毫秒)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetFrameSync(frame As Integer)
            If frame < 1 Then Throw New ValueUnavailableException("理想执行周期最低不得低于1毫秒")
            MainLoop.GetInstance.Span = frame
            MessageService.GetInstance.SendMessage("[SYSTEM]LOOP_FRAME_CHANGE")
        End Sub

        ''' <summary>
        ''' 获取理想执行周期
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>执行周期</returns>
        ''' <remarks></remarks>
        Public Shared Function GetFrame() As Integer
            Return MainLoop.GetInstance.Span
        End Function

        ''' <summary>
        ''' 添加一个循环接收器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Sub AddLoopSync(loopContent As ILoopReceiver)
            LoopReceiverList.Add(loopContent)
        End Sub

        ''' <summary>
        ''' 等待一个循环接收器完成并退出
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Sub WaitLoopSync(loopContent As ILoopReceiver)
            While True
                MessageAPI.WaitSync("[SYSTEM]LOOP_CONTENT_REMOVE")
                If Not LoopReceiverList.Contains(loopContent) Then Exit While
            End While
        End Sub

        ''' <summary>
        ''' 将当前线程挂起指定帧数
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="count">要挂起的帧数</param>
        ''' <remarks></remarks>
        Public Shared Sub WaitFrameSync(count As Integer)
            Dim tmpWaiter As New EmptyLooping(count)
            AddLoopSync(tmpWaiter)
            WaitLoopSync(tmpWaiter)
        End Sub

        ''' <summary>
        ''' 启动游戏循环
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StartSync()
            MainLoop.GetInstance.Start()
        End Sub

        ''' <summary>
        ''' 终止游戏循环
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopSync()
            MainLoop.GetInstance.Stop()
        End Sub

        ''' <summary>
        ''' 获取当前的帧计数
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function CurrentFrame() As Integer
            Return MainLoop.GetInstance.CurrentFrame
        End Function

        ''' <summary>
        ''' 获取游戏循环的状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetStatus() As Boolean
            Return MainLoop.GetInstance.Status
        End Function

        ''' <summary>
        ''' 将指定帧数转换为理想运行时间
        ''' </summary>
        ''' <param name="count">要转换的帧数目</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function TranslateToTime(count As Integer) As TimeSpan
            Dim day, hour, minute, second, millionsecond As Integer
            Dim currentFps = GetFrame()
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
        Public Shared Function TranslateToFrame(time As TimeSpan) As Integer
            Dim currentFps = GetFrame()
            Return (time.Days * 216000 + time.Hours * 3600 + time.Minutes * 60 + time.Seconds) * currentFps + CInt((CDbl(time.Milliseconds) / 1000.0) * currentFps)
        End Function

    End Class

End Namespace