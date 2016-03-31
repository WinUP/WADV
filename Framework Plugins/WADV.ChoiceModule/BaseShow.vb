Imports System.Windows.Controls

''' <summary>
''' 基本显示效果
''' </summary>
''' <remarks></remarks>
Public Class BaseShow
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
        IsOver = True
        SendMessage()
    End Sub

    ''' <summary>
    ''' 等待效果完成
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub Wait()
        While True
            Message.Wait("[CHOICE]SHOW_FINISH")
            If IsOver Then Exit While
        End While
    End Sub

    ''' <summary>
    ''' 标记效果为已完成
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SendMessage()
        Message.Send("[CHOICE]SHOW_FINISH")
    End Sub
End Class