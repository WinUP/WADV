Imports System.Windows
Imports System.Windows.Controls

''' <summary>
''' 精灵列表
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class SpriteList
    Private Shared ReadOnly List As New Dictionary(Of String, FrameworkElement)

    ''' <summary>
    ''' 注册一个现有界面元素为精灵
    ''' </summary>
    ''' <param name="name">精灵的名称</param>
    ''' <param name="target">要注册的元素</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Add(name As String, target As FrameworkElement) As FrameworkElement
        If target Is Nothing Then Return Nothing
        If List.ContainsKey(name) Then Return Nothing
        WindowAPI.InvokeSync(Sub()
                                 If target.Tag IsNot Nothing Then
                                     If TryCast(target.Tag, Sprite) IsNot Nothing Then
                                         DirectCast(target.Tag, Sprite).RemoveReceiver()
                                     Else
                                         target.Tag = Nothing
                                     End If
                                 End If
                                 target.Tag = New Sprite(target)
                             End Sub)
        List.Add(name, target)
        MessageAPI.SendSync("[SPRITE]SPRITE_ADD")
        Return target
    End Function

    ''' <summary>
    ''' 获得已注册的精灵
    ''' </summary>
    ''' <param name="name">精灵的名称</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared ReadOnly Property Item(name As String) As FrameworkElement
        Get
            If Not Contains(name) Then Return Nothing
            Return List(name)
        End Get
    End Property

    ''' <summary>
    ''' 确认指定名称的精灵是否存在
    ''' </summary>
    ''' <param name="name">要检查的名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Contains(name As String) As Boolean
        Return List.ContainsKey(name)
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
        WindowAPI.InvokeAsync(Sub()
                                  If target.Parent IsNot Nothing Then DirectCast(target.Parent, Panel).Children.Remove(target)
                                  DirectCast(target.Tag, Sprite).RemoveReceiver()
                              End Sub)
        List.Remove(name)
        MessageAPI.SendSync("[SPRITE]SPRITE_DELETE")
        Return True
    End Function

    ''' <summary>
    ''' 删除一个精灵
    ''' </summary>
    ''' <param name="target">要删除的精灵</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Delete(target As FrameworkElement) As Boolean
        Dim key = List.Where(Function(e) e.Value Is target).Select(Function(e) e.Key)
        If key.Count = 0 Then Return False
        Delete(key.FirstOrDefault)
        Return True
    End Function
End Class
