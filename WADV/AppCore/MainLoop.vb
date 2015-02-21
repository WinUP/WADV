Imports System.Threading
Imports WADV.AppCore.API
Imports WADV.AppCore.PluginInterface

Namespace AppCore

    ''' <summary>
    ''' 游戏循环
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class MainLoop
        Private Shared _self As MainLoop
        Private _loopListCount As Integer
        Private _frameCount As Integer
        Private ReadOnly _loopThread As Thread
        Private ReadOnly _messager As MessageService
        Friend ReadOnly LoopList As New List(Of ILoopReceiver)

        ''' <summary>
        ''' 添加一个循环体
        ''' </summary>
        ''' <param name="loopContent">循环函数</param>
        ''' <remarks></remarks>
        Public Sub AddLoop(loopContent As ILoopReceiver)
            If Not LoopList.Contains(loopContent) Then
                LoopList.Add(loopContent)
                _messager.SendMessage("[SYSTEM]LOOP_CONTENT_ADD")
            End If
        End Sub

        Private Sub New()
            Status = False
            Span = 33
            _loopThread = New Thread(AddressOf LoopingContent)
            _loopThread.IsBackground = True
            _loopThread.Name = "游戏循环线程"
            _loopThread.Priority = ThreadPriority.AboveNormal
            _frameCount = 0
            _messager = MessageService.GetInstance
            _messager.SendMessage("[SYSTEM]LOOP_INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 获取或设置逻辑循环的状态
        ''' </summary>
        ''' <value>新的状态</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Status As Boolean

        ''' <summary>
        ''' 获取或设置两次逻辑循环间的时间间隔(毫秒)
        ''' </summary>
        ''' <value>新的间隔</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Span As Integer

        ''' <summary>
        ''' 获取当前的帧计数
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property CurrentFrame As Integer
            Get
                Return _frameCount
            End Get
        End Property

        ''' <summary>
        ''' 获取逻辑循环的唯一实例
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetInstance() As MainLoop
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
            While (Status)
                timeNow = Now.Ticks
                i = 0
                _loopListCount = LoopList.Count
                While i < _loopListCount
                    loopContent = LoopList(i)
                    If loopContent.Logic(_frameCount) Then
                        i += 1
                    Else
                        LoopList.Remove(loopContent)
                        _messager.SendMessage("[SYSTEM]LOOP_CONTENT_REMOVE")
                        _loopListCount -= 1
                    End If
                    gameDispatcher.Invoke(Sub(ByRef tmploop As ILoopReceiver) tmploop.Render(), loopContent)
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
        Public Sub Start()
            If Status Then Return
            Status = True
            _loopThread.Start()
            _messager.SendMessage("[SYSTEM]LOOP_START")
        End Sub

        ''' <summary>
        ''' 终止逻辑循环
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Abort()
            Status = False
        End Sub

    End Class

    ''' <summary>
    ''' 循环辅助类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class LoopFunction
        Private Shared _frame As Integer = 30

        ''' <summary>
        ''' 等待指定循环的结束
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Sub Wait(loopContent As ILoopReceiver)
            Dim loopList = MainLoop.GetInstance.LoopList
            While True
                MessageAPI.WaitSync("[SYSTEM]LOOP_CONTENT_REMOVE")
                If Not loopList.Contains(loopContent) Then Exit While
            End While
        End Sub

        ''' <summary>
        ''' 获取或设置逻辑循环每秒的理想执行次数
        ''' </summary>
        ''' <value>执行次数</value>
        ''' <returns></returns>
        Public Shared Property Frame As Integer
            Get
                Return _frame
            End Get
            Set(value As Integer)
                _frame = value
                MainLoop.GetInstance.Span = 1000 / Frame
                MessageAPI.SendSync("[SYSTEM]LOOP_FRAME_CHANGE")
            End Set
        End Property

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

End Namespace
