Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Shapes
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives

Friend NotInheritable Class UiOperation

    ''' <summary>
    ''' 生成新的界面元素
    ''' </summary>
    ''' <param name="type">元素类型</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function GenerateElement(type As String) As FrameworkElement
        Dim tmpElement As FrameworkElement
        Select Case type
            Case "Border"
                tmpElement = New Border
            Case "Button"
                tmpElement = New Button
            Case "Canvas"
                tmpElement = New Canvas
            Case "CheckBox"
                tmpElement = New CheckBox
            Case "ComboBox"
                tmpElement = New ComboBox
            Case "Ellipse"
                tmpElement = New Ellipse
            Case "Expander"
                tmpElement = New Expander
            Case "Grid"
                tmpElement = New Grid
            Case "GroupBox"
                tmpElement = New GroupBox
            Case "Image"
                tmpElement = New Image
            Case "Label"
                tmpElement = New Label
            Case "ListBox"
                tmpElement = New ListBox
            Case "ListView"
                tmpElement = New ListView
            Case "ProgressBar"
                tmpElement = New ProgressBar
            Case "RadioButton"
                tmpElement = New RadioButton
            Case "Rectangle"
                tmpElement = New Rectangle
            Case "ScrollBar"
                tmpElement = New ScrollBar
            Case "Slider"
                tmpElement = New Slider
            Case "TabControl"
                tmpElement = New TabControl
            Case "TextBlock"
                tmpElement = New TextBlock
            Case "TextBox"
                tmpElement = New TextBox
            Case "ViewBox"
                tmpElement = New Viewbox
            Case Else
                tmpElement = Nothing
        End Select
        Return tmpElement
    End Function

    ''' <summary>
    ''' 根据名称获取元素的子元素
    ''' </summary>
    ''' <typeparam name="T">子元素类型</typeparam>
    ''' <param name="obj">父元素</param>
    ''' <param name="name">子元素的名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function GetChildByName(Of T As FrameworkElement)(obj As DependencyObject, name As String) As T
        Dim child As DependencyObject
        Dim grandChild As T
        For i = 0 To VisualTreeHelper.GetChildrenCount(obj) - 1
            child = VisualTreeHelper.GetChild(obj, i)
            If (TypeOf child Is T) AndAlso (name = "" OrElse DirectCast(child, T).Name = name) Then
                Return DirectCast(child, T)
            Else
                grandChild = GetChildByName(Of T)(child, name)
                If grandChild IsNot Nothing Then Return grandChild
            End If
        Next
        Return Nothing
    End Function

End Class
