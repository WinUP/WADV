Imports System.Windows.Input
Imports WADV.TextModule.Config

Friend NotInheritable Class Events

    Friend Shared Sub Ctrl_Down(sender As Object, e As KeyEventArgs)
        If e.Key = Key.LeftCtrl Then ModuleConfig.Fast = True
    End Sub

    Friend Shared Sub Ctrl_Up(sender As Object, e As KeyEventArgs)
        If e.Key = Key.LeftCtrl Then ModuleConfig.Fast = False
    End Sub

    Friend Shared Sub TextArea_Click(sender As Object, e As MouseButtonEventArgs)
        ModuleConfig.Clicked = True
        If ModuleConfig.Auto Then
            Message.Send("[TEXT]AUTOMODE_USER_CLICK")
        ElseIf ModuleConfig.Fast Then
            Message.Send("[TEXT]FASTMODE_USER_CLICK")
        Else
            Message.Send("[TEXT]USER_CLICK")
        End If
    End Sub

End Class
