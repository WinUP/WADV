Imports System.Windows.Controls

Namespace API

    Public Class ConfigAPI

        Public Shared Sub Init(Optional contentName As String = "")
            Initialiser.LoadEffect()
            WindowAPI.InvokeSync(Sub()
                                     If contentName = "" Then
                                         UiConfig.ImagePanel = WindowAPI.GetRoot(Of Grid)()
                                     Else
                                         UiConfig.ImagePanel = WindowAPI.SearchObject(Of Grid)(contentName)
                                     End If
                                 End Sub)
            MessageAPI.SendSync("[TE]INIT_FINISH")
        End Sub

    End Class

End Namespace