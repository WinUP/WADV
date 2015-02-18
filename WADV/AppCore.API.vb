Imports System.CodeDom.Compiler
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Windows.Markup
Imports System.Xml
Imports Microsoft.CSharp
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
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="text">新标题</param>
        ''' <remarks></remarks>
        Public Shared Sub SetTitleSync(text As String)
            GetDispatcher.Invoke(Sub() GetWindow.Title = text)
            MessageAPI.SendSync("WINDOW_TITLE_CHANGE")
        End Sub

        ''' <summary>
        ''' 清空指定容器
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <remarks></remarks>
        Public Shared Sub ClearContentSync(content As Panel)
            content.Dispatcher.Invoke(Sub() content.Children.Clear())
            MessageAPI.SendSync("PANEL_CONTENT_CLEAR")
        End Sub

        ''' <summary>
        ''' 为指定容器加载子元素
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <param name="name">子元素所在文件的路径(从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadElementSync(content As Panel, name As String)
            content.Dispatcher.Invoke(Sub() content.Children.Add(XamlReader.Load(XmlTextReader.Create(PathFunction.GetFullPath(PathFunction.PathType.Skin, name)))))
            MessageAPI.SendSync("PANEL_CONTENT_CHANGE")
        End Sub

        ''' <summary>
        ''' 为指定容器加载子元素
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <param name="name">子元素所在的文件名(从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadElementAsync(content As Panel, name As String)
            content.Dispatcher.BeginInvoke(
                Sub()
                    content.Children.Add(XamlReader.Load(XmlTextReader.Create(PathFunction.GetFullPath(PathFunction.PathType.Skin, name))))
                    MessageAPI.SendSync("PANEL_CONTENT_CHANGE")
                End Sub)
        End Sub

        ''' <summary>
        ''' 从文件加载指定的页面布局
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="fileName">文件路径(从Skin目录下开始)</param>
        Public Shared Sub LoadPageAsync(fileName As String)
            GetDispatcher.BeginInvoke(Sub() GetWindow.NavigationService.Navigate(New Uri(PathAPI.GetPath(PathFunction.PathType.Skin, fileName))))
        End Sub

        ''' <summary>
        ''' 从文件加载指定的页面布局
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="fileName">文件路径(从Skin目录下开始)</param>
        Public Shared Sub LoadPageSync(fileName As String)
            LoadPageAsync(fileName)
            MessageAPI.WaitSync("WINDOW_PAGE_CHANGE")
        End Sub

        ''' <summary>
        ''' 从对象加载指定的页面布局
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="target">目标对象</param>
        Public Shared Sub LoadObjectAsync(target As Page)
            GetDispatcher.BeginInvoke(Sub() GetWindow.NavigationService.Navigate(target))
        End Sub

        ''' <summary>
        ''' 从对象加载指定的页面布局
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="target">目标对象</param>
        Public Shared Sub LoadObjectSync(target As Page)
            LoadObjectAsync(target)
            MessageAPI.WaitSync("WINDOW_PAGE_CHANGE")
        End Sub

        ''' <summary>
        ''' 从路径加载制定的页面布局
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="uri">目标路径</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadUriAsync(uri As String)
            GetDispatcher.BeginInvoke(Sub() GetWindow.NavigationService.Navigate(New Uri(uri)))
        End Sub

        ''' <summary>
        ''' 从路径加载制定的页面布局
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="uri">目标路径</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadUriSync(uri As String)
            LoadUriAsync(uri)
            MessageAPI.WaitSync("WINDOW_PAGE_CHANGE")
        End Sub

        ''' <summary>
        ''' 返回上一个页面
        ''' 同步方法|UI线程
        ''' </summary>
        Public Shared Sub GoBackSync()
            GetDispatcher.Invoke(Sub()
                                     If GetWindow.NavigationService.CanGoBack Then
                                         GetWindow.NavigationService.GoBack()
                                         MessageAPI.SendSync("WINDOW_PAGE_GOBACK")
                                     End If
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 前进到下一个页面
        ''' 同步方法|UI线程
        ''' </summary>
        Public Shared Sub GoForwardSync()
            GetDispatcher.Invoke(Sub()
                                     If GetWindow.NavigationService.CanGoForward Then
                                         GetWindow.NavigationService.GoForward()
                                         MessageAPI.SendSync("WINDOW_PAGE_GOFORWARD")
                                     End If
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 移除一个最近的返回记录
        ''' 同步方法|UI线程
        ''' </summary>
        Public Shared Sub RemoveOneBackSync()
            GetDispatcher.Invoke(Sub()
                                     If GetWindow.NavigationService.CanGoBack Then
                                         GetWindow.NavigationService.RemoveBackEntry()
                                         MessageAPI.SendSync("WINDOW_NAVIGATE_REMOVEONE")
                                     End If
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 移除所有的返回记录
        ''' 同步方法|UI线程
        ''' </summary>
        Public Shared Sub RemoveBackListSync()
            GetDispatcher.Invoke(Sub()
                                     While GetWindow.NavigationService.CanGoBack
                                         GetWindow.NavigationService.RemoveBackEntry()
                                     End While
                                     MessageAPI.SendSync("WINDOW_NAVIGATE_REMOVEALL")
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="color">颜色对象</param>
        ''' <remarks></remarks>
        Public Shared Sub SetBackgroundByColorSync(color As Color)
            GetDispatcher.Invoke(Sub()
                                     GetWindow.Background = New SolidColorBrush(color)
                                     MessageAPI.SendSync("WINDOW_BACKGROUND_CHANGE")
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="r">红色值</param>
        ''' <param name="g">绿色值</param>
        ''' <param name="b">蓝色值</param>
        ''' <remarks></remarks>
        Public Shared Sub SetBackgroundByRGBSync(r As Byte, g As Byte, b As Byte)
            GetDispatcher.Invoke(Sub()
                                     GetWindow.Background = New SolidColorBrush(Color.FromRgb(r, g, b))
                                     MessageAPI.SendSync("WINDOW_BACKGROUND_CHANGE")
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="hex">16进制颜色值</param>
        ''' <remarks></remarks>
        Public Shared Sub SetBackgroundByHexSync(hex As String)
            GetDispatcher.Invoke(Sub()
                                     GetWindow.Background = New SolidColorBrush(ColorConverter.ConvertFromString(hex))
                                     MessageAPI.SendSync("WINDOW_BACKGROUND_CHANGE")
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 修改窗口宽度
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="width">新的宽度</param>
        ''' <remarks></remarks>
        Public Shared Sub SetWidthSync(width As Double)
            GetDispatcher.Invoke(Sub()
                                     GetWindow.Width = width
                                     MessageAPI.SendSync("WINDOW_WIDTH_CHANGE")
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 修改窗口高度
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="height">新的高度</param>
        ''' <remarks></remarks>
        Public Shared Sub SetHeightSync(height As Double)
            GetDispatcher.Invoke(Sub()
                                     GetWindow.Height = height
                                     MessageAPI.SendSync("WINDOW_HEIGHT_CHANGE")
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 设置窗口调整模式
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="canResize">是否能够调整大小</param>
        ''' <remarks></remarks>
        Public Shared Sub SetResizeModeSync(canResize As Boolean)
            GetDispatcher.Invoke(Sub()
                                     GetWindow.ResizeMode = If(canResize, ResizeMode.CanResize, ResizeMode.CanMinimize)
                                     MessageAPI.SendSync("WINDOW_RESIZEMODE_CHANGE")
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 设置窗口置顶模式
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="isTopmost">是否保持最前</param>
        ''' <remarks></remarks>
        Public Shared Sub SetTopmostSync(isTopmost As Boolean)
            GetDispatcher.Invoke(Sub()
                                     GetWindow.Topmost = isTopmost
                                     MessageAPI.SendSync("WINDOW_TOPMOST_CHANGE")
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 设置窗口图标
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="fileName">图标文件路径(ICO格式且从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetIconSync(fileName As String)
            GetDispatcher.Invoke(Sub()
                                     GetWindow.Icon = BitmapFrame.Create(New Uri(PathFunction.GetFullPath(PathFunction.PathType.Skin, fileName)))
                                     MessageAPI.SendSync("WINDOW_ICON_CHANGE")
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 设置窗口指针
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="fileName">指针文件名称(ANI或CUR格式且从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetCursorSync(fileName As String)
            GetDispatcher.Invoke(Sub()
                                     GetWindow.Cursor = New Cursor(PathFunction.GetFullPath(PathFunction.PathType.Skin, fileName))
                                     MessageAPI.SendSync("WINDOW_CURSOR_CHANGE")
                                 End Sub)
        End Sub

        ''' <summary>
        ''' 根据名称获取元素的子元素(支持多级查找)
        ''' 同步方法|UI线程
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
        ''' 在窗口中查找具有指定名称的元素
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <typeparam name="T">元素类型</typeparam>
        ''' <param name="name">元素名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function SearchObject(Of T As FrameworkElement)(name As String) As T
            Return GetChildByName(Of T)(GetWindow, name)
        End Function

        ''' <summary>
        ''' 获得窗口中的根元素
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <typeparam name="T">元素类型</typeparam>
        ''' <returns>根元素的实例</returns>
        ''' <remarks></remarks>
        Public Shared Function GetRoot(Of T As FrameworkElement)() As T
            If GetWindow.Content Is Nothing Then Return Nothing
            Dim contentPage As Page = GetWindow.Content
            Return TryCast(contentPage.Content, T)
        End Function

        ''' <summary>
        ''' 获取窗口线程工作队列
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetDispatcher() As Windows.Threading.Dispatcher
            Return WindowConfig.BaseWindow.Dispatcher
        End Function

        ''' <summary>
        ''' 获取窗口对象
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetWindow() As NavigationWindow
            Return WindowConfig.BaseWindow
        End Function

        ''' <summary>
        ''' 获取主窗口的截图
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>截图的编码器</returns>
        ''' <remarks></remarks>
        Public Shared Function GetImage() As JpegBitmapEncoder
            Dim panel = GetRoot(Of FrameworkElement)()
            Dim targetImage As New RenderTargetBitmap(panel.ActualWidth, panel.ActualHeight, 96, 96, PixelFormats.Pbgra32)
            targetImage.Render(GetWindow)
            Dim encoder As New JpegBitmapEncoder
            encoder.Frames.Add(BitmapFrame.Create(targetImage))
            encoder.QualityLevel = 100
            Return encoder
        End Function

        ''' <summary>
        ''' 将主窗口的截图保存到文件中
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="fileName">要保存的路径(从UserFile目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub SaveImage(fileName As String)
            Dim image = GetImage()
            Dim stream As New FileStream(PathAPI.GetPath(PathFunction.PathType.UserFile, fileName), FileMode.Create)
            image.Save(stream)
            stream.Close()
            MessageAPI.SendSync("WINDOW_IMAGE_SAVE")
        End Sub

    End Class

    ''' <summary>
    ''' 资源API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ResourceAPI

        ''' <summary>
        ''' 加载资源到游戏全局
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="fileName">资源文件路径(从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadToGameSync(fileName As String)
            Dim tmpDictionart As New ResourceDictionary
            tmpDictionart.Source = New Uri(PathFunction.GetFullPath(PathFunction.PathType.Skin, fileName))
            Application.Current.Resources.MergedDictionaries.Add(tmpDictionart)
            MessageAPI.SendSync("GAME_RESOURCE_ADD")
        End Sub

        ''' <summary>
        ''' 加载资源到主窗口
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="fileName">资源文件路径(从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadToWindowSync(fileName As String)
            Dim tmpDictionart As New ResourceDictionary
            tmpDictionart.Source = New Uri(PathFunction.GetFullPath(PathFunction.PathType.Skin, fileName))
            WindowAPI.GetDispatcher.Invoke(Sub() WindowAPI.GetWindow.Resources.MergedDictionaries.Add(tmpDictionart))
            MessageAPI.SendSync("WINDOW_RESOURCE_ADD")
        End Sub

        ''' <summary>
        ''' 清空全局资源
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ClearGameSync()
            Application.Current.Resources.MergedDictionaries.Clear()
            MessageAPI.SendSync("GAME_RESOURCE_CLEAR")
        End Sub

        ''' <summary>
        ''' 清空主窗口资源
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ClearWindowSync()
            WindowAPI.GetDispatcher.Invoke(Sub() WindowAPI.GetWindow.Resources.MergedDictionaries.Clear())
            MessageAPI.SendSync("WINDOW_RESOURCE_CLEAR")
        End Sub

        ''' <summary>
        ''' 清除指定全局资源
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="resource">要清除的资源对象</param>
        ''' <remarks></remarks>
        Public Shared Sub RemoveFromGameSync(resource As ResourceDictionary)
            Application.Current.Resources.MergedDictionaries.Remove(resource)
            MessageAPI.SendSync("GAME_RESOURCE_REMOVE")
        End Sub

        ''' <summary>
        ''' 清除指定主窗口资源
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="resource">要清除的资源对象</param>
        ''' <remarks></remarks>
        Public Shared Sub RemoveFromWindowSync(resource As ResourceDictionary)
            WindowAPI.GetDispatcher.Invoke(Sub() WindowAPI.GetWindow.Resources.MergedDictionaries.Remove(resource))
            MessageAPI.SendSync("WINDOW_RESOURCE_REMOVE")
        End Sub

        ''' <summary>
        ''' 获取全局资源对象
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetFromGame() As ResourceDictionary
            Return Application.Current.Resources
        End Function

        ''' <summary>
        ''' 获取主窗口资源对象
        ''' 同步方法|UI线程
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
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="fileName">插件文件路径(从Plugin目录下开始)</param>
        ''' <returns>是否加载成功</returns>
        ''' <remarks></remarks>
        Public Shared Function Add(fileName As String) As Boolean
            Return PluginFunction.AddPlugin(PathAPI.GetPath(PathFunction.PathType.Plugin, fileName))
        End Function

        ''' <summary>
        ''' 编译一个代码文件
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="fileName">文件路径(从Resource目录下开始)</param>
        ''' <returns>编译得到的程序集</returns>
        ''' <remarks></remarks>
        Public Shared Function CompileCode(fileName As String, Optional options As String = "") As System.Reflection.Assembly
            Dim codeProvider As CodeDomProvider
            Dim codeFile = New FileInfo(PathAPI.GetPath(PathFunction.PathType.Resource, fileName))
            If codeFile.Extension.ToLower = ".vb" Then
                codeProvider = New VBCodeProvider
            ElseIf codeFile.Extension.ToLower = ".cs" Then
                codeProvider = New CSharpCodeProvider
            Else
                Throw New FileFormatException("目前只能编译VB.NET和CSharp的代码文件，并且目标.NET Framework版本不能高于4.0")
            End If
            Dim param As New CompilerParameters
            param.GenerateExecutable = False
            param.GenerateInMemory = True
            param.IncludeDebugInformation = False
            Dim asList As New XmlDocument
            asList.Load(codeFile.FullName & ".xml")
            For Each clrAssemblies As XmlNode In asList.SelectNodes("/assemblies/clr")
                param.ReferencedAssemblies.Add(clrAssemblies.InnerXml)
            Next
            For Each gameAssemblies As XmlNode In asList.SelectNodes("/assemblies/game")
                param.ReferencedAssemblies.Add(PathAPI.GetPath(PathFunction.PathType.Plugin, gameAssemblies.InnerXml))
            Next
            For Each ownAssemblies As XmlNode In asList.SelectNodes("/assemblies/own")
                param.ReferencedAssemblies.Add(PathAPI.GetPath(PathFunction.PathType.Game, ownAssemblies.InnerXml))
            Next
            param.CompilerOptions = options
            Dim result = codeProvider.CompileAssemblyFromFile(param, codeFile.FullName)
            If result.Errors.HasErrors Then
                Dim errorString As New StringBuilder
                For Each tmpError As CompilerError In result.Errors
                    errorString.Append(Environment.NewLine & tmpError.ErrorText)
                Next
                MessageBox.Show("编译" & fileName & "时没有通过：" & Environment.NewLine & errorString.ToString, "错误", MessageBoxButton.OK, MessageBoxImage.Error)
                Return Nothing
            End If
            MessageAPI.SendSync("GAME_CODE_COMPILE")
            Return result.CompiledAssembly
        End Function

        ''' <summary>
        ''' 加载一个动态链接库
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="fileName">文件路径(从游戏主目录下开始)</param>
        ''' <returns>编译得到的程序集</returns>
        ''' <remarks></remarks>
        Public Shared Function LoadLibrary(fileName As String) As System.Reflection.Assembly
            Dim filePath = PathAPI.GetPath(PathFunction.PathType.Game, fileName)
            If Not My.Computer.FileSystem.FileExists(filePath) Then
                Throw New FileNotFoundException("找不到要载入的链接库文件")
                Return Nothing
            End If
            MessageAPI.SendSync("GAME_ASSEMBLE_LOAD")
            Return Reflection.Assembly.LoadFrom(filePath)
        End Function

    End Class

    ''' <summary>
    ''' 循环API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LoopingAPI

        ''' <summary>
        ''' 设置理想帧率
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="frame">新的次数</param>
        ''' <remarks></remarks>
        Public Shared Sub SetFrameSync(frame As Integer)
            If frame < 1 Then Throw New ValueUnavailableException("逻辑更新频率不能小于每秒1次")
            LoopingFunction.Frame = frame
        End Sub

        ''' <summary>
        ''' 获取理想帧率
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>循环次数</returns>
        ''' <remarks></remarks>
        Public Shared Function GetFrame() As Integer
            Return LoopingFunction.Frame
        End Function

        ''' <summary>
        ''' 添加一个循环体
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Sub AddLoopSync(loopContent As Plugin.ILoopReceiver)
            MainLooping.GetInstance.AddLooping(loopContent)
        End Sub

        ''' <summary>
        ''' 等待一个子循环的完成
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="loopContent">循环体</param>
        ''' <remarks></remarks>
        Public Shared Sub WaitLoopSync(loopContent As Plugin.ILoopReceiver)
            Dim receiver As New LoopingFunction
            receiver.WaitLooping(loopContent)
        End Sub

        ''' <summary>
        ''' 将当前线程挂起指定帧数
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="count">要挂起的帧数</param>
        ''' <remarks></remarks>
        Public Shared Sub WaitFrameSync(count As Integer)
            Dim tmpWaiter As New Looping.EmptyLooping(count)
            AddLoopSync(tmpWaiter)
            WaitLoopSync(tmpWaiter)
        End Sub

        ''' <summary>
        ''' 标记游戏循环为进行状态
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StartMainLoopSync()
            LoopingFunction.StartMainLooping()
        End Sub

        ''' <summary>
        ''' 标记游戏循环为停止状态
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopMainLoopSync()
            LoopingFunction.StopMainLooping()
        End Sub

        ''' <summary>
        ''' 获取当前的帧计数
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function CurrentFrame() As Integer
            Return Looping.MainLooping.GetInstance.CurrentFrame
        End Function

        ''' <summary>
        ''' 将指定帧数转换为理想运行时间
        ''' </summary>
        ''' <param name="count">要转换的帧数目</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function TranslateToTime(count As Integer) As TimeSpan
            Dim day, hour, minute, second, millionsecond As Integer
            Dim currentFps = GetFrame()
            day = count / (216000 * currentFps)
            count -= day * 216000 * currentFps
            hour = count / (3600 * currentFps)
            count -= hour * 3600 * currentFps
            minute = count / (60 * currentFps)
            count -= minute * 60 * currentFps
            second = count / currentFps
            count -= second * currentFps
            millionsecond = count * 1000 / currentFps
            Return New TimeSpan(day, hour, minute, second, millionsecond)
        End Function

        ''' <summary>
        ''' 将指定时间长度转换为理想帧数
        ''' </summary>
        ''' <param name="time">要转换的时间长度</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function TranslateToFrame(time As TimeSpan) As Integer
            Dim currentFps = GetFrame()
            Return (time.Days * 216000 + time.Hours * 3600 + time.Minutes * 60 + time.Seconds) * currentFps + CInt((CDbl(time.Milliseconds) / 1000.0) * currentFps)
        End Function

    End Class

    ''' <summary>
    ''' 消息API类
    ''' </summary>
    Public Class MessageAPI

        ''' <summary>
        ''' 添加一个接收器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Public Shared Sub AddSync(receiver As IMessageReceiver)
            Message.MessageService.GetInstance.AddReceiver(receiver)
        End Sub

        ''' <summary>
        ''' 删除一个接收器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Public Shared Sub DeleteSync(receiver As IMessageReceiver)
            Message.MessageService.GetInstance.DeleteReceiver(receiver)
        End Sub

        ''' <summary>
        ''' 发送一个消息
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="message">消息内容</param>
        Public Shared Sub SendSync(message As String)
            AppCore.Message.MessageService.GetInstance.SendMessage(message)
        End Sub

        ''' <summary>
        ''' 等待下一个指定消息的出现
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="message">消息内容</param>
        Public Shared Sub WaitSync(message As String)
            Dim waiter As New AppCore.Message.WaitReceiver
            waiter.WaitMessage(message)
        End Sub

    End Class

    ''' <summary>
    ''' 脚本API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ScriptAPI

        ''' <summary>
        ''' 显示提示信息
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="content">内容</param>
        ''' <param name="title">标题</param>
        ''' <remarks></remarks>
        Public Shared Sub ShowMessageSync(content As String, title As String)
            MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Information)
            MessageAPI.SendSync("SCRIPT_MESSAGE_SHOW")
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码
        ''' 异步方法|调用线程
        ''' </summary>
        ''' <param name="fileName">文件路径(从Script目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub RunFileAsync(fileName As String)
            Dim tmpThread As New Thread(CType(Sub() ScriptCore.GetInstance.RunFile(PathAPI.GetPath(PathFunction.PathType.Script, fileName)), ThreadStart))
            tmpThread.Name = "脚本文件执行线程"
            tmpThread.IsBackground = True
            tmpThread.Priority = ThreadPriority.Normal
            tmpThread.Start()
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="filename">文件路径(从Script目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub RunFileSync(filename As String)
            ScriptCore.GetInstance.RunFile(PathAPI.GetPath(PathFunction.PathType.Script, filename))
        End Sub

        ''' <summary>
        ''' 执行一段字符串脚本
        ''' 异步方法|调用线程
        ''' </summary>
        ''' <param name="content">脚本代码内容</param>
        ''' <remarks></remarks>
        Public Shared Sub RunStrngAsync(content As String)
            Dim tmpThread As New Thread(CType(Sub() ScriptCore.GetInstance.RunStrng(content), ThreadStart))
            tmpThread.IsBackground = True
            tmpThread.Priority = ThreadPriority.Normal
            tmpThread.Start(content)
        End Sub

        ''' <summary>
        ''' 执行一段字符串脚本
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="content">脚本代码内容</param>
        ''' <remarks></remarks>
        Public Shared Sub RunStringSync(content As String)
            Script.ScriptCore.GetInstance.RunStrng(content)
        End Sub

        ''' <summary>
        ''' 执行一个存在的脚本函数
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="functionName">函数名</param>
        ''' <param name="params">参数列表</param>
        ''' <returns>返回值列表</returns>
        ''' <remarks></remarks>
        Public Shared Function RunFunction(functionName As String, params() As Object) As Object()
            Return Script.ScriptCore.GetInstance.RunFunction(functionName, params)
        End Function

        ''' <summary>
        ''' 设置脚本全局变量的值(该变量内容不是字符串)
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">变量内容(字符串形式)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetSync(name As String, value As String)
            RunStringSync(String.Format("{0}={1}", name, value))
        End Sub

        ''' <summary>
        ''' 设置脚本全局变量的值(该变量内容是字符串)
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">变量内容</param>
        ''' <remarks></remarks>
        Public Shared Sub SetStringSync(name As String, value As String)
            SetSync(name, """" & value & """")
        End Sub

        ''' <summary>
        ''' 获取脚本全局变量的值
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetSync(name As String) As Object
            Return ScriptCore.GetInstance.GetVariable(name)
        End Function

        ''' <summary>
        ''' 获取脚本全局变量(Table类型)中某个项的值
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="tableName">表名</param>
        ''' <param name="key">键</param>
        ''' <returns>值</returns>
        ''' <remarks></remarks>
        Public Shared Function GetInTableSync(tableName As String, key As String) As Object
            Return ScriptCore.GetInstance.GetVariableInTable(tableName, key)
        End Function

        ''' <summary>
        ''' 使用预定义规则注册脚本函数
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="instance">要注册的类的类型声明</param>
        ''' <param name="prefix">函数前缀</param>
        ''' <param name="toLower">是否转换函数名为小写</param>
        Public Shared Sub RegisterSync(instance As Type, prefix As String, Optional toLower As Boolean = False)
            Register.RegisterFunction(instance, prefix, toLower)
        End Sub

        ''' <summary>
        ''' 获取游戏脚本主机对象
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>脚本主机</returns>
        ''' <remarks></remarks>
        Public Shared Function GetVm() As NLua.Lua
            Return ScriptCore.GetInstance.ScriptVM
        End Function

    End Class

    ''' <summary>
    ''' 路径API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PathAPI

        ''' <summary>
        ''' 获取程序资源文件的存放路径
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>程序资源文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Resource() As String
            Return PathConfig.Resource
        End Function

        ''' <summary>
        ''' 获取程序皮肤文件的存放路径
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>皮肤文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Skin() As String
            Return PathConfig.Skin
        End Function

        ''' <summary>
        ''' 获取程序插件的存放路径
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>插件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Plugin() As String
            Return PathConfig.Plugin
        End Function

        ''' <summary>
        ''' 获取程序脚本文件的存放路径
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>脚本文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Script() As String
            Return PathConfig.Script
        End Function

        ''' <summary>
        ''' 获取程序用户个人文件的存放路径
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>用户个人文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function UserFile() As String
            Return PathConfig.UserFile
        End Function

        ''' <summary>
        ''' 获取程序主存储目录
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>主存储目录</returns>
        ''' <remarks></remarks>
        Public Shared Function Game() As String
            Return My.Application.Info.DirectoryPath
        End Function

        ''' <summary>
        ''' 获取完整路径
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="urlType">资源路径类型</param>
        ''' <param name="fileURL">从资源目录开始的文件相对路径</param>
        ''' <returns>文件的绝对路径</returns>
        ''' <remarks></remarks>
        Public Shared Function GetPath(urlType As PathFunction.PathType, Optional fileURL As String = "") As String
            Return PathFunction.GetFullPath(urlType, fileURL)
        End Function

        ''' <summary>
        ''' 获取完整路径的URI表示形式
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="urlType">资源路径类型</param>
        ''' <param name="fileURL">从资源目录开始的文件相对路径</param>
        ''' <returns>文件的绝对路径</returns>
        ''' <remarks></remarks>
        Public Shared Function GetUri(urlType As PathFunction.PathType, Optional fileURL As String = "") As Uri
            Return New Uri(PathFunction.GetFullPath(urlType, fileURL))
        End Function

    End Class

    ''' <summary>
    ''' 计时器API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TimerAPI

        ''' <summary>
        ''' 启动计时器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StartSync()
            Timer.MainTimer.GetInstance.StartTimer()
        End Sub

        ''' <summary>
        ''' 停止计时器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopTimerSync()
            Timer.MainTimer.GetInstance.StopTimer()
        End Sub

        ''' <summary>
        ''' 设计计时器的计时间隔
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="tick">目标间隔</param>
        ''' <remarks></remarks>
        Public Shared Sub SetTickSync(tick As Integer)
            Timer.MainTimer.GetInstance.Span = tick
        End Sub

        ''' <summary>
        ''' 获取计时器的计时间隔
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTick() As Integer
            Return Timer.MainTimer.GetInstance.Span
        End Function

        ''' <summary>
        ''' 获取计时器的状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetStatus() As Boolean
            Return Timer.MainTimer.GetInstance.Status
        End Function

    End Class

End Namespace
