Imports System.Threading
Imports System.Windows.Controls
Imports System.Collections.Concurrent
Imports System.Windows.Media.Animation
Imports WADV.Core.Render

Friend NotInheritable Class ShowList
    Private Shared _animation As Storyboard
    Private Shared _list As ConcurrentQueue(Of Achievement)
    Private Shared _showThread As Thread
    Private Shared _isShowing As Boolean
    Private Shared ReadOnly Window As WindowBase
    Private Shared _target As Sprite
    Private Shared _scene As Scene = Nothing

    Shared Sub New()
        Window = Game.Window
    End Sub

    Friend Shared ReadOnly Property IsShowing As Boolean
        Get
            Return _isShowing
        End Get
    End Property

    Friend Shared Sub Run()
        _list = New ConcurrentQueue(Of Achievement)
        _animation = Config.Storyboard
        AddHandler _animation.Completed, AddressOf Storyboard_Complete
        _showThread = New Thread(AddressOf ShowThread_Start)
        _showThread.Name = "[WADV.WPF.AchievementModule]成就显示线程"
        _showThread.IsBackground = True
        _showThread.Priority = ThreadPriority.BelowNormal
        NewSprite()
    End Sub

    Private Shared Sub NewSprite()
        _scene = Window.SceneNow
        _target = _scene.AddSprite("[WADV.WPF.AchievementModule]成就显示精灵")
        Dim xamlElement = New Renderer.XamlElement(Config.Element)
        _target.Components.Add(xamlElement)
    End Sub

    Private Shared Sub Storyboard_Complete(sender As Object, e As EventArgs)
        DirectCast(_scene.Content, Panel).Children.Remove(Config.Element)
        Message.Send("[ACHIEVEMENT]SHOW_FINISH")
    End Sub

    Private Shared Sub ShowThread_Start()
        _isShowing = True
        While _list.Count > 0
            If Window.SceneNow IsNot _scene Then
                _target.Components.Remove(Of Renderer.XamlElement)()
                _scene.RemoveSprite(_target)
                NewSprite()
            End If
            Dim target As Achievement = Nothing
            If Not _list.TryDequeue(target) Then Throw New Exception("从等待队列中获取成就失败")
            Window.Run(New Action(Of Achievement)(AddressOf ShowAchievement), False, target)
            Message.Wait("[ACHIEVEMENT]SHOW_FINISH")
        End While
        _isShowing = False
    End Sub

    Private Shared Sub ShowAchievement(target As Achievement)
        DirectCast(Config.Element.FindName("AchievementTitle"), TextBlock).Text = target.Name
        Config.Element.BeginStoryboard(_animation)
    End Sub

    Friend Shared Sub Add(target As Achievement)
        If Not _list.Contains(target) Then
            _list.Enqueue(target)
            If Not _showThread.IsAlive Then _showThread.Start()
        End If
    End Sub
End Class
