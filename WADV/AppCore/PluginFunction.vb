Imports System.IO
Imports WADV.AppCore.PluginInterface

Namespace AppCore

    ''' <summary>
    ''' 插件设定类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class PluginFunction
        Private Shared ReadOnly PluginFileList As New List(Of String)
        Private Shared ReadOnly InitialiserList As New List(Of IGameStart)
        Private Shared ReadOnly DestructorList As New List(Of IGameClose)
        Private Shared ReadOnly Messanger As MessageService = MessageService.GetInstance

        ''' <summary>
        ''' 载入所有插件
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub InitialiseAllPlugins()
            Dim tmpPluginFileList = Directory.GetDirectories(PathFunction.GetFullPath(PathType.Plugin, ""))
            For Each fileName In tmpPluginFileList
                Try
                    If Not AddPlugin(String.Format("{0}\{1}.dll", fileName, fileName.Substring(fileName.LastIndexOf("\", StringComparison.Ordinal) + 1))) Then Throw New Exception("插件的初始化函数报告它失败了")
                    PluginFileList.Add(fileName)
                Catch ex As Exception
                    MessageBox.Show("插件" & My.Computer.FileSystem.GetName(fileName) & "初始化失败，这是详细信息：" & Environment.NewLine & ex.Message)
                End Try
            Next
            Messanger.SendMessage("[SYSTEM]PLUGIN_INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 添加一个插件
        ''' </summary>
        ''' <param name="fileName">插件路径(从Plugin目录开始)</param>
        ''' <returns>是否添加成功</returns>
        ''' <remarks></remarks>
        Public Shared Function AddPlugin(fileName As String) As Boolean
            If PluginFileList.Contains(fileName) Then Return True
            Dim pluginTypes = Reflection.Assembly.LoadFrom(fileName).GetTypes
            Dim initFunction As IGameStart = Nothing
            Dim destructFunction As IGameClose = Nothing
            For Each tmpTypeName In pluginTypes
                If tmpTypeName.GetInterface("WADV.AppCore.PluginInterface.IInitialise") <> Nothing Then
                    If Not TryCast(Activator.CreateInstance(tmpTypeName), IInitialise).Initialising() Then
                        Return False
                    End If
                End If
                If tmpTypeName.GetInterface("WADV.AppCore.PluginInterface.IGameStart") <> Nothing Then
                    initFunction = Activator.CreateInstance(tmpTypeName)
                End If
                If tmpTypeName.GetInterface("WADV.AppCore.PluginInterface.IGameClose") <> Nothing Then
                    destructFunction = Activator.CreateInstance(tmpTypeName)
                End If
            Next
            If initFunction IsNot Nothing Then InitialiserList.Add(initFunction)
            If destructFunction IsNot Nothing Then DestructorList.Add(destructFunction)
            PluginFileList.Add(fileName)
            Messanger.SendMessage("[SYSTEM]PLUGIN_ADD")
            Return True
        End Function

        ''' <summary>
        ''' 使用插件初始化游戏
        ''' </summary>
        Public Shared Sub InitialisingGame()
            For Each initialiser In InitialiserList
                initialiser.InitialisingGame()
            Next
        End Sub

        ''' <summary>
        ''' 使用插件解构游戏
        ''' <param name="e">可取消事件的数据</param>
        ''' </summary>
        Public Shared Sub DestructuringGame(e As ComponentModel.CancelEventArgs)
            For Each destructor In DestructorList
                destructor.DestructuringGame(e)
            Next
        End Sub

    End Class

End Namespace
