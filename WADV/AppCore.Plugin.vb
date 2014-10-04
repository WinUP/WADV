Imports System.IO
Imports WADV.AppCore.API

Namespace AppCore.Plugin

    ''' <summary>
    ''' 逻辑循环接口
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ILooping

        ''' <summary>
        ''' 执行一次循环
        ''' </summary>
        Function StartLooping(frame As Integer) As Boolean

        ''' <summary>
        ''' 执行一次渲染
        ''' </summary>
        ''' <remarks></remarks>
        Sub StartRendering(window As Window)

    End Interface

    ''' <summary>
    ''' 消息循环接口
    ''' </summary>
    Public Interface IMessage

        ''' <summary>
        ''' 执行一次消息处理
        ''' </summary>
        ''' <param name="message">消息标识符</param>
        Sub ReceivingMessage(message As String)

    End Interface

    ''' <summary>
    ''' 初始化接口
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IInitialise

        ''' <summary>
        ''' 开始初始化
        ''' </summary>
        Function StartInitialising() As Boolean

    End Interface

    ''' <summary>
    ''' 脚本函数注册接口
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IScript

        ''' <summary>
        ''' 注册脚本函数
        ''' </summary>
        ''' <param name="scriptVM">脚本执行主机</param>
        Sub StartRegisting(scriptVM As LuaInterface.Lua)

    End Interface

    ''' <summary>
    ''' 游戏初始化接口
    ''' </summary>
    Public Interface IGameStart

        ''' <summary>
        ''' 开始初始化游戏
        ''' </summary>
        Sub InitialisingGame()

    End Interface

    ''' <summary>
    ''' 游戏解构接口
    ''' </summary>
    Public Interface IGameClose

        ''' <summary>
        ''' 开始解构游戏
        ''' <param name="e">可取消事件的数据</param>
        ''' </summary>
        Sub DestructuringGame(e As ComponentModel.CancelEventArgs)

    End Interface

    ''' <summary>
    ''' 插件设定类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PluginFunction
        Private Shared pluginFileList As New List(Of String)
        Private Shared initialiserList As New List(Of IGameStart)
        Private Shared destructorList As New List(Of IGameClose)

        ''' <summary>
        ''' 载入所有插件
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub InitialiseAllPlugins()
            Dim pluginFileList = Directory.GetDirectories(Path.PathFunction.GetFullPath(Path.PathFunction.PathType.Plugin, ""))
            For Each fileName In pluginFileList
                Try
                    If Not AddPlugin(String.Format("{0}\{1}.dll", fileName, fileName.Substring(fileName.LastIndexOf("\") + 1))) Then Throw New Exception("插件的初始化函数报告它失败了")
                Catch ex As Exception
                    MessageBox.Show("插件" & My.Computer.FileSystem.GetName(fileName) & "初始化失败，这是详细信息：" & Environment.NewLine & ex.Message)
                End Try
            Next
            MessageAPI.Send("GAME_PLUGIN_INITFINISH")
        End Sub

        ''' <summary>
        ''' 添加一个插件
        ''' </summary>
        ''' <param name="fileName">插件路径(从Plugin目录开始)</param>
        ''' <returns>是否添加成功</returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function AddPlugin(fileName As String) As Boolean
            If pluginFileList.Contains(fileName) Then Return True
            Dim pluginTypes = Reflection.Assembly.LoadFrom(fileName).GetTypes
            Dim scriptFunction As IScript = Nothing
            Dim initFunction As IGameStart = Nothing
            Dim destructFunction As IGameClose = Nothing
            For Each tmpTypeName In pluginTypes
                If tmpTypeName.GetInterface("IInitialise") <> Nothing Then
                    If Not TryCast(Activator.CreateInstance(tmpTypeName), Plugin.IInitialise).StartInitialising() Then
                        Return False
                    End If
                End If
                If tmpTypeName.GetInterface("IScript") <> Nothing Then
                    scriptFunction = Activator.CreateInstance(tmpTypeName)
                End If
                If tmpTypeName.GetInterface("IGameStart") <> Nothing Then
                    initFunction = Activator.CreateInstance(tmpTypeName)
                End If
                If tmpTypeName.GetInterface("IGameStop") <> Nothing Then
                    destructFunction = Activator.CreateInstance(tmpTypeName)
                End If
            Next
            If scriptFunction IsNot Nothing Then Script.Register.AddFunction(scriptFunction)
            If initFunction IsNot Nothing Then initialiserList.Add(initFunction)
            If destructFunction IsNot Nothing Then destructorList.Add(destructFunction)
            pluginFileList.Add(fileName)
            MessageAPI.Send("GAME_PLUGIN_ADD")
            Return True
        End Function

        ''' <summary>
        ''' 使用插件初始化游戏
        ''' </summary>
        Protected Friend Shared Sub InitialisingGame()
            For Each initialiser In initialiserList
                initialiser.InitialisingGame()
            Next
        End Sub

        ''' <summary>
        ''' 使用插件解构游戏
        ''' <param name="e">可取消事件的数据</param>
        ''' </summary>
        Protected Friend Shared Sub DestructuringGame(e As ComponentModel.CancelEventArgs)
            For Each destructor In destructorList
                destructor.DestructuringGame(e)
            Next
        End Sub

    End Class

End Namespace
