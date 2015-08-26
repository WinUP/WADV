Imports System.Windows
Imports WADV.Core.GameSystem

Namespace API
    ''' <summary>
    ''' 资源API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Resource
        ''' <summary>
        ''' 加载资源到游戏全局
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="filePath">资源文件路径(Skin目录下)</param>
        ''' <remarks></remarks>
        Public Sub LoadToGame(filePath As String)
            Dim tmpDictionart As New ResourceDictionary
            tmpDictionart.Source = PathFunction.GetFullUri(PathType.Skin, filePath)
            Application.Current.Resources.MergedDictionaries.Add(tmpDictionart)
            Config.MessageService.SendMessage("[SYSTEM]GAME_RESOURCE_ADD")
        End Sub

        ''' <summary>
        ''' 加载资源到主窗口
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="filePath">资源文件路径(Skin目录下)</param>
        ''' <remarks></remarks>
        Public Sub LoadToWindow(filePath As String)
            Dim tmpDictionart As New ResourceDictionary
            tmpDictionart.Source = PathFunction.GetFullUri(PathType.Skin, filePath)
            Window.Invoke(Sub() Window.Window.Resources.MergedDictionaries.Add(tmpDictionart))
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_RESOURCE_ADD")
        End Sub

        ''' <summary>
        ''' 清空全局资源
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearGame()
            Application.Current.Resources.MergedDictionaries.Clear()
            Config.MessageService.SendMessage("[SYSTEM]GAME_RESOURCE_CLEAR")
        End Sub

        ''' <summary>
        ''' 清空主窗口资源
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearWindow()
            Window.Invoke(Sub() Window.Window.Resources.MergedDictionaries.Clear())
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_RESOURCE_CLEAR")
        End Sub

        ''' <summary>
        ''' 清除指定全局资源
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="resource">要清除的资源对象</param>
        ''' <remarks></remarks>
        Public Sub RemoveFromGame(resource As ResourceDictionary)
            Application.Current.Resources.MergedDictionaries.Remove(resource)
            Config.MessageService.SendMessage("[SYSTEM]GAME_RESOURCE_REMOVE")
        End Sub

        ''' <summary>
        ''' 清除指定主窗口资源
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="resource">要清除的资源对象</param>
        ''' <remarks></remarks>
        Public Sub RemoveFromWindow(resource As ResourceDictionary)
            Window.Invoke(Sub() Window.Window.Resources.MergedDictionaries.Remove(resource))
            Config.MessageService.SendMessage("[SYSTEM]WINDOW_RESOURCE_REMOVE")
        End Sub

        ''' <summary>
        ''' 获取全局资源对象
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetFromGame() As ResourceDictionary
            Return Application.Current.Resources
        End Function

        ''' <summary>
        ''' 获取主窗口资源对象
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetFromWindow() As ResourceDictionary
            Return Window.Dispatcher.Invoke(Function() Window.Window.Resources)
        End Function

        ''' <summary>
        ''' 为元素注册一个名称
        ''' </summary>
        ''' <param name="name">要使用的名称</param>
        ''' <param name="target">要注册的元素</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Register(name As String, target As FrameworkElement) As Boolean
            Return MainObjectList.Register(name, target)
        End Function

        ''' <summary>
        ''' 收回一个已分配的名称
        ''' </summary>
        ''' <param name="name">要收回的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Unregister(name As String) As Boolean
            Return MainObjectList.Unregister(name)
        End Function

        ''' <summary>
        ''' 根据名称获取元素
        ''' </summary>
        ''' <param name="name">目标元素的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetByName(name As String) As FrameworkElement
            Return MainObjectList.Get(name)
        End Function

        ''' <summary>
        ''' 注册一个事件处理函数到指定对象的指定事件
        ''' </summary>
        ''' <param name="target">要注册到的对象</param>
        ''' <param name="eventName">要注册的事件名称</param>
        ''' <param name="code">事件处理委托</param>
        ''' <remarks></remarks>
        Public Sub Handle(target As Object, eventName As String, code As [Delegate])
            Dim targetEvent = target.GetType.GetEvent(eventName)
            If targetEvent Is Nothing Then Exit Sub
            targetEvent.AddEventHandler(target, code)
        End Sub

        ''' <summary>
        ''' 从指定对象的指定事件上解绑一个事件处理函数
        ''' </summary>
        ''' <param name="target">要解绑的对象</param>
        ''' <param name="eventName">要解绑的事件名称</param>
        ''' <param name="code">事件处理委托</param>
        ''' <remarks></remarks>
        Public Sub Unhandle(target As Object, eventName As String, code As [Delegate])
            Dim targetEvent = target.GetType.GetEvent(eventName)
            If targetEvent Is Nothing Then Exit Sub
            targetEvent.RemoveEventHandler(target, code)
        End Sub
    End Module
End Namespace