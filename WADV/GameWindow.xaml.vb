Imports System.Windows.Media.Animation
Imports WADV.AppCore.API

Public Class GameWindow
    Private _allowDirectNavigation = False

    Private Sub GameWindow_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        AppCore.Plugin.PluginFunction.DestructuringGame(e)
    End Sub

    Private Sub GameWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        '绑定事件
        AddHandler NavigationService.LoadCompleted, Sub() MessageAPI.SendSync("WINDOW_PAGE_CHANGE")
        '设定参数
        AppCore.Path.PathConfig.Plugin = My.Settings.PluginURL
        AppCore.Path.PathConfig.Resource = My.Settings.ResourceURL
        AppCore.Path.PathConfig.Script = My.Settings.ScriptURL
        AppCore.Path.PathConfig.Skin = My.Settings.SkinURL
        AppCore.Path.PathConfig.UserFile = My.Settings.UserFileURL
        AppCore.UI.WindowConfig.BaseWindow = Me
        '注册脚本函数
        ScriptAPI.RunStringSync("api_system={}")
        For Each tmpAPIClass In (From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Namespace = "WADV.AppCore.API" AndAlso tmpClass.IsClass AndAlso tmpClass.Name.LastIndexOf("API", StringComparison.Ordinal) = tmpClass.Name.Length - 3 Select tmpClass)
            Dim registerName = tmpAPIClass.Name.Substring(0, tmpAPIClass.Name.Length - 3).ToLower
            ScriptAPI.RunStringSync("api_system." & registerName & "={}")
            ScriptAPI.RegisterSync(tmpAPIClass, "api_system." & registerName)
        Next
        '加载插件
        AppCore.Plugin.PluginFunction.InitialiseAllPlugins()
        '执行插件初始化函数
        AppCore.Plugin.PluginFunction.InitialisingGame()
        '初始化环境变量
        ScriptAPI.RunStringSync("env={}")
        ScriptAPI.RunStringSync("env.version=""1.0""")
        ScriptAPI.RunStringSync("env.path={}")
        ScriptAPI.RunStringSync("env.path.game=""" & My.Application.Info.DirectoryPath.Replace("\", "\\") & """")
        ScriptAPI.RunStringSync("env.path.user=""" & PathAPI.GetPath(AppCore.Path.PathFunction.PathType.UserFile).Replace("\", "\\") & """")
        ScriptAPI.RunStringSync("env.path.plugin=""" & PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Plugin).Replace("\", "\\") & """")
        ScriptAPI.RunStringSync("env.path.resource=""" & PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Resource).Replace("\", "\\") & """")
        ScriptAPI.RunStringSync("env.path.script=""" & PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Script).Replace("\", "\\") & """")
        ScriptAPI.RunStringSync("env.path.skin=""" & PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Skin).Replace("\", "\\") & """")
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
