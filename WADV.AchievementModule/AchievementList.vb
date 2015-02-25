Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

''' <summary>
''' 一般成就的基类
''' </summary>
''' <remarks></remarks>
<Serializable> Public MustInherit Class Achievement
    Private ReadOnly _name As String
    Private ReadOnly _substance As String
    Private _isEarn As Boolean

    Public Sub New(name As String, substance As String)
        _name = name
        _substance = substance
        _isEarn = False
    End Sub

    Public MustOverride Sub Check()

    Public MustOverride Sub Register()

    Public Function GetName() As String
        Return _name
    End Function

    Public Function GetSubstance() As String
        Return _substance
    End Function

    Public Function IsEarn() As Boolean
        Return _isEarn
    End Function

    Protected Sub SetEarn()
        _isEarn = True
        ShowList.Add(Me)
        MessageAPI.SendSync("[ACHIEVE]ACHIEVE_EARN")
    End Sub

End Class

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
        If Contains(achievement.GetName) Then Return
        _list.Add(achievement.GetName, achievement)
        achievement.Register()
        MessageAPI.SendSync("[ACHIEVE]ACHIEVE_ADD")
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
        If Not Contains(name) Then Return
        _list.Remove(name)
        MessageAPI.SendSync("[ACHIEVE]ACHIEVE_DELETE")
    End Sub

    ''' <summary>
    ''' 获取指定成就
    ''' </summary>
    ''' <param name="name">成就的名字</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Item(name As String) As Achievement
        If Not Contains(name) Then Return Nothing
        Return _list.Item(name)
    End Function

    ''' <summary>
    ''' 保存成就列表到文件
    ''' </summary>
    ''' <param name="fileName">要保存到的文件</param>
    ''' <remarks></remarks>
    Friend Shared Sub Save(fileName As String)
        Dim stream As New FileStream(PathAPI.GetPath(PathType.UserFile, fileName), FileMode.Create)
        Dim formatter As New BinaryFormatter
        formatter.Binder = New DeserializationBinder
        formatter.Serialize(stream, _list)
        stream.Close()
        MessageAPI.SendSync("[ACHIEVE]ACHIEVE_SAVE")
    End Sub

    ''' <summary>
    ''' 从文件中恢复成就列表
    ''' </summary>
    ''' <param name="fileName">要读取的文件</param>
    ''' <remarks></remarks>
    Friend Shared Sub Load(fileName As String)
        Dim path = PathAPI.GetPath(PathType.UserFile, fileName)
        If Not My.Computer.FileSystem.FileExists(path) Then Return
        Dim stream As New FileStream(path, FileMode.Open)
        Dim formatter As New BinaryFormatter
        formatter.Binder = New DeserializationBinder
        _list = TryCast(formatter.Deserialize(stream), Dictionary(Of String, Achievement))
        stream.Close()
        MessageAPI.SendSync("[ACHIEVE]ACHIEVE_LOAD")
    End Sub

    ''' <summary>
    ''' 获取所有成就
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetList() As Achievement()
        Return _list.Values.ToArray
    End Function

End Class
