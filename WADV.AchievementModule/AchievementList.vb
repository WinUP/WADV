Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports WADV.AppCore.Path.PathFunction

''' <summary>
''' 表示一个成就
''' </summary>
''' <remarks></remarks>
Friend Structure Achievement

    Friend Name As String
    Friend Substance As String
    Friend IsEarn As Boolean
    Friend Data As String

End Structure

''' <summary>
''' 成就列表
''' </summary>
''' <remarks></remarks>
Friend Class AchievementList

    Private Shared _list As Dictionary(Of String, Achievement)

    Friend Shared Sub Add(achievement As Achievement)
        If Contains(achievement.Name) Then Return
        _list.Add(achievement.Name, achievement)
        MessageAPI.SendSync("ACHIEVE_LIST_ADD")
    End Sub

    Friend Shared Function Contains(name As String) As Boolean
        Return _list.ContainsKey(name)
    End Function

    Friend Shared Sub Delete(name As String)
        If Not Contains(name) Then Return
        _list.Remove(name)
        MessageAPI.SendSync("ACHIEVE_LIST_DELETE")
    End Sub

    Friend Shared Function Item(name As String) As Achievement
        If Not Contains(name) Then Return Nothing
        Return _list.Item(name)
    End Function

    Friend Shared Sub Change(name As String, newAchieve As Achievement)
        If Not Contains(name) Then Return
        _list.Item(name) = newAchieve
    End Sub

    Friend Shared Sub Save(fileName As String)
        Dim stream As New FileStream(PathAPI.GetPath(PathType.UserFile, fileName), FileMode.Create)
        Dim formatter As New BinaryFormatter
        formatter.Serialize(stream, _list)
        stream.Close()
        MessageAPI.SendSync("ACHIEVE_LIST_SAVE")
    End Sub

    Friend Shared Sub Load(fileName As String)
        Dim stream As New FileStream(PathAPI.GetPath(PathType.UserFile, fileName), FileMode.Open)
        Dim formatter As New BinaryFormatter
        _list = TryCast(formatter.Deserialize(stream), Dictionary(Of String, Achievement))
        stream.Close()
        MessageAPI.SendSync("ACHIEVE_LIST_LOAD")
    End Sub

End Class
