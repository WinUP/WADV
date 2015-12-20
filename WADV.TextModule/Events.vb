Imports System.Windows.Input
Imports WADV.TextModule.Config

Friend NotInheritable Class Events

    Friend Shared Sub Ctrl_Down(sender As Object, e As KeyEventArgs)
        If e.Key = Key.LeftCtrl Then ModuleConfig.FastMode = True
    End Sub

    Friend Shared Sub Ctrl_Up(sender As Object, e As KeyEventArgs)
        If e.Key = Key.LeftCtrl Then ModuleConfig.FastMode = False
    End Sub

    Friend Shared Sub TextArea_Click(sender As Object, e As MouseButtonEventArgs)
        ModuleConfig.ClickedSkip = True
        If ModuleConfig.AutoMode Then
            Message.Send("[TEXT]AUTOMODE_USER_CLICK")
        ElseIf ModuleConfig.FastMode Then
            Message.Send("[TEXT]FASTMODE_USER_CLICK")
        Else
            Message.Send("[TEXT]USER_CLICK")
        End If
    End Sub

End Class
