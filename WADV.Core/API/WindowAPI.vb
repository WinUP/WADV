Imports System.IO
Imports System.Xml
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation

Namespace API

    ''' <summary>
    ''' 窗口API类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class WindowAPI
        Private Shared ReadOnly Dispatcher As Windows.Threading.Dispatcher = Config.BaseWindow.Dispatcher

        ''' <summary>
        ''' 修改窗口标题
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="text">新标题</param>
        ''' <remarks></remarks>
        Public Shared Sub SetTitleSync(text As String)
            Dispatcher.Invoke(Sub() Config.BaseWindow.Title = text)
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_TITLE_CHANGE")
        End Sub

        ''' <summary>
        ''' 清空指定容器
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <remarks></remarks>
        Public Shared Sub ClearContentSync(content As Panel)
            content.Dispatcher.Invoke(Sub() content.Children.Clear())
            MessageService.GetInstance.SendMessage("[SYSTEM]PANEL_CONTENT_CLEAR")
        End Sub

        ''' <summary>
        ''' 为指定容器加载子元素
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <param name="filePath">子元素所在文件的路径(从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadElementSync(content As Panel, filePath As String)
            content.Dispatcher.Invoke(Sub() content.Children.Add(XamlReader.Load(XmlTextReader.Create(PathFunction.GetFullPath(PathType.Skin, filePath)))))
            MessageService.GetInstance.SendMessage("[SYSTEM]PANEL_CONTENT_CHANGE")
        End Sub

        ''' <summary>
        ''' 为指定容器加载子元素
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <param name="filePath">子元素所在的文件名(从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadElementAsync(content As Panel, filePath As String)
            content.Dispatcher.BeginInvoke(
                Sub()
                    content.Children.Add(XamlReader.Load(XmlTextReader.Create(PathFunction.GetFullPath(PathType.Skin, filePath))))
                    MessageService.GetInstance.SendMessage("[SYSTEM]PANEL_CONTENT_CHANGE")
                End Sub)
        End Sub

        ''' <summary>
        ''' 从文件加载指定的页面布局
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="filePath">文件路径(从Skin目录下开始)</param>
        Public Shared Sub LoadPageAsync(filePath As String, Optional data As NavigateOperation = NavigateOperation.Normal)
            Dispatcher.BeginInvoke(Sub() Config.BaseWindow.NavigationService.Navigate(New Uri(PathAPI.GetPath(PathType.Skin, filePath)), data))
        End Sub

        ''' <summary>
        ''' 从文件加载指定的页面布局
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="filePath">文件路径(从Skin目录下开始)</param>
        Public Shared Sub LoadPageSync(filePath As String, Optional data As NavigateOperation = NavigateOperation.Normal)
            LoadPageAsync(filePath, data)
            MessageAPI.WaitSync("[SYSTEM]WINDOW_PAGE_CHANGE")
        End Sub

        ''' <summary>
        ''' 从对象加载指定的页面布局
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="target">目标对象</param>
        Public Shared Sub LoadObjectAsync(target As Page, Optional data As NavigateOperation = NavigateOperation.Normal)
            Dispatcher.BeginInvoke(Sub() Config.BaseWindow.NavigationService.Navigate(target, data))
        End Sub

        ''' <summary>
        ''' 从对象加载指定的页面布局
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="target">目标对象</param>
        Public Shared Sub LoadObjectSync(target As Page, Optional data As NavigateOperation = NavigateOperation.Normal)
            LoadObjectAsync(target, data)
            MessageAPI.WaitSync("[SYSTEM]WINDOW_PAGE_CHANGE")
        End Sub

        ''' <summary>
        ''' 从路径加载制定的页面布局
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="uri">目标路径</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadUriAsync(uri As Uri, Optional data As NavigateOperation = NavigateOperation.Normal)
            Dispatcher.BeginInvoke(Sub() Config.BaseWindow.NavigationService.Navigate(uri, data))
        End Sub

        ''' <summary>
        ''' 从路径加载制定的页面布局
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="uri">目标路径</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadUriSync(uri As Uri, Optional data As NavigateOperation = NavigateOperation.Normal)
            LoadUriAsync(uri, data)
            MessageAPI.WaitSync("[SYSTEM]WINDOW_PAGE_CHANGE")
        End Sub

        ''' <summary>
        ''' 淡出当前页面
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="time">淡出时间</param>
        ''' <remarks></remarks>
        Public Shared Sub FadeOutPageAsync(time As Integer)
            Dispatcher.BeginInvoke(Sub()
                                       Dim fadeOut As New DoubleAnimation(0.0, New Duration(TimeSpan.FromMilliseconds(time)))
                                       fadeOut.EasingFunction = New QuarticEase With {.EasingMode = EasingMode.EaseOut}
                                       AddHandler fadeOut.Completed, Sub() MessageAPI.SendSync("[SYSTEM]WINDOW_PAGE_FADEOUT")
                                   End Sub)
        End Sub

        ''' <summary>
        ''' 淡出当前页面
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="time">淡出时间</param>
        ''' <remarks></remarks>
        Public Shared Sub FadeOutPageSync(time As Integer)
            FadeOutPageAsync(time)
            MessageAPI.WaitSync("[SYSTEM]WINDOW_PAGE_FADEOUT")
        End Sub

        ''' <summary>
        ''' 淡入当前页面
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="time">淡入时间</param>
        ''' <remarks></remarks>
        Public Shared Sub FadeInPageAsync(time As Integer)
            Dispatcher.BeginInvoke(Sub()
                                       Dim fadeOut As New DoubleAnimation(1.0, New Duration(TimeSpan.FromMilliseconds(time)))
                                       fadeOut.EasingFunction = New QuarticEase With {.EasingMode = EasingMode.EaseOut}
                                       AddHandler fadeOut.Completed, Sub() MessageAPI.SendSync("[SYSTEM]WINDOW_PAGE_FADEIN")
                                   End Sub)
        End Sub

        ''' <summary>
        ''' 淡入当前页面
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="time">淡入时间</param>
        ''' <remarks></remarks>
        Public Shared Sub FadeInPageSync(time As Integer)
            FadeInPageAsync(time)
            MessageAPI.WaitSync("[SYSTEM]WINDOW_PAGE_FADIN")
        End Sub

        ''' <summary>
        ''' 返回上一个页面
        ''' 同步方法|UI线程
        ''' </summary>
        Public Shared Sub GoBackSync()
            Dispatcher.Invoke(Sub()
                                  If Config.BaseWindow.NavigationService.CanGoBack Then
                                      Config.BaseWindow.NavigationService.GoBack()
                                      MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_PAGE_GOBACK")
                                  End If
                              End Sub)
        End Sub

        ''' <summary>
        ''' 前进到下一个页面
        ''' 同步方法|UI线程
        ''' </summary>
        Public Shared Sub GoForwardSync()
            Dispatcher.Invoke(Sub()
                                  If Config.BaseWindow.NavigationService.CanGoForward Then
                                      Config.BaseWindow.NavigationService.GoForward()
                                      MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_PAGE_GOFORWARD")
                                  End If
                              End Sub)
        End Sub

        ''' <summary>
        ''' 移除一个最近的返回记录
        ''' 同步方法|UI线程
        ''' </summary>
        Public Shared Sub RemoveOneBackSync()
            Dispatcher.Invoke(Sub()
                                  If Config.BaseWindow.NavigationService.CanGoBack Then
                                      Config.BaseWindow.NavigationService.RemoveBackEntry()
                                      MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_NAVIGATE_REMOVE")
                                  End If
                              End Sub)
        End Sub

        ''' <summary>
        ''' 移除所有的返回记录
        ''' 同步方法|UI线程
        ''' </summary>
        Public Shared Sub RemoveBackListSync()
            Dispatcher.Invoke(Sub()
                                  While Config.BaseWindow.NavigationService.CanGoBack
                                      Config.BaseWindow.NavigationService.RemoveBackEntry()
                                  End While
                              End Sub)
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_NAVIGATE_REMOVEALL")
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="color">颜色对象</param>
        ''' <remarks></remarks>
        Public Shared Sub SetBackgroundByColorSync(color As Color)
            Dispatcher.Invoke(Sub() Config.BaseWindow.Background = New SolidColorBrush(color))
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_BACKGROUND_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="r">红色值</param>
        ''' <param name="g">绿色值</param>
        ''' <param name="b">蓝色值</param>
        ''' <remarks></remarks>
        Public Shared Sub SetBackgroundByRgbSync(r As Byte, g As Byte, b As Byte)
            Dispatcher.Invoke(Sub() Config.BaseWindow.Background = New SolidColorBrush(Color.FromRgb(r, g, b)))
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_BACKGROUND_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="hex">16进制颜色值</param>
        ''' <remarks></remarks>
        Public Shared Sub SetBackgroundByHexSync(hex As String)
            Dispatcher.Invoke(Sub() Config.BaseWindow.Background = New SolidColorBrush(ColorConverter.ConvertFromString(hex)))
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_BACKGROUND_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改窗口宽度
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="width">新的宽度</param>
        ''' <remarks></remarks>
        Public Shared Sub SetWidthSync(width As Double)
            Dispatcher.Invoke(Sub() Config.BaseWindow.Width = width)
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_WIDTH_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改窗口高度
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="height">新的高度</param>
        ''' <remarks></remarks>
        Public Shared Sub SetHeightSync(height As Double)
            Dispatcher.Invoke(Sub() Config.BaseWindow.Height = height)
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_HEIGHT_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置窗口调整模式
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="canResize">是否能够调整大小</param>
        ''' <remarks></remarks>
        Public Shared Sub SetResizeModeSync(canResize As Boolean)
            Dispatcher.Invoke(Sub() Config.BaseWindow.ResizeMode = If(canResize, ResizeMode.CanResize, ResizeMode.CanMinimize))
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_RESIZEMODE_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置窗口置顶模式
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="isTopmost">是否保持最前</param>
        ''' <remarks></remarks>
        Public Shared Sub SetTopmostSync(isTopmost As Boolean)
            Dispatcher.Invoke(Sub() Config.BaseWindow.Topmost = isTopmost)
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_TOPMOST_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置窗口图标
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="filePath">图标文件路径(ICO格式且从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetIconSync(filePath As String)
            Dispatcher.Invoke(Sub() Config.BaseWindow.Icon = BitmapFrame.Create(PathFunction.GetFullUri(PathType.Skin, filePath)))
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_ICON_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置窗口指针
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="filePath">指针文件名称(ANI或CUR格式且从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetCursorSync(filePath As String)
            Dispatcher.Invoke(Sub() Config.BaseWindow.Cursor = New Cursor(PathFunction.GetFullPath(PathType.Skin, filePath)))
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_CURSOR_CHANGE")
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
            Return obj.Dispatcher.Invoke(Function() UiOperation.GetChildByName(Of T)(obj, name))
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
            Return GetChildByName(Of T)(Config.BaseWindow, name)
        End Function

        ''' <summary>
        ''' 获得窗口中的根元素
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <typeparam name="T">元素类型</typeparam>
        ''' <returns>根元素的实例</returns>
        ''' <remarks></remarks>
        Public Shared Function GetRoot(Of T As FrameworkElement)() As T
            Return Dispatcher.Invoke(Function()
                                         If Config.BaseWindow.Content Is Nothing Then Return Nothing
                                         Dim contentPage As Page = Config.BaseWindow.Content
                                         Return DirectCast(contentPage.Content, T)
                                     End Function)
        End Function

        ''' <summary>
        ''' 获取窗口线程工作队列
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetDispatcher() As Windows.Threading.Dispatcher
            Return Dispatcher
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
        ''' 在UI线程上执行一个无参委托
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="target">要执行的委托</param>
        ''' <remarks></remarks>
        Public Shared Sub InvokeSync(target As Action)
            Dispatcher.Invoke(target)
        End Sub

        ''' <summary>
        ''' 在UI线程上执行一个有一个参数且具有返回值的委托
        ''' </summary>
        ''' <param name="target">要执行的委托</param>
        ''' <param name="params">要传递的参数</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function InvokeFunction(target As Func(Of Object, Object), params As Object) As Object
            Return Dispatcher.Invoke(Function() target.Invoke(params))
        End Function

        ''' <summary>
        ''' 在UI线程执行一个无参委托
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="content"></param>
        ''' <remarks></remarks>
        Public Shared Sub InvokeAsync(content As Action)
            Dispatcher.BeginInvoke(content)
        End Sub

        ''' <summary>
        ''' 获取主窗口的截图
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>截图的编码器</returns>
        ''' <remarks></remarks>
        Public Shared Function GetImage() As JpegBitmapEncoder
            Dim panel = GetRoot(Of FrameworkElement)()
            Dim targetImage As New RenderTargetBitmap(panel.ActualWidth, panel.ActualHeight, 96, 96, PixelFormats.Pbgra32)
            targetImage.Render(Config.BaseWindow)
            Dim encoder As New JpegBitmapEncoder
            encoder.Frames.Add(BitmapFrame.Create(targetImage))
            encoder.QualityLevel = 100
            Return encoder
        End Function

        ''' <summary>
        ''' 将主窗口的截图保存到文件中
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="filePath">要保存的路径(从UserFile目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub SaveImage(filePath As String)
            Dim image = GetImage()
            Dim stream As New FileStream(PathAPI.GetPath(PathType.UserFile, filePath), FileMode.Create)
            image.Save(stream)
            stream.Close()
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_IMAGE_SAVE")
        End Sub

        ''' <summary>
        ''' 添加一个控件到当前页面
        ''' </summary>
        ''' <param name="type">控件类型</param>
        ''' <param name="name">控件名称</param>
        ''' <param name="x">控件左上角水平坐标</param>
        ''' <param name="y">控件左上角垂直坐标</param>
        ''' <param name="width">控件宽度</param>
        ''' <param name="height">控件高度</param>
        ''' <param name="parentName">控件父容器名称(留空则添加到根容器)</param>
        ''' <returns>添加完成的控件</returns>
        ''' <remarks></remarks>
        Public Shared Function AddElement(type As String, name As String, x As Double, y As Double, width As Double, height As Double, Optional parentName As String = "") As FrameworkElement
            Dim target As FrameworkElement = Dispatcher.Invoke(Function() UiOperation.GenerateElement(type))
            If target Is Nothing Then
                Return Nothing
            Else
                target.Name = name
                target.Width = width
                target.Height = height
                target.Margin = New Thickness(x, y, 0, 0)
                Dim parent As Panel
                If parentName = "" Then
                    parent = GetRoot(Of Panel)()
                Else
                    parent = SearchObject(Of Panel)(parentName)
                End If
                If parent Is Nothing Then
                    Return Nothing
                Else
                    Dispatcher.Invoke(Sub() parent.Children.Add(target))
                End If
                Return target
            End If
        End Function

    End Class

End Namespace