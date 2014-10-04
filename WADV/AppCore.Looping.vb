﻿Imports System.Threading
Imports WADV.AppCore.API

Namespace AppCore.Looping

    ''' <summary>
    ''' 游戏循环
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MainLooping
        Private Shared self As MainLooping
        Private loopList As New List(Of Plugin.ILooping)
        Private loopListCount As Integer
        Protected Friend loopThread As Thread
        Private frameCount As Integer

        ''' <summary>
        ''' 添加一个循环
        ''' </summary>
        ''' <param name="loopContent">循环函数</param>
        ''' <remarks></remarks>
        Protected Friend Sub AddLooping(loopContent As Plugin.ILooping)
            If Not loopList.Contains(loopContent) Then
                loopList.Add(loopContent)
                MessageAPI.Send("LOOP_CONTENT_ADD")
            End If
        End Sub

        ''' <summary>
        ''' 等待某个循环体的完全结束
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Protected Friend Sub WaitLooping(loopContent As Plugin.ILooping)
            Dim loopThread = New Thread(New ThreadStart(Sub()
                                                            While (Status)
                                                                If loopList.Contains(loopContent) Then
                                                                    Thread.Sleep(Span)
                                                                Else
                                                                    Exit Sub
                                                                End If
                                                            End While
                                                        End Sub))
            loopThread.IsBackground = True
            loopThread.Priority = ThreadPriority.BelowNormal
            loopThread.Start()
            loopThread.Join()
        End Sub

        Private Sub New()
            Status = False
            Span = 17
            loopThread = New Thread(AddressOf LoopingContent)
            loopThread.IsBackground = True
            loopThread.Name = "游戏循环线程"
            loopThread.Priority = ThreadPriority.AboveNormal
            frameCount = 0
            MessageAPI.Send("LOOP_INIT_FINISH")
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
        ''' 获取当前的帧计数
        ''' </summary>
        ''' <returns></returns>
        Protected Friend ReadOnly Property CurrentFrame As Integer
            Get
                Return frameCount
            End Get
        End Property

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
        ''' 游戏循环体
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoopingContent()
            Dim i As Integer
            Dim loopContent As Plugin.ILooping
            Dim timeNow As Long
            Dim nextStartTime = timeNow
            Dim sleepTime = 0
            Dim gameWindow = WindowAPI.GetWindow
            Dim gameDispatcher = WindowAPI.GetDispatcher
            While (Status)
                timeNow = Now.Ticks
                i = 0
                loopListCount = loopList.Count
                While i < loopListCount
                    loopContent = loopList(i)
                    If loopContent.StartLooping(frameCount) Then
                        i += 1
                    Else
                        loopList.Remove(loopContent)
                        loopListCount -= 1
                    End If
                    gameDispatcher.Invoke(Sub() loopContent.StartRendering(gameWindow))
                End While
                sleepTime = timeNow + Span - Now.Ticks
                If sleepTime > 10 Then Thread.Sleep(sleepTime)
                frameCount += 1
            End While
        End Sub

    End Class

    Public Class LoopingFunction
        Private Shared _frame As Integer = 60

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
                MessageAPI.Send("LOOP_FRAME_CHANGE")
            End Set
        End Property

        ''' <summary>
        ''' 结束循环
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub StopMainLooping()
            MainLooping.GetInstance.Status = False
            MainLooping.GetInstance.loopThread.Abort()
            MessageAPI.Send("LOOP_CONTENT_ABORT")
        End Sub

        ''' <summary>
        ''' 开始循环
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub StartMainLooping()
            If Not MainLooping.GetInstance.Status Then
                MainLooping.GetInstance.Status = True
                MainLooping.GetInstance.loopThread.Start()
                MessageAPI.Send("LOOP_CONTENT_START")
            End If
        End Sub

    End Class

End Namespace
