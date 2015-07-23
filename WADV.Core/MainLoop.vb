Imports System.Threading
Imports WADV.Core.PluginInterface
Imports WADV.Core.ReceiverList

''' <summary>
''' 游戏循环
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class MainLoop
    Private Shared _self As MainLoop
    Private _loopListCount As Integer
    Private _frameCount As Integer
    Private ReadOnly _loopThread As Thread
    Private ReadOnly _messager As MessageService
    Private ReadOnly _deletagte As Action(Of ILoopReceiver)

    Private Sub New()
        Status = False
        Span = 40
        _loopThread = New Thread(AddressOf LoopingContent)
        _loopThread.IsBackground = True
        _loopThread.Name = "[系统]游戏循环线程"
        _loopThread.Priority = ThreadPriority.AboveNormal
        _deletagte = New Action(Of ILoopReceiver)(Sub(tmploop As ILoopReceiver) tmploop.Render())
        _messager = MessageService.GetInstance
        _messager.SendMessage("[SYSTEM]LOOP_INIT_FINISH")
    End Sub

    ''' <summary>
    ''' 获取或设置逻辑循环的状态
    ''' </summary>
    ''' <value>新的状态</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property Status As Boolean

    ''' <summary>
    ''' 获取或设置两次逻辑循环间的时间间隔(毫秒)
    ''' </summary>
    ''' <value>新的间隔</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property Span As Integer

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
    ''' 获取逻辑循环的唯一实例
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function GetInstance() As MainLoop
        If _self Is Nothing Then _self = New MainLoop
        Return _self
    End Function

    Private Sub LoopingContent()
        Dim i As Integer
        Dim loopContent As ILoopReceiver
        Dim timeNow As Long
        Dim sleepTime As Integer
        Dim gameWindow = Config.BaseWindow
        Dim gameDispatcher = gameWindow.Dispatcher
        _messager.SendMessage("[SYSTEM]LOOP_START")
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
                gameDispatcher.Invoke(_deletagte, loopContent)
            End While
            sleepTime = timeNow + Span - Now.Ticks
            If sleepTime > 0 Then Thread.Sleep(sleepTime)
            _frameCount += 1
        End While
        _messager.SendMessage("[SYSTEM]LOOP_ABORT")
    End Sub

    ''' <summary>
    ''' 启动逻辑循环
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Start()
        If Status Then Return
        Status = True
        _loopThread.Start()
    End Sub

    ''' <summary>
    ''' 终止逻辑循环
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub [Stop]()
        Status = False
    End Sub

End Class

''' <summary>
''' 辅助空循环
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class EmptyLooping : Implements ILoopReceiver
    Private _count As Integer

    Public Sub New(count As Integer)
        _count = count
    End Sub

    Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
        If _count = 0 Then Return False
        _count -= 1
        Return True
    End Function

    Public Sub Render() Implements ILoopReceiver.Render
    End Sub
End Class