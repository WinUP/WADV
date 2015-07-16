' WinUP Adventure Game Engine Core Framework
' Plugin Class
' This is the main class of game plugin system

Imports System.Windows
Imports System.Xml
Imports WADV.Core.PluginInterface
Imports WADV.Core.ReceiverList
Imports WADV.Core.Exception

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
        If PluginFileList.Count <> 0 Then Throw New IllegalMultipleStartException
        Dim config As New XmlDocument
        config.Load(PathFunction.GetFullPath(PathType.Plugin, "plugin.xml"))
        For Each pluginName In From fileName As XmlNode In config.SelectNodes("/plugin/sequence/item") Select fileName.InnerXml
            Try
                AddPlugin(String.Format("{0}\{1}.dll", pluginName, pluginName))
            Catch ex As System.Exception
                MessageBox.Show(ex.Message, "插件" & pluginName & "加载失败")
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
        Messanger.SendMessage("[SYSTEM]PLUGIN_ADD")
    End Sub
End Class
