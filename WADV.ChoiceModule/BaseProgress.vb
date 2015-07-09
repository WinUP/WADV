Imports System.Windows
Imports System.Windows.Controls

''' <summary>
''' 基本等待效果
''' </summary>
''' <remarks></remarks>
Public Class BaseProgress
    Protected ReadOnly Choices() As Button
    Protected WaitFrame As Integer
    Private _answer As String

    ''' <summary>
    ''' 获得一个新的效果
    ''' </summary>
    ''' <param name="choices">选项内容</param>
    ''' <param name="waitFrame">等待时间(-1为无限长)</param>
    ''' <remarks></remarks>
    Public Sub New(choices() As Button, waitFrame As Integer)
        Me.Choices = choices
        Me.WaitFrame = waitFrame
        _answer = ""
        choices(0).Dispatcher.Invoke(Sub() choices.ToList.ForEach(Sub(e) AddHandler e.Click, AddressOf Button_Click))
    End Sub

    ''' <summary>
    ''' 更新等待状态
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function Logic() As Boolean
        If WaitFrame > 0 Then WaitFrame -= 1
        If WaitFrame = 0 Then
            Message.Send("[CHOICE]USER_OVERTIME")
            Return False
        End If
        If _answer <> "" Then Return False
        Return True
    End Function

    ''' <summary>
    ''' 更新显示状态
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub Render()
    End Sub

    ''' <summary>
    ''' 获得用户选择的选项
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAnswer() As String
        Return _answer
    End Function

    ''' <summary>
    ''' 用户的按钮点击事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        _answer = DirectCast(sender, Button).Content
        Message.Send("[CHOICE]USER_CLICKED")
    End Sub
End Class