Imports WADV.Core.API
Imports WADV.Core.Render

Namespace Component
    ''' <summary>
    ''' 组件列表类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class ComponentList : Implements IDisposable
        Private ReadOnly _componentlist As New List(Of Component)
        Private ReadOnly _element As Sprite

        ''' <summary>
        ''' 获得一个新的组件列表
        ''' </summary>
        ''' <param name="element">组件列表所属的元素</param>
        ''' <remarks></remarks>
        Friend Sub New(element As Sprite)
            _element = element
        End Sub

        ''' <summary>
        ''' 获取已存在的组件的个数
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Count() As Integer
            Return _componentlist.Count
        End Function

        ''' <summary>
        ''' 添加一个组件到组件列表
        ''' </summary>
        ''' <param name="target">要添加的组件</param>
        ''' <remarks></remarks>
        Public Function Add(target As Component) As BindingResult
            If _componentlist.Contains(target) Then Return BindingResult.NoNeed
            If Not target.BeforeBinding(_element) Then Return BindingResult.Cancel
            target.Bind(_element)
            _componentlist.Add(target)
            Return BindingResult.Success
        End Function

        ''' <summary>
        ''' 根据类型获取组件列表中第一个满足要求的组件
        ''' </summary>
        ''' <typeparam name="T">要获取的组件的类型</typeparam>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function [Get](Of T As Component)() As T
            Dim targetList = GetAll(Of T)()
            Return If(targetList.Length > 0, targetList(0), Nothing)
        End Function

        ''' <summary>
        ''' 根据名称获取组件列表中第一个满足要求的组件
        ''' </summary>
        ''' <param name="type">要获取的组件的类型名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function [Get](type As String) As Component
            Dim targetList = GetAll(type)
            Return If(targetList.Length > 0, targetList(0), Nothing)
        End Function

        ''' <summary>
        ''' 根据类型获取组件列表中所有满足要求的组件
        ''' </summary>
        ''' <typeparam name="T">要获取的组件的类型</typeparam>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetAll(Of T As Component)() As T()
            Dim targetType = GetType(T).Name
            Return _componentlist.Where(Function(e) e.GetType.Name.Equals(targetType)).ToArray
        End Function

        ''' <summary>
        ''' 根据名称获取组件列表中所有满足要求的组件
        ''' </summary>
        ''' <param name="type">要获取的组件的类型名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetAll(type As String) As Component()
            Return _componentlist.Where(Function(e) e.GetType.Name.Equals(type)).ToArray
        End Function

        ''' <summary>
        ''' 根据类型删除组件列表中第一个满足要求的组件
        ''' </summary>
        ''' <typeparam name="T">要删除的组件的类型</typeparam>
        ''' <remarks></remarks>
        Public Function Remove(Of T As Component)() As UnbindingResult
            Dim targetList = GetAll(Of T)()
            If targetList.Length = 0 Then Return UnbindingResult.CannotFind
            Dim target = targetList(0)
            If Not target.BeforeUnbinding(_element) Then Return UnbindingResult.Cancel
            target.ListenLoop(False)
            target.ListenMessage(False)
            target.Unbind(_element)
            _componentlist.Remove(target)
            Return UnbindingResult.Success
        End Function

        ''' <summary>
        ''' 根据名称删除组件列表中第一个满足要求的组件
        ''' </summary>
        ''' <param name="type">要删除的组件的类型名称</param>
        ''' <remarks></remarks>
        Public Function Remove(type As String) As UnbindingResult
            Dim targetList = GetAll(type)
            If targetList.Length = 0 Then Return UnbindingResult.CannotFind
            Dim target = targetList(0)
            If Not target.BeforeUnbinding(_element) Then Return UnbindingResult.Cancel
            target.ListenLoop(False)
            target.ListenMessage(False)
            target.Unbind(_element)
            _componentlist.Remove(target)
            Return UnbindingResult.Success
        End Function

        ''' <summary>
        ''' 根据类型删除组件列表中所有满足要求的组件
        ''' </summary>
        ''' <typeparam name="T">要删除的组件的类型</typeparam>
        ''' <remarks></remarks>
        Public Function RemoveAll(Of T As Component)() As UnbindingResult()
            Dim targetList = GetAll(Of T)()
            If targetList.Length = 0 Then Return {UnbindingResult.CannotFind}
            Dim result(targetList.Length - 1) As UnbindingResult
            Dim target As Component
            For i = 0 To result.Length - 1
                target = targetList(i)
                If Not target.BeforeUnbinding(_element) Then
                    result(i) = UnbindingResult.Cancel
                    Continue For
                End If
                target.ListenLoop(False)
                target.ListenMessage(False)
                target.Unbind(_element)
                _componentlist.Remove(target)
                result(i) = UnbindingResult.Success
            Next
            Return result
        End Function

        ''' <summary>
        ''' 根据名称删除组件列表中所有满足要求的组件
        ''' </summary>
        ''' <param name="type">要删除的组件的类型名称</param>
        ''' <remarks></remarks>
        Public Function RemoveAll(type As String) As UnbindingResult()
            Dim targetList = GetAll(type)
            If targetList.Length = 0 Then Return {UnbindingResult.CannotFind}
            Dim result(targetList.Length - 1) As UnbindingResult
            Dim target As Component
            For i = 0 To result.Length - 1
                target = targetList(i)
                If Not target.BeforeUnbinding(_element) Then
                    result(i) = UnbindingResult.Cancel
                    Continue For
                End If
                target.ListenLoop(False)
                target.ListenMessage(False)
                target.Unbind(_element)
                _componentlist.Remove(target)
                result(i) = UnbindingResult.Success
            Next
            Return result
        End Function

        ''' <summary>
        ''' 清除组件列表中的所有组件
        ''' 这个操作当组件本身否决删除操作时会保留那个组件
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Clear()
            Dim i = 0
            Dim target As Component
            While i < _componentlist.Count
                target = _componentlist(i)
                If target.BeforeUnbinding(_element, True) Then
                    target.ListenLoop(False)
                    target.ListenMessage(False)
                    target.Unbind(_element)
                    _componentlist.Remove(target)
                Else
                    i += 1
                End If
            End While
        End Sub

        Friend Sub Dispose() Implements IDisposable.Dispose
            Clear()
            For Each e In _componentlist
                e.ForceUnbinding(_element)
                e.ListenLoop(False)
                e.ListenMessage(False)
            Next
            _componentlist.Clear()
        End Sub
    End Class
End Namespace