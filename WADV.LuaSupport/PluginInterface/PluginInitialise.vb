Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports Neo.IronLua
Imports WADV.Core
Imports WADV.Core.PluginInterface

Namespace PluginInterface
    Friend NotInheritable Class PluginInitialise : Implements IPluginInitialise
        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            Dim system, script As LuaTable
            ScriptCore.GetInstance.Environment("core") = New LuaTable
            system = ScriptCore.GetInstance.Environment("core")
            'Loop
            system("loop") = New LuaTable
            script = system("loop")
            script("start") = New Action(AddressOf [Loop].Start)
            script("stop") = New Action(AddressOf [Loop].Stop)
            script("frame") = New Func(Of Integer, Integer)(AddressOf [Loop].Frame)
            script("totalFrame") = New Func(Of Integer)(AddressOf [Loop].TotalFrame)
            script("waitFrame") = New Action(Of Integer)(AddressOf [Loop].WaitFrame)
            script("waitLoop") = New Action(Of ILoopReceiver)(AddressOf [Loop].WaitLoop)
            script("listen") = New Action(Of ILoopReceiver)(AddressOf [Loop].Listen)
            script("status") = New Func(Of Boolean)(AddressOf [Loop].Status)
            script("toTime") = New Func(Of Integer, TimeSpan)(AddressOf [Loop].ToTime)
            script("toFrame") = New Func(Of TimeSpan, Integer)(AddressOf [Loop].ToFrame)
            'Message
            system("message") = New LuaTable
            script = system("message")
            script("start") = New Action(AddressOf Message.Start)
            script("stop") = New Action(AddressOf Message.[Stop])
            script("status") = New Func(Of Boolean)(AddressOf Message.Status)
            script("listen") = New Action(Of IMessageReceiver)(AddressOf Message.Listen)
            script("remove") = New Action(Of IMessageReceiver)(AddressOf Message.Remove)
            script("send") = New Action(Of String)(AddressOf Message.Send)
            script("wait") = New Action(Of String)(AddressOf Message.Wait)
            script("last") = New Func(Of String)(AddressOf Message.Last)
            'Path
            system("path") = New LuaTable
            script = system("path")
            script("resource") = New Func(Of String, String)(AddressOf Path.Resource)
            script("skin") = New Func(Of String, String)(AddressOf Path.Skin)
            script("plugin") = New Func(Of String, String)(AddressOf Path.Plugin)
            script("script") = New Func(Of String, String)(AddressOf Path.Script)
            script("userfile") = New Func(Of String, String)(AddressOf Path.UserFile)
            script("game") = New Func(Of String)(AddressOf Path.Game)
            script("combine") = New Func(Of PathType, String, String)(AddressOf Path.Combine)
            script("combineUri") = New Func(Of PathType, String, Uri)(AddressOf Path.CombineUri)
            'Plugin
            system("plugin") = New LuaTable
            script = system("plugin")
            script("add") = New Action(Of String)(AddressOf Plugin.Add)
            script("listen") = New Func(Of IPluginLoadReceiver, Boolean)(AddressOf Plugin.Listen)
            script("compile") = New Func(Of String, String, Reflection.Assembly)(AddressOf Plugin.Compile)
            script("load") = New Func(Of String, Reflection.Assembly)(AddressOf Plugin.Load)
            script("handleEvent") = New Action(Of Object, String, [Delegate])(AddressOf Plugin.HandleEvent)
            script("unhandleEvent") = New Action(Of Object, String, [Delegate])(AddressOf Plugin.UnhandleEvent)
            'Resource
            system("resource") = New LuaTable
            script = system("resource")
            script("loadToGame") = New Action(Of String)(AddressOf Resource.LoadToGame)
            script("loadToWindow") = New Action(Of String)(AddressOf Resource.LoadToWindow)
            script("clearGame") = New Action(AddressOf Resource.ClearGame)
            script("clearWindow") = New Action(AddressOf Resource.ClearWindow)
            script("removeFromGame") = New Action(Of ResourceDictionary)(AddressOf Resource.RemoveFromGame)
            script("removeFromWindow") = New Action(Of ResourceDictionary)(AddressOf Resource.RemoveFromWindow)
            script("getFromGame") = New Func(Of ResourceDictionary)(AddressOf Resource.GetFromGame)
            script("getFromWindow") = New Func(Of ResourceDictionary)(AddressOf Resource.GetFromWindow)
            script("register") = New Func(Of String, FrameworkElement, Boolean)(AddressOf Resource.Register)
            script("unregister") = New Func(Of String, Boolean)(AddressOf Resource.Unregister)
            script("getByName") = New Func(Of String, FrameworkElement)(AddressOf Resource.GetByName)
            'Timer
            system("timer") = New LuaTable
            script = system("timer")
            script("start") = New Action(AddressOf Timer.Start)
            script("stop") = New Action(AddressOf Timer.[Stop])
            script("tick") = New Func(Of Integer, Integer)(AddressOf Timer.Tick)
            script("status") = New Func(Of Boolean)(AddressOf Timer.Status)
            'Window[不包括Search GetChildByName InvokeSync InvokeAsync]
            system("window") = New LuaTable
            script = system("window")
            script("title") = New Func(Of String, String)(AddressOf Core.API.Window.Title)
            script("clearContent") = New Action(Of Panel)(AddressOf Core.API.Window.ClearContent)
            script("loadElement") = New Action(Of Panel, String)(AddressOf Core.API.Window.LoadElement)
            script("loadElementA") = New Action(Of Panel, String)(AddressOf Core.API.Window.LoadElementAsync)
            script("loadPage") = New Action(Of String, NavigateOperation)(AddressOf Core.API.Window.LoadPage)
            script("loadPageA") = New Action(Of String, NavigateOperation)(AddressOf Core.API.Window.LoadPageAsync)
            script("loadObject") = New Action(Of Page, NavigateOperation)(AddressOf Core.API.Window.LoadObject)
            script("loadObjectA") = New Action(Of Page, NavigateOperation)(AddressOf Core.API.Window.LoadObjectAsync)
            script("loadUri") = New Action(Of Uri, NavigateOperation)(AddressOf Core.API.Window.LoadUri)
            script("loadUriA") = New Action(Of Uri, NavigateOperation)(AddressOf Core.API.Window.LoadUriAsync)
            script("fadeOut") = New Action(Of Integer)(AddressOf Core.API.Window.FadeOut)
            script("fadeOutA") = New Action(Of Integer)(AddressOf Core.API.Window.FadeOutAsync)
            script("fadeIn") = New Action(Of Integer)(AddressOf Core.API.Window.FadeIn)
            script("fadeInA") = New Action(Of Integer)(AddressOf Core.API.Window.FadeInAsync)
            script("back") = New Action(AddressOf Core.API.Window.Back)
            script("forward") = New Action(AddressOf Core.API.Window.Forward)
            script("removeOneBack") = New Action(AddressOf Core.API.Window.RemoveOneBack)
            script("removeBackList") = New Action(AddressOf Core.API.Window.RemoveBackList)
            script("setBackgroundByColor") = New Action(Of Color)(AddressOf Core.API.Window.SetBackgroundByColor)
            script("setBackgroundByRGB") = New Action(Of Byte, Byte, Byte)(AddressOf Core.API.Window.SetBackgroundByRgb)
            script("setBackgroundByHex") = New Action(Of String)(AddressOf Core.API.Window.SetBackgroundByHex)
            script("width") = New Func(Of Double, Double)(AddressOf Core.API.Window.Width)
            script("height") = New Func(Of Double, Double)(AddressOf Core.API.Window.Height)
            script("resize") = New Action(Of Boolean)(AddressOf Core.API.Window.Resize)
            script("topmost") = New Action(Of Boolean)(AddressOf Core.API.Window.Topmost)
            script("icon") = New Action(Of String)(AddressOf Core.API.Window.Icon)
            script("cursor") = New Action(Of String)(AddressOf Core.API.Window.Cursor)
            script("search") = New Func(Of String, FrameworkElement)(AddressOf Core.API.Window.Search(Of FrameworkElement))
            script("root") = New Func(Of Panel)(AddressOf Core.API.Window.Root(Of Panel))
            script("dispatcher") = New Func(Of Threading.Dispatcher)(AddressOf Core.API.Window.Dispatcher)
            script("window") = New Func(Of NavigationWindow)(AddressOf Core.API.Window.Window)
            script("invoke") = New Func(Of Func(Of Object, Object), Object, Object)(AddressOf Core.API.Window.InvokeFunction)
            script("image") = New Func(Of JpegBitmapEncoder)(AddressOf Core.API.Window.Image)
            script("save") = New Action(Of String)(AddressOf Core.API.Window.Save)
            script("add") = New Func(Of String, String, Double, Double, Double, Double, String, FrameworkElement)(AddressOf Core.API.Window.Add)
            '组件
            system("component") = New LuaTable
            script = system("component")
            script("from") = New Func(Of FrameworkElement, Component.ComponentList)(AddressOf Core.API.Component.From)
            script("remove") = New Action(Of FrameworkElement)(AddressOf Core.API.Component.Remove)
            '环境变量
            system("env") = New LuaTable
            script = system("env")
            script("version") = "1.0"
            script("luaEngine") = LuaGlobal.VersionString
            '注册组件到系统
            Core.API.Script.Register(ScriptCore.GetInstance)
            Send("[LUA]SCRIPT_INIT_FINISH")
            Return Core.API.Plugin.Listen(New PluginLoadReceiver)
        End Function
    End Class
End Namespace