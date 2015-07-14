Imports System.Windows

Namespace Component
    ''' <summary>
    ''' 组件列表类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class ComponentList
        Private ReadOnly _componentlist As New List(Of Component)
        Private ReadOnly _element As frameworkelement

        ''' <summary>
        ''' 获得一个新的组件列表
        ''' </summary>
        ''' <param name="element">组件列表所属的元素</param>
        ''' <remarks></remarks>
        Friend Sub New(element As FrameworkElement)
            _element = element
        End Sub

        ''' <summary>
        ''' 添加一个组件到组件列表
        ''' </summary>
        ''' <param name="target">要添加的组件</param>
        ''' <remarks></remarks>
        Public Sub Add(target As Component)
            If Not _componentlist.Contains(target) Then
                target.Binding(_element)
                target.BindElement(_element)
                _componentlist.Add(target)
            End If
        End Sub

        ''' <summary>
        ''' 根据类型获取组件列表中第一个满足要求的组件
        ''' </summary>
        ''' <typeparam name="T">要获取的组件的类型</typeparam>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function [Get](Of T As Component)() As T
            Dim targetList = Gets(Of T)()
            Return If(targetList.Length > 0, targetList(0), Nothing)
        End Function

        ''' <summary>
        ''' 根据名称获取组件列表中第一个满足要求的组件
        ''' </summary>
        ''' <param name="type">要获取的组件的类型名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function [Get](type As String) As Component
            Dim targetList = Gets(type)
            Return If(targetList.Length > 0, targetList(0), Nothing)
        End Function

        ''' <summary>
        ''' 根据类型获取组件列表中所有满足要求的组件
        ''' </summary>
        ''' <typeparam name="T">要获取的组件的类型</typeparam>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Gets(Of T As Component)() As T()
            Dim targetType = GetType(T).Name
            Return _componentlist.Where(Function(e) e.GetType.Name.Equals(targetType)).ToArray
        End Function

        ''' <summary>
        ''' 根据名称获取组件列表中所有满足要求的组件
        ''' </summary>
        ''' <param name="type">要获取的组件的类型名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Gets(type As String) As Component()
            Return _componentlist.Where(Function(e) e.GetType.Name.Equals(type)).ToArray
        End Function

        ''' <summary>
        ''' 根据类型删除组件列表中第一个满足要求的组件
        ''' </summary>
        ''' <typeparam name="T">要删除的组件的类型</typeparam>
        ''' <remarks></remarks>
        Public Sub Remove(Of T As Component)()
            Dim targetList = Gets(Of T)()
            If targetList.Length > 0 Then
                Dim target = targetList(0)
                target.Unbinding(_element)
                target.UnbindElement(_element)
                _componentlist.Remove(target)
            End If
        End Sub

        ''' <summary>
        ''' 根据名称删除组件列表中第一个满足要求的组件
        ''' </summary>
        ''' <param name="type">要删除的组件的类型名称</param>
        ''' <remarks></remarks>
        Public Sub Remove(type As String)
            Dim targetList = Gets(type)
            If targetList.Length > 0 Then
                Dim target = targetList(0)
                target.Unbinding(_element)
                target.UnbindElement(_element)
                _componentlist.Remove(target)
            End If
        End Sub

        ''' <summary>
        ''' 根据类型删除组件列表中所有满足要求的组件
        ''' </summary>
        ''' <typeparam name="T">要删除的组件的类型</typeparam>
        ''' <remarks></remarks>
        Public Sub RemoveAll(Of T As Component)()
            Dim targetList = Gets(Of T)()
            For Each target In targetList
                target.Unbinding(_element)
                target.UnbindElement(_element)
                _componentlist.Remove(target)
            Next
        End Sub

        ''' <summary>
        ''' 根据名称删除组件列表中所有满足要求的组件
        ''' </summary>
        ''' <param name="type">要删除的组件的类型名称</param>
        ''' <remarks></remarks>
        Public Sub RemoveAll(type As String)
            Dim targetList = Gets(type)
            For Each target In targetList
                target.Unbinding(_element)
                target.UnbindElement(_element)
                _componentlist.Remove(target)
            Next
        End Sub

        ''' <summary>
        ''' 清除组建列表中的所有组件
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Clear()
            For Each target In _componentlist
                target.Unbinding(_element)
                target.UnbindElement(_element)
            Next
            _componentlist.Clear()
        End Sub
    End Class
End Namespace