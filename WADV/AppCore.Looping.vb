Imports System.Threading

Namespace AppCore.Looping

    ''' <summary>
    ''' 游戏循环
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MainLooping : Inherits Windows.Threading.DispatcherObject
        Private Shared self As MainLooping
        Private loopList As New List(Of Plugin.ILooping)
        Private loopListCount As Integer

        ''' <summary>
        ''' 添加一个循环
        ''' </summary>
        ''' <param name="loopContent">循环函数</param>
        ''' <remarks></remarks>
        Protected Friend Sub AddLooping(loopContent As Plugin.ILooping)
            VerifyAccess()
            If Not loopList.Contains(loopContent) Then loopList.Add(loopContent)
        End Sub

        ''' <summary>
        ''' 等待某个循环体的完全结束
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Protected Friend Sub WaitLooping(loopContent As Plugin.ILooping)
            Dim loopThread = New Thread(New ParameterizedThreadStart(Sub()
                                                                         While (Status)
                                                                             If loopList.Contains(loopContent) Then
                                                                                 Thread.Sleep(Span)
                                                                             Else
                                                                                 Exit Sub
                                                                             End If
                                                                         End While
                                                                     End Sub))
            loopThread.IsBackground = True
            loopThread.Start(loopContent)
            loopThread.Join()
        End Sub

        Private Sub New()
            Status = False
            Span = 17
        End Sub

        ''' <summary>
        ''' 获取或设置逻辑循环的状态
        ''' </summary>
        ''' <value>新的状态</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Property Status As Boolean

        ''' <summary>
        ''' 获取或设置两次逻辑循环间的时间间隔(毫秒)
        ''' </summary>
        ''' <value>新的间隔</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Property Span As Integer

        ''' <summary>
        ''' 获取逻辑循环的唯一实例
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function GetInstance() As MainLooping
            If self Is Nothing Then self = New MainLooping
            Return self
        End Function

        ''' <summary>
        ''' 逻辑循环体
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Sub LoopingContent()
            loopListCount = loopList.Count
            Dim i As Integer
            Dim loopContent As Plugin.ILooping
            Dim timeNow = DateTime.Now.Ticks
            Dim nextStartTime = timeNow
            Dim sleepTime = 0
            While (Status)
                i = 0
                While i < loopListCount
                    loopContent = loopList(i)
                    If Not loopContent.StartLooping Then
                        loopList.Remove(loopContent)
                        loopListCount -= 1
                    Else
                        i += 1
                    End If
                    API.WindowAPI.GetDispatcher.Invoke(Sub() loopContent.StartRendering(API.WindowAPI.GetWindow))
                End While
                timeNow = DateTime.Now.Ticks
                nextStartTime += Span
                sleepTime = nextStartTime - timeNow
                If sleepTime > 0 Then
                    Thread.Sleep(sleepTime)
                Else
                    nextStartTime = timeNow
                End If
            End While
        End Sub

    End Class

    Public Class LoopingFunction
        Private Shared _frame As Integer

        ''' <summary>
        ''' 获取或设置逻辑循环每秒的理想执行次数
        ''' </summary>
        ''' <value>执行次数</value>
        ''' <returns></returns>
        Protected Friend Shared Property Frame As Integer
            Get
                Return _frame
            End Get
            Set(value As Integer)
                _frame = value
                MainLooping.GetInstance.Span = 1000 / Frame
            End Set
        End Property

        ''' <summary>
        ''' 结束循环
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub StopMainLooping()
            MainLooping.GetInstance.Status = False
        End Sub

        ''' <summary>
        ''' 开始循环
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub StartMainLooping()
            If Not MainLooping.GetInstance.Status Then
                Dim loopThread = New Thread(AddressOf MainLooping.GetInstance.LoopingContent)
                loopThread.IsBackground = True
                MainLooping.GetInstance.Status = True
                loopThread.Start()
            End If
        End Sub

    End Class

End Namespace
