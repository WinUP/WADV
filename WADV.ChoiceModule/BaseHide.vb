Imports System.Windows
Imports System.Windows.Controls

Public Class BaseHide : Implements IHideEffect
    Protected ReadOnly Choices() As Button
    Protected IsOver As Boolean

    Public Sub New(choices() As Button)
        Me.Choices = choices
    End Sub

    Public Overridable Sub Render() Implements IHideEffect.Render
        For Each choice In Choices
            choice.Visibility = Visibility.Hidden
        Next
        IsOver = True
        SendMessage()
    End Sub

    Public Overridable Sub Wait() Implements IHideEffect.Wait
        While True
            MessageAPI.WaitSync("[CHOICE]HIDE_FINISH")
            If IsOver Then Exit While
        End While
    End Sub

    Protected Sub SendMessage()
        MessageAPI.SendSync("[CHOICE]HIDE_FINISH")
    End Sub

End Class