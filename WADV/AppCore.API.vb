Imports System.Xml
Imports System.Windows.Markup
Imports System.IO
Imports System.Threading
Imports System.Windows.Threading

Namespace AppCore.API

    ''' <summary>
    ''' 窗口API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WindowAPI

        ''' <summary>
        ''' 修改窗口标题
        ''' </summary>
        ''' <param name="text">新标题</param>
        ''' <param name="dispatcher">渲染管线</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeTitle(text As String, dispatcher As Dispatcher)
            Config.WindowConfig.GetDispatcher(dispatcher).Invoke(Sub() Config.WindowConfig.BaseWindow.Title = text)
        End Sub

        ''' <summary>
        ''' 清空指定容器并加载子元素
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <param name="fileName">子元素所在的文件名(Skin目录下)</param>
        ''' <param name="dispatcher">渲染管线</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadPage(content As Panel, fileName As String, dispatcher As Dispatcher)
            Config.WindowConfig.GetDispatcher(dispatcher).Invoke(Sub() content.Children.Clear())
            LoadElement(content, fileName, dispatcher)
        End Sub

        ''' <summary>
        ''' 为指定容器加载子元素
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <param name="name">子元素所在的文件名(Skin目录下)</param>
        ''' <param name="dispatcher">渲染管线</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadElement(content As Panel, name As String, dispatcher As Dispatcher)
            Config.WindowConfig.GetDispatcher(dispatcher).Invoke(Sub() content.Children.Add(XamlReader.Load(XmlTextReader.Create(Config.URLConfig.GetFullURI(Config.URLConfig.Skin, name)))))
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' </summary>
        ''' <param name="color">颜色对象</param>
        ''' <param name="dispatcher">渲染管线</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeBackgroundColorByColor(color As Color, dispatcher As Dispatcher)
            Config.WindowConfig.GetDispatcher(dispatcher).Invoke(Sub() Config.WindowConfig.BaseWindow.Background = New SolidColorBrush(color))
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' </summary>
        ''' <param name="r">红色值</param>
        ''' <param name="g">绿色值</param>
        ''' <param name="b">蓝色值</param>
        ''' <param name="dispatcher">渲染管线</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeBackgroundColorByRGB(r As Byte, g As Byte, b As Byte, dispatcher As Dispatcher)
            Config.WindowConfig.GetDispatcher(dispatcher).Invoke(Sub() Config.WindowConfig.BaseWindow.Background = New SolidColorBrush(Color.FromRgb(r, g, b)))
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' </summary>
        ''' <param name="hex">16进制颜色值</param>
        ''' <param name="dispatcher">渲染管线</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeBackgroundColorByHex(hex As String, dispatcher As Dispatcher)
            Config.WindowConfig.GetDispatcher(dispatcher).Invoke(Sub() Config.WindowConfig.BaseWindow.Background = New SolidColorBrush(ColorConverter.ConvertFromString(hex)))
        End Sub

        ''' <summary>
        ''' 修改窗口宽度
        ''' </summary>
        ''' <param name="width">新的宽度</param>
        ''' <param name="dispatcher">渲染管线</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeWindowWidth(width As Double, dispatcher As Dispatcher)
            Config.WindowConfig.GetDispatcher(dispatcher).Invoke(Sub() Config.WindowConfig.BaseWindow.Width = width)
        End Sub

        ''' <summary>
        ''' 修改窗口高度
        ''' </summary>
        ''' <param name="height">新的高度</param>
        ''' <param name="dispatcher">渲染管线</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeWindowHeight(height As Double, dispatcher As Dispatcher)
            Config.WindowConfig.GetDispatcher(dispatcher).Invoke(Sub() Config.WindowConfig.BaseWindow.Height = height)
        End Sub

        ''' <summary>
        ''' 设置窗口调整模式
        ''' </summary>
        ''' <param name="canResize">是否能够调整大小</param>
        ''' <param name="dispatcher">渲染管线</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeWindowResizeMod(canResize As Boolean, dispatcher As Dispatcher)
            Config.WindowConfig.GetDispatcher(dispatcher).Invoke(Sub() Config.WindowConfig.BaseWindow.ResizeMode = If(canResize, ResizeMode.CanResize, ResizeMode.CanMinimize))
        End Sub

        ''' <summary>
        ''' 设置窗口覆盖模式
        ''' </summary>
        ''' <param name="isTopmost">是否保持最前</param>
        ''' <param name="dispatcher">渲染管线</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeWindowTopmost(isTopmost As Boolean, dispatcher As Dispatcher)
            Config.WindowConfig.GetDispatcher(dispatcher).Invoke(Sub() Config.WindowConfig.BaseWindow.Topmost = isTopmost)
        End Sub

        ''' <summary>
        ''' 设置窗口图标
        ''' </summary>
        ''' <param name="fileName">图标文件名称(ICO格式且放置在Skin目录下)</param>
        ''' <param name="dispatcher">渲染管线</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeWindowIcon(fileName As String, dispatcher As Dispatcher)
            Config.WindowConfig.GetDispatcher(dispatcher).Invoke(Sub() Config.WindowConfig.BaseWindow.Icon = BitmapFrame.Create(New Uri(Config.URLConfig.GetFullURI(Config.URLConfig.Skin, fileName))))
        End Sub

        ''' <summary>
        ''' 设置窗口指针
        ''' </summary>
        ''' <param name="fileName">指针文件名称(ANI或CUR格式且放置在Skin目录下)</param>
        ''' <param name="dispatcher">渲染管线</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeWindowCursor(fileName As String, dispatcher As Dispatcher)
            Config.WindowConfig.GetDispatcher(dispatcher).Invoke(Sub() Config.WindowConfig.BaseWindow.Cursor = New Cursor(Config.URLConfig.GetFullURI(Config.URLConfig.Skin, fileName)))
        End Sub

        ''' <summary>
        ''' 获取窗口渲染管线
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetWindowDispatcher() As Windows.Threading.Dispatcher
            Return Config.WindowConfig.BaseWindow.Dispatcher
        End Function

        ''' <summary>
        ''' 获取窗口对象
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetWindow() As Window
            Return Config.WindowConfig.BaseWindow
        End Function

        ''' <summary>
        ''' 获取主窗口的Grid
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetMainGrid() As Grid
            Return Config.WindowConfig.BaseGrid
        End Function

    End Class

    ''' <summary>
    ''' 资源API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ResourceAPI

        ''' <summary>
        ''' 加载资源到游戏全局
        ''' </summary>
        ''' <param name="fileName">资源文件名(Skin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadResourceToApplication(fileName As String)
            Dim tmpDictionart As New ResourceDictionary
            tmpDictionart.Source = New Uri(Config.URLConfig.GetFullURI(Config.URLConfig.Skin, fileName))
            Application.Current.Resources.MergedDictionaries.Add(tmpDictionart)
        End Sub

        ''' <summary>
        ''' 加载资源到主窗口
        ''' </summary>
        ''' <param name="fileName">资源文件名(Skin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadResourceToWindow(fileName As String)
            Dim tmpDictionart As New ResourceDictionary
            tmpDictionart.Source = New Uri(Config.URLConfig.GetFullURI(Config.URLConfig.Skin, fileName))
            WindowAPI.GetWindowDispatcher.Invoke(Sub() WindowAPI.GetWindow.Resources.MergedDictionaries.Add(tmpDictionart))
        End Sub

        ''' <summary>
        ''' 清空全局资源
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ClearResourceFromApplication()
            Application.Current.Resources.MergedDictionaries.Clear()
        End Sub

        ''' <summary>
        ''' 清空主窗口资源
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ClearResourceFromWindow()
            WindowAPI.GetWindowDispatcher.Invoke(Sub() WindowAPI.GetWindow.Resources.MergedDictionaries.Clear())
        End Sub

        ''' <summary>
        ''' 清除指定全局资源
        ''' </summary>
        ''' <param name="resource">要清除的资源对象</param>
        ''' <remarks></remarks>
        Public Shared Sub RemoveResourceFromApplication(resource As ResourceDictionary)
            Application.Current.Resources.MergedDictionaries.Remove(resource)
        End Sub

        ''' <summary>
        ''' 清除指定主窗口资源
        ''' </summary>
        ''' <param name="resource">要清除的资源对象</param>
        ''' <remarks></remarks>
        Public Shared Sub RemoveResourceFromWindow(resource As ResourceDictionary)
            WindowAPI.GetWindowDispatcher.Invoke(Sub() WindowAPI.GetWindow.Resources.MergedDictionaries.Remove(resource))
        End Sub

        ''' <summary>
        ''' 获取全局资源对象
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetApplicationResource() As ResourceDictionary
            Return Application.Current.Resources
        End Function

        ''' <summary>
        ''' 获取主窗口资源对象
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetWindowResource() As ResourceDictionary
            Return WindowAPI.GetWindowDispatcher.Invoke(Function() WindowAPI.GetWindow.Resources)
        End Function

        ''' <summary>
        ''' 根据名称获取元素的子元素(支持多级查找)
        ''' </summary>
        ''' <typeparam name="T">子元素类型</typeparam>
        ''' <param name="obj">父元素</param>
        ''' <param name="name">子元素的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetChildByName(Of T As FrameworkElement)(obj As DependencyObject, name As String) As T
            Return obj.Dispatcher.Invoke(Function()
                                             Dim child As DependencyObject = Nothing
                                             Dim grandChild As T = Nothing
                                             For i = 0 To VisualTreeHelper.GetChildrenCount(obj) - 1
                                                 child = VisualTreeHelper.GetChild(obj, i)
                                                 If (TypeOf child Is T) AndAlso (name = "" OrElse TryCast(child, T).Name = name) Then
                                                     Return TryCast(child, T)
                                                 Else
                                                     grandChild = GetChildByName(Of T)(child, name)
                                                     If grandChild IsNot Nothing Then Return grandChild
                                                 End If
                                             Next
                                             Return Nothing
                                         End Function)
        End Function

    End Class

    ''' <summary>
    ''' 插件API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PluginAPI

        ''' <summary>
        ''' 加载一个插件
        ''' </summary>
        ''' <param name="fileName">插件文件名(Plugin目录下)</param>
        ''' <returns>是否加载成功</returns>
        ''' <remarks></remarks>
        Public Shared Function AddPlugin(fileName As String) As Boolean
            Return Config.PluginConfig.AddPlugin(fileName)
        End Function

    End Class

    ''' <summary>
    ''' 逻辑循环API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LoopAPI

        ''' <summary>
        ''' 初始化或重新初始化逻辑循环
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub InitialiseLoop()
            [Loop].MainLoop.InitialiseLoop()
        End Sub

        ''' <summary>
        ''' 改变每秒的循环次数
        ''' </summary>
        ''' <param name="frame">新的次数</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeFrame(frame As Integer)
            If frame < 1 Then Throw New Exception("逻辑更新频率不能小于每秒1次")
            Config.LoopConfig.Frame = frame
        End Sub

        ''' <summary>
        ''' 获取每秒的循环次数
        ''' </summary>
        ''' <returns>循环次数</returns>
        ''' <remarks></remarks>
        Public Shared Function GetFrame() As Integer
            Return Config.LoopConfig.Frame
        End Function

        ''' <summary>
        ''' 添加一个逻辑循环
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Sub AddLoop(loopContent As Plugin.ILoop)
            [Loop].MainLoop.AddLoop(loopContent)
        End Sub

        ''' <summary>
        ''' 添加一个小型逻辑循环
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Sub AddCustomizedLoop(loopContent As Plugin.ICustomizedLoop)
            [Loop].MainLoop.AddCustomizedLoop(loopContent)
        End Sub

        ''' <summary>
        ''' 等待一个小型逻辑循环退出
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Sub WaitCustomizedLoop(loopContent As Plugin.ICustomizedLoop)
            [Loop].MainLoop.WaitCustomizedLoop(loopContent)
        End Sub

        ''' <summary>
        ''' 标记逻辑循环为进行状态
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StartMainLoop()
            [Loop].MainLoop.StartMainLoop()
        End Sub

        ''' <summary>
        ''' 标记逻辑循环为停止状态
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopMainLoop()
            [Loop].MainLoop.StopMainLoop()
        End Sub

    End Class

    ''' <summary>
    ''' 脚本API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ScriptAPI

        Public Shared Sub ShowMessage(content As String, title As String)
            MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Information)
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码
        ''' </summary>
        ''' <param name="fileName">文件名(Script目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub RunFile(fileName As String)
            Script.Exchanger.RunFile(Config.URLConfig.GetFullURI(Config.URLConfig.Script, fileName))
        End Sub

        Public Shared Sub Wait(frame As Integer)
            System.Threading.Thread.Sleep(frame * (1000.0 / Config.LoopConfig.Frame))
        End Sub

        Public Shared Sub RunFileInThread(fileName As String)
            Dim tmpThread As New Thread(New ParameterizedThreadStart(AddressOf RunFile))
            tmpThread.Start(fileName)
        End Sub

        ''' <summary>
        ''' 执行一段字符串脚本
        ''' </summary>
        ''' <param name="script">脚本代码内容</param>
        ''' <remarks></remarks>
        Public Shared Sub RunStrng(script As String)
            AppCore.Script.Exchanger.RunStrng(script)
        End Sub

        ''' <summary>
        ''' 执行一个存在的脚本函数
        ''' </summary>
        ''' <param name="functionName">函数名</param>
        ''' <param name="params">参数列表</param>
        ''' <returns>返回值列表</returns>
        ''' <remarks></remarks>
        Public Shared Function RunFunction(functionName As String, params() As Object) As Object()
            Return Script.Exchanger.RunFunction(functionName, params)
        End Function

        ''' <summary>
        ''' 设置脚本全局变量的值
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">变量内容(字符串形式)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetGlobalVariable(name As String, value As String)
            RunStrng(String.Format("{0}={1}", name, value))
        End Sub

        ''' <summary>
        ''' 设置脚本全局变量的值(字符串限定)
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">变量内容</param>
        ''' <remarks></remarks>
        Public Shared Sub SetGlobalStringVariable(name As String, value As String)
            SetGlobalVariable(name, """" & value & """")
        End Sub

        ''' <summary>
        ''' 获取脚本全局变量的值
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetVariable(name As String) As Object
            Return Script.Exchanger.GetVariable(name)
        End Function

        ''' <summary>
        ''' 获取脚本全局变量的值的字符串形式
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetStringVariable(name As String) As String
            Return Script.Exchanger.GetStringVariable(name)
        End Function

        ''' <summary>
        ''' 获取脚本全局变量的值的浮点数形式
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetDoubleVariable(name As String) As Double
            Return Script.Exchanger.GetDoubleVariable(name)
        End Function

        ''' <summary>
        ''' 获取脚本全局变量的值的整数形式
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetIntegerVariable(name As String) As Integer
            Return CInt(GetDoubleVariable(name))
        End Function

        ''' <summary>
        ''' 获取脚本全局变量的值的LUA表的形式
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTableVariable(name As String) As LuaInterface.LuaTable
            Return Script.Exchanger.GetTableVariable(name)
        End Function

        ''' <summary>
        ''' 获取获取脚本全局变量(Table类型限定)中某个项的值
        ''' </summary>
        ''' <param name="tableName">表名</param>
        ''' <param name="key">键</param>
        ''' <returns>值</returns>
        ''' <remarks></remarks>
        Public Shared Function GetVariableInTable(tableName As String, key As String) As Object
            Return Script.Exchanger.GetVariableInTable(tableName, key)
        End Function

        ''' <summary>
        ''' 获取游戏脚本主机对象
        ''' </summary>
        ''' <returns>脚本主机</returns>
        ''' <remarks></remarks>
        Public Shared Function GetScriptVM() As LuaInterface.Lua
            Return Script.ScriptCore.ScriptVM
        End Function

    End Class

    ''' <summary>
    ''' 路径API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class URLAPI

        ''' <summary>
        ''' 获取程序资源文件的存放路径
        ''' </summary>
        ''' <returns>程序资源文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function GetResourceURL() As String
            Return Config.URLConfig.Resource
        End Function

        ''' <summary>
        ''' 获取程序皮肤文件的存放路径
        ''' </summary>
        ''' <returns>皮肤文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function GetSkinURL() As String
            Return Config.URLConfig.Skin
        End Function

        ''' <summary>
        ''' 获取程序插件的存放路径
        ''' </summary>
        ''' <returns>插件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function GetPluginURL() As String
            Return Config.URLConfig.Plugin
        End Function

        ''' <summary>
        ''' 获取程序脚本文件的存放路径
        ''' </summary>
        ''' <returns>脚本文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function GetScriptURL() As String
            Return Config.URLConfig.Script
        End Function

        ''' <summary>
        ''' 获取程序用户个人文件的存放路径
        ''' </summary>
        ''' <returns>用户个人文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function GetUserFileURL() As String
            Return Config.URLConfig.UserFile
        End Function

        ''' <summary>
        ''' 合并路径
        ''' </summary>
        ''' <param name="urlType">资源路径类型</param>
        ''' <param name="fileURL">从资源目录开始的文件相对路径</param>
        ''' <returns>文件的绝对路径</returns>
        ''' <remarks></remarks>
        Public Shared Function CombineURL(urlType As String, Optional fileURL As String = "") As String
            Return Config.URLConfig.GetFullURI(urlType, fileURL)
        End Function

    End Class

End Namespace
