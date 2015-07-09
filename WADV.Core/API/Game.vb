Imports System.Windows.Navigation

Namespace API
    ''' <summary>
    ''' 游戏系统API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Game
        ''' <summary>
        ''' 启动游戏系统
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="baseWindow">游戏主窗口</param>
        ''' <param name="frameSpan">每帧间的时间间隔</param>
        ''' <param name="tick">计时器计时频率</param>
        ''' <remarks></remarks>
        Public Sub Start(baseWindow As NavigationWindow, frameSpan As Integer, tick As Integer)
            Config.BaseWindow = baseWindow
            MessageService.GetInstance.Start()
            PluginFunction.InitialiseAllPlugins()
            MainTimer.GetInstance.Span = tick
            MainTimer.GetInstance.Start()
            MainLoop.GetInstance.Span = frameSpan
            MainLoop.GetInstance.Start()
            ReceiverList.InitialiseReceiverList.InitialisingGame()
            MessageService.GetInstance.SendMessage("[SYSTEM]GAME_INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 停止游戏系统
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="e">要传递的事件</param>
        ''' <remarks></remarks>
        Public Sub [Stop](e As ComponentModel.CancelEventArgs)
            ReceiverList.DestructReceiverList.DestructingGame(e)
            If Not e.Cancel Then
                Timer.[Stop]()
                [Loop].[Stop]()
                Message.[Stop]()
            End If
        End Sub
    End Module
End Namespace
