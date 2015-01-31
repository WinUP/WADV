Imports WADV
Imports System.Windows
Imports System.Windows.Controls

Namespace API

    Public Class GameAPI

        Public Shared Sub ShowMenu(sender As Object, e As EventArgs)
            WindowAPI.LoadPageAsync("menu.xaml")
            MessageAPI.WaitSync("WINDOW_PAGE_CHANGE")
            CGModule.API.ImageAPI.Show("image\menu.png", "FadeIn", 60, "MenuMainGrid")
            Dim tmpButton As Button
            tmpButton = WindowAPI.GetChildByName(Of Button)(WindowAPI.GetWindow, "StartGame")
            AddHandler tmpButton.Click, AddressOf StartGame
            tmpButton = WindowAPI.GetChildByName(Of Button)(WindowAPI.GetWindow, "LoadGame")
            AddHandler tmpButton.Click, AddressOf LoadGameScreen
            tmpButton = WindowAPI.GetChildByName(Of Button)(WindowAPI.GetWindow, "GameGallery")
            AddHandler tmpButton.Click, AddressOf GameGallery
            tmpButton = WindowAPI.GetChildByName(Of Button)(WindowAPI.GetWindow, "GameSetting")
            AddHandler tmpButton.Click, AddressOf GameSetting
        End Sub

        Public Shared Sub StartGame(sender As Object, e As EventArgs)
            WindowAPI.LoadPageAsync("game.xaml")
            MessageAPI.WaitSync("WINDOW_PAGE_CHANGE")
            TextModule.API.ConfigAPI.SetTextArea(WindowAPI.SearchObject(Of TextBlock)("MainTextArea"))
            TextModule.API.ConfigAPI.SetSpeakerArea(WindowAPI.SearchObject(Of TextBlock)("MainCharacter"))
            TextModule.API.ConfigAPI.SetMainArea(WindowAPI.SearchObject(Of FrameworkElement)("ConversationArea"))
            TextModule.API.ConfigAPI.SetVisibility(False)
            TextModule.API.ConfigAPI.RegisterEvent()
            ChoiceModule.API.UIAPI.SetContent(WindowAPI.SearchObject(Of Panel)("MainChoiceArea"))
            ChoiceModule.API.UIAPI.SetStyle("choice.xaml")
            ChoiceModule.API.UIAPI.SetMargin(50)
            ScriptAPI.RunFileAsync("logic\game.lua")
        End Sub

        Public Shared Sub LoadGameScreen(sender As Object, e As EventArgs)
            WindowAPI.LoadPageAsync("load.xaml")
        End Sub

        Public Shared Sub LoadGame(sender As Object, e As EventArgs)

        End Sub

        Public Shared Sub GameGallery(sender As Object, e As EventArgs)
            WindowAPI.LoadPageAsync("gallery.xaml")
        End Sub

        Public Shared Sub GameSetting(sender As Object, e As EventArgs)
            WindowAPI.LoadPageAsync("setting.xaml")
        End Sub

        Public Shared Sub ExitGame()
            WindowAPI.GetDispatcher.Invoke(Sub() WindowAPI.GetWindow.Close())
        End Sub

        Public Shared Sub SaveGame()
            Dim fileName = "savedata\" & DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")
            WindowAPI.GetDispatcher.Invoke(Sub() WindowAPI.SaveImage(fileName & ".jpg"))
            Serializer.SaveToFile(fileName & ".vm")
        End Sub

    End Class




End Namespace
