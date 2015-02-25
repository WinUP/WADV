Imports System.Windows.Controls

''' <summary>
''' 立绘列表
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class TeList
    Private Shared ReadOnly ImageList As New Dictionary(Of Integer, Panel)
    Private Shared _id As Integer = 0

    ''' <summary>
    ''' 添加一个立绘
    ''' </summary>
    ''' <param name="target"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Add(target As Panel) As Integer
        If ImageList.ContainsValue(target) Then Return (From tmpPare In ImageList Where tmpPare.Value Is target Select tmpPare.Key).FirstOrDefault
        ImageList.Add(_id, target)
        _id += 1
        MessageAPI.SendSync("[TE]IMAGE_ADD")
        Return _id - 1
    End Function

    ''' <summary>
    ''' 获取一个立绘
    ''' 当对应ID的立绘不存在时返回Nothing
    ''' </summary>
    ''' <param name="id">要获取的立绘的ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Item(id As Integer) As Panel
        If Not Contains(id) Then Return Nothing
        Return ImageList(id)
    End Function

    Friend Shared Function Contains(id As Integer) As Boolean
        Return ImageList.ContainsKey(id)
    End Function

    ''' <summary>
    ''' 删除一个立绘
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Delete(id As Integer) As Boolean
        If Not Contains(id) Then Return False
        ImageList.Remove(id)
        MessageAPI.SendSync("[TE]IMAGE_DELETE")
        Return True
    End Function

End Class
