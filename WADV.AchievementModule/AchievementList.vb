Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports WADV.Core.Enumeration

''' <summary>
''' 成就存储类
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class AchievementList
    Private Shared _list As New Dictionary(Of String, Achievement)

    ''' <summary>
    ''' 添加一个成就
    ''' </summary>
    ''' <param name="achievement">要添加的成就</param>
    ''' <remarks></remarks>
    Friend Shared Sub Add(achievement As Achievement)
        If Contains(achievement.Name) Then Exit Sub
        SyncLock (_list)
            _list.Add(achievement.Name, achievement)
        End SyncLock
        achievement.Register()
    End Sub

    ''' <summary>
    ''' 确定指定成就是否存在
    ''' </summary>
    ''' <param name="name">成就的名字</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Contains(name As String) As Boolean
        Return _list.ContainsKey(name)
    End Function

    ''' <summary>
    ''' 删除指定成就
    ''' </summary>
    ''' <param name="name">成就的名字</param>
    ''' <remarks></remarks>
    Friend Shared Sub Delete(name As String)
        If Not Contains(name) Then Exit Sub
        SyncLock (_list)
            _list.Remove(name)
        End SyncLock
    End Sub

    ''' <summary>
    ''' 获取已注册的成就
    ''' </summary>
    ''' <param name="name">成就的名字</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared ReadOnly Property Item(name As String) As Achievement
        Get
            If Not Contains(name) Then Return Nothing
            Return _list.Item(name)
        End Get
    End Property

    ''' <summary>
    ''' 保存成就列表到文件<br></br>
    ''' 将会发送消息 [ACHIEVE]ACHIEVE_SAVE
    ''' </summary>
    ''' <param name="fileName">要保存到的文件</param>
    ''' <remarks></remarks>
    Friend Shared Sub Save(fileName As String)
        Dim stream As New FileStream(Core.API.Path.Combine(PathType.UserFile, fileName), FileMode.Create)
        Dim formatter As New BinaryFormatter With {.Binder = New DeserializationBinder}
        SyncLock (_list)
            formatter.Serialize(stream, _list)
            stream.Close()
        End SyncLock
        Message.Send("[ACHIEVE]ACHIEVE_SAVE")
    End Sub

    ''' <summary>
    ''' 从文件中恢复成就列表<br></br>
    ''' 将会发送消息 [ACHIEVE]ACHIEVE_LOAD
    ''' </summary>
    ''' <param name="fileName">要读取的文件</param>
    ''' <remarks></remarks>
    Friend Shared Sub Load(fileName As String)
        Dim path = Core.API.Path.Combine(PathType.UserFile, fileName)
        If Not My.Computer.FileSystem.FileExists(path) Then Return
        Dim stream As New FileStream(path, FileMode.Open)
        Dim formatter As New BinaryFormatter With {.Binder = New DeserializationBinder}
        SyncLock (_list)
            _list = TryCast(formatter.Deserialize(stream), Dictionary(Of String, Achievement))
        End SyncLock
        stream.Close()
        Message.Send("[ACHIEVE]ACHIEVE_LOAD")
    End Sub

    ''' <summary>
    ''' 获取所有成就
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetList() As ReadOnlyCollection(Of Achievement)
        Return _list.Values.ToList.AsReadOnly
    End Function
End Class
