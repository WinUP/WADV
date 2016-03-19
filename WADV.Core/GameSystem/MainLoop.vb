Imports System.Threading
Imports WADV.Core.PluginInterface

Namespace GameSystem
    ''' <summary>
    ''' 游戏循环
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class MainLoop
        Private _loopListCount As Integer
        Private _status As Boolean = False
        Private _frameCount As Integer
        Private _span As Integer
        Private ReadOnly _loopThread As Thread
        Private Shared _instance As MainLoop

        ''' <summary>
        ''' 获取游戏循环的唯一实例
        ''' </summary>
        ''' <returns></returns>
        Friend Shared Function GetInstance() As MainLoop
            If _instance Is Nothing Then _instance = New MainLoop
            Return _instance
        End Function

        ''' <summary>
        ''' 获得一个游戏循环实例
        ''' </summary>
        Private Sub New()
            _loopThread = New Thread(AddressOf LoopingContent)
            _loopThread.IsBackground = True
            _loopThread.Name = "[系统]游戏循环线程"
            _loopThread.Priority = ThreadPriority.AboveNormal
        End Sub

        ''' <summary>
        ''' 获取或设置逻辑循环的状态
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
        Friend ReadOnly Property Frames As Integer
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
            Dim loopContent As ILoopReceiver
            Dim timeNow As Long
            Dim sleepTime As Integer
            Dim gameWindow = Configuration.System.MainWindow
            Configuration.System.MessageService.SendMessage("[SYSTEM]LOOP_START", 1)
            While (Status)
                timeNow = Now.Ticks
                _loopListCount = Configuration.Receiver.LoopReceiver.Count
                For i = 0 To _loopListCount
                    loopContent = Configuration.Receiver.LoopReceiver(i)
                    If Not loopContent.Logic(_frameCount) Then
                        Configuration.Receiver.LoopReceiver.Delete(i)
                    End If
                    gameWindow.RunRenderDelegate(AddressOf loopContent.Render)
                Next
                Configuration.Receiver.LoopReceiver.Update()
                sleepTime = (timeNow + _span - Now.Ticks) / 10000
                If sleepTime > 0 Then Thread.Sleep(sleepTime)
                _frameCount += 1
            End While
            Configuration.System.MessageService.SendMessage("[SYSTEM]LOOP_ABORT", 1)
        End Sub
    End Class
End Namespace