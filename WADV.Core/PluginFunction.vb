Imports System.Xml
Imports System.Windows
Imports WADV.Core.PluginInterface
Imports WADV.Core.ReceiverList
Imports WADV.Core.Exception

''' <summary>
''' 插件设定类
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class PluginFunction
    Private Shared ReadOnly PluginFileList As New List(Of String)

    ''' <summary>
    ''' 载入所有插件
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared Sub InitialiseAllPlugins()
        If Config.IsPluginInitialised Then Throw New PluginsMultiInitialiseException
        Dim pluginConfig As New XmlDocument
        pluginConfig.Load(PathFunction.GetFullPath(PathType.Plugin, "plugin.xml"))
        For Each pluginName In From fileName As XmlNode In pluginConfig.SelectNodes("/plugin/sequence/item") Select fileName.InnerXml
            Try
                AddPlugin($"{pluginName}\{pluginName}.dll")
            Catch ex As System.Exception
                MessageBox.Show(ex.Message, "插件" & pluginName & "加载失败")
            End Try
        Next
        Config.IsPluginInitialised = True
        Config.MessageService.SendMessage("[SYSTEM]PLUGIN_INIT_FINISH")
    End Sub

    ''' <summary>
    ''' 添加一个插件
    ''' </summary>
    ''' <param name="fileName">插件路径(从Plugin目录开始)</param>
    ''' <remarks></remarks>
    Friend Shared Sub AddPlugin(fileName As String)
        If PluginFileList.Contains(fileName) Then Throw New PluginMultiInitialiseException
        Dim pluginTypes = Reflection.Assembly.LoadFrom(PathFunction.GetFullPath(PathType.Plugin, fileName)).GetTypes
        PluginLoadReceiverList.BeforeLoad(pluginTypes)
        For Each tmpTypeName In pluginTypes
            If tmpTypeName.GetInterface("WADV.Core.PluginInterface.IPluginInitialise") <> Nothing Then
                If Not DirectCast(Activator.CreateInstance(tmpTypeName), IPluginInitialise).Initialising() Then Throw New PluginInitialiseFailedException(My.Computer.FileSystem.GetName(fileName))
            End If
            If tmpTypeName.GetInterface("WADV.Core.PluginInterface.IGameInitialiserReceiver") <> Nothing Then
                InitialiseReceiverList.Add(Activator.CreateInstance(tmpTypeName))
            End If
            If tmpTypeName.GetInterface("WADV.Core.PluginInterface.IGameDestructorReceiver") <> Nothing Then
                DestructReceiverList.Add(Activator.CreateInstance(tmpTypeName))
            End If
            If tmpTypeName.GetInterface("WADV.Core.PluginInterface.INavigationReceiver") <> Nothing Then
                NavigateReceiverList.Add(Activator.CreateInstance(tmpTypeName))
            End If
        Next
        PluginFileList.Add(fileName)
        Config.MessageService.SendMessage("[SYSTEM]PLUGIN_ADD")
    End Sub
End Class
