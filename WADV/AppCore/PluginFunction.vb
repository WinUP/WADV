Imports System.IO
Imports WADV.AppCore.PluginInterface

Namespace AppCore

    ''' <summary>
    ''' 插件设定类
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class PluginFunction
        Private Shared ReadOnly PluginFileList As New List(Of String)
        Private Shared ReadOnly InitialiserList As New List(Of IGameStart)
        Private Shared ReadOnly DestructorList As New List(Of IGameClose)
        Friend Shared ReadOnly NavigaionReceiverList As New List(Of PluginInterface.INavigationReceiver)
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
                If tmpTypeName.GetInterface("WADV.AppCore.PluginInterface.IInitialise") <> Nothing Then
                    If Not DirectCast(Activator.CreateInstance(tmpTypeName), IInitialise).Initialising() Then Throw New Exception("插件初始化无法顺利完成")
                End If
                If tmpTypeName.GetInterface("WADV.AppCore.PluginInterface.IGameStart") <> Nothing Then
                    InitialiserList.Add(Activator.CreateInstance(tmpTypeName))
                End If
                If tmpTypeName.GetInterface("WADV.AppCore.PluginInterface.IGameClose") <> Nothing Then
                    DestructorList.Add(Activator.CreateInstance(tmpTypeName))
                End If
                If tmpTypeName.GetInterface("WADV.AppCore.PluginInterface.INavigationReceiver") <> Nothing Then
                    NavigaionReceiverList.Add(Activator.CreateInstance(tmpTypeName))
                End If
            Next
            PluginFileList.Add(fileName)
            Messanger.SendMessage("[SYSTEM]PLUGIN_ADD")
        End Sub

        ''' <summary>
        ''' 使用插件初始化游戏
        ''' </summary>
        Friend Shared Sub InitialisingGame()
            For Each initialiser In InitialiserList
                initialiser.InitialisingGame()
            Next
        End Sub

        ''' <summary>
        ''' 使用插件解构游戏
        ''' <param name="e">可取消事件的数据</param>
        ''' </summary>
        Friend Shared Sub DestructuringGame(e As ComponentModel.CancelEventArgs)
            For Each destructor In DestructorList
                destructor.DestructuringGame(e)
            Next
        End Sub

    End Class

End Namespace
