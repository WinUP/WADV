Namespace AppCore.API

    ''' <summary>
    ''' 资源API类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class ResourceAPI

        ''' <summary>
        ''' 加载资源到游戏全局
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="filePath">资源文件路径(从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadToGameSync(filePath As String)
            Dim tmpDictionart As New ResourceDictionary
            tmpDictionart.Source = PathFunction.GetFullUri(PathType.Skin, filePath)
            Application.Current.Resources.MergedDictionaries.Add(tmpDictionart)
            MessageService.GetInstance.SendMessage("[SYSTEM]GAME_RESOURCE_ADD")
        End Sub

        ''' <summary>
        ''' 加载资源到主窗口
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="filePath">资源文件路径(从Skin目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub LoadToWindowSync(filePath As String)
            Dim tmpDictionart As New ResourceDictionary
            tmpDictionart.Source = PathFunction.GetFullUri(PathType.Skin, filePath)
            WindowAPI.InvokeSync(Sub() WindowAPI.GetWindow.Resources.MergedDictionaries.Add(tmpDictionart))
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_RESOURCE_ADD")
        End Sub

        ''' <summary>
        ''' 清空全局资源
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ClearGameSync()
            Application.Current.Resources.MergedDictionaries.Clear()
            MessageService.GetInstance.SendMessage("[SYSTEM]GAME_RESOURCE_CLEAR")
        End Sub

        ''' <summary>
        ''' 清空主窗口资源
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ClearWindowSync()
            WindowAPI.InvokeSync(Sub() WindowAPI.GetWindow.Resources.MergedDictionaries.Clear())
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_RESOURCE_CLEAR")
        End Sub

        ''' <summary>
        ''' 清除指定全局资源
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="resource">要清除的资源对象</param>
        ''' <remarks></remarks>
        Public Shared Sub RemoveFromGameSync(resource As ResourceDictionary)
            Application.Current.Resources.MergedDictionaries.Remove(resource)
            MessageService.GetInstance.SendMessage("[SYSTEM]GAME_RESOURCE_REMOVE")
        End Sub

        ''' <summary>
        ''' 清除指定主窗口资源
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="resource">要清除的资源对象</param>
        ''' <remarks></remarks>
        Public Shared Sub RemoveFromWindowSync(resource As ResourceDictionary)
            WindowAPI.InvokeSync(Sub() WindowAPI.GetWindow.Resources.MergedDictionaries.Remove(resource))
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_RESOURCE_REMOVE")
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

End Namespace