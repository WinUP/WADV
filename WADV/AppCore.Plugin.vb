Imports System.IO

Namespace AppCore.Plugin

    ''' <summary>
    ''' 逻辑循环接口
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ILooping

        ''' <summary>
        ''' 执行一次循环
        ''' </summary>
        Function StartLooping() As Boolean

        ''' <summary>
        ''' 执行一次渲染
        ''' </summary>
        ''' <remarks></remarks>
        Sub StartRendering(window As Window)

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
    Public Interface IScriptFunction

        ''' <summary>
        ''' 注册脚本函数
        ''' </summary>
        ''' <param name="ScriptVM">脚本执行主机</param>
        Sub StartRegisting(ScriptVM As LuaInterface.Lua)

    End Interface

    ''' <summary>
    ''' 插件设定类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PluginFunction
        Private Shared pluginFileList As New List(Of String)

        ''' <summary>
        ''' 载入所有插件
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub InitialiseAllPlugins()
            Dim pluginFileList = Directory.GetFiles(Path.PathFunction.GetFullPath(Path.PathConfig.Plugin), "*.dll", SearchOption.TopDirectoryOnly)
            For Each fileName In pluginFileList
                Try
                    If Not AddPlugin(fileName) Then Throw New Exception("插件的初始化函数报告它失败了")
                Catch ex As Exception
                    MessageBox.Show("插件" & My.Computer.FileSystem.GetName(fileName) & "初始化失败，这是详细信息：" & Environment.NewLine & ex.Message)
                End Try
            Next
        End Sub

        ''' <summary>
        ''' 添加一个插件
        ''' </summary>
        ''' <param name="fileName">插件文件名</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function AddPlugin(fileName As String) As Boolean
            If pluginFileList.Contains(fileName) Then Return True
            Dim pluginTypes = System.Reflection.Assembly.LoadFrom(Path.PathFunction.GetFullPath(Path.PathConfig.Plugin, fileName)).GetTypes
            Dim returnData As Boolean = True
            For Each tmpTypeName In pluginTypes
                If tmpTypeName.GetInterface("IInitialise") <> Nothing Then
                    returnData = TryCast(Activator.CreateInstance(tmpTypeName), Plugin.IInitialise).StartInitialising()
                End If
                If tmpTypeName.GetInterface("IScriptFunction") <> Nothing Then
                    Script.Register.AddFunction(Activator.CreateInstance(tmpTypeName))
                End If
            Next
            pluginFileList.Add(fileName)
            Return returnData
        End Function

    End Class

End Namespace
