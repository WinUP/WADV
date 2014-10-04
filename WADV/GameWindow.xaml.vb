Imports WADV.AppCore.API

Public Class GameWindow

    Private Sub GameWindow_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        AppCore.Plugin.PluginFunction.DestructuringGame(e)
    End Sub

    Private Sub GameWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        '绑定事件
        AddHandler NavigationService.LoadCompleted, Sub() MessageAPI.Send("WINDOW_PAGE_CHANGE")
        '设定参数
        AppCore.Path.PathConfig.Plugin = My.Settings.PluginURL
        AppCore.Path.PathConfig.Resource = My.Settings.ResourceURL
        AppCore.Path.PathConfig.Script = My.Settings.ScriptURL
        AppCore.Path.PathConfig.Skin = My.Settings.SkinURL
        AppCore.Path.PathConfig.UserFile = My.Settings.UserFileURL
        AppCore.UI.WindowConfig.BaseWindow = Me
        '加载插件
        AppCore.Plugin.PluginFunction.InitialiseAllPlugins()
        '注册脚本函数
        AppCore.Script.Register.RegisterFunction()
        '执行插件初始化函数
        AppCore.Plugin.PluginFunction.InitialisingGame()
        '调用初始化脚本
        ScriptAPI.RunFile("init.lua")
    End Sub

End Class
