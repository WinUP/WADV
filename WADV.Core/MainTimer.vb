Imports System.Threading

''' <summary>
''' 游戏计时器
''' </summary>
Friend NotInheritable Class MainTimer
    Private Shared _self As MainTimer
    Private ReadOnly _loopThread As Thread
    Private ReadOnly _messager As MessageService

    Private Sub New()
        Status = False
        Span = 60000
        _loopThread = New Thread(AddressOf TimerContent)
        _loopThread.IsBackground = True
        _loopThread.Name = "[系统]游戏计时线程"
        _loopThread.Priority = ThreadPriority.AboveNormal
        _messager = MessageService.GetInstance
        _messager.SendMessage("[SYSTEM]TIMER_INIT_FINISH")
    End Sub

    ''' <summary>
    ''' 获取或设置计时器的状态
    ''' </summary>
    ''' <value>新的状态</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property Status As Boolean

    ''' <summary>
    ''' 获取或设置计时器两次计时之间的间隔(毫秒)
    ''' </summary>
    ''' <value>目标间隔</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property Span As Integer '!注意：对计时器的间隔修改只能从下次计时开始生效

    ''' <summary>
    ''' 获取计时器的唯一实例
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function GetInstance() As MainTimer
        If _self Is Nothing Then _self = New MainTimer
        Return _self
    End Function

    ''' <summary>
    ''' 启动计时器
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Start()
        If Not Status Then
            Status = True
            _loopThread.Start()
        End If
    End Sub

    ''' <summary>
    ''' 停止计时器
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub [Stop]()
        If Status Then
            Status = False
            _loopThread.Abort()
        End If
    End Sub

    ''' <summary>
    ''' 计时器线程
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TimerContent()
        While (Status)
            _messager.SendMessage("[SYSTEM]TIMER_TICK")
            Thread.Sleep(Span)
        End While
    End Sub
End Class