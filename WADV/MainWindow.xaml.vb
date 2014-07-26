Class MainWindow

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        '设定路径
        AppCore.Path.PathConfig.Plugin = My.Settings.PluginURL
        AppCore.Path.PathConfig.Resource = My.Settings.ResourceURL
        AppCore.Path.PathConfig.Script = My.Settings.ScriptURL
        AppCore.Path.PathConfig.Skin = My.Settings.SkinURL
        AppCore.Path.PathConfig.UserFile = My.Settings.UserFileURL
        AppCore.UI.WindowConfig.BaseGrid = MainGrid
        '加载插件
        AppCore.Plugin.PluginFunction.InitialiseAllPlugins()
        '注册脚本函数
        AppCore.Script.Register.RegisterFunction()
        '调用初始化脚本
        AppCore.API.ScriptAPI.RunFile("logic\init.lua")
    End Sub

    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        If MessageBox.Show("要退出游戏吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes Then
            AppCore.Looping.LoopingFunction.StopMainLooping()
            Application.Current.Shutdown()
        Else
            e.Cancel = True
        End If
    End Sub
End Class
