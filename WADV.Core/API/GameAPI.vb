Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports WADV.Core.PluginInterface
Imports Neo.IronLua

Namespace API

    Public Class GameAPI

        ''' <summary>
        ''' 启动游戏系统
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="baseWindow">游戏主窗口</param>
        ''' <param name="frameSpan">每帧间的时间间隔</param>
        ''' <param name="tick">计时器计时频率</param>
        ''' <remarks></remarks>
        Public Shared Sub StartGame(baseWindow As NavigationWindow, frameSpan As Integer, tick As Integer)
            Config.BaseWindow = baseWindow
            RegisterScript()
            MessageService.GetInstance.Start()
            PluginFunction.InitialiseAllPlugins()
            MainTimer.GetInstance.Span = tick
            MainTimer.GetInstance.Start()
            MainLoop.GetInstance.Span = frameSpan
            MainLoop.GetInstance.Start()
            ReceiverList.InitialiserReceiverList.InitialisingGame()
            MessageService.GetInstance.SendMessage("[SYSTEM]GAME_INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 停止游戏系统
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="e">要传递的事件</param>
        ''' <remarks></remarks>
        Public Shared Sub StopGame(e As ComponentModel.CancelEventArgs)
            ReceiverList.DestructorReceiverList.DestructingGame(e)
            If Not e.Cancel Then
                TimerAPI.StopSync()
                LoopAPI.StopSync()
                MessageAPI.StopSync()
            End If
        End Sub

        ''' <summary>
        ''' 注册系统脚本接口
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared Sub RegisterScript()
            Dim system, script As LuaTable
            ScriptCore.GetInstance.Environment("system") = New LuaTable
            system = ScriptCore.GetInstance.Environment("system")
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
            script("getStatus") = New Func(Of Boolean)(AddressOf LoopAPI.GetStatus)
            script("translateToTime") = New Func(Of Integer, TimeSpan)(AddressOf LoopAPI.TranslateToTime)
            script("translateToFrame") = New Func(Of TimeSpan, Integer)(AddressOf LoopAPI.TranslateToFrame)
            'Message
            system("message") = New LuaTable
            script = system("message")
            script("start") = New Action(AddressOf MessageAPI.StartSync)
            script("stop") = New Action(AddressOf MessageAPI.StopSync)
            script("getStatus") = New Func(Of Boolean)(AddressOf MessageAPI.GetStatus)
            script("add") = New Action(Of IMessageReceiver)(AddressOf MessageAPI.AddSync)
            script("delete") = New Action(Of IMessageReceiver)(AddressOf MessageAPI.DeleteSync)
            script("send") = New Action(Of String)(AddressOf MessageAPI.SendSync)
            script("wait") = New Action(Of String)(AddressOf MessageAPI.WaitSync)
            script("lastMessage") = New Func(Of String)(AddressOf MessageAPI.LastMessage)
            'Path
            system("path") = New LuaTable
            script = system("path")
            script("resource") = PathAPI.GetPath(PathType.Resource)
            script("setRecource") = New Action(Of String)(AddressOf PathAPI.SetResource)
            script("skin") = PathAPI.GetPath(PathType.Skin)
            script("setSkin") = New Action(Of String)(AddressOf PathAPI.SetSkin)
            script("plugin") = PathAPI.GetPath(PathType.Plugin)
            script("setPlugin") = New Action(Of String)(AddressOf PathAPI.SetPlugin)
            script("script") = PathAPI.GetPath(PathType.Script)
            script("setScript") = New Action(Of String)(AddressOf PathAPI.SetScript)
            script("userfile") = PathAPI.GetPath(PathType.UserFile)
            script("setUserfile") = New Action(Of String)(AddressOf PathAPI.SetUserFile)
            script("game") = PathAPI.GetPath(PathType.Game)
            script("getPath") = New Func(Of PathType, String, String)(AddressOf PathAPI.GetPath)
            script("getUri") = New Func(Of PathType, String, Uri)(AddressOf PathAPI.GetUri)
            'Plugin
            system("plugin") = New LuaTable
            script = system("plugin")
            script("add") = New Action(Of String)(AddressOf PluginAPI.Add)
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
            'Window[不包括GetChildByName InvokeSync InvokeAsync这些涉及委托或泛型的API]
            system("window") = New LuaTable
            script = system("window")
            script("setTitle") = New Action(Of String)(AddressOf WindowAPI.SetTitleSync)
            script("clearContent") = New Action(Of Panel)(AddressOf WindowAPI.ClearContentSync)
            script("loadElement") = New Action(Of Panel, String)(AddressOf WindowAPI.LoadElementSync)
            script("loadElementA") = New Action(Of Panel, String)(AddressOf WindowAPI.LoadElementAsync)
            script("loadPage") = New Action(Of String, NavigateOperation)(AddressOf WindowAPI.LoadPageSync)
            script("loadPageA") = New Action(Of String, NavigateOperation)(AddressOf WindowAPI.LoadPageAsync)
            script("loadObject") = New Action(Of Page, NavigateOperation)(AddressOf WindowAPI.LoadObjectSync)
            script("loadObjectA") = New Action(Of Page, NavigateOperation)(AddressOf WindowAPI.LoadObjectAsync)
            script("loadUri") = New Action(Of Uri, NavigateOperation)(AddressOf WindowAPI.LoadUriSync)
            script("loadUriA") = New Action(Of Uri, NavigateOperation)(AddressOf WindowAPI.LoadUriAsync)
            script("fadeOutPage") = New Action(Of Integer)(AddressOf WindowAPI.FadeOutPageSync)
            script("fadeOutPageA") = New Action(Of Integer)(AddressOf WindowAPI.FadeOutPageAsync)
            script("fadeInPage") = New Action(Of Integer)(AddressOf WindowAPI.FadeInPageSync)
            script("fadeInPageA") = New Action(Of Integer)(AddressOf WindowAPI.FadeInPageAsync)
            script("goBack") = New Action(AddressOf WindowAPI.GoBackSync)
            script("goForward") = New Action(AddressOf WindowAPI.GoForwardSync)
            script("removeOneBack") = New Action(AddressOf WindowAPI.RemoveOneBackSync)
            script("removeBackList") = New Action(AddressOf WindowAPI.RemoveBackListSync)
            script("setBackgroundByColor") = New Action(Of Color)(AddressOf WindowAPI.SetBackgroundByColorSync)
            script("setBackgroundByRGB") = New Action(Of Byte, Byte, Byte)(AddressOf WindowAPI.SetBackgroundByRgbSync)
            script("setBackgroundByHex") = New Action(Of String)(AddressOf WindowAPI.SetBackgroundByHexSync)
            script("setWidth") = New Action(Of Double)(AddressOf WindowAPI.SetWidthSync)
            script("setHeight") = New Action(Of Double)(AddressOf WindowAPI.SetHeightSync)
            script("setResizeMode") = New Action(Of Boolean)(AddressOf WindowAPI.SetResizeModeSync)
            script("setTopmost") = New Action(Of Boolean)(AddressOf WindowAPI.SetTopmostSync)
            script("setIcon") = New Action(Of String)(AddressOf WindowAPI.SetIconSync)
            script("setCursor") = New Action(Of String)(AddressOf WindowAPI.SetCursorSync)
            script("searchObject") = New Func(Of String, FrameworkElement)(AddressOf WindowAPI.SearchObject(Of FrameworkElement))
            script("getRoot") = New Func(Of Panel)(AddressOf WindowAPI.GetRoot(Of Panel))
            script("getDispatcher") = New Func(Of Windows.Threading.Dispatcher)(AddressOf WindowAPI.GetDispatcher)
            script("getWindow") = New Func(Of NavigationWindow)(AddressOf WindowAPI.GetWindow)
            script("invokeFunction") = New Func(Of Func(Of Object, Object), Object, Object)(AddressOf WindowAPI.InvokeFunction)
            script("getImage") = New Func(Of JpegBitmapEncoder)(AddressOf WindowAPI.GetImage)
            script("saveImage") = New Action(Of String)(AddressOf WindowAPI.SaveImage)
            script("addElement") = New Func(Of String, String, Double, Double, Double, Double, String, FrameworkElement)(AddressOf WindowAPI.AddElement)
            '环境变量
            system("env") = New LuaTable
            script = system("env")
            script("version") = "1.0"
            script("luaEngine") = LuaGlobal.VersionString
        End Sub

    End Class

End Namespace
