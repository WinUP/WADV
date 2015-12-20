Imports System.ComponentModel
Imports Neo.IronLua
Imports WADV.Core.Script
Imports WADV.Core.Render
Imports WADV.Core.PluginInterface

Namespace PluginInterface
    Friend NotInheritable Class PluginInitialise : Implements IPluginInitialise
        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            '注册组件到系统
            Script.Chorus80_Register(New ScriptCore)
            Dim target As New Field With {.Name = "loop"}
            target.Content.Add("span", New Func(Of Integer, Integer)(AddressOf [Loop].Span))
            target.Content.Add("totalFrame", New Func(Of Integer)(AddressOf [Loop].TotalFrame))
            target.Content.Add("waitFrame", New Action(Of Integer)(AddressOf [Loop].WaitFrame))
            target.Content.Add("waitLoop", New Action(Of ILoopReceiver)(AddressOf [Loop].WaitLoop))
            target.Content.Add("listen", New Action(Of ILoopReceiver)(AddressOf [Loop].Listen))
            target.Content.Add("status", New Func(Of Object, Boolean)(AddressOf [Loop].Status))
            target.Content.Add("toTime", New Func(Of Integer, TimeSpan)(AddressOf [Loop].ToTime))
            target.Content.Add("toFrame", New Func(Of TimeSpan, Integer)(AddressOf [Loop].ToFrame))
            Script.RegisterField(target)
            target = New Field With {.Name = "message"}
            target.Content.Add("status", New Func(Of Object, Boolean)(AddressOf Message.Status))
            target.Content.Add("listen", New Action(Of IMessageReceiver)(AddressOf Message.Listen))
            target.Content.Add("remove", New Action(Of IMessageReceiver)(AddressOf Message.Remove))
            target.Content.Add("send", New Action(Of String)(AddressOf Message.Send))
            target.Content.Add("wait", New Action(Of String)(AddressOf Message.Wait))
            target.Content.Add("last", New Func(Of String)(AddressOf Message.Last))
            Script.RegisterField(target)
            target = New Field With {.Name = "path"}
            target.Content.Add("resource", New Func(Of String, String)(AddressOf Path.Resource))
            target.Content.Add("skin", New Func(Of String, String)(AddressOf Path.Skin))
            target.Content.Add("plugin", New Func(Of String, String)(AddressOf Path.Plugin))
            target.Content.Add("script", New Func(Of String, String)(AddressOf Path.Script))
            target.Content.Add("userfile", New Func(Of String, String)(AddressOf Path.UserFile))
            target.Content.Add("game", New Func(Of String)(AddressOf Path.Game))
            target.Content.Add("combine", New Func(Of Core.PathType, String, String)(AddressOf Path.Combine))
            target.Content.Add("combineUri", New Func(Of Core.PathType, String, Uri)(AddressOf Path.CombineUri))
            Script.RegisterField(target)
            target = New Field With {.Name = "plugin"}
            target.Content.Add("add", New Action(Of String)(AddressOf Plugin.Add))
            target.Content.Add("listen", New Func(Of IPluginLoadReceiver, Boolean)(AddressOf Plugin.Listen))
            target.Content.Add("compile", New Func(Of String, String, Reflection.Assembly)(AddressOf Plugin.Compile))
            target.Content.Add("load", New Func(Of String, Reflection.Assembly)(AddressOf Plugin.Load))
            target.Content.Add("create", New Func(Of String, Object(), Object)(AddressOf Plugin.Create))
            Script.RegisterField(target)
            target = New Field With {.Name = "timer"}
            target.Content.Add("tick", New Func(Of Integer, Integer)(AddressOf Timer.Tick))
            target.Content.Add("status", New Func(Of Object, Boolean)(AddressOf Timer.Status))
            Script.RegisterField(target)
            target = New Field With {.Name = "game"}
            target.Content.Add("chorus01_PrepareSystem", New Action(AddressOf Game.Chorus01_PrepareSystem))
            target.Content.Add("chorus02_LoadPlugins", New Action(AddressOf Game.Chorus02_LoadPlugins))
            target.Content.Add("chorus03_Start", New Action(Of WindowBase, Integer, Integer)(AddressOf Game.Chorus03_Start))
            target.Content.Add("chorusFF_Stop", New Action(Of CancelEventArgs)(AddressOf Game.ChorusFF_Stop))
            target.Content.Add("handle", New Action(Of Object, String, [Delegate])(AddressOf Game.Handle))
            target.Content.Add("unhandle", New Action(Of Object, String, [Delegate])(AddressOf Game.Unhandle))
            target.Content.Add("window", New Func(Of WindowBase)(AddressOf Game.Window))
            Script.RegisterField(target)
            target = New Field With {.Name = "env"}
            target.Content.Add("version", "1.1")
            target.Content.Add("luaEngine", LuaGlobal.VersionString)
            Script.RegisterField(target)
            Message.Send("[LUA]SCRIPT_INIT_FINISH")
            Return True
        End Function
    End Class
End Namespace