Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports WADV.AppCore.Path.PathFunction

''' <summary>
''' 表示一个属性
''' </summary>
''' <remarks></remarks>
Public Structure AchievementProperty

    Public Name As String
    Public Data As Integer
    Public Triggle As List(Of IAchievement)

End Structure

''' <summary>
''' 属性存储类
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class PropertyList
    Private Shared _list As New Dictionary(Of String, AchievementProperty)

    ''' <summary>
    ''' 添加一个属性(新属性的默认数据是0且不关联任何成就)
    ''' </summary>
    ''' <param name="name">属性的名字</param>
    ''' <remarks></remarks>
    Friend Shared Sub Add(name As String)
        If Contains(name) Then Return
        Dim prop As New AchievementProperty
        prop.Name = name
        prop.Data = 0
        prop.Triggle = New List(Of IAchievement)
        _list.Add(prop.Name, prop)
        MessageAPI.SendSync("ACHIEVE_PROP_ADD")
    End Sub

    ''' <summary>
    ''' 确定指定属性是否存在
    ''' </summary>
    ''' <param name="name">属性的名字</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Contains(name As String) As Boolean
        Return _list.ContainsKey(name)
    End Function

    ''' <summary>
    ''' 更新指定属性的数据(该操作会触发所有和该属性关联的成就进行状态检查)
    ''' </summary>
    ''' <param name="name">属性的名字</param>
    ''' <param name="data">属性的新数据</param>
    ''' <remarks></remarks>
    Friend Shared Sub SetData(name As String, data As Integer)
        If Not Contains(name) Then Return
        Dim prop = _list.Item(name)
        prop.Data = data
        _list.Item(name) = prop
        For Each tmpAchievement In prop.Triggle
            tmpAchievement.Check()
        Next
        MessageAPI.SendSync("ACHIEVE_PROP_SETDATA")
    End Sub

    ''' <summary>
    ''' 获取指定属性的数据
    ''' </summary>
    ''' <param name="name">属性的名字</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function GetData(name As String) As Integer
        If Not Contains(name) Then Throw New KeyNotFoundException("找不到对应名称的属性")
        Return _list.Item(name).Data
    End Function

    ''' <summary>
    ''' 关联一个成就到指定属性
    ''' </summary>
    ''' <param name="name">属性的名字</param>
    ''' <param name="target">要关联的成就</param>
    ''' <remarks></remarks>
    Friend Shared Sub Register(name As String, target As IAchievement)
        If Not Contains(name) Then Return
        Dim triggle = _list.Item(name).Triggle
        If Not triggle.Contains(target) Then
            triggle.Add(target)
            MessageAPI.SendSync("ACHIEVE_PROP_ADDTRIGGLE")
        End If
    End Sub

    ''' <summary>
    ''' 取消一个成就和指定属性的关联
    ''' </summary>
    ''' <param name="name">属性的名字</param>
    ''' <param name="target">要取消关联的成就</param>
    ''' <remarks></remarks>
    Friend Shared Sub Unregister(name As String, target As IAchievement)
        If Not Contains(name) Then Return
        Dim triggle = _list.Item(name).Triggle
        If triggle.Contains(target) Then
            triggle.Remove(target)
            MessageAPI.SendSync("ACHIEVE_PROP_DELETETRIGGLE")
        End If
    End Sub

    ''' <summary>
    ''' 删除指定属性
    ''' </summary>
    ''' <param name="name">属性的名字</param>
    ''' <remarks></remarks>
    Friend Shared Sub Delete(name As String)
        If Not Contains(name) Then Return
        _list.Remove(name)
        MessageAPI.SendSync("ACHIEVE_PROP_DELETE")
    End Sub

    ''' <summary>
    ''' 从文件中恢复属性列表
    ''' </summary>
    ''' <param name="fileName">要读取的文件</param>
    ''' <remarks></remarks>
    Friend Shared Sub Load(fileName As String)
        Dim stream As New FileStream(PathAPI.GetPath(PathType.UserFile, fileName), FileMode.Open)
        Dim formatter As New BinaryFormatter
        _list = TryCast(formatter.Deserialize(stream), Dictionary(Of String, AchievementProperty))
        stream.Close()
        MessageAPI.SendSync("ACHIEVE_PROP_LOAD")
    End Sub

    ''' <summary>
    ''' 保存属性列表到文件
    ''' </summary>
    ''' <param name="fileName">要保存到的文件</param>
    ''' <remarks></remarks>
    Friend Shared Sub Save(fileName As String)
        Dim stream As New FileStream(PathAPI.GetPath(PathType.UserFile, fileName), FileMode.Create)
        Dim formatter As New BinaryFormatter
        formatter.Serialize(stream, _list)
        stream.Close()
        MessageAPI.SendSync("ACHIEVE_PROP_SAVE")
    End Sub

End Class
