Imports System.Windows
Imports System.Windows.Controls

''' <summary>
''' 基本退出效果
''' </summary>
''' <remarks></remarks>
Public Class BaseHide
    Protected ReadOnly Choices() As Button
    Protected IsOver As Boolean

    ''' <summary>
    ''' 获得一个新的效果
    ''' </summary>
    ''' <param name="choices">选项内容</param>
    ''' <remarks></remarks>
    Public Sub New(choices() As Button)
        Me.Choices = choices
    End Sub

    ''' <summary>
    ''' 显示效果
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub Render()
        For Each choice In Choices
            choice.Visibility = Visibility.Hidden
        Next
        IsOver = True
        SendFinished()
    End Sub

    ''' <summary>
    ''' 等待效果结束
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Wait()
        While True
            Message.Wait("[CHOICE]HIDE_FINISH")
            If IsOver Then Exit While
        End While
    End Sub

    ''' <summary>
    ''' 标记效果为已完成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SendFinished()
        Message.Send("[CHOICE]HIDE_FINISH")
    End Sub
End Class