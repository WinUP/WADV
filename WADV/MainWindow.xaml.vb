Class MainWindow

    Protected Overrides Sub OnRender(ByVal dc As DrawingContext)
        For Each tmpPlugin In AppCore.Config.PluginConfig.RenderingList
            tmpPlugin.StartRendering(Me, dc)
        Next
        MyBase.OnRender(dc)
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        '设定路径
        AppCore.Config.URLConfig.Plugin = My.Settings.PluginURL
        AppCore.Config.URLConfig.Resource = My.Settings.ResourceURL
        AppCore.Config.URLConfig.Script = My.Settings.ScriptURL
        AppCore.Config.URLConfig.Skin = My.Settings.SkinURL
        AppCore.Config.URLConfig.UserFile = My.Settings.UserFileURL
        AppCore.Config.WindowConfig.BaseGrid = MainGrid
        '加载插件
        AppCore.Config.PluginConfig.InitialiseAllPlugins()
        '注册脚本函数
        AppCore.Script.Register.RegisterFunction()
        '调用初始化脚本
        AppCore.API.ScriptAPI.RunFileInThread("logic\init.lua")
    End Sub

    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        If MessageBox.Show("要退出游戏吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes Then
            AppCore.Loop.MainLoop.StopMainLoop()
            Application.Current.Shutdown()
        Else
            e.Cancel = True
        End If
    End Sub
End Class
