Imports WADV.Core.RAL
Imports WADV.Core.Exception
Imports WADV.Core.GameSystem

Namespace API
    ''' <summary>
    ''' 游戏系统API
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Game

        ''' <summary>
        ''' 启动游戏系统
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="baseWindow">游戏主窗口</param>
        ''' <param name="frameSpan">每帧间的时间间隔</param>
        ''' <param name="tick">计时器计时频率</param>
        Public Shared Sub Fire(baseWindow As WindowBase, Optional frameSpan As Integer = 40, Optional tick As Integer = 60000)
            If Configuration.Status.IsSystemRunning Then Throw New SystemMultiPreparedException
            Configuration.Receiver.InitialiserReceiver = New ReceiverList.InitialiseReceiverList
            Configuration.Receiver.PluginLoadingReceiver = New ReceiverList.PluginLoadReceiverList
            Configuration.Receiver.MessageReceiver = New ReceiverList.MessageReceiverList
            Configuration.Receiver.LoopReceiver = New ReceiverList.LoopReceiverList
            Configuration.Receiver.NavigateReceiver = New ReceiverList.NavigateReceiverList
            Configuration.Receiver.DestructReceiver = New ReceiverList.DestructReceiverList
            Configuration.System.MessageService = MessageService.GetInstance
            Configuration.System.MainLoop = MainLoop.GetInstance
            Configuration.System.MainTimer = MainTimer.GetInstance
            Configuration.System.MainWindow = baseWindow
            Configuration.System.MainTimer.Span = tick
            Configuration.System.MainLoop.Span = frameSpan
            Configuration.System.MessageService.Status = True
            Configuration.System.MainTimer.Status = True
            Configuration.System.MainLoop.Status = True
            PluginFunction.InitialiseAllPlugins()
            Configuration.Receiver.InitialiserReceiver.InitialisingGame()
            Configuration.Status.IsSystemRunning = True
        End Sub

        ''' <summary>
        ''' 停止游戏系统<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL | GAME_CLOSE_CANCEL
        ''' </summary>
        ''' <param name="e">要传递给游戏解构接收器的数据</param>
        ''' <remarks></remarks>
        Public Shared Sub Cut(e As ComponentModel.CancelEventArgs)
            Configuration.Receiver.DestructReceiver.DestructingGame(e)
            If Not e.Cancel Then
                Configuration.System.MainTimer.Status = False
                Configuration.System.MainLoop.Status = False
                Configuration.System.MessageService.Status = False
                Configuration.Status.IsSystemRunning = False
            Else
                Configuration.System.MessageService.SendMessage("[SYSTEM]GAME_CUT_CANCELED", 1)
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
            Return Configuration.System.MainWindow
        End Function
    End Class
End Namespace
