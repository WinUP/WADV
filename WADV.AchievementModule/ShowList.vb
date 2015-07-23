Imports System.Threading
Imports System.Windows
Imports System.Windows.Markup
Imports System.Windows.Controls
Imports System.Windows.Threading
Imports System.Collections.Concurrent
Imports System.Windows.Media.Animation

Friend NotInheritable Class ShowList
    Private Shared _mainStoryBoard As Storyboard
    Private Shared _showingWindow As Border
    Private Shared _list As ConcurrentQueue(Of Achievement)
    Private Shared _showThread As Thread
    Private Shared _dispatcher As Dispatcher
    Private Shared _isShowing As Boolean

    Friend Shared ReadOnly Property IsShowing As Boolean
        Get
            Return _isShowing
        End Get
    End Property

    Friend Shared Sub Initialise()
        _list = New ConcurrentQueue(Of Achievement)
        _dispatcher = Core.API.Window.Dispatcher
        _dispatcher.Invoke(New Action(AddressOf SetStoryboard))
        _showThread = New Thread(AddressOf ShowContent)
        _showThread.Name = "成就显示线程"
        _showThread.IsBackground = True
        _showThread.Priority = ThreadPriority.BelowNormal
    End Sub

    Private Shared Sub SetStoryboard()
        _mainStoryBoard = New Storyboard
        Dim ease As EasingFunctionBase = New QuarticEase
        ease.EasingMode = EasingMode.EaseOut
        Dim fade As New DoubleAnimation(1.0, New Duration(TimeSpan.FromMilliseconds(600)))
        fade.EasingFunction = ease
        _mainStoryBoard.Children.Add(fade)
        Storyboard.SetTargetProperty(fade, New PropertyPath(Border.OpacityProperty))
        fade = New DoubleAnimation(0.0, New Duration(TimeSpan.FromMilliseconds(600)))
        fade.EasingFunction = ease
        fade.BeginTime = TimeSpan.FromMilliseconds(3600)
        _mainStoryBoard.Children.Add(fade)
        Storyboard.SetTargetProperty(fade, New PropertyPath(Border.OpacityProperty))
        AddHandler _mainStoryBoard.Completed, AddressOf Storyboard_Complete
    End Sub

    Private Shared Sub Storyboard_Complete(sender As Object, e As EventArgs)
        Core.API.Window.Root(Of Grid).Children.Remove(_showingWindow)
        Message.Send("[ACHIEVE]SHOW_FINISH")
    End Sub

    Private Shared Sub ShowContent()
        _isShowing = True
        While _list.Count > 0
            Dim target As Achievement = Nothing
            If Not _list.TryDequeue(target) Then Throw New Exception("从等待队列中获取成就失败")
            _dispatcher.Invoke(New Action(Of Achievement)(AddressOf ShowWindow), target)
            Message.Wait("[ACHIEVE]SHOW_FINISH")
        End While
        _isShowing = False
    End Sub

    Private Shared Sub ShowWindow(target As Achievement)
        _showingWindow = XamlReader.Parse(ModuleConfig.WindowStyle)
        _showingWindow.Opacity = 0.0
        Core.API.Window.Root(Of Grid).Children.Add(_showingWindow)
        Core.API.Window.GetChild(Of TextBlock)(_showingWindow, "AchievementTitle").Text = target.Name
        _showingWindow.BeginStoryboard(_mainStoryBoard)
    End Sub

    Friend Shared Sub Add(target As Achievement)
        If Not _list.Contains(target) Then
            _list.Enqueue(target)
            If Not _showThread.IsAlive Then _showThread.Start()
        End If
    End Sub
End Class
