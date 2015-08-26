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
    ''' 窗口API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Window
        Private ReadOnly MainDispatcher As Threading.Dispatcher = Config.BaseWindow.Dispatcher

        ''' <summary>
        ''' 获取或设置窗口标题
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="value">新标题</param>
        ''' <remarks></remarks>
        Public Function Title(Optional value As String = "") As String
            If value = "" Then Return MainDispatcher.Invoke(Function() Config.BaseWindow.Title)
            MainDispatcher.Invoke(Sub() Config.BaseWindow.Title = value)
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_TITLE_CHANGE")
            Return value
        End Function

        ''' <summary>
        ''' 清空指定容器
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <remarks></remarks>
        Public Sub Clear(content As Panel)
            content.Dispatcher.Invoke(Sub() content.Children.Clear())
            Config.MessageService.SendMessage("[SYSTEM]PANEL_CONTENT_CLEAR")
        End Sub

        ''' <summary>
        ''' 为指定容器加载子元素
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <param name="filePath">子元素所在文件的路径(Skin目录下)</param>
        ''' <remarks></remarks>
        Public Sub LoadElement(content As Panel, filePath As String)
            LoadElementAsync(content, filePath)
            Message.Wait("[SYSTEM]PANEL_CONTENT_CHANGE")
        End Sub

        ''' <summary>
        ''' 为指定容器加载子元素
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <param name="filePath">子元素所在的文件名(Skin目录下)</param>
        ''' <remarks></remarks>
        Public Sub LoadElementAsync(content As Panel, filePath As String)
            content.Dispatcher.BeginInvoke(
                Sub()
                    content.Children.Add(XamlReader.Load(XmlTextReader.Create(PathFunction.GetFullPath(PathType.Skin, filePath))))
                    Config.MessageService.SendMessage("[SYSTEM]PANEL_CONTENT_CHANGE")
                End Sub)
        End Sub

        ''' <summary>
        ''' 从文件加载指定的页面布局
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="filePath">文件路径(从Skin目录下开始)</param>
        Public Sub LoadPage(filePath As String, Optional data As NavigateOperation = NavigateOperation.Normal)
            LoadPageAsync(filePath, data)
            Message.Wait("[SYSTEM]WINDOW_PAGE_CHANGE")
        End Sub

        ''' <summary>
        ''' 从文件加载指定的页面布局
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="filePath">文件路径(从Skin目录下开始)</param>
        Public Sub LoadPageAsync(filePath As String, Optional data As NavigateOperation = NavigateOperation.Normal)
            MainDispatcher.BeginInvoke(Sub() Config.BaseWindow.NavigationService.Navigate(New Uri(Path.Combine(PathType.Skin, filePath)), data))
        End Sub

        ''' <summary>
        ''' 从对象加载指定的页面布局
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="target">目标对象</param>
        Public Sub LoadObject(target As Page, Optional data As NavigateOperation = NavigateOperation.Normal)
            LoadObjectAsync(target, data)
            Message.Wait("[SYSTEM]WINDOW_PAGE_CHANGE")
        End Sub

        ''' <summary>
        ''' 从对象加载指定的页面布局
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="target">目标对象</param>
        Public Sub LoadObjectAsync(target As Page, Optional data As NavigateOperation = NavigateOperation.Normal)
            MainDispatcher.BeginInvoke(Sub() Config.BaseWindow.NavigationService.Navigate(target, data))
        End Sub

        ''' <summary>
        ''' 从路径加载制定的页面布局
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="uri">目标路径</param>
        ''' <remarks></remarks>
        Public Sub LoadUri(uri As Uri, Optional data As NavigateOperation = NavigateOperation.Normal)
            LoadUriAsync(uri, data)
            Message.Wait("[SYSTEM]WINDOW_PAGE_CHANGE")
        End Sub

        ''' <summary>
        ''' 从路径加载制定的页面布局
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="uri">目标路径</param>
        ''' <remarks></remarks>
        Public Sub LoadUriAsync(uri As Uri, Optional data As NavigateOperation = NavigateOperation.Normal)
            MainDispatcher.BeginInvoke(Sub() Config.BaseWindow.NavigationService.Navigate(uri, data))
        End Sub

        ''' <summary>
        ''' 淡出当前页面
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="time">淡出时间</param>
        ''' <remarks></remarks>
        Public Sub FadeOut(time As Integer)
            FadeOutAsync(time)
            Message.Wait("[SYSTEM]WINDOW_PAGE_FADEOUT")
        End Sub

        ''' <summary>
        ''' 淡出当前页面
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="time">淡出时间</param>
        ''' <remarks></remarks>
        Public Sub FadeOutAsync(time As Integer)
            MainDispatcher.BeginInvoke(Sub()
                                           Dim fadeOut As New DoubleAnimation(0.0, New Duration(TimeSpan.FromMilliseconds(time)))
                                           fadeOut.EasingFunction = New QuarticEase With {.EasingMode = EasingMode.EaseOut}
                                           AddHandler fadeOut.Completed, Sub() Message.Send("[SYSTEM]WINDOW_PAGE_FADEOUT")
                                       End Sub)
        End Sub

        ''' <summary>
        ''' 淡入当前页面
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="time">淡入时间</param>
        ''' <remarks></remarks>
        Public Sub FadeIn(time As Integer)
            FadeInAsync(time)
            Message.Wait("[SYSTEM]WINDOW_PAGE_FADEIN")
        End Sub

        ''' <summary>
        ''' 淡入当前页面
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="time">淡入时间</param>
        ''' <remarks></remarks>
        Public Sub FadeInAsync(time As Integer)
            MainDispatcher.BeginInvoke(Sub()
                                           Dim fadeOut As New DoubleAnimation(1.0, New Duration(TimeSpan.FromMilliseconds(time)))
                                           fadeOut.EasingFunction = New QuarticEase With {.EasingMode = EasingMode.EaseOut}
                                           AddHandler fadeOut.Completed, Sub() Message.Send("[SYSTEM]WINDOW_PAGE_FADEIN")
                                       End Sub)
        End Sub

        ''' <summary>
        ''' 返回上一个页面
        ''' 同步方法|UI线程
        ''' </summary>
        Public Sub Back()
            MainDispatcher.Invoke(Sub()
                                      If Config.BaseWindow.NavigationService.CanGoBack Then
                                          Config.BaseWindow.NavigationService.GoBack()
                                          Config.MessageService.SendMessage("[SYSTEM]WINDOW_PAGE_GOBACK")
                                      End If
                                  End Sub)
        End Sub

        ''' <summary>
        ''' 前进到下一个页面
        ''' 同步方法|UI线程
        ''' </summary>
        Public Sub Forward()
            MainDispatcher.Invoke(Sub()
                                      If Config.BaseWindow.NavigationService.CanGoForward Then
                                          Config.BaseWindow.NavigationService.GoForward()
                                          Config.MessageService.SendMessage("[SYSTEM]WINDOW_PAGE_GOFORWARD")
                                      End If
                                  End Sub)
        End Sub

        ''' <summary>
        ''' 移除一个最近的返回记录
        ''' 同步方法|UI线程
        ''' </summary>
        Public Sub RemoveBack()
            MainDispatcher.Invoke(Sub()
                                      If Config.BaseWindow.NavigationService.CanGoBack Then
                                          Config.BaseWindow.NavigationService.RemoveBackEntry()
                                          Config.MessageService.SendMessage("[SYSTEM]WINDOW_NAVIGATE_REMOVE")
                                      End If
                                  End Sub)
        End Sub

        ''' <summary>
        ''' 移除所有的返回记录
        ''' 同步方法|UI线程
        ''' </summary>
        Public Sub RemoveBackList()
            MainDispatcher.Invoke(Sub()
                                      While Config.BaseWindow.NavigationService.CanGoBack
                                          Config.BaseWindow.NavigationService.RemoveBackEntry()
                                      End While
                                  End Sub)
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_NAVIGATE_REMOVEALL")
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="color">颜色对象</param>
        ''' <remarks></remarks>
        Public Sub SetBackgroundByColor(color As Color)
            MainDispatcher.Invoke(Sub() Config.BaseWindow.Background = New SolidColorBrush(color))
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_BACKGROUND_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="r">红色值</param>
        ''' <param name="g">绿色值</param>
        ''' <param name="b">蓝色值</param>
        ''' <remarks></remarks>
        Public Sub SetBackgroundByRgb(r As Byte, g As Byte, b As Byte)
            MainDispatcher.Invoke(Sub() Config.BaseWindow.Background = New SolidColorBrush(Color.FromRgb(r, g, b)))
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_BACKGROUND_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改窗口背景色
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="hex">16进制颜色值</param>
        ''' <remarks></remarks>
        Public Sub SetBackgroundByHex(hex As String)
            MainDispatcher.Invoke(Sub() Config.BaseWindow.Background = New SolidColorBrush(ColorConverter.ConvertFromString(hex)))
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_BACKGROUND_CHANGE")
        End Sub

        ''' <summary>
        ''' 获取或设置窗口宽度
        ''' </summary>
        ''' <param name="value">新的宽度，不需要修改的话不要传递数值</param>
        ''' <remarks></remarks>
        Public Function Width(Optional value As Double = 0.0) As Double
            If value.Equals(0.0) Then Return MainDispatcher.Invoke(Function() Config.BaseWindow.Width)
            MainDispatcher.Invoke(Sub() Config.BaseWindow.Width = value)
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_WIDTH_CHANGE")
            Return value
        End Function

        ''' <summary>
        ''' 获取或设置窗口高度
        ''' </summary>
        ''' <param name="value">新的高度，不需要修改的话不要传递数值</param>
        ''' <remarks></remarks>
        Public Function Height(Optional value As Double = 0.0) As Double
            If value.Equals(0.0) Then Return MainDispatcher.Invoke(Function() Config.BaseWindow.Height)
            MainDispatcher.Invoke(Sub() Config.BaseWindow.Height = value)
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_HEIGHT_CHANGE")
            Return value
        End Function

        ''' <summary>
        ''' 设置窗口调整模式
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="canResize">是否能够调整大小</param>
        ''' <remarks></remarks>
        Public Sub Resize(canResize As Boolean)
            MainDispatcher.Invoke(Sub() Config.BaseWindow.ResizeMode = If(canResize, ResizeMode.CanResize, ResizeMode.CanMinimize))
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_RESIZEMODE_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置窗口置顶模式
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="isTopmost">是否保持最前</param>
        ''' <remarks></remarks>
        Public Sub Topmost(isTopmost As Boolean)
            MainDispatcher.Invoke(Sub() Config.BaseWindow.Topmost = isTopmost)
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_TOPMOST_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置窗口图标
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="filePath">图标文件路径(ICO格式且从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Sub Icon(filePath As String)
            MainDispatcher.Invoke(Sub() Config.BaseWindow.Icon = BitmapFrame.Create(PathFunction.GetFullUri(PathType.Skin, filePath)))
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_ICON_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置窗口指针
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="filePath">指针文件名称(ANI或CUR格式且从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Sub Cursor(filePath As String)
            MainDispatcher.Invoke(Sub() Config.BaseWindow.Cursor = New Cursor(PathFunction.GetFullPath(PathType.Skin, filePath)))
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_CURSOR_CHANGE")
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
        Public Function GetChild(Of T As FrameworkElement)(obj As DependencyObject, name As String) As T
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
        Public Function Search(Of T As FrameworkElement)(name As String) As T
            Return GetChild(Of T)(Config.BaseWindow, name)
        End Function

        ''' <summary>
        ''' 获得当前页的根元素
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <typeparam name="T">元素类型</typeparam>
        ''' <returns>根元素的实例</returns>
        ''' <remarks></remarks>
        Public Function Root(Of T As FrameworkElement)() As T
            Return MainDispatcher.Invoke(Function()
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
        Public Function Dispatcher() As Threading.Dispatcher
            Return MainDispatcher
        End Function

        ''' <summary>
        ''' 获取窗口对象
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Window() As NavigationWindow
            Return Config.BaseWindow
        End Function

        ''' <summary>
        ''' 在UI线程上执行一个无参委托
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="target">要执行的委托</param>
        ''' <remarks></remarks>
        Public Sub Invoke(target As Action)
            MainDispatcher.Invoke(target)
        End Sub

        ''' <summary>
        ''' 在UI线程执行一个无参委托
        ''' 异步方法|UI线程
        ''' </summary>
        ''' <param name="content"></param>
        ''' <remarks></remarks>
        Public Sub InvokeAsync(content As Action)
            MainDispatcher.BeginInvoke(content)
        End Sub

        ''' <summary>
        ''' 在UI线程上执行一个有一个参数且具有返回值的委托
        ''' </summary>
        ''' <param name="target">要执行的委托</param>
        ''' <param name="params">要传递的参数</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InvokeFunction(target As Func(Of Object, Object), params As Object) As Object
            Return MainDispatcher.Invoke(Function() target.Invoke(params))
        End Function

        Public Function InvokeFunction(target As Func(Of Object)) As Object
            Return MainDispatcher.Invoke(Function() target.Invoke())
        End Function

        ''' <summary>
        ''' 获取主窗口的截图
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="quality">图像质量[0-100]</param>
        ''' <returns>截图的编码器</returns>
        ''' <remarks></remarks>
        Public Function Image(Optional quality As Integer = 100) As JpegBitmapEncoder
            Dim panel = Root(Of FrameworkElement)()
            Dim targetImage As New RenderTargetBitmap(panel.ActualWidth, panel.ActualHeight, 96, 96, PixelFormats.Pbgra32)
            targetImage.Render(Config.BaseWindow)
            Dim encoder As New JpegBitmapEncoder
            encoder.Frames.Add(BitmapFrame.Create(targetImage))
            encoder.QualityLevel = quality
            Return encoder
        End Function

        ''' <summary>
        ''' 将主窗口的截图保存到文件中
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="filePath">要保存的路径(UserFile目录下)</param>
        ''' <remarks></remarks>
        Public Sub Save(filePath As String)
            Dim image = API.Image
            Dim stream As New FileStream(PathFunction.GetFullPath(PathType.UserFile, filePath), FileMode.Create)
            image.Save(stream)
            stream.Close()
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_IMAGE_SAVE")
        End Sub

        ''' <summary>
        ''' 广播游戏窗口转场消息
        ''' </summary>
        ''' <param name="e">可取消的导航事件</param>
        ''' <remarks></remarks>
        Public Sub BoardcastNavigate(e As NavigatingCancelEventArgs)
            ReceiverList.NavigateReceiverList.Boardcast(e)
        End Sub

        ''' <summary>
        ''' 生成新控件
        ''' </summary>
        ''' <param name="type">控件类型</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Generate(type As ElementType) As FrameworkElement
            Return MainDispatcher.Invoke(Function() UiOperation.GenerateElement(type))
        End Function
    End Module
End Namespace