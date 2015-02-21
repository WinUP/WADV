Imports System.Windows.Input

Namespace Core

    Public Class TextCore

        Public Shared Sub Ctrl_Down(sender As Object, e As KeyEventArgs)
            If e.Key = Key.LeftCtrl Then Config.ModuleConfig.Fast = True
        End Sub

        Public Shared Sub Ctrl_Up(sender As Object, e As KeyEventArgs)
            If e.Key = Key.LeftCtrl Then Config.ModuleConfig.Fast = False
        End Sub

        Public Shared Sub TextArea_Click(sender As Object, e As MouseButtonEventArgs)
            Config.ModuleConfig.Clicked = True
            MessageAPI.SendSync("[TEXT]USER_CLICK")
        End Sub

    End Class

End Namespace
