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
            MessageAPI.SendSync("[TEXT]AUTOMODE_USER_CLICK")
        ElseIf ModuleConfig.Fast Then
            MessageAPI.SendSync("[TEXT]FASTMODE_USER_CLICK")
        Else
            MessageAPI.SendSync("[TEXT]USER_CLICK")
        End If
    End Sub

End Class
