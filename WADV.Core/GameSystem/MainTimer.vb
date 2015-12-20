Imports System.Threading

Namespace GameSystem
    ''' <summary>
    ''' 游戏计时器
    ''' </summary>
    Friend NotInheritable Class MainTimer
        Private ReadOnly _loopThread As Thread

        ''' <summary>
        ''' 获得一个计时器实例
        ''' </summary>
        Friend Sub New()
            _loopThread = New Thread(AddressOf TimerContent)
            _loopThread.IsBackground = True
            _loopThread.Name = "[系统]游戏计时线程"
            _loopThread.Priority = ThreadPriority.AboveNormal
            Configuration.System.MessageService.SendMessage("[SYSTEM]TIMER_INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 获取计时器的状态
        ''' </summary>
        ''' <value>新的状态</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend ReadOnly Property Status As Boolean = False

        ''' <summary>
        ''' 获取或设置计时器两次计时之间的间隔(毫秒)
        ''' </summary>
        ''' <value>目标间隔</value>
        ''' <returns></returns>
        ''' <remarks>对计时器的间隔修改只能从下次计时开始生效</remarks>
        Friend Property Span As Integer

        ''' <summary>
        ''' 启动计时器（如果计时器正在运行则不进行任何操作）
        ''' </summary>
        ''' <remarks></remarks>
        Friend Sub Start()
            If Status Then Exit Sub
            _Status = True
            _loopThread.Start()
        End Sub

        ''' <summary>
        ''' 停止计时器（如果计时器本身并没有启动则不进行任何操作）
        ''' </summary>
        ''' <remarks></remarks>
        Friend Sub [Stop]()
            If Not Status Then Exit Sub
            _Status = False
            _loopThread.Abort()
        End Sub

        ''' <summary>
        ''' 计时器主函数
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub TimerContent()
            While (Status)
                Configuration.System.MessageService.SendMessage("[SYSTEM]TIMER_TICK")
                Thread.Sleep(Span)
            End While
        End Sub
    End Class
End Namespace