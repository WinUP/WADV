Imports System.IO

Namespace AppCore.Config

    ''' <summary>
    ''' 路径设置类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class URLConfig

        Private Shared pluginURI As String
        Private Shared resourceURI As String
        Private Shared scriptURI As String
        Private Shared userFileURI As String
        Private Shared skinURI As String

        ''' <summary>
        ''' 获取或设置插件路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Protected Friend Shared Property Plugin As String
            Get
                Return pluginURI
            End Get
            Set(value As String)
                pluginURI = value
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置资源路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Protected Friend Shared Property Resource As String
            Get
                Return resourceURI
            End Get
            Set(value As String)
                resourceURI = value
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置皮肤路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Protected Friend Shared Property Skin As String
            Get
                Return skinURI
            End Get
            Set(value As String)
                skinURI = value
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置脚本路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Protected Friend Shared Property Script As String
            Get
                Return scriptURI
            End Get
            Set(value As String)
                scriptURI = value
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置用户文件路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Protected Friend Shared Property UserFile As String
            Get
                Return userFileURI
            End Get
            Set(value As String)
                userFileURI = value
            End Set
        End Property

        ''' <summary>
        ''' 获取文件的绝对路径
        ''' </summary>
        ''' <param name="type">路径类型</param>
        ''' <param name="fileURL">类型路径下的相对路径</param>
        ''' <returns>文件的绝对路径</returns>
        Protected Friend Shared Function GetFullURI(type As String, Optional fileURL As String = "") As String
            Return Path.Combine(My.Application.Info.DirectoryPath, type, fileURL)
        End Function


    End Class

    ''' <summary>
    ''' 窗口设定类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WindowConfig

        ''' <summary>
        ''' 游戏主窗口
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared BaseWindow As Window = Application.Current.MainWindow

    End Class

    ''' <summary>
    ''' 循环设定类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LoopConfig

        Private Shared frameCheckPerSecond As Integer

        ''' <summary>
        ''' 获取或设置逻辑循环每秒的理想执行次数
        ''' </summary>
        ''' <value>执行次数</value>
        ''' <returns></returns>
        Protected Friend Shared Property Frame As Integer
            Get
                Return frameCheckPerSecond
            End Get
            Set(value As Integer)
                frameCheckPerSecond = value
            End Set
        End Property

    End Class

    ''' <summary>
    ''' 插件设定类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PluginConfig

        Private Shared renderPluginList As New List(Of Plugin.IRender)
        Private Shared pluginFileList As New List(Of String)

        ''' <summary>
        ''' 获取需要渲染的插件列表
        ''' </summary>
        Protected Friend Shared ReadOnly Property RenderingList As Plugin.IRender()
            Get
                Return renderPluginList.ToArray
            End Get
        End Property

        ''' <summary>
        ''' 载入所有插件
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub InitialiseAllPlugins()
            Dim pluginFileList = Directory.GetFiles(URLConfig.GetFullURI(URLConfig.Plugin), "*.dll", SearchOption.TopDirectoryOnly)
            For Each fileName In pluginFileList
                Try
                    If Not AddPlugin(fileName) Then Throw New Exception("有的插件的初始化函数说它失败了")
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
            Dim pluginTypes = System.Reflection.Assembly.LoadFrom(URLConfig.GetFullURI(URLConfig.Plugin, fileName)).GetTypes
            Dim returnData As Boolean = True
            For Each tmpTypeName In pluginTypes
                If tmpTypeName.GetInterface("ILoop") <> Nothing Then
                    [Loop].MainLoop.AddLoop(Activator.CreateInstance(tmpTypeName))
                End If
                If tmpTypeName.GetInterface("IInitialise") <> Nothing Then
                    returnData = TryCast(Activator.CreateInstance(tmpTypeName), Plugin.IInitialise).StartInitialising()
                End If
                If tmpTypeName.GetInterface("IRender") <> Nothing Then
                    renderPluginList.Add(Activator.CreateInstance(tmpTypeName))
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
