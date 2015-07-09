Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

''' <summary>
''' 属性存储列表
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class AchievementPropertyList
    Private Shared _list As New Dictionary(Of String, AchievementProperty)

    ''' <summary>
    ''' 添加一个属性(新属性的默认数据是0且不关联任何成就)
    ''' </summary>
    ''' <param name="name">属性的名字</param>
    ''' <remarks></remarks>
    Friend Shared Sub Add(name As String)
        If Contains(name) Then Exit Sub
        Dim prop As New AchievementProperty With {.Name = name, .Data = 0, .Triggle = New List(Of Achievement)}
        _list.Add(prop.Name, prop)
        Message.Send("[ACHIEVE]PROP_ADD")
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
        If Not Contains(name) Then Exit Sub
        Dim target = _list.Item(name)
        target.Data = data
        _list.Item(name) = target
        target.Triggle.Where(Function(e) Not e.IsEarn).ToList.ForEach(Sub(e1) e1.Check())
        Message.Send("[ACHIEVE]PROP_DATA_SET")
        API.AchievementProperty.Save()
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
    Friend Shared Sub Register(name As String, target As Achievement)
        If Not Contains(name) Then Exit Sub
        Dim triggle = _list.Item(name).Triggle
        If Not triggle.Contains(target) Then
            triggle.Add(target)
            Message.Send("[ACHIEVE]PROP_TRIGGLE_ADD")
            API.AchievementProperty.Save()
        End If
    End Sub

    ''' <summary>
    ''' 取消一个成就和指定属性的关联
    ''' </summary>
    ''' <param name="name">属性的名字</param>
    ''' <param name="target">要取消关联的成就</param>
    ''' <remarks></remarks>
    Friend Shared Sub Unregister(name As String, target As Achievement)
        If Not Contains(name) Then Exit Sub
        Dim triggle = _list.Item(name).Triggle
        If triggle.Contains(target) Then
            triggle.Remove(target)
            Message.Send("[ACHIEVE]PROP_TRIGGLE_DELETE")
            API.AchievementProperty.Save()
        End If
    End Sub

    ''' <summary>
    ''' 删除指定属性
    ''' </summary>
    ''' <param name="name">属性的名字</param>
    ''' <remarks></remarks>
    Friend Shared Sub Delete(name As String)
        If Not Contains(name) Then Exit Sub
        _list.Remove(name)
        Message.Send("[ACHIEVE]PROP_DELETE")
        API.AchievementProperty.Save()
    End Sub

    ''' <summary>
    ''' 从文件中恢复属性列表
    ''' </summary>
    ''' <param name="fileName">要读取的文件</param>
    ''' <remarks></remarks>
    Friend Shared Sub Load(fileName As String)
        Dim path = Combine(PathType.UserFile, fileName)
        If Not My.Computer.FileSystem.FileExists(path) Then Return
        Dim stream As New FileStream(path, FileMode.Open)
        Dim formatter As New BinaryFormatter With {.Binder = New DeserializationBinder}
        _list = TryCast(formatter.Deserialize(stream), Dictionary(Of String, AchievementProperty))
        stream.Close()
        Message.Send("[ACHIEVE]PROP_LOAD")
    End Sub

    ''' <summary>
    ''' 保存属性列表到文件
    ''' </summary>
    ''' <param name="fileName">要保存到的文件</param>
    ''' <remarks></remarks>
    Friend Shared Sub Save(fileName As String)
        Dim stream As New FileStream(Combine(PathType.UserFile, fileName), FileMode.Create)
        Dim formatter As New BinaryFormatter With {.Binder = New DeserializationBinder}
        formatter.Serialize(stream, _list)
        stream.Close()
        Message.Send("[ACHIEVE]PROP_SAVE")
    End Sub
End Class
