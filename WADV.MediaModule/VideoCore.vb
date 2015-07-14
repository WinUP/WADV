Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media

''' <summary>
''' 视频播放器
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class VideoCore : Inherits Grid
    Private ReadOnly _videoElement As MediaElement

    ''' <summary>
    ''' 是否可以点击跳过视频
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property CanSkip As Boolean

    ''' <summary>
    ''' 是否使用全黑背景
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property BlackBackground As Boolean
        Get
            If Background IsNot Nothing Then Return True
            Return False
        End Get
        Set(value As Boolean)
            If value Then
                Background = New SolidColorBrush(Color.FromRgb(0, 0, 0))
            Else
                Background = Nothing
            End If
        End Set
    End Property

    ''' <summary>
    ''' 获得一个视频播放器
    ''' </summary>
    ''' <param name="filePath">要播放的文件的路径(Resource目录下)</param>
    ''' <remarks></remarks>
    Public Sub New(filePath As String)
        _videoElement = New MediaElement
        _videoElement.LoadedBehavior = MediaState.Manual
        _videoElement.Source = New Uri(Combine(PathType.Resource, filePath))
        _videoElement.Margin = New Windows.Thickness(0)
        Children.Add(_videoElement)
        AddHandler _videoElement.MediaEnded, AddressOf Video_Ended
        AddHandler _videoElement.MouseLeftButtonDown, AddressOf Video_Click
    End Sub

    ''' <summary>
    ''' 将播放器作为目标元素的子元素显示到界面上
    ''' </summary>
    ''' <param name="target"></param>
    ''' <remarks></remarks>
    Friend Sub AsChild(target As Panel)
        Dispatcher.Invoke(Sub()
                              If Parent IsNot Nothing Then DirectCast(Parent, Panel).Children.Remove(Me)
                              target.Children.Add(Me)
                              Margin = New Windows.Thickness(0)
                          End Sub)
    End Sub

    ''' <summary>
    ''' 播放视频
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Play()
        _videoElement.Play()
    End Sub

    ''' <summary>
    ''' 暂停视频
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Pause()
        _videoElement.Pause()
    End Sub

    Private Sub Video_Click(sender As Object, e As Windows.Input.MouseButtonEventArgs)
        If CanSkip Then
            Send("[MEDIA]VIDEO_SKIP")
            Video_Ended(_videoElement, New Windows.RoutedEventArgs)
        End If
        Send("[MEDIA]VIDEO_CLICK")
    End Sub

    Private Sub Video_Ended(sender As Object, e As MouseButtonEventArgs)
        _videoElement.Visibility = Windows.Visibility.Collapsed
        Send("[MEDIA]VIDEO_END")
        _videoElement.Close()
        If Parent IsNot Nothing Then DirectCast(Parent, Panel).Children.Remove(Me)
    End Sub
End Class
