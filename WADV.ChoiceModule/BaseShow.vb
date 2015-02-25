Imports System.Windows.Controls

Public Class BaseShow : Implements IShowEffect
    Protected ReadOnly Choices() As Button
    Protected IsOver As Boolean

    Public Sub New(choices() As Button)
        Me.Choices = choices
    End Sub

    Public Overridable Sub Render() Implements IShowEffect.Render
        IsOver = True
        SendMessage()
    End Sub

    Public Overridable Sub Wait() Implements IShowEffect.Wait
        While True
            MessageAPI.WaitSync("[CHOICE]SHOW_FINISH")
            If IsOver Then Exit While
        End While
    End Sub

    Protected Sub SendMessage()
        MessageAPI.SendSync("[CHOICE]SHOW_FINISH")
    End Sub

End Class