Imports System.Xml
Imports System.Windows.Markup
Imports System.IO
Imports System.Threading
Imports System.Windows.Threading
Imports WADV.AppCore.Looping
Imports WADV.AppCore.Path
Imports WADV.AppCore.Plugin
Imports WADV.AppCore.Script
Imports WADV.AppCore.UI

Namespace AppCore.API

    ''' <summary>
    ''' 窗口API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WindowAPI

        ''' <summary>
        ''' 修改窗口标题
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="text">新标题</param>
        ''' <remarks></remarks>
        Public Shared Sub SetTitle(text As String)
            WindowConfig.BaseWindow.Dispatcher.Invoke(Sub() WindowConfig.BaseWindow.Title = text)
        End Sub

        ''' <summary>
        ''' 清空指定容器并加载子元素
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <remarks></remarks>
        Public Shared Sub ClearContent(content As Panel)
            content.Dispatcher.Invoke(Sub() content.Children.Clear())
        End Sub

        ''' <summary>
        ''' 为指定容器加载子元素
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <param name="name">子元素所在的文件名(Skin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadElement(content As Panel, name As String)
            content.Dispatcher.Invoke(Sub() content.Children.Add(XamlReader.Load(XmlTextReader.Create(PathFunction.GetFullPath(PathConfig.Skin, name)))))
        End Sub

        ''' <summary>
        ''' 为指定容器加载子元素
        ''' 该方法会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <param name="name">子元素所在的文件名(Skin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadElementNow(content As Panel, name As String)
            content.Dispatcher.BeginInvoke(Sub() content.Children.Add(XamlReader.Load(XmlTextReader.Create(PathFunction.GetFullPath(PathConfig.Skin, name)))))
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="color">颜色对象</param>
        ''' <remarks></remarks>
        Public Shared Sub SetBackgroundByColor(color As Color)
            WindowConfig.BaseWindow.Dispatcher.Invoke(Sub() WindowConfig.BaseWindow.Background = New SolidColorBrush(color))
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="r">红色值</param>
        ''' <param name="g">绿色值</param>
        ''' <param name="b">蓝色值</param>
        ''' <remarks></remarks>
        Public Shared Sub SetBackgroundByRGB(r As Byte, g As Byte, b As Byte)
            WindowConfig.BaseWindow.Dispatcher.Invoke(Sub() WindowConfig.BaseWindow.Background = New SolidColorBrush(Color.FromRgb(r, g, b)))
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="hex">16进制颜色值</param>
        ''' <remarks></remarks>
        Public Shared Sub SetBackgroundByHex(hex As String)
            WindowConfig.BaseWindow.Dispatcher.Invoke(Sub() WindowConfig.BaseWindow.Background = New SolidColorBrush(ColorConverter.ConvertFromString(hex)))
        End Sub

        ''' <summary>
        ''' 修改窗口宽度
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="width">新的宽度</param>
        ''' <remarks></remarks>
        Public Shared Sub SetWidth(width As Double)
            WindowConfig.BaseWindow.Dispatcher.Invoke(Sub() WindowConfig.BaseWindow.Width = width)
        End Sub

        ''' <summary>
        ''' 修改窗口高度
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="height">新的高度</param>
        ''' <remarks></remarks>
        Public Shared Sub SetHeight(height As Double)
            WindowConfig.BaseWindow.Dispatcher.Invoke(Sub() WindowConfig.BaseWindow.Height = height)
        End Sub

        ''' <summary>
        ''' 设置窗口调整模式
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="canResize">是否能够调整大小</param>
        ''' <remarks></remarks>
        Public Shared Sub SetResizeMode(canResize As Boolean)
            WindowConfig.BaseWindow.Dispatcher.Invoke(Sub() WindowConfig.BaseWindow.ResizeMode = If(canResize, ResizeMode.CanResize, ResizeMode.CanMinimize))
        End Sub

        ''' <summary>
        ''' 设置窗口覆盖模式
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="isTopmost">是否保持最前</param>
        ''' <remarks></remarks>
        Public Shared Sub SetTopmost(isTopmost As Boolean)
            WindowConfig.BaseWindow.Dispatcher.Invoke(Sub() WindowConfig.BaseWindow.Topmost = isTopmost)
        End Sub

        ''' <summary>
        ''' 设置窗口图标
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="fileName">图标文件名称(ICO格式且放置在Skin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetIcon(fileName As String)
            WindowConfig.BaseWindow.Dispatcher.Invoke(Sub() WindowConfig.BaseWindow.Icon = BitmapFrame.Create(New Uri(PathFunction.GetFullPath(PathConfig.Skin, fileName))))
        End Sub

        ''' <summary>
        ''' 设置窗口指针
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="fileName">指针文件名称(ANI或CUR格式且放置在Skin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetCursor(fileName As String)
            WindowConfig.BaseWindow.Dispatcher.Invoke(Sub() WindowConfig.BaseWindow.Cursor = New Cursor(PathFunction.GetFullPath(PathConfig.Skin, fileName)))
        End Sub

        ''' <summary>
        ''' 根据名称获取元素的子元素(支持多级查找)
        ''' 该方法在UI线程中执行
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

        ''' <summary>
        ''' 获取窗口线程工作队列
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetDispatcher() As Windows.Threading.Dispatcher
            Return WindowConfig.BaseWindow.Dispatcher
        End Function

        ''' <summary>
        ''' 获取窗口对象
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetWindow() As Window
            Return WindowConfig.BaseWindow
        End Function

        ''' <summary>
        ''' 获取主窗口的Grid
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetGrid() As Grid
            Return WindowConfig.BaseGrid
        End Function

    End Class

    ''' <summary>
    ''' 资源API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ResourceAPI

        ''' <summary>
        ''' 加载资源到游戏全局
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="fileName">资源文件名(Skin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadToGame(fileName As String)
            Dim tmpDictionart As New ResourceDictionary
            tmpDictionart.Source = New Uri(PathFunction.GetFullPath(PathConfig.Skin, fileName))
            Application.Current.Resources.MergedDictionaries.Add(tmpDictionart)
        End Sub

        ''' <summary>
        ''' 加载资源到主窗口
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="fileName">资源文件名(Skin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadToWindow(fileName As String)
            Dim tmpDictionart As New ResourceDictionary
            tmpDictionart.Source = New Uri(PathFunction.GetFullPath(PathConfig.Skin, fileName))
            WindowAPI.GetDispatcher.Invoke(Sub() WindowAPI.GetWindow.Resources.MergedDictionaries.Add(tmpDictionart))
        End Sub

        ''' <summary>
        ''' 清空全局资源
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ClearGame()
            Application.Current.Resources.MergedDictionaries.Clear()
        End Sub

        ''' <summary>
        ''' 清空主窗口资源
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ClearWindow()
            WindowAPI.GetDispatcher.Invoke(Sub() WindowAPI.GetWindow.Resources.MergedDictionaries.Clear())
        End Sub

        ''' <summary>
        ''' 清除指定全局资源
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="resource">要清除的资源对象</param>
        ''' <remarks></remarks>
        Public Shared Sub RemoveFromGame(resource As ResourceDictionary)
            Application.Current.Resources.MergedDictionaries.Remove(resource)
        End Sub

        ''' <summary>
        ''' 清除指定主窗口资源
        ''' 该方法不会立即返回
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <param name="resource">要清除的资源对象</param>
        ''' <remarks></remarks>
        Public Shared Sub RemoveFromWindow(resource As ResourceDictionary)
            WindowAPI.GetDispatcher.Invoke(Sub() WindowAPI.GetWindow.Resources.MergedDictionaries.Remove(resource))
        End Sub

        ''' <summary>
        ''' 获取全局资源对象
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetFromGame() As ResourceDictionary
            Return Application.Current.Resources
        End Function

        ''' <summary>
        ''' 获取主窗口资源对象
        ''' 该方法在UI线程中执行
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetFromWindow() As ResourceDictionary
            Return WindowAPI.GetDispatcher.Invoke(Function() WindowAPI.GetWindow.Resources)
        End Function

    End Class

    ''' <summary>
    ''' 插件API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PluginAPI

        ''' <summary>
        ''' 加载一个插件
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="fileName">插件文件名(Plugin目录下)</param>
        ''' <returns>是否加载成功</returns>
        ''' <remarks></remarks>
        Public Shared Function Add(fileName As String) As Boolean
            Return PluginFunction.AddPlugin(fileName)
        End Function

    End Class

    ''' <summary>
    ''' 循环API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LoopingAPI

        ''' <summary>
        ''' 设置理想帧率
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="frame">新的次数</param>
        ''' <remarks></remarks>
        Public Shared Sub SetFrame(frame As Integer)
            If frame < 1 Then Throw New Exception("逻辑更新频率不能小于每秒1次")
            LoopingFunction.Frame = frame
        End Sub

        ''' <summary>
        ''' 获取理想帧率
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <returns>循环次数</returns>
        ''' <remarks></remarks>
        Public Shared Function GetFrame() As Integer
            Return LoopingFunction.Frame
        End Function

        ''' <summary>
        ''' 添加一个循环体
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Sub AddLoop(loopContent As Plugin.ILooping)
            MainLooping.GetInstance.AddLooping(loopContent)
        End Sub

        ''' <summary>
        ''' 等待一个小型逻辑循环退出
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Sub WaitLoop(loopContent As Plugin.ILooping)
            MainLooping.GetInstance.WaitLooping(loopContent)
        End Sub

        ''' <summary>
        ''' 标记逻辑循环为进行状态
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StartMainLoop()
            LoopingFunction.StartMainLooping()
        End Sub

        ''' <summary>
        ''' 标记逻辑循环为停止状态
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopMainLoop()
            LoopingFunction.StopMainLooping()
        End Sub

    End Class

    ''' <summary>
    ''' 脚本API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ScriptAPI

        ''' <summary>
        ''' 显示提示信息
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="content">内容</param>
        ''' <param name="title">标题</param>
        ''' <remarks></remarks>
        Public Shared Sub ShowMessage(content As String, title As String)
            MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Information)
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码
        ''' 该方法会立即返回
        ''' 该方法在新的线程中执行
        ''' 注意：当脚本主机正忙时方法将不执行任何操作，调用前请使用GetStatus()方法确认脚本主机状态
        ''' </summary>
        ''' <param name="fileName">文件路径(Script目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub RunFile(fileName As String)
            If Not Script.ScriptCore.GetInstance.BusyStatus Then
                Dim tmpThread As New Thread(Sub() Script.ScriptCore.GetInstance.RunFile(PathAPI.GetPath(PathAPI.Script, fileName)))
                tmpThread.Name = "脚本文件执行线程"
                tmpThread.IsBackground = True
                tmpThread.Priority = ThreadPriority.Normal
                tmpThread.Start()
            End If
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' 注意：当脚本主机正忙时方法将不执行任何操作，调用前请使用GetStatus()方法确认脚本主机状态
        ''' </summary>
        ''' <param name="filename">文件路径(Script目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub RunFileAndWait(filename As String)
            If Not Script.ScriptCore.GetInstance.BusyStatus Then Script.ScriptCore.GetInstance.RunFile(PathAPI.GetPath(PathAPI.Script, filename))
        End Sub

        ''' <summary>
        ''' 执行一段字符串脚本
        ''' 该方法会立即返回
        ''' 该方法在新的线程中执行
        ''' 注意：当脚本主机正忙时方法将不执行任何操作，调用前请使用GetStatus()方法确认脚本主机状态
        ''' </summary>
        ''' <param name="content">脚本代码内容</param>
        ''' <remarks></remarks>
        Public Shared Sub RunStrng(content As String)
            If Not script.ScriptCore.GetInstance.BusyStatus Then
                Dim tmpThread As New Thread(Sub() Script.ScriptCore.GetInstance.RunStrng(content))
                tmpThread.IsBackground = True
                tmpThread.Priority = ThreadPriority.Normal
                tmpThread.Start(content)
            End If
        End Sub

        ''' <summary>
        ''' 执行一段字符串脚本
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' 注意：当脚本主机正忙时方法将不执行任何操作，调用前请使用GetStatus()方法确认脚本主机状态
        ''' </summary>
        ''' <param name="content">脚本代码内容</param>
        ''' <remarks></remarks>
        Public Shared Sub RunStringAndWait(content As String)
            If Not Script.ScriptCore.GetInstance.BusyStatus Then Script.ScriptCore.GetInstance.RunStrng(content)
        End Sub

        ''' <summary>
        ''' 执行一个存在的脚本函数
        ''' 该方法在调用线程中执行
        ''' 注意：当脚本主机正忙时方法将不执行任何操作，调用前请使用GetStatus()方法确认脚本主机状态
        ''' </summary>
        ''' <param name="functionName">函数名</param>
        ''' <param name="params">参数列表</param>
        ''' <returns>返回值列表</returns>
        ''' <remarks></remarks>
        Public Shared Function RunFunction(functionName As String, params() As Object) As Object()
            If Not Script.ScriptCore.GetInstance.BusyStatus Then Return Script.ScriptCore.GetInstance.RunFunction(functionName, params)
            Return Nothing
        End Function

        ''' <summary>
        ''' 设置脚本全局变量的值(该变量内容不是字符串)
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">变量内容(字符串形式)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetGlobalVariable(name As String, value As String)
            RunStringAndWait(String.Format("{0}={1}", name, value))
        End Sub

        ''' <summary>
        ''' 设置脚本全局变量的值(该变量内容是字符串)
        ''' 该方法不会立即返回
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">变量内容</param>
        ''' <remarks></remarks>
        Public Shared Sub SetGlobalStringVariable(name As String, value As String)
            SetGlobalVariable(name, """" & value & """")
        End Sub

        ''' <summary>
        ''' 获取脚本全局变量的值
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetVariable(name As String) As Object
            Return Script.ScriptCore.GetInstance.GetVariable(name)
        End Function

        ''' <summary>
        ''' 获取脚本全局变量的值的字符串形式
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetStringVariable(name As String) As String
            Return Script.ScriptCore.GetInstance.GetStringVariable(name)
        End Function

        ''' <summary>
        ''' 获取脚本全局变量的值的浮点数形式
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetDoubleVariable(name As String) As Double
            Return Script.ScriptCore.GetInstance.GetDoubleVariable(name)
        End Function

        ''' <summary>
        ''' 获取脚本全局变量的值的整数形式
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetIntegerVariable(name As String) As Integer
            Return CInt(GetDoubleVariable(name))
        End Function

        ''' <summary>
        ''' 获取脚本全局变量的值的LUA表的形式
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTableVariable(name As String) As LuaInterface.LuaTable
            Return Script.ScriptCore.GetInstance.GetTableVariable(name)
        End Function

        ''' <summary>
        ''' 获取脚本全局变量(Table类型)中某个项的值
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="tableName">表名</param>
        ''' <param name="key">键</param>
        ''' <returns>值</returns>
        ''' <remarks></remarks>
        Public Shared Function GetVariableInTable(tableName As String, key As String) As Object
            Return Script.ScriptCore.GetInstance.GetVariableInTable(tableName, key)
        End Function

        Public Shared Sub RegisterFunction(types() As Type, belong As String, prefix As String)
            Register.RegisterFunction(types, belong, prefix)
        End Sub


        ''' <summary>
        ''' 获取脚本主机空闲状态
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetStatus() As Boolean
            Return Script.ScriptCore.GetInstance.BusyStatus
        End Function

        ''' <summary>
        ''' 获取游戏脚本主机对象
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <returns>脚本主机</returns>
        ''' <remarks></remarks>
        Public Shared Function GetVM() As LuaInterface.Lua
            Return Script.ScriptCore.GetInstance.ScriptVM
        End Function

    End Class

    ''' <summary>
    ''' 路径API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PathAPI

        ''' <summary>
        ''' 获取程序资源文件的存放路径
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <returns>程序资源文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Resource() As String
            Return PathConfig.Resource
        End Function

        ''' <summary>
        ''' 获取程序皮肤文件的存放路径
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <returns>皮肤文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Skin() As String
            Return PathConfig.Skin
        End Function

        ''' <summary>
        ''' 获取程序插件的存放路径
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <returns>插件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Plugin() As String
            Return PathConfig.Plugin
        End Function

        ''' <summary>
        ''' 获取程序脚本文件的存放路径
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <returns>脚本文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Script() As String
            Return PathConfig.Script
        End Function

        ''' <summary>
        ''' 获取程序用户个人文件的存放路径
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <returns>用户个人文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function UserFile() As String
            Return PathConfig.UserFile
        End Function

        ''' <summary>
        ''' 获取完整路径
        ''' 该方法在调用线程中执行
        ''' </summary>
        ''' <param name="urlType">资源路径类型</param>
        ''' <param name="fileURL">从资源目录开始的文件相对路径</param>
        ''' <returns>文件的绝对路径</returns>
        ''' <remarks></remarks>
        Public Shared Function GetPath(urlType As String, Optional fileURL As String = "") As String
            Return PathFunction.GetFullPath(urlType, fileURL)
        End Function

    End Class

End Namespace
