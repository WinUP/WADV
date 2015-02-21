Imports System.IO
Imports System.Windows.Markup
Imports System.Xml

Namespace AppCore.API

    ''' <summary>
    ''' 窗口API类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class WindowAPI

        ''' <summary>
        ''' 修改窗口标题
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="text">新标题</param>
        ''' <remarks></remarks>
        Public Shared Sub SetTitleSync(text As String)
            GetDispatcher.Invoke(Sub() GetWindow.Title = text)
            MessageAPI.SendSync("[SYSTEM]WINDOW_TITLE_CHANGE")
        End Sub

        ''' <summary>
        ''' 清空指定容器
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <remarks></remarks>
        Public Shared Sub ClearContentSync(content As Panel)
            content.Dispatcher.Invoke(Sub() content.Children.Clear())
            MessageAPI.SendSync("[SYSTEM]PANEL_CONTENT_CLEAR")
        End Sub

        ''' <summary>
        ''' 为指定容器加载子元素
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <param name="name">子元素所在文件的路径(从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadElementSync(content As Panel, name As String)
            content.Dispatcher.Invoke(Sub() content.Children.Add(XamlReader.Load(XmlTextReader.Create(PathFunction.GetFullPath(PathType.Skin, name)))))
            MessageAPI.SendSync("[SYSTEM]PANEL_CONTENT_CHANGE")
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
                    content.Children.Add(XamlReader.Load(XmlTextReader.Create(PathFunction.GetFullPath(PathType.Skin, name))))
                    MessageAPI.SendSync("[SYSTEM]PANEL_CONTENT_CHANGE")
                End Sub)
        End Sub

        ''' <summary>
        ''' 从文件加载指定的页面布局
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="fileName">文件路径(从Skin目录下开始)</param>
        Public Shared Sub LoadPageAsync(fileName As String)
            GetDispatcher.BeginInvoke(Sub() GetWindow.NavigationService.Navigate(New Uri(PathAPI.GetPath(PathType.Skin, fileName))))
        End Sub

        ''' <summary>
        ''' 从文件加载指定的页面布局
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="fileName">文件路径(从Skin目录下开始)</param>
        Public Shared Sub LoadPageSync(fileName As String)
            LoadPageAsync(fileName)
            MessageAPI.WaitSync("[SYSTEM]WINDOW_PAGE_CHANGE")
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
            MessageAPI.WaitSync("[SYSTEM]WINDOW_PAGE_CHANGE")
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
            MessageAPI.WaitSync("[SYSTEM]WINDOW_PAGE_CHANGE")
        End Sub

        ''' <summary>
        ''' 返回上一个页面
        ''' 同步方法|UI线程
        ''' </summary>
        Public Shared Sub GoBackSync()
            GetDispatcher.Invoke(Sub()
                                     If GetWindow.NavigationService.CanGoBack Then
                                         GetWindow.NavigationService.GoBack()
                                         MessageAPI.SendSync("[SYSTEM]WINDOW_PAGE_GOBACK")
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
                                         MessageAPI.SendSync("[SYSTEM]WINDOW_PAGE_GOFORWARD")
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
                                         MessageAPI.SendSync("[SYSTEM]WINDOW_NAVIGATE_REMOVE")
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
                                     MessageAPI.SendSync("[SYSTEM]WINDOW_NAVIGATE_REMOVEALL")
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
                                     MessageAPI.SendSync("[SYSTEM]WINDOW_BACKGROUND_CHANGE")
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
                                     MessageAPI.SendSync("[SYSTEM]WINDOW_BACKGROUND_CHANGE")
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
                                     MessageAPI.SendSync("[SYSTEM]WINDOW_BACKGROUND_CHANGE")
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
                                     MessageAPI.SendSync("[SYSTEM]WINDOW_WIDTH_CHANGE")
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
                                     MessageAPI.SendSync("[SYSTEM]WINDOW_HEIGHT_CHANGE")
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
                                     MessageAPI.SendSync("[SYSTEM]WINDOW_RESIZEMODE_CHANGE")
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
                                     MessageAPI.SendSync("[SYSTEM]WINDOW_TOPMOST_CHANGE")
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
                                     GetWindow.Icon = BitmapFrame.Create(New Uri(PathFunction.GetFullPath(PathType.Skin, fileName)))
                                     MessageAPI.SendSync("[SYSTEM]WINDOW_ICON_CHANGE")
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
                                     GetWindow.Cursor = New Cursor(PathFunction.GetFullPath(PathType.Skin, fileName))
                                     MessageAPI.SendSync("[SYSTEM]WINDOW_CURSOR_CHANGE")
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
                                             Dim child As DependencyObject
                                             Dim grandChild As T
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
            Return Config.BaseWindow.Dispatcher
        End Function

        ''' <summary>
        ''' 获取窗口对象
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetWindow() As NavigationWindow
            Return Config.BaseWindow
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
            Dim stream As New FileStream(PathAPI.GetPath(PathType.UserFile, fileName), FileMode.Create)
            image.Save(stream)
            stream.Close()
            MessageAPI.SendSync("[SYSTEM]WINDOW_IMAGE_SAVE")
        End Sub

    End Class

End Namespace