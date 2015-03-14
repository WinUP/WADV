Imports System.Windows.Controls

Namespace API

    Public Class ConfigAPI

        Public Shared Sub Init(Optional contentName As String = "")
            Initialiser.LoadEffect()
            MessageAPI.SendSync("[SPRITE]INIT_FINISH")
        End Sub

    End Class

End Namespace