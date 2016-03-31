Imports System.Threading
Imports System.Windows.Media.Animation
Imports WADV
Imports WADV.Core
Imports WADV.Core.API
Imports WADV.Core.Enumeration
Imports WADV.WPF.Renderer

Public Class GameWindow
    Private _directNavigation = False
    Private _processingE As NavigatingCancelEventArgs

    ''' <summary>
    ''' 游戏解构函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub GameWindow_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        Game.Cut(e)
    End Sub

    ''' <summary>
    ''' 游戏启动函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GameWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        '显示等待页面
        NavigationService.Navigate(New WaitingPage)
        '设定参数
        Path.Plugin(IO.Path.Combine(Path.Game, My.Settings.PluginURL))
        Path.Resource(IO.Path.Combine(Path.Game, My.Settings.ResourceURL))
        Path.Script(IO.Path.Combine(Path.Game, My.Settings.ScriptURL))
        Path.Skin(IO.Path.Combine(Path.Game, My.Settings.SkinURL))
        Path.UserFile(IO.Path.Combine(Path.Game, My.Settings.UserFileURL))
        '启动游戏核心
        Game.StartGame(New WindowDelegate(Me), 40, 3600000)
        Extension.Plug()
        '判断是否是第一次启动
        If My.Computer.FileSystem.FileExists(Path.Combine(PathType.UserFile, "first_run")) Then
            '第一次启动要执行的逻辑
            Dim tmpThread As New Thread(CType(Sub()
                                                  Script.RunFile("init.lua")
                                                  My.Computer.FileSystem.DeleteFile(Path.Combine(PathType.UserFile, "first_run"), FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                                                  Script.RunFileAsync("game.lua")
                                              End Sub, ThreadStart))
            tmpThread.IsBackground = False
            tmpThread.Name = "游戏初始化承载线程"
            tmpThread.Priority = ThreadPriority.Normal
            tmpThread.Start()
        Else
            '其他情况要执行的逻辑
            Script.RunFileAsync("game.lua")
        End If
    End Sub

    ''' <summary>
    ''' 页面转场处理函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GameWindow_Navigating(sender As Object, e As NavigatingCancelEventArgs) Handles Me.Navigating
        '重置导航状态
        If _directNavigation OrElse Content Is Nothing Then
            If _processingE Is Nothing Then Exit Sub
            Select Case DirectCast(_processingE.ExtraData, NavigateOperation)
                Case NavigateOperation.Normal, NavigateOperation.NavigateAndIn
                    FadeIn()
            End Select
            _directNavigation = False
            IsHitTestVisible = True
            _processingE = Nothing
            Exit Sub
        End If
        '处理窗口属性
        _directNavigation = True
        IsHitTestVisible = False
        e.Cancel = True
        '处理导航标志
        _processingE = e
        Dim data = DirectCast(_processingE.ExtraData, NavigateOperation)
        Select Case data
            Case NavigateOperation.Normal, NavigateOperation.OutAndNavigate, NavigateOperation.OutAndShow, NavigateOperation.OnlyOut
                Dim fadeOut As New DoubleAnimation(0.0, New Duration(TimeSpan.FromMilliseconds(540)))
                fadeOut.EasingFunction = New QuarticEase With {.EasingMode = EasingMode.EaseOut}
                AddHandler fadeOut.Completed, Sub() FadeOutComplete()
                BeginAnimation(OpacityProperty, fadeOut)
            Case NavigateOperation.NavigateAndIn, NavigateOperation.NoEffect
                CustomizedNavigate()
            Case NavigateOperation.NavigateAndHide
                CustomizedNavigate()
                Opacity = 0.0
            Case NavigateOperation.OnlyIn
                FadeIn()
        End Select
    End Sub

    ''' <summary>
    ''' 页面淡入处理函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FadeOutComplete()
        If DirectCast(_processingE.ExtraData, NavigateOperation) = NavigateOperation.OutAndShow Then Opacity = 1.0
        CustomizedNavigate()
    End Sub

    ''' <summary>
    ''' 自定义导航数据处理函数
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CustomizedNavigate()
        Select Case _processingE.NavigationMode
            Case NavigationMode.Back
                NavigationService.GoBack()
            Case NavigationMode.Forward
                NavigationService.GoForward()
            Case NavigationMode.Refresh
                NavigationService.Refresh()
            Case NavigationMode.New
                If _processingE.Uri Is Nothing Then
                    NavigationService.Navigate(_processingE.Content)
                Else
                    NavigationService.Navigate(_processingE.Uri)
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
        fadeIn.EasingFunction = New QuadraticEase With {.EasingMode = EasingMode.EaseOut}
        BeginAnimation(OpacityProperty, fadeIn)
    End Sub
End Class