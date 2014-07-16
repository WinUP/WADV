Imports System.Threading

Namespace AppCore.Loop

    ''' <summary>
    ''' 游戏逻辑循环
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MainLoop

        Private Shared loopStatus As Boolean = False
        Private Shared loopTimeSpan As Integer
        Private Shared loopList As New List(Of Plugin.ILoop)
        Private Shared customizedLoopList As New List(Of Plugin.ICustomizedLoop)

        ''' <summary>
        ''' 添加一个逻辑循环
        ''' </summary>
        ''' <param name="loopContent">循环函数</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub AddLoop(loopContent As Plugin.ILoop)
            If Not loopList.Contains(loopContent) Then loopList.Add(loopContent)
        End Sub

        ''' <summary>
        ''' 添加一个小型逻辑循环
        ''' </summary>
        ''' <param name="loopContent">循环函数</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub AddCustomizedLoop(loopContent As Plugin.ICustomizedLoop)
            If Not customizedLoopList.Contains(loopContent) Then customizedLoopList.Add(loopContent)
        End Sub

        ''' <summary>
        ''' 游戏逻辑循环体
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared Sub MainLoop()
            While (loopStatus)
                For Each tmpPlugin In loopList
                    tmpPlugin.StartLooping()
                Next
                For Each tmpLoop In customizedLoopList
                    If Not tmpLoop.StartLooping() Then
                        customizedLoopList.Remove(tmpLoop)
                    End If
                Next
                Thread.Sleep(loopTimeSpan)
            End While
        End Sub

        ''' <summary>
        ''' 结束循环
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub StopMainLoop()
            loopStatus = False
        End Sub

        ''' <summary>
        ''' 开始循环
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub StartMainLoop()
            If Not loopStatus Then
                Dim loopThread = New Thread(AddressOf MainLoop)
                loopThread.IsBackground = True
                loopStatus = True
                loopThread.Start()
            End If
        End Sub

        ''' <summary>
        ''' 初始化循环
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub InitialiseLoop()
            loopTimeSpan = 1000 / Config.LoopConfig.Frame
        End Sub

    End Class

End Namespace
