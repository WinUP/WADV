Imports System.Windows.Media.Animation
Imports WADV.AppCore
Imports WADV.AppCore.API

Public Class GameWindow
    Private _allowDirectNavigation = False

    Private Sub GameWindow_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        PluginFunction.DestructuringGame(e)
    End Sub

    Private Sub GameWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        '绑定事件
        AddHandler NavigationService.LoadCompleted, Sub() MessageAPI.SendSync("[SYSTEM]WINDOW_PAGE_CHANGE")
        '设定参数
        Config.PluginPath = My.Settings.PluginURL
        Config.ResourcePath = My.Settings.ResourceURL
        Config.ScriptPath = My.Settings.ScriptURL
        Config.SkinPath = My.Settings.SkinURL
        Config.UserFilePath = My.Settings.UserFileURL
        Config.BaseWindow = Me
        '注册脚本函数
        Dim vm = ScriptCore.GetInstance.ScriptVM
        vm.DoString("api_system={}")
        For Each tmpAPIClass In (From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Namespace = "WADV.AppCore.API" AndAlso tmpClass.IsClass AndAlso tmpClass.Name.LastIndexOf("API", StringComparison.Ordinal) = tmpClass.Name.Length - 3 Select tmpClass)
            Dim registerName = tmpAPIClass.Name.Substring(0, tmpAPIClass.Name.Length - 3).ToLower
            vm.DoString("api_system." & registerName & "={}")
            ScriptAPI.RegisterSync(tmpAPIClass, "api_system." & registerName)
        Next
        '加载插件
        PluginFunction.InitialiseAllPlugins()
        '执行插件初始化函数
        PluginFunction.InitialisingGame()
        '初始化环境变量
        vm.DoString("env={}")
        vm.DoString("env.version=""1.0""")
        vm.DoString("env.path={}")
        vm.DoString("env.path.game=""" & My.Application.Info.DirectoryPath.Replace("\", "\\") & """")
        vm.DoString("env.path.user=""" & PathAPI.GetPath(PathType.UserFile).Replace("\", "\\") & """")
        vm.DoString("env.path.plugin=""" & PathAPI.GetPath(PathType.Plugin).Replace("\", "\\") & """")
        vm.DoString("env.path.resource=""" & PathAPI.GetPath(PathType.Resource).Replace("\", "\\") & """")
        vm.DoString("env.path.script=""" & PathAPI.GetPath(PathType.Script).Replace("\", "\\") & """")
        vm.DoString("env.path.skin=""" & PathAPI.GetPath(PathType.Skin).Replace("\", "\\") & """")
        '执行游戏逻辑
        ScriptAPI.RunFileAsync("init.lua")
    End Sub

    ''' <summary>
    ''' 页面转场动画处理函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GameWindow_Navigating(sender As Object, e As NavigatingCancelEventArgs) Handles Me.Navigating
        If Content Is Nothing OrElse _allowDirectNavigation Then Return
        e.Cancel = True
        IsHitTestVisible = False
        Dim fadeOut As New DoubleAnimation(0.0, New Duration(TimeSpan.FromMilliseconds(700)))
        fadeOut.EasingFunction = New QuinticEase
        AddHandler fadeOut.Completed, Sub()
                                          IsHitTestVisible = True
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
                                          _allowDirectNavigation = False
                                          Dispatcher.BeginInvoke(Sub()
                                                                     Dim fadeIn As New DoubleAnimation(1.0, New Duration(TimeSpan.FromMilliseconds(700)))
                                                                     fadeIn.EasingFunction = New QuinticEase
                                                                     BeginAnimation(OpacityProperty, fadeIn)
                                                                 End Sub)
                                      End Sub
        BeginAnimation(OpacityProperty, fadeOut)
        _allowDirectNavigation = True
    End Sub
End Class
