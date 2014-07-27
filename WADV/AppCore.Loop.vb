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
        Private Shared customizedLoopListCount As Integer
        Private Shared frame As Integer

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
            If Not customizedLoopList.Contains(loopContent) Then
                customizedLoopList.Add(loopContent)
            End If
        End Sub

        Protected Friend Shared Sub WaitCustomizedLoop(loopContent As Plugin.ICustomizedLoop)
            Dim loopThread = New Thread(New ParameterizedThreadStart(AddressOf WaitForExit))
            loopThread.IsBackground = True
            loopThread.Start(loopContent)
            loopThread.Join()
        End Sub

        Private Shared Sub WaitForExit(loopContent As Plugin.ICustomizedLoop)
            While (loopStatus)
                If customizedLoopList.Contains(loopContent) Then
                    Thread.Sleep(loopTimeSpan)
                Else
                    Exit Sub
                End If
            End While
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
                customizedLoopListCount = customizedLoopList.Count
                Dim i As Integer = 0
                While i < customizedLoopListCount
                    If Not customizedLoopList(i).StartLooping() Then
                        customizedLoopList.RemoveAt(i)
                        customizedLoopListCount = customizedLoopList.Count
                        Continue While
                    End If
                    i += 1
                End While
#If DEBUG Then
                frame += 1
                If frame Mod 10 = 0 Then System.Diagnostics.Debug.Write(frame & Environment.NewLine)
#End If
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
