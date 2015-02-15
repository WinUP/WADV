Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports WADV.AppCore.Path.PathFunction

''' <summary>
''' 表示一个成就
''' </summary>
''' <remarks></remarks>
Public Interface IAchievement

    Sub Register()

    Sub Check()

    Function GetName() As String

    Function GetSubstance() As String

    Function IsEarn() As Boolean

End Interface

''' <summary>
''' 一般成就的基类
''' </summary>
''' <remarks></remarks>
Public MustInherit Class Achievement : Implements IAchievement
    Private ReadOnly _name As String
    Private ReadOnly _substance As String
    Private _isEarn As Boolean

    Public Sub New(name As String, substance As String)
        _name = name
        _substance = substance
        _isEarn = False
    End Sub

    Public MustOverride Sub Check() Implements IAchievement.Check

    Public MustOverride Sub Register() Implements IAchievement.Register

    Public Function GetName() As String Implements IAchievement.GetName
        Return _name
    End Function

    Public Function GetSubstance() As String Implements IAchievement.GetSubstance
        Return _substance
    End Function

    Public Function IsEarn() As Boolean Implements IAchievement.IsEarn
        Return _isEarn
    End Function

End Class

''' <summary>
''' 成就存储类
''' </summary>
''' <remarks></remarks>
Friend Class AchievementList
    Private Shared _list As Dictionary(Of String, Achievement)

    ''' <summary>
    ''' 添加一个成就
    ''' </summary>
    ''' <param name="achievement">要添加的成就</param>
    ''' <remarks></remarks>
    Friend Shared Sub Add(achievement As Achievement)
        If Contains(achievement.GetName) Then Return
        _list.Add(achievement.GetName, achievement)
        MessageAPI.SendSync("ACHIEVE_LIST_ADD")
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
        MessageAPI.SendSync("ACHIEVE_LIST_DELETE")
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
        formatter.Serialize(stream, _list)
        stream.Close()
        MessageAPI.SendSync("ACHIEVE_LIST_SAVE")
    End Sub

    ''' <summary>
    ''' 从文件中恢复成就列表
    ''' </summary>
    ''' <param name="fileName">要读取的文件</param>
    ''' <remarks></remarks>
    Friend Shared Sub Load(fileName As String)
        Dim stream As New FileStream(PathAPI.GetPath(PathType.UserFile, fileName), FileMode.Open)
        Dim formatter As New BinaryFormatter
        _list = TryCast(formatter.Deserialize(stream), Dictionary(Of String, Achievement))
        stream.Close()
        MessageAPI.SendSync("ACHIEVE_LIST_LOAD")
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
