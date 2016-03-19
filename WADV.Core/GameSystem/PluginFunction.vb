﻿Imports System.Windows
Imports System.Xml
Imports WADV.Core.Enumeration
Imports WADV.Core.Exception
Imports WADV.Core.PluginInterface
Imports WADV.Core.ReceiverList

Namespace GameSystem

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
            Dim pluginConfig As New XmlDocument
            pluginConfig.Load(PathFunction.CombineToString(PathType.Plugin, "plugin.xml"))
            For Each pluginName In From fileName As XmlNode In pluginConfig.SelectNodes("/plugin/sequence/item") Select fileName.InnerXml
                Try
                    LoadPlugin($"{pluginName}\{pluginName}.dll")
                Catch ex As System.Exception
                    Throw New PluginInitialiseFailedException(pluginName)
                End Try
            Next
        End Sub

        ''' <summary>
        ''' 添加一个插件
        ''' </summary>
        ''' <param name="fileName">插件路径(从Plugin目录开始)</param>
        ''' <remarks></remarks>
        Friend Shared Sub LoadPlugin(fileName As String)
            If PluginFileList.Contains(fileName) Then Throw New PluginMultiInitialisesException
            Dim pluginTypes = Reflection.Assembly.LoadFrom(PathFunction.CombineToString(PathType.Plugin, fileName)).GetTypes
            Configuration.Receiver.PluginLoadingReceiver.BeforeLoad(pluginTypes)
            For Each tmpTypeName In pluginTypes
                If tmpTypeName.GetInterface("WADV.Core.PluginInterface.IPluginInitialise") <> Nothing Then
                    If Not DirectCast(Activator.CreateInstance(tmpTypeName), IPluginInitialise).Initialising() Then Throw New PluginInitialiseFailedException(My.Computer.FileSystem.GetName(fileName))
                End If
                If tmpTypeName.GetInterface("WADV.Core.PluginInterface.IGameInitialiserReceiver") <> Nothing Then
                    Configuration.Receiver.InitialiserReceiver.Add(Activator.CreateInstance(tmpTypeName))
                End If
                If tmpTypeName.GetInterface("WADV.Core.PluginInterface.IGameDestructorReceiver") <> Nothing Then
                    Configuration.Receiver.DestructReceiver.Add(Activator.CreateInstance(tmpTypeName))
                End If
                If tmpTypeName.GetInterface("WADV.Core.PluginInterface.INavigationReceiver") <> Nothing Then
                    Configuration.Receiver.NavigateReceiver.Add(Activator.CreateInstance(tmpTypeName))
                End If
            Next
            PluginFileList.Add(fileName)
            Configuration.System.MessageService.SendMessage("[SYSTEM]PLUGIN_ADD", 1)
        End Sub
    End Class
End Namespace