Imports WADV.AppCore.PluginInterface

Namespace AppCore.API

    ''' <summary>
    ''' 循环API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LoopingAPI

        ''' <summary>
        ''' 设置理想帧率
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="frame">新的次数</param>
        ''' <remarks></remarks>
        Public Shared Sub SetFrameSync(frame As Integer)
            If frame < 1 Then Throw New ValueUnavailableException("帧率最低不得低于1")
            LoopFunction.Frame = frame
        End Sub

        ''' <summary>
        ''' 获取理想帧率
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>循环次数</returns>
        ''' <remarks></remarks>
        Public Shared Function GetFrame() As Integer
            Return LoopFunction.Frame
        End Function

        ''' <summary>
        ''' 添加一个循环体
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Sub AddLoopSync(loopContent As ILoopReceiver)
            MainLoop.GetInstance.AddLoop(loopContent)
        End Sub

        ''' <summary>
        ''' 等待一个子循环的完成
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Sub WaitLoopSync(loopContent As ILoopReceiver)
            Dim receiver As New LoopFunction
            receiver.Wait(loopContent)
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
        ''' 标记游戏循环为进行状态
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StartMainLoopSync()
            MainLoop.GetInstance.Start()
        End Sub

        ''' <summary>
        ''' 标记游戏循环为停止状态
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopMainLoopSync()
            MainLoop.GetInstance.Abort()
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