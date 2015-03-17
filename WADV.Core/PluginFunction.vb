Imports System.IO
Imports System.Windows
Imports WADV.Core.PluginInterface
Imports WADV.Core.ReceiverList

''' <summary>
''' 插件设定类
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class PluginFunction
    Private Shared ReadOnly PluginFileList As New List(Of String)
    Private Shared ReadOnly Messanger As MessageService = MessageService.GetInstance

    ''' <summary>
    ''' 载入所有插件
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared Sub InitialiseAllPlugins()
        Dim tmpPluginFileList = Directory.GetDirectories(PathFunction.GetFullPath(PathType.Plugin, ""))
        For Each fileName In tmpPluginFileList
            Try
                AddPlugin(String.Format("{0}\{1}.dll", fileName, fileName.Substring(fileName.LastIndexOf("\", StringComparison.Ordinal) + 1)))
                PluginFileList.Add(fileName)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "插件" & My.Computer.FileSystem.GetName(fileName) & "加载失败")
            End Try
        Next
        Messanger.SendMessage("[SYSTEM]PLUGIN_INIT_FINISH")
    End Sub

    ''' <summary>
    ''' 添加一个插件
    ''' </summary>
    ''' <param name="fileName">插件路径(从Plugin目录开始)</param>
    ''' <remarks></remarks>
    Friend Shared Sub AddPlugin(fileName As String)
        If PluginFileList.Contains(fileName) Then Exit Sub
        Dim pluginTypes = Reflection.Assembly.LoadFrom(fileName).GetTypes
        For Each tmpTypeName In pluginTypes
            If tmpTypeName.GetInterface("WADV.Core.PluginInterface.IPluginInitialise") <> Nothing Then
                If Not DirectCast(Activator.CreateInstance(tmpTypeName), IPluginInitialise).Initialising() Then Throw New Exception("插件初始化无法顺利完成")
            End If
            If tmpTypeName.GetInterface("WADV.Core.PluginInterface.IGameInitialiserReceiver") <> Nothing Then
                InitialiserReceiverList.Add(Activator.CreateInstance(tmpTypeName))
            End If
            If tmpTypeName.GetInterface("WADV.Core.PluginInterface.IGameDestructorReceiver") <> Nothing Then
                DestructorReceiverList.Add(Activator.CreateInstance(tmpTypeName))
            End If
            If tmpTypeName.GetInterface("WADV.Core.PluginInterface.INavigationReceiver") <> Nothing Then
                NavigationReceiverList.Add(Activator.CreateInstance(tmpTypeName))
            End If
        Next
        PluginFileList.Add(fileName)
        Messanger.SendMessage("[SYSTEM]PLUGIN_ADD")
    End Sub

End Class
