Imports System.Windows.Media.Animation
Imports Neo.IronLua
Imports WADV.AppCore
Imports WADV.AppCore.API
Imports WADV.AppCore.PluginInterface

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
        RegisterScript()
        '加载插件
        PluginFunction.InitialiseAllPlugins()
        '执行插件初始化函数
        PluginFunction.InitialisingGame()
        MessageService.GetInstance.SendMessage("[SYSTEM]GAME_INIT_FINISH")
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

    Private Sub RegisterScript()
        Dim system, script As LuaTable
        ScriptCore.GetInstance.Environment("api_system") = New LuaTable
        system = ScriptCore.GetInstance.Environment("api_system")
        'Loop
        system("loop") = New LuaTable
        script = system("loop")
        script("start") = New Action(AddressOf LoopAPI.StartSync)
        script("stop") = New Action(AddressOf LoopAPI.StopSync)
        script("setFrame") = New Action(Of Integer)(AddressOf LoopAPI.SetFrameSync)
        script("getFrame") = New Func(Of Integer)(AddressOf LoopAPI.GetFrame)
        script("waitFrame") = New Action(Of Integer)(AddressOf LoopAPI.WaitFrameSync)
        script("addLoop") = New Action(Of ILoopReceiver)(AddressOf LoopAPI.AddLoopSync)
        script("waitLoop") = New Action(Of ILoopReceiver)(AddressOf LoopAPI.WaitLoopSync)
        script("currentFrame") = New Func(Of Integer)(AddressOf LoopAPI.CurrentFrame)
        script("translateToTime") = New Func(Of Integer, TimeSpan)(AddressOf LoopAPI.TranslateToTime)
        script("translateToFrame") = New Func(Of TimeSpan, Integer)(AddressOf LoopAPI.TranslateToFrame)
        'Message
        system("message") = New LuaTable
        script = system("message")
        script("add") = New Action(Of IMessageReceiver)(AddressOf MessageAPI.AddSync)
        script("delete") = New Action(Of IMessageReceiver)(AddressOf MessageAPI.DeleteSync)
        script("send") = New Action(Of String)(AddressOf MessageAPI.SendSync)
        script("wait") = New Action(Of String)(AddressOf MessageAPI.WaitSync)
        script("lastMessage") = New Func(Of String)(AddressOf MessageAPI.LastMessage)
        'Path
        system("path") = New LuaTable
        script = system("path")
        script("resource") = New Func(Of String)(AddressOf PathAPI.Resource)
        script("skin") = New Func(Of String)(AddressOf PathAPI.Skin)
        script("plugin") = New Func(Of String)(AddressOf PathAPI.Plugin)
        script("script") = New Func(Of String)(AddressOf PathAPI.Script)
        script("userfile") = New Func(Of String)(AddressOf PathAPI.UserFile)
        script("game") = New Func(Of String)(AddressOf PathAPI.Game)
        script("getPath") = New Func(Of PathType, String, String)(AddressOf PathAPI.GetPath)
        script("getUri") = New Func(Of PathType, String, Uri)(AddressOf PathAPI.GetUri)
        'Plugin
        system("plugin") = New LuaTable
        script = system("plugin")
        script("add") = New Func(Of String, Boolean)(AddressOf PluginAPI.Add)
        script("compile") = New Func(Of String, String, Reflection.Assembly)(AddressOf PluginAPI.Compile)
        script("load") = New Func(Of String, Reflection.Assembly)(AddressOf PluginAPI.Load)
        'Resource
        system("resource") = New LuaTable
        script = system("resource")
        script("loadToGame") = New Action(Of String)(AddressOf ResourceAPI.LoadToGameSync)
        script("loadToWindow") = New Action(Of String)(AddressOf ResourceAPI.LoadToWindowSync)
        script("clearGame") = New Action(AddressOf ResourceAPI.ClearGameSync)
        script("clearWindow") = New Action(AddressOf ResourceAPI.ClearWindowSync)
        script("removeFromGame") = New Action(Of ResourceDictionary)(AddressOf ResourceAPI.RemoveFromGameSync)
        script("removeFromWindow") = New Action(Of ResourceDictionary)(AddressOf ResourceAPI.RemoveFromWindowSync)
        script("getFromGame") = New Func(Of ResourceDictionary)(AddressOf ResourceAPI.GetFromGame)
        script("getFromWindow") = New Func(Of ResourceDictionary)(AddressOf ResourceAPI.GetFromWindow)
        'Script[不包括Lua本身可直接实现的功能所涉及的API]
        system("script") = New LuaTable
        script = system("script")
        script("show") = New Action(Of String, String)(AddressOf ScriptAPI.ShowSync)
        script("getVm") = New Func(Of Lua)(AddressOf ScriptAPI.GetVm)
        script("getEnv") = New Func(Of LuaGlobal)(AddressOf ScriptAPI.GetEnv)
        'Timer
        system("timer") = New LuaTable
        script = system("timer")
        script("start") = New Action(AddressOf TimerAPI.StartSync)
        script("stop") = New Action(AddressOf TimerAPI.StopSync)
        script("setTick") = New Action(Of Integer)(AddressOf TimerAPI.SetTickSync)
        script("getTick") = New Func(Of Integer)(AddressOf TimerAPI.GetTick)
        script("getStatus") = New Func(Of Boolean)(AddressOf TimerAPI.GetStatus)
        'Window[不包括GetChildByName SearchObject GetRoot这些泛型API]
        system("window") = New LuaTable
        script = system("window")
        script("setTitle") = New Action(Of String)(AddressOf WindowAPI.SetTitleSync)
        script("clearContent") = New Action(Of Panel)(AddressOf WindowAPI.ClearContentSync)
        script("loadElement") = New Action(Of Panel, String)(AddressOf WindowAPI.LoadElementSync)
        script("loadElementA") = New Action(Of Panel, String)(AddressOf WindowAPI.LoadElementAsync)
        script("loadPage") = New Action(Of String)(AddressOf WindowAPI.LoadPageSync)
        script("loadPageA") = New Action(Of String)(AddressOf WindowAPI.LoadPageAsync)
        script("loadObject") = New Action(Of Page)(AddressOf WindowAPI.LoadObjectSync)
        script("loadObjectA") = New Action(Of Page)(AddressOf WindowAPI.LoadObjectAsync)
        script("loadUri") = New Action(Of String)(AddressOf WindowAPI.LoadUriSync)
        script("loadUriA") = New Action(Of String)(AddressOf WindowAPI.LoadUriAsync)
        script("goBack") = New Action(AddressOf WindowAPI.GoBackSync)
        script("goForward") = New Action(AddressOf WindowAPI.GoForwardSync)
        script("removeOneBack") = New Action(AddressOf WindowAPI.RemoveOneBackSync)
        script("removeBackList") = New Action(AddressOf WindowAPI.RemoveBackListSync)
        script("setBackgroundByColor") = New Action(Of Color)(AddressOf WindowAPI.SetBackgroundByColorSync)
        script("setBackgroundByRGB") = New Action(Of Byte, Byte, Byte)(AddressOf WindowAPI.SetBackgroundByRGBSync)
        script("setBackgroundByHex") = New Action(Of String)(AddressOf WindowAPI.SetBackgroundByHexSync)
        script("setWidth") = New Action(Of Double)(AddressOf WindowAPI.SetWidthSync)
        script("setHeight") = New Action(Of Double)(AddressOf WindowAPI.SetHeightSync)
        script("setResizeMode") = New Action(Of Boolean)(AddressOf WindowAPI.SetResizeModeSync)
        script("setTopmost") = New Action(Of Boolean)(AddressOf WindowAPI.SetTopmostSync)
        script("setIcon") = New Action(Of String)(AddressOf WindowAPI.SetIconSync)
        script("setCursor") = New Action(Of String)(AddressOf WindowAPI.SetCursorSync)
        script("getDispatcher") = New Func(Of Windows.Threading.Dispatcher)(AddressOf WindowAPI.GetDispatcher)
        script("getWindow") = New Func(Of NavigationWindow)(AddressOf WindowAPI.GetWindow)
        script("getImage") = New Func(Of JpegBitmapEncoder)(AddressOf WindowAPI.GetImage)
        script("saveImage") = New Action(Of String)(AddressOf WindowAPI.SaveImage)
        '环境变量
        ScriptCore.GetInstance.Environment("env") = New LuaTable
        system = ScriptCore.GetInstance.Environment("env")
        system("version") = "1.0"
        system("luaEngine") = LuaGlobal.VersionString
        system("path") = New LuaTable
        script = system("path")
        script("game") = My.Application.Info.DirectoryPath
        script("user") = PathFunction.GetFullPath(PathType.UserFile)
        script("plugin") = PathFunction.GetFullPath(PathType.Plugin)
        script("resource") = PathFunction.GetFullPath(PathType.Resource)
        script("script") = PathFunction.GetFullPath(PathType.Script)
        script("skin") = PathFunction.GetFullPath(PathType.Skin)
    End Sub

End Class
