Imports System.Windows

''' <summary>
''' 元素命名列表
''' </summary>
''' <remarks>允许一个元素具有多个名称，但不允许多个元素共享一个名称</remarks>
Friend NotInheritable Class NamedElementList
    Private Shared ReadOnly List As New Dictionary(Of String, FrameworkElement)

    ''' <summary>
    ''' 为元素注册一个名称
    ''' </summary>
    ''' <param name="name">要使用的名称</param>
    ''' <param name="target">要注册的元素</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Register(name As String, target As FrameworkElement) As Boolean
        If List.ContainsKey(name) Then Return False
        List.Add(name, target)
        Return True
    End Function

    ''' <summary>
    ''' 根据名称获取元素
    ''' </summary>
    ''' <param name="name">目标元素的名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function [Get](name As String) As FrameworkElement
        If Not List.ContainsKey(name) Then Return Nothing
        Return List(name)
    End Function

    ''' <summary>
    ''' 收回一个已分配的名称
    ''' </summary>
    ''' <param name="name">要收回的名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Unregister(name As String) As Boolean
        If Not List.ContainsKey(name) Then Return False
        List.Remove(name)
        Return True
    End Function
End Class
