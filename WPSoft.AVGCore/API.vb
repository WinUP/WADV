Imports WADV
Imports System.Windows.Media
Imports System.Windows
Imports System.Windows.Controls

Namespace API

    Public Class GameAPI

        Public Shared Sub Initialise(frame As Integer)
            TextModule.API.ConfigAPI.SetUITextArea(AppCore.API.ResourceAPI.GetChildByName(Of TextBlock)(AppCore.API.WindowAPI.GetMainGrid, "MainTextArea"))
            TextModule.API.ConfigAPI.SetCharacterArea(AppCore.API.ResourceAPI.GetChildByName(Of TextBlock)(AppCore.API.WindowAPI.GetMainGrid, "MainCharacter"))
            TextModule.API.ConfigAPI.RegisterEvent()
            ChoiceModule.API.UIAPI.SetChoiceContent(AppCore.API.ResourceAPI.GetChildByName(Of Panel)(AppCore.API.WindowAPI.GetMainGrid, "MainChoiceArea"))
            ChoiceModule.API.UIAPI.SetChoiceStyle("choice.xaml")
            AppCore.API.LoopAPI.ChangeFrame(frame)
            AppCore.API.LoopAPI.InitialiseLoop()
            AppCore.API.LoopAPI.StartMainLoop()
        End Sub

        Public Shared Sub ExitGame()
            AppCore.API.WindowAPI.GetWindowDispatcher.Invoke(Sub() WADV.Application.Current.Shutdown())
        End Sub

    End Class

End Namespace
