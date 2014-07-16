Class MainWindow

    Private Sub Grid_MouseLeftButtonUp_1(sender As Object, e As MouseButtonEventArgs)

    End Sub

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
        '设定逻辑循环
        AppCore.API.LoopAPI.ChangeFrame(25)
        AppCore.API.LoopAPI.InitialiseLoop()
        '加载插件
        AppCore.Config.PluginConfig.InitialiseAllPlugins()
        '注册脚本函数
        AppCore.Script.Register.RegisterFunction()
        '调用初始化脚本
        AppCore.API.ScriptAPI.RunFile("logic/init.lua")
    End Sub

End Class
