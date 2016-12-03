Imports System.Threading

Namespace GameSystem
    ''' <summary>
    ''' 游戏计时器
    ''' </summary>
    Friend NotInheritable Class MainTimer
        Private ReadOnly _loopThread As Thread
        Private _status As Boolean = False
        Private Shared ReadOnly Instance As MainTimer = New MainTimer

        ''' <summary>
        ''' 获取计时器的唯一实例
        ''' </summary>
        ''' <returns></returns>
        Friend Shared Function GetInstance() As MainTimer
            Return Instance
        End Function

        ''' <summary>
        ''' 获得一个计时器实例
        ''' </summary>
        Private Sub New()
            _loopThread = New Thread(AddressOf TimerContent)
            _loopThread.IsBackground = True
            _loopThread.Name = "[系统]游戏计时线程"
            _loopThread.Priority = ThreadPriority.AboveNormal
        End Sub

        ''' <summary>
        ''' 获取计时器的状态
        ''' </summary>
        ''' <value>新的状态</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Property Status As Boolean
            Get
                Return _status
            End Get
            Set(value As Boolean)
                If value = _status Then Exit Property
                _status = value
                If value Then _loopThread.Start()
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置计时器两次计时之间的间隔(毫秒)
        ''' </summary>
        ''' <value>目标间隔</value>
        ''' <returns></returns>
        ''' <remarks>对计时器的间隔修改只能从下次计时开始生效</remarks>
        Friend Property Span As Integer

        ''' <summary>
        ''' 计时器主函数
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub TimerContent()
            While (_status)
                Configuration.System.MessageService.SendMessage("[SYSTEM]TIMER_TICK", 1)
                Thread.Sleep(Span)
            End While
        End Sub
    End Class
End Namespace