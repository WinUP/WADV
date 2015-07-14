Imports System.Windows

Namespace Component
    ''' <summary>
    ''' 组件基础类
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class Component
        Private ReadOnly _elementList As New List(Of FrameworkElement)

        ''' <summary>
        ''' 当与元素的连接断开时执行的操作
        ''' </summary>
        ''' <param name="sourceElement">针对的元素</param>
        ''' <remarks></remarks>
        Protected Friend Overridable Sub Unbinding(sourceElement As FrameworkElement)

        End Sub

        ''' <summary>
        ''' 当与元素连接时执行的操作
        ''' </summary>
        ''' <param name="sourceElement">针对的元素</param>
        ''' <remarks></remarks>
        Protected Friend Overridable Sub Binding(sourceElement As FrameworkElement)

        End Sub

        ''' <summary>
        ''' 组件的关联对象
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property BindedElements As FrameworkElement()
            Get
                Return _elementList.ToArray
            End Get
        End Property

        ''' <summary>
        ''' 添加一个关联对象
        ''' </summary>
        ''' <param name="element">要设置到的对象</param>
        ''' <remarks></remarks>
        Friend Sub BindElement(element As FrameworkElement)
            If Not _elementList.Contains(element) Then _elementList.Add(element)
        End Sub

        ''' <summary>
        ''' 删除一个关联对象
        ''' </summary>
        ''' <param name="element"></param>
        ''' <remarks></remarks>
        Friend Sub UnbindElement(element As FrameworkElement)
            If _elementList.Contains(element) Then _elementList.Remove(element)
        End Sub
    End Class
End Namespace
