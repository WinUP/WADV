Imports WADV
Imports System.Windows.Media
Imports System.Windows
Imports System.Windows.Controls

Namespace API

    Public Class GameAPI

        Public Shared Sub Initialise(frame As Integer)
            TextModule.API.ConfigAPI.SetUITextArea(WindowAPI.GetChildByName(Of TextBlock)(WindowAPI.GetGrid, "MainTextArea"))
            TextModule.API.ConfigAPI.SetCharacterArea(WindowAPI.GetChildByName(Of TextBlock)(WindowAPI.GetGrid, "MainCharacter"))
            TextModule.API.ConfigAPI.RegisterEvent()
            ChoiceModule.API.UIAPI.SetContent(WindowAPI.GetChildByName(Of Panel)(WindowAPI.GetGrid, "MainChoiceArea"))
            ChoiceModule.API.UIAPI.SetStyle("choice.xaml")
            LoopingAPI.SetFrame(frame)
            LoopingAPI.StartMainLoop()
        End Sub

        Public Shared Sub ExitGame()
            WindowAPI.GetDispatcher.Invoke(Sub() AppCore.API.WindowAPI.GetWindow.Close())
        End Sub

    End Class

End Namespace
