Imports WADV.AppCore.API

Public Class GameWindow

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
        ScriptAPI.RunStringSync("env.path=""" & My.Application.Info.DirectoryPath.Replace("\", "\\") & """")
        '执行游戏逻辑
        ScriptAPI.RunFileAsync("init.lua")
    End Sub

End Class
