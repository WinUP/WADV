Imports System.Windows.Input

Friend NotInheritable Class TextCore

    Friend Shared Sub Ctrl_Down(sender As Object, e As KeyEventArgs)
        If e.Key = Key.LeftCtrl Then ModuleConfig.Fast = True
    End Sub

    Friend Shared Sub Ctrl_Up(sender As Object, e As KeyEventArgs)
        If e.Key = Key.LeftCtrl Then ModuleConfig.Fast = False
    End Sub

    Friend Shared Sub TextArea_Click(sender As Object, e As MouseButtonEventArgs)
        ModuleConfig.Clicked = True
        MessageAPI.SendSync("[TEXT]USER_CLICK")
    End Sub

End Class
