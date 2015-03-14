Imports System.Windows.Controls

''' <summary>
''' 立绘列表
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class SpriteList
    Private Shared ReadOnly List As New Dictionary(Of String, Panel)

    ''' <summary>
    ''' 添加一个精灵
    ''' </summary>
    ''' <param name="name">精灵的名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Add(name As String) As Canvas
        Dim target As Canvas = WindowAPI.GetDispatcher.Invoke(Function() New Canvas)
        If Add(name, target) Then
            Return target
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' 注册一个现有界面元素为精灵
    ''' </summary>
    ''' <param name="name">精灵的名称</param>
    ''' <param name="target">要注册的元素</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Add(name As String, target As Panel) As Boolean
        If target Is Nothing Then Return False
        If List.ContainsKey(name) Then Return False
        List.Add(name, target)
        MessageAPI.SendSync("[SPRITE]SPRITE_ADD")
        Return True
    End Function

    ''' <summary>
    ''' 获取一个精灵
    ''' 当对应名称的精灵不存在时返回Nothing
    ''' </summary>
    ''' <param name="name">要获取的精灵的名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Item(name As String) As Panel
        If Not Contains(name) Then Return Nothing
        Return List(name)
    End Function

    Friend Shared Function Contains(id As String) As Boolean
        Return List.ContainsKey(id)
    End Function

    ''' <summary>
    ''' 删除一个精灵
    ''' </summary>
    ''' <param name="name">要删除的精灵的名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Delete(name As String) As Boolean
        If Not Contains(name) Then Return False
        Dim target = List(name)
        WindowAPI.GetDispatcher.Invoke(Sub() If target.Parent IsNot Nothing Then DirectCast(target.Parent, Panel).Children.Remove(target))
        List.Remove(name)
        MessageAPI.SendSync("[SPRITE]SPRITE_DELETE")
        Return True
    End Function

End Class
