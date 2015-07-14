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
            If target.Tag Is Nothing Then
                target.Tag = New ComponentList(target)
            ElseIf Not TypeOf target.Tag Is ComponentList Then
                target.Tag = New ComponentList(target)
            End If
            Return DirectCast(target.Tag, ComponentList)
        End Function
    End Module
End Namespace
