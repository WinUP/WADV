Imports WADV
Imports System.Windows.Media
Imports System.Windows
Imports System.Windows.Controls

Namespace API

    Public Class GameAPI

        Public Shared Sub Initialise(frame As Integer, width As Integer, height As Integer)
            WindowAPI.SetResizeMode(False)
            WindowAPI.SetBackgroundByHex("#000000")
            WindowAPI.SetIcon("icon.ico")
            WindowAPI.SetWidth(width)
            WindowAPI.SetHeight(height)
            LoopingAPI.SetFrame(frame)
            LoopingAPI.StartMainLoop()
            ResourceAPI.LoadToGame("app_resource.xaml")
            ResourceAPI.LoadToWindow("window_resource.xaml")
        End Sub

        Public Shared Sub ShowLogo(sender As Object, e As EventArgs)
            ImageModule.API.ImageAPI.Show("image\logo.png", "FadeInEffect", 30, "MainGrid")
            System.Threading.Thread.Sleep(1500)
            ImageModule.API.ImageAPI.Show("image\logo.png", "FadeOutEffect", 30, "MainGrid")
        End Sub

        Public Shared Sub ShowMenu(sender As Object, e As EventArgs)
            WindowAPI.ClearContent(WindowAPI.GetGrid)
            WindowAPI.LoadElement(WindowAPI.GetGrid, "menu.xaml")
            ImageModule.API.ImageAPI.Show("image\menu.png", "FadeInEffect", 20, "MainGrid")
            Dim tmpButton As Button
            tmpButton = WindowAPI.GetChildByName(Of Button)(WindowAPI.GetGrid, "StartGame")
            AddHandler tmpButton.Click, AddressOf StartGame
            tmpButton = WindowAPI.GetChildByName(Of Button)(WindowAPI.GetGrid, "LoadGame")
            AddHandler tmpButton.Click, AddressOf LoadGameScreen
            tmpButton = WindowAPI.GetChildByName(Of Button)(WindowAPI.GetGrid, "GameGallery")
            AddHandler tmpButton.Click, AddressOf GameGallery
            tmpButton = WindowAPI.GetChildByName(Of Button)(WindowAPI.GetGrid, "GameSetting")
            AddHandler tmpButton.Click, AddressOf GameSetting
        End Sub

        Public Shared Sub StartGame(sender As Object, e As EventArgs)
            Executer.Executer.canSave = True
            Executer.Executer.canLoad = True
            WindowAPI.ClearContent(WindowAPI.GetGrid)
            WindowAPI.LoadElement(WindowAPI.GetGrid, "text_area.xaml")
            WindowAPI.LoadElement(WindowAPI.GetGrid, "choice_area.xaml")
            TextModule.API.ConfigAPI.SetTextArea(WindowAPI.GetChildByName(Of TextBlock)(WindowAPI.GetGrid, "MainTextArea"))
            TextModule.API.ConfigAPI.SetCharacterArea(WindowAPI.GetChildByName(Of TextBlock)(WindowAPI.GetGrid, "MainCharacter"))
            TextModule.API.ConfigAPI.SetFrameArea(WindowAPI.GetChildByName(Of FrameworkElement)(WindowAPI.GetGrid, "ConversationArea"))
            TextModule.API.ConfigAPI.SetUIVisibility(False)
            TextModule.API.ConfigAPI.RegisterEvent()
            ChoiceModule.API.UIAPI.SetContent(WindowAPI.GetChildByName(Of Panel)(WindowAPI.GetGrid, "MainChoiceArea"))
            ChoiceModule.API.UIAPI.SetStyle("choice.xaml")
            ChoiceModule.API.UIAPI.SetMargin(50)
            ScriptAPI.RunFile("logic\game.lua")
        End Sub

        Public Shared Sub LoadGameScreen(sender As Object, e As EventArgs)
            WindowAPI.ClearContent(WindowAPI.GetGrid)
            WindowAPI.LoadElement(WindowAPI.GetGrid, "load.xaml")
        End Sub

        Public Shared Sub LoadGame(sender As Object, e As EventArgs)

        End Sub

        Public Shared Sub GameGallery(sender As Object, e As EventArgs)
            WindowAPI.ClearContent(WindowAPI.GetGrid)
            WindowAPI.LoadElement(WindowAPI.GetGrid, "gallery.xaml")
        End Sub

        Public Shared Sub GameSetting(sender As Object, e As EventArgs)
            WindowAPI.ClearContent(WindowAPI.GetGrid)
            WindowAPI.LoadElement(WindowAPI.GetGrid, "setting.xaml")
        End Sub

        Public Shared Sub ExitGame()
            WindowAPI.GetDispatcher.Invoke(Sub() AppCore.API.WindowAPI.GetWindow.Close())
        End Sub

        Public Shared Sub Execute(fileName As String, startLine As Integer, endLine As Integer)
            Executer.Executer.ExecuteData(fileName, startLine, endLine)
        End Sub

        Public Shared Sub SetLoad(value As Boolean)
            SaveLoad.Load.CanLoad = value
            Executer.Executer.canLoad = value
        End Sub

        Public Shared Sub SetSave(value As Boolean)
            SaveLoad.Save.CanSave = value
            Executer.Executer.canSave = value
        End Sub

        Public Shared Sub Jump(name As String)
            Executer.Executer.JumpToMark(name)
        End Sub

    End Class




End Namespace
