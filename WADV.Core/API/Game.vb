Imports WADV.Core.Exception
Imports WADV.Core.GameSystem
Imports WADV.Core.Render

Namespace API
    ''' <summary>
    ''' 游戏系统API
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Game
        ''' <summary>
        ''' 第一合奏：准备引擎核心启动数据<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL<br></br>
        ''' 异常：<br></br>
        '''  SystemMultiPreparedException
        ''' </summary>
        Public Shared Sub Chorus01_PrepareSystem()
            If Configuration.Status.IsSystemPrepared Then Throw New SystemMultiPreparedException
            Configuration.System.MessageService = New MessageService
            Configuration.System.MainLoop = New MainLoop
            Configuration.System.MainTimer = New MainTimer
            Configuration.Status.IsSystemPrepared = True
        End Sub

        ''' <summary>
        ''' 第二合奏：加载Plugin目录下所有插件<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL<br></br>
        ''' 异常：<br></br>
        '''  PluginsMultiInitialiseException
        ''' </summary>
        Public Shared Sub Chorus02_LoadPlugins()
            If Configuration.Status.IsPluginInitialised Then Throw New PluginsMultiInitialiseException
            PluginFunction.InitialiseAllPlugins()
        End Sub

        ''' <summary>
        ''' 第三合奏：启动游戏系统<br></br>
        ''' 该函数在前两个合奏未完成时会自动完成它们<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="baseWindow">游戏主窗口</param>
        ''' <param name="frameSpan">每帧间的时间间隔</param>
        ''' <param name="tick">计时器计时频率</param>
        Public Shared Sub Chorus03_Start(baseWindow As WindowBase, Optional frameSpan As Integer = 40, Optional tick As Integer = 60000)
            Configuration.System.BaseWindow = baseWindow
            If Not Configuration.Status.IsSystemPrepared Then Chorus01_PrepareSystem()
            Configuration.System.MessageService.Start()
            If Not Configuration.Status.IsPluginInitialised Then Chorus02_LoadPlugins()
            Configuration.System.MainTimer.Span = tick
            Configuration.System.MainTimer.Start()
            Configuration.System.MainLoop.Span = frameSpan
            Configuration.System.MainLoop.Start()
            ReceiverList.InitialiseReceiverList.InitialisingGame()
            [Loop].Listen(New Component.ComponentLoopReceiver)
            Message.Listen(New Component.ComponentMessageReceiver)
            Configuration.Status.IsSystemRunning = True
        End Sub

        ''' <summary>
        ''' 最终合奏：停止游戏系统<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL | GAME_CLOSE_CANCEL
        ''' </summary>
        ''' <param name="e">要传递给游戏解构接收器的数据</param>
        ''' <remarks></remarks>
        Public Shared Sub ChorusFF_Stop(e As ComponentModel.CancelEventArgs)
            ReceiverList.DestructReceiverList.DestructingGame(e)
            If Not e.Cancel Then
                Configuration.System.MainTimer.Stop()
                Configuration.System.MainLoop.Stop()
                Configuration.System.MessageService.Stop()
                Configuration.Status.IsSystemRunning = False
            Else
                Configuration.System.MessageService.SendMessage("[SYSTEM]GAME_CLOSE_CANCEL")
            End If
        End Sub

        ''' <summary>
        ''' 注册一个事件处理函数到指定对象的指定事件<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="target">要注册到的对象</param>
        ''' <param name="eventName">要注册的事件名称</param>
        ''' <param name="code">事件处理委托</param>
        ''' <remarks></remarks>
        Public Shared Sub Handle(target As Object, eventName As String, code As [Delegate])
            Dim targetEvent = target.GetType.GetEvent(eventName)
            If targetEvent Is Nothing Then Exit Sub
            targetEvent.AddEventHandler(target, code)
        End Sub

        ''' <summary>
        ''' 从指定对象的指定事件上解绑一个事件处理函数<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="target">要解绑的对象</param>
        ''' <param name="eventName">要解绑的事件名称</param>
        ''' <param name="code">事件处理委托</param>
        ''' <remarks></remarks>
        Public Shared Sub Unhandle(target As Object, eventName As String, code As [Delegate])
            Dim targetEvent = target.GetType.GetEvent(eventName)
            If targetEvent Is Nothing Then Exit Sub
            targetEvent.RemoveEventHandler(target, code)
        End Sub

        ''' <summary>
        ''' 获取游戏系统的主渲染窗口<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function Window() As WindowBase
            Return Configuration.System.BaseWindow
        End Function
    End Class
End Namespace
