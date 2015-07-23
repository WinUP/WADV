Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Shapes
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives

''' <summary>
''' UI操作类
''' </summary>
''' <remarks>公开API位于Window</remarks>
Friend NotInheritable Class UiOperation

    ''' <summary>
    ''' 生成新的界面元素
    ''' </summary>
    ''' <param name="type">元素类型</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function GenerateElement(type As ElementType) As FrameworkElement
        Dim tmpElement As FrameworkElement
        Select Case type
            Case ElementType.Border
                tmpElement = New Border
            Case ElementType.Button
                tmpElement = New Button
            Case ElementType.Canvas
                tmpElement = New Canvas
            Case ElementType.CheckBox
                tmpElement = New CheckBox
            Case ElementType.ComboBox
                tmpElement = New ComboBox
            Case ElementType.Ellipse
                tmpElement = New Ellipse
            Case ElementType.Expander
                tmpElement = New Expander
            Case ElementType.Grid
                tmpElement = New Grid
            Case ElementType.GroupBox
                tmpElement = New GroupBox
            Case ElementType.Image
                tmpElement = New Image
            Case ElementType.Label
                tmpElement = New Label
            Case ElementType.ListBox
                tmpElement = New ListBox
            Case ElementType.ListView
                tmpElement = New ListView
            Case ElementType.ProgressBar
                tmpElement = New ProgressBar
            Case ElementType.RadioButton
                tmpElement = New RadioButton
            Case ElementType.Rectangle
                tmpElement = New Rectangle
            Case ElementType.RichTextBox
                tmpElement = New RichTextBox
            Case ElementType.ScrollBar
                tmpElement = New ScrollBar
            Case ElementType.Slider
                tmpElement = New Slider
            Case ElementType.TabControl
                tmpElement = New TabControl
            Case ElementType.TextBlock
                tmpElement = New TextBlock
            Case ElementType.TextBox
                tmpElement = New TextBox
            Case ElementType.ViewBox
                tmpElement = New Viewbox
            Case ElementType.Viewport3D
                tmpElement = New RichTextBox
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
                Return child
            Else
                grandChild = GetChildByName(Of T)(child, name)
                If grandChild IsNot Nothing Then Return grandChild
            End If
        Next
        Return Nothing
    End Function
End Class
