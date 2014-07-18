Imports WADV
Imports System.Windows.Media
Imports System.Windows
Imports System.Windows.Controls

Namespace API

    Public Class GameAPI

        Public Shared Sub Initialise()
            TextModule.API.ConfigAPI.SetUITextArea(AppCore.API.ResourceAPI.GetChildByName(Of TextBlock)(AppCore.API.WindowAPI.GetMainGrid, "MainTextArea"))
            TextModule.API.ConfigAPI.RegisterEvent()
        End Sub

    End Class

End Namespace
