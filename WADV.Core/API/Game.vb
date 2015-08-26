Imports System.Windows.Navigation
Imports WADV.Core.GameSystem

Namespace API
    ''' <summary>
    ''' 游戏系统API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Game
        ''' <summary>
        ''' 准备引擎核心启动数据
        ''' 同步方法|调用线程
        ''' </summary>
        Public Sub Prepare()
            If Config.IsSystemPrepared Then Throw New Exception.SystemMultiPreparedException
            Config.MessageService = New MessageService
            Config.MainLoop = New MainLoop
            Config.MainTimer = New MainTimer
            Config.IsSystemPrepared = True
        End Sub

        ''' <summary>
        ''' 加载Plugin目录下所有插件
        ''' 同步方法|调用线程
        ''' </summary>
        Public Sub LoadPlugins()
            PluginFunction.InitialiseAllPlugins()
        End Sub

        ''' <summary>
        ''' 启动游戏系统
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="baseWindow">游戏主窗口</param>
        ''' <param name="frameSpan">每帧间的时间间隔</param>
        ''' <param name="tick">计时器计时频率</param>
        ''' <remarks></remarks>
        Public Sub Start(baseWindow As NavigationWindow, Optional frameSpan As Integer = 40, Optional tick As Integer = 60000)
            Config.BaseWindow = baseWindow
            If Not Config.IsSystemPrepared Then Prepare()
            Config.MessageService.Start()
            If Not Config.IsPluginInitialised Then LoadPlugins()
            Config.MainTimer.Span = tick
            Config.MainTimer.Start()
            Config.MainLoop.Span = frameSpan
            Config.MainLoop.Start()
            ReceiverList.InitialiseReceiverList.InitialisingGame()
            [Loop].Listen(New Core.Component.ComponentLoopReceiver)
            Message.Listen(New Core.Component.ComponentMessageReceiver)
            Config.IsSystemRunning = True
            Config.MessageService.SendMessage("[SYSTEM]GAME_INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 停止游戏系统
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="e">要传递给游戏解构接收器的数据</param>
        ''' <remarks></remarks>
        Public Sub [Stop](e As ComponentModel.CancelEventArgs)
            ReceiverList.DestructReceiverList.DestructingGame(e)
            If Not e.Cancel Then
                Timer.[Stop]()
                [Loop].[Stop]()
                Message.[Stop]()
                Config.IsSystemRunning = False
            End If
        End Sub
    End Module
End Namespace
