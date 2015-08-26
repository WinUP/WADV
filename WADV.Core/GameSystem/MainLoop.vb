Imports System.Threading
Imports WADV.Core.PluginInterface
Imports WADV.Core.ReceiverList

Namespace GameSystem
    ''' <summary>
    ''' 游戏循环
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class MainLoop
        Private _loopListCount As Integer
        Private _frameCount As Integer
        Private _span As Integer
        Private ReadOnly _loopThread As Thread
        Private ReadOnly _renderDelegate As New Action(Of ILoopReceiver)(Sub(tmploop As ILoopReceiver) tmploop.Render())

        ''' <summary>
        ''' 获得一个游戏循环实例
        ''' </summary>
        Friend Sub New()
            _loopThread = New Thread(AddressOf LoopingContent)
            _loopThread.IsBackground = True
            _loopThread.Name = "[系统]游戏循环线程"
            _loopThread.Priority = ThreadPriority.AboveNormal
            Config.MessageService.SendMessage("[SYSTEM]LOOP_INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 获取逻辑循环的状态
        ''' </summary>
        ''' <value>新的状态</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend ReadOnly Property Status As Boolean = False

        ''' <summary>
        ''' 获取或设置两次逻辑循环间的时间间隔(毫秒)
        ''' </summary>
        ''' <value>新的间隔</value>
        ''' <returns></returns>
        ''' <remarks>
        '''   由于采用Tick作为最小计数单位，因此所有与Span交互的数据都要转换为毫秒
        '''   属性内部已经进行了处理
        '''   1 Tick = 1/10000 MS
        ''' </remarks>
        Friend Property Span As Integer
            Get
                Return _span / 10000
            End Get
            Set(value As Integer)
                _span = value * 10000
            End Set
        End Property

        ''' <summary>
        ''' 获取当前的帧计数
        ''' </summary>
        ''' <returns></returns>
        Friend ReadOnly Property CurrentFrame As Integer
            Get
                Return _frameCount
            End Get
        End Property

        ''' <summary>
        ''' 游戏循环主函数
        ''' </summary>
        ''' <remarks>
        '''   在任一循环接收器的逻辑部分执行完成后函数都会根据它的返回值确定是否将其从下次循环的列表中移除
        '''   不过不管它下次是否会被移除，这次循环的渲染部分依然会正常执行
        ''' </remarks>
        Private Sub LoopingContent()
            Dim i As Integer
            Dim loopContent As ILoopReceiver
            Dim timeNow As Long
            Dim sleepTime As Integer
            Dim gameWindow = Config.BaseWindow
            Dim gameDispatcher = gameWindow.Dispatcher
            Config.MessageService.SendMessage("[SYSTEM]LOOP_START")
            While (Status)
                timeNow = Now.Ticks
                i = 0
                _loopListCount = LoopReceiverList.Count
                While i < _loopListCount
                    loopContent = LoopReceiverList.Get(i)
                    If loopContent.Logic(_frameCount) Then
                        i += 1
                    Else
                        LoopReceiverList.Delete(i)
                        _loopListCount -= 1
                    End If
                    gameDispatcher.Invoke(_renderDelegate, loopContent)
                End While
                sleepTime = (timeNow + _span - Now.Ticks) / 10000
                If sleepTime > 0 Then Thread.Sleep(sleepTime)
                _frameCount += 1
            End While
            Config.MessageService.SendMessage("[SYSTEM]LOOP_ABORT")
        End Sub

        ''' <summary>
        ''' 启动游戏循环（如果游戏循环正在运行则不进行任何操作）
        ''' </summary>
        ''' <remarks></remarks>
        Friend Sub Start()
            If Status Then Exit Sub
            _Status = True
            _loopThread.Start()
        End Sub

        ''' <summary>
        ''' 终止游戏循环（如果游戏循环本身并没有启动则不进行任何操作）
        ''' </summary>
        ''' <remarks></remarks>
        Friend Sub [Stop]()
            _Status = False
        End Sub
    End Class
End Namespace