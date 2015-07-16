Imports System.Windows
Imports WADV.Core.Component

Namespace API
    ''' <summary>
    ''' 组件API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Component
        ''' <summary>
        ''' 从元素获取组件列表
        ''' 当目标元素没有关联组件列表时，元素的Tag属性会被清除，新的组件列表会被建立
        ''' </summary>
        ''' <param name="target">目标元素</param>
        ''' <returns></returns>
        ''' <remarks>由于组件列表挂载在元素的Tag属性上，请不要随意修改Tag属性</remarks>
        Public Function From(target As FrameworkElement) As ComponentList
            Dim tag = TryCast(InvokeFunction(Function(e As FrameworkElement) e.Tag, target), ComponentList)
            If tag IsNot Nothing Then
                Dim result = New ComponentList(target)
                Invoke(Sub() target.Tag = result)
                Return result
            Else
                Return tag
            End If
        End Function

        ''' <summary>
        ''' 删除元素的组件列表
        ''' </summary>
        ''' <param name="target">目标元素</param>
        ''' <remarks></remarks>
        Public Sub Remove(target As FrameworkElement)
            Dim tag = TryCast(InvokeFunction(Function(e As FrameworkElement) e.Tag, target), ComponentList)
            If tag IsNot Nothing Then
                tag.Dispose()
                Invoke(Sub() target.Tag = Nothing)
            End If
        End Sub
    End Module
End Namespace
