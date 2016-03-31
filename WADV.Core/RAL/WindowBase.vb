Imports WADV.Core.Enumeration
Imports WADV.Core.GameSystem
Imports WADV.Core.RAL.Tool

Namespace RAL
    Public MustInherit Class WindowBase
        Private _fullscreen As Boolean = False
        Private _canFullscreen As Boolean = True
        Private _resolution As Vector2
        Private _canClose As Boolean = True
        Private _iconPath As String
        Private _cursorPath As String
        Private _title As String
        Private _canResize As Boolean = False
        Private _topmost As Boolean = False

        ''' <summary>
        ''' 窗口是否可以全屏<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI
        ''' </summary>
        ''' <returns></returns>
        Public Property CanFullscreen As Boolean
            Get
                Return _canFullscreen
            End Get
            Set(value As Boolean)
                If value = _canFullscreen Then Exit Property
                SetCanFullscreen_Implement(value)
                _canFullscreen = value
            End Set
        End Property

        ''' <summary>
        ''' CanFullscreen的Set实现<br></br>
        ''' 仅在为CanFullscreen设置和当前值不同的值时才会调用
        ''' </summary>
        ''' <param name="value">是否允许全屏</param>
        Protected MustOverride Sub SetCanFullscreen_Implement(value As Boolean)

        ''' <summary>
        ''' 获取或设置窗口是否全屏<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI | WINDOW_FULLSCREEN_ENTER/WINDOW_FULLSCREEN_EXIT<br></br>
        ''' 异常：<br></br>
        '''  FullscreenUnsupportedException
        ''' </summary>
        ''' <returns></returns>
        Public Property FullScreen As Boolean
            Get
                Return _fullscreen
            End Get
            Set(value As Boolean)
                If Not CanFullscreen Then Throw New Exception.FullscreenUnsupportedException
                If value = _fullscreen Then Exit Property
                SetFullscreen_Implement(value)
                If value Then
                    Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_FULLSCREEN_ENTER", 1)
                Else
                    Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_FULLSCREEN_EXIT", 1)
                End If
                _fullscreen = value
            End Set
        End Property

        ''' <summary>
        ''' Fullscreen的Set实现<br></br>
        ''' 仅在为Fullscreen设置和当前值不同的值时才会调用，已进行CanFullscreen检查。
        ''' </summary>
        ''' <param name="value">是否进入全屏</param>
        Protected MustOverride Sub SetFullscreen_Implement(value As Boolean)

        ''' <summary>
        ''' 获取或设置窗口分辨率<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI | WINDOW_RESOLUTION_CHANGE
        ''' </summary>
        ''' <returns></returns>
        Public Property Resolution As Vector2
            Get
                Return _resolution
            End Get
            Set(value As Vector2)
                If value = _resolution Then Exit Property
                SetResolution_Implement(value)
                Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_RESOLUTION_CHANGE", 1)
                _resolution = value
            End Set
        End Property

        ''' <summary>
        ''' Resolution的Set实现<br></br>
        ''' 仅在为Resolution设置和当前值不同的值时才会调用
        ''' </summary>
        ''' <param name="value">要使用的分辨率</param>
        Protected MustOverride Sub SetResolution_Implement(value As Vector2)

        ''' <summary>
        ''' 获取或设置窗口是否允许关闭
        ''' 属性：<br></br>
        '''  同步 | UI
        ''' </summary>
        ''' <returns></returns>
        Public Property CanClose As Boolean
            Get
                Return _canClose
            End Get
            Set(value As Boolean)
                If value = _canClose Then Exit Property
                SetCanClose_Implement(value)
                _canClose = value
            End Set
        End Property

        ''' <summary>
        ''' CanClose的Set实现<br></br>
        ''' 仅在为CanClose设置和当前值不同的值时才会调用
        ''' </summary>
        ''' <param name="value">是否允许关闭</param>
        Protected MustOverride Sub SetCanClose_Implement(value As Boolean)

        ''' <summary>
        ''' 关闭窗口<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI | WINDOW_CLOSE<br></br>
        ''' 异常：<br></br>
        '''  CloseUnsupportedException
        ''' </summary>
        Public Sub Close()
            If Not CanClose Then Throw New Exception.CloseUnsupportedException
            Close_Implement()
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_CLOSE", 1)
        End Sub

        ''' <summary>
        ''' CloseWindow的实现<br></br>
        ''' 已进行CanClose检查
        ''' </summary>
        Protected MustOverride Sub Close_Implement()

        ''' <summary>
        ''' 显示窗口<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI | WINDOW_SHOW
        ''' </summary>
        Public Sub Show()
            Show_Implement()
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_SHOW", 1)
        End Sub

        ''' <summary>
        ''' 打开窗口
        ''' </summary>
        Public MustOverride Sub Show_Implement()

        ''' <summary>
        ''' 获取或设置窗口图标<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI | WINDOW_ICON_CHANGE
        ''' </summary>
        ''' <value>图标文件路径（Skin目录下）</value>
        ''' <returns></returns>
        Public Property IconPath As String
            Get
                Return _iconPath
            End Get
            Set(value As String)
                If value = _iconPath Then Exit Property
                SetIconFromFile_Implement(PathFunction.CombineToString(PathType.Skin, value))
                Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_ICON_CHANGE", 1)
                _iconPath = value
            End Set
        End Property

        ''' <summary>
        ''' IconPath的Set实现<br></br>
        ''' 仅在为IconPath设置和当前值不同的值时才会调用
        ''' </summary>
        ''' <param name="value">要使用的文件，路径已映射到Skin文件夹</param>
        Protected MustOverride Sub SetIconFromFile_Implement(value As String)

        ''' <summary>
        ''' 获取或设置窗口指针<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI | WINDOW_CURSOR_CHANGE
        ''' </summary>
        ''' <value>指针文件路径（Skin目录下）</value>
        ''' <returns></returns>
        Public Property CursorPath As String
            Get
                Return _cursorPath
            End Get
            Set(value As String)
                If value = _cursorPath Then Exit Property
                SetCursorFromFile_Implement(PathFunction.CombineToString(PathType.Skin, value))
                Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_CURSOR_CHANGE", 1)
                _cursorPath = value
            End Set
        End Property

        ''' <summary>
        ''' CursorPath的Set实现<br></br>
        ''' 仅在为CursorPath设置和当前值不同的值时才会调用
        ''' </summary>
        ''' <param name="value">要使用的文件，路径已映射到Skin文件夹</param>
        Protected MustOverride Sub SetCursorFromFile_Implement(value As String)

        ''' <summary>
        ''' 获取或设置窗口标题<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI | WINDOW_TITLE_CHANGE
        ''' </summary>
        ''' <returns></returns>
        Public Property Title As String
            Get
                Return _title
            End Get
            Set(value As String)
                If value = _title Then Exit Property
                SetTitle_Implement(value)
                Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_TITLE_CHANGE", 1)
                _title = value
            End Set
        End Property

        ''' <summary>
        ''' Title的Set实现<br></br>
        ''' 仅在为Title设置和当前值不同的值时才会调用
        ''' </summary>
        ''' <param name="value">要使用的标题</param>
        Protected MustOverride Sub SetTitle_Implement(value As String)

        ''' <summary>
        ''' 获取或设置窗口是否允许缩放<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI | WINDOW_CAN_RESIZE/WINDOW_CANNOT_RESIZE
        ''' </summary>
        ''' <returns></returns>
        Public Property CanResize As Boolean
            Get
                Return _canResize
            End Get
            Set(value As Boolean)
                If value = _canResize Then Exit Property
                SetCanResize_Implement(value)
                If value Then
                    Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_CAN_RESIZE", 1)
                Else
                    Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_CANNOT_RESIZE", 1)
                End If
                _canResize = value
            End Set
        End Property

        ''' <summary>
        ''' CanResize的Set实现<br></br>
        ''' 仅在为CanResize设置和当前值不同的值时才会调用
        ''' </summary>
        ''' <param name="value">是否允许缩放</param>
        Protected MustOverride Sub SetCanResize_Implement(value As Boolean)

        ''' <summary>
        ''' 获取或设置窗口是否置顶<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI | WINDOW_ENTER_TOPMOST/WINDOW_EXIT_TOPMOST
        ''' </summary>
        ''' <returns></returns>
        Public Property Topmost As Boolean
            Get
                Return _topmost
            End Get
            Set(value As Boolean)
                If value = _topmost Then Exit Property
                SetTopmost_Implement(value)
                If value Then
                    Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_ENTER_TOPMOST", 1)
                Else
                    Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_EXIT_TOPMOST", 1)
                End If
                _topmost = value
            End Set
        End Property

        ''' <summary>
        ''' Topmost的Set实现<br></br>
        ''' 仅在为Topmost设置和当前值不同的值时才会调用
        ''' </summary>
        ''' <param name="value">是否置顶</param>
        Protected MustOverride Sub SetTopmost_Implement(value As Boolean)

        ''' <summary>
        ''' 转到指定场景<br></br>
        ''' 属性：<br></br>
        '''  同步检查转场条件，可能异步进行转场 | UI | WINDOW_NAVIGATION_CANCEL/WINDOW_NAVIGATION_STANDBY/WINDOW_NAVITATION_FINISH
        ''' </summary>
        ''' <param name="target">要转到的场景</param>
        ''' <param name="param">转场参数</param>
        Public Sub [Go](target As Scene, ParamArray param As Object())
            Dim e As New NavigationParameter(SceneNow, target) With {.Canceled = False, .ExtraData = param}
            Configuration.Receiver.NavigateReceiver.Boardcast(e)
            If e.Canceled Then
                Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_NAVIGATION_CANCEL", 1)
                Exit Sub
            End If
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_NAVIGATION_STANDBY", 1)
            Go_Implement(e)
        End Sub

        ''' <summary>
        ''' Go的实现，已处理NavigationParameter.Canceled<br></br>
        ''' 转场完成后，你必须调用基类(WindowBase)的NavigateFinished函数告知系统转场已完成<br></br>
        ''' 换句话说，你不调用这个函数也就意味着游戏系统不会知道转场完成
        ''' </summary>
        ''' <param name="e">转场参数</param>
        Protected MustOverride Sub Go_Implement(e As NavigationParameter)

        ''' <summary>
        ''' 获取正在展示的场景对象<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride Function SceneNow() As Scene

        ''' <summary>
        ''' 获取历史记录中出现过的场景对象<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI
        ''' </summary>
        ''' <param name="name">场景名称</param>
        ''' <returns></returns>
        Public MustOverride Function GetScene(name As String) As Scene

        ''' <summary>
        ''' 确定是否允许后退到上一个场景<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride Function CanGoBack() As Boolean

        ''' <summary>
        ''' 回到上一个场景<br></br>
        ''' 属性：<br></br>
        '''  由渲染插件决定 | UI | WINDOW_GOBACK
        ''' </summary>
        Public Sub GoBack()
            If Not CanGoBack() Then Throw New Exception.IlleagleGoBackException
            GoBack_Implement()
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_GOBACK", 1)
        End Sub

        ''' <summary>
        ''' GoBack的实现<br></br>
        ''' 已进行CanGoBack检查
        ''' </summary>
        Protected MustOverride Sub GoBack_Implement()

        ''' <summary>
        ''' 确定是否允许前进到下一个场景<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride Function CanGoForward() As Boolean

        ''' <summary>
        ''' 前进到下一个场景<br></br>
        ''' 属性：<br></br>
        '''  由渲染插件决定 | UI | WINDOW_GOFORWARD
        ''' </summary>
        Public Sub GoForward()
            If Not CanGoForward() Then Throw New Exception.IlleagleGoForwardException
            GoForward_Implemet()
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_GOFORWARD", 1)
        End Sub

        ''' <summary>
        ''' GoForward的实现<br></br>
        ''' 已进行CanGoForward检查
        ''' </summary>
        Protected MustOverride Sub GoForward_Implemet()

        ''' <summary>
        ''' 删除最近一个可后退场景的记录<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI
        ''' </summary>
        Public MustOverride Sub RemoveOneBackHistory()

        ''' <summary>
        ''' 清除所有可后退场景的记录<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI
        ''' </summary>
        Public MustOverride Sub ClearBackHistory()

        ''' <summary>
        ''' 从文件载入一个资源<br></br>
        ''' 属性：<br></br>
        '''  由渲染插件决定 | UI | WINDOW_LOAD_RESOURCE
        ''' </summary>
        ''' <param name="name">资源的名称</param>
        ''' <param name="path">资源文件路径(Resource目录下)</param>
        Public Sub LoadResource(name As String, path As String)
            LoadResource_Implement(name, PathFunction.CombineToString(PathType.Resource, path))
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_LOAD_RESOURCE", 1)
        End Sub

        ''' <summary>
        ''' LoadResource的实现
        ''' </summary>
        ''' <param name="name">资源的名称</param>
        ''' <param name="path">资源文件路径，路径已映射到Resource文件夹</param>
        Protected MustOverride Sub LoadResource_Implement(name As String, path As String)

        ''' <summary>
        ''' 从对象载入一个资源<br></br>
        ''' 属性：<br></br>
        '''  由渲染插件决定 | UI | WINDOW_LOAD_RESOURCE
        ''' </summary>
        ''' <param name="name">资源的名称</param>
        ''' <param name="target">资源对象</param>
        Public Sub LoadResource(name As String, target As Object)
            LoadResource_Implement(name, target)
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_LOAD_RESOURCE", 1)
        End Sub

        ''' <summary>
        ''' LoadResource的实现
        ''' </summary>
        ''' <param name="name">资源的名称</param>
        ''' <param name="target">资源对象</param>
        Protected MustOverride Sub LoadResource_Implement(name As String, target As Object)

        ''' <summary>
        ''' 删除一个资源<br></br>
        ''' 属性：<br></br>
        '''  由渲染插件决定 | UI | WINDOW_REMOVE_RESOURCE
        ''' </summary>
        ''' <param name="name">资源的名称</param>
        ''' <returns></returns>
        Public Function RemoveResource(name As String) As Object
            Dim target = RemoveResource_Implement(name)
            If target IsNot Nothing Then Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_REMOVE_RESOURCE", 1)
            Return target
        End Function

        Public MustOverride Function RemoveResource_Implement(name As String) As Object

        ''' <summary>
        ''' 获取指定名称的资源对象<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI
        ''' </summary>
        ''' <param name="name">资源的名称</param>
        ''' <returns></returns>
        Public MustOverride Function GetResource(name As String) As Object

        ''' <summary>
        ''' 在窗口上执行一个委托<br></br>
        ''' 属性：<br></br>
        '''  由sync参数决定 | UI
        ''' </summary>
        ''' <param name="target">要执行的委托</param>
        ''' <param name="sync">是否同步执行</param>
        ''' <param name="params">委托参数</param>
        ''' <returns></returns>
        Public MustOverride Function Run(target As [Delegate], sync As Boolean, ParamArray params As Object())

        ''' <summary>
        ''' 在窗口上执行游戏循环的渲染委托<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI
        ''' </summary>
        ''' <param name="target">目标委托</param>
        Protected Friend MustOverride Sub RunRenderDelegate(target As Action)

        ''' <summary>
        ''' 在窗口上执行自定义指令<br></br>
        ''' 属性：<br></br>
        '''  由渲染插件决定 | UI
        ''' </summary>
        ''' <param name="order">指令</param>
        ''' <param name="param">指令参数</param>
        Public MustOverride Sub RunCustomizedOrder(order As String, param As Object)

        ''' <summary>
        ''' 告知游戏系统窗口转场完成<br></br>
        ''' 属性：<br></br>
        '''  同步 | UI
        ''' </summary>
        Protected Sub NavigateFinished()
            Configuration.System.MessageService.SendMessage("[SYSTEM]WINDOW_NAVITATION_FINISH", 1)
        End Sub
    End Class
End Namespace