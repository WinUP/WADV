﻿Imports System.Windows

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
            MessageService.GetInstance.SendMessage("[SYSTEM]GAME_RESOURCE_ADD")
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
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_RESOURCE_ADD")
        End Sub

        ''' <summary>
        ''' 清空全局资源
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearGame()
            Application.Current.Resources.MergedDictionaries.Clear()
            MessageService.GetInstance.SendMessage("[SYSTEM]GAME_RESOURCE_CLEAR")
        End Sub

        ''' <summary>
        ''' 清空主窗口资源
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearWindow()
            Window.Invoke(Sub() Window.Window.Resources.MergedDictionaries.Clear())
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_RESOURCE_CLEAR")
        End Sub

        ''' <summary>
        ''' 清除指定全局资源
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="resource">要清除的资源对象</param>
        ''' <remarks></remarks>
        Public Sub RemoveFromGame(resource As ResourceDictionary)
            Application.Current.Resources.MergedDictionaries.Remove(resource)
            MessageService.GetInstance.SendMessage("[SYSTEM]GAME_RESOURCE_REMOVE")
        End Sub

        ''' <summary>
        ''' 清除指定主窗口资源
        ''' 同步方法|UI线程
        ''' </summary>
        ''' <param name="resource">要清除的资源对象</param>
        ''' <remarks></remarks>
        Public Sub RemoveFromWindow(resource As ResourceDictionary)
            Window.Invoke(Sub() Window.Window.Resources.MergedDictionaries.Remove(resource))
            MessageService.GetInstance.SendMessage("[SYSTEM]WINDOW_RESOURCE_REMOVE")
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
    End Module
End Namespace