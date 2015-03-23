Imports System.Threading
Imports System.Windows.Media.Animation
Imports WADV.Core
Imports WADV.Core.API

Public Class GameWindow
    Private _directNavigation = False

    ''' <summary>
    ''' 游戏解构函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GameWindow_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        GameAPI.StopGame(e)
    End Sub

    ''' <summary>
    ''' 游戏启动函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GameWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        '绑定事件
        AddHandler NavigationService.LoadCompleted, Sub() MessageAPI.SendSync("[SYSTEM]WINDOW_PAGE_CHANGE")
        '设定参数
        PathAPI.SetPlugin(IO.Path.Combine(PathAPI.Game, My.Settings.PluginURL))
        PathAPI.SetResource(IO.Path.Combine(PathAPI.Game, My.Settings.ResourceURL))
        PathAPI.SetScript(IO.Path.Combine(PathAPI.Game, My.Settings.ScriptURL))
        PathAPI.SetSkin(IO.Path.Combine(PathAPI.Game, My.Settings.SkinURL))
        PathAPI.SetUserFile(IO.Path.Combine(PathAPI.Game, My.Settings.UserFileURL))
        '启动游戏核心
        GameAPI.StartGame(Me, 40, 3600000)
        '判断是否是第一次启动
        If My.Computer.FileSystem.FileExists(PathAPI.GetPath(PathType.UserFile, "first_run")) Then
            '第一次启动要执行的逻辑
            Dim tmpThread As New Thread(CType(Sub()
                                                  ScriptAPI.RunFileSync("init.lua")
                                                  My.Computer.FileSystem.DeleteFile(PathAPI.GetPath(PathType.UserFile, "first_run"), FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                                                  ScriptAPI.RunFileAsync("game.lua")
                                              End Sub, ThreadStart))
            tmpThread.IsBackground = False
            tmpThread.Name = "游戏初始化承载线程"
            tmpThread.Priority = ThreadPriority.Normal
            tmpThread.Start()
        Else
            '其他情况要执行的逻辑
            ScriptAPI.RunFileAsync("game.lua")
        End If
    End Sub

    ''' <summary>
    ''' 页面转场处理函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GameWindow_Navigating(sender As Object, e As NavigatingCancelEventArgs) Handles Me.Navigating
        '重置导航状态
        If _directNavigation OrElse Content Is Nothing Then
            _directNavigation = False
            Return
        End If
        '执行所有接收器

        '判断导航是否已被取消
        If e.Cancel Then Exit Sub
        '处理窗口属性
        _directNavigation = True
        IsHitTestVisible = False
        e.Cancel = True
        '处理导航标志
        Dim data = DirectCast(e.ExtraData, NavigateOperation)
        Select Case data
            Case NavigateOperation.Normal, NavigateOperation.FadeOutAndNavigate, NavigateOperation.FadeOutAndShow, NavigateOperation.OnlyFadeOut
                Dim fadeOut As New DoubleAnimation(0.0, New Duration(TimeSpan.FromMilliseconds(540)))
                fadeOut.EasingFunction = New QuarticEase With {.EasingMode = EasingMode.EaseOut}
                AddHandler fadeOut.Completed, Sub() FadeOutComplete(e)
                BeginAnimation(OpacityProperty, fadeOut)
                Exit Sub
            Case NavigateOperation.NavigateAndFadeIn
                CustomizedNavigate(e)
                FadeIn()
            Case NavigateOperation.NavigateAndHide
                e.Cancel = False
                Opacity = 0.0
            Case NavigateOperation.OnlyFadeIn
                FadeIn()
            Case NavigateOperation.NoEffect
                e.Cancel = False
        End Select
    End Sub

    ''' <summary>
    ''' 页面淡入处理函数
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FadeOutComplete(e As NavigatingCancelEventArgs)
        Dim data = DirectCast(e.ExtraData, NavigateOperation)
        IsHitTestVisible = True
        Select Case data
            Case NavigateOperation.Normal
                CustomizedNavigate(e)
                FadeIn()
            Case NavigateOperation.FadeOutAndNavigate
                CustomizedNavigate(e)
            Case NavigateOperation.FadeOutAndShow
                CustomizedNavigate(e)
                Opacity = 1.0
        End Select
    End Sub

    ''' <summary>
    ''' 自定义导航数据处理函数
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CustomizedNavigate(e As NavigatingCancelEventArgs)
        Select Case e.NavigationMode
            Case NavigationMode.Back
                NavigationService.GoBack()
            Case NavigationMode.Forward
                NavigationService.GoForward()
            Case NavigationMode.Refresh
                NavigationService.Refresh()
            Case NavigationMode.New
                If e.Uri Is Nothing Then
                    NavigationService.Navigate(e.Content)
                Else
                    NavigationService.Navigate(e.Uri)
                End If
        End Select
    End Sub

    ''' <summary>
    ''' 页面淡出处理函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FadeIn()
        Opacity = 0.0
        Dim fadeIn As New DoubleAnimation(1.0, New Duration(TimeSpan.FromMilliseconds(540)))
        fadeIn.EasingFunction = New QuinticEase With {.EasingMode = EasingMode.EaseOut}
        BeginAnimation(OpacityProperty, fadeIn)
    End Sub

End Class
