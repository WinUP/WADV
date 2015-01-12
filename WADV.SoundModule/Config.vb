Imports System.Windows.Media
Imports System.Xml
Imports WADV.MediaModule.AudioCore
Imports System.Windows.Controls

Namespace Config

    ''' <summary>
    ''' 声音设置类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SoundConfig

        Protected Friend Shared LastReadingID As Integer = -1

        ''' <summary>
        ''' 获取或设置背景音量
        ''' </summary>
        ''' <value>音量</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Background As Integer

        ''' <summary>
        ''' 获取或设置对话音量
        ''' </summary>
        ''' <value>音量</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Reading As Integer

        ''' <summary>
        ''' 获取或设置效果音量
        ''' </summary>
        ''' <value>音量</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Effect As Integer

    End Class

    ''' <summary>
    ''' 视频设置类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class VideoConfig
        Protected Friend Shared VideoContent As MediaElement = Nothing
        Protected Friend Shared ClickToSkip As Boolean = False
        Protected Friend Shared IsPlayFinished As Boolean = False

        ''' <summary>
        ''' 播放新的视频
        ''' </summary>
        ''' <param name="fileName">视频文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub GetNewContent(fileName As String)
            If VideoContent IsNot Nothing Then
                VideoContent.Dispatcher.Invoke(Sub()
                                                   WindowAPI.GetRoot(Of Grid).Children.Remove(VideoContent)
                                                   RemoveHandler VideoContent.MediaEnded, AddressOf Video_Ended
                                                   RemoveHandler VideoContent.MouseLeftButtonDown, AddressOf Video_Click
                                                   VideoContent.Close()
                                                   VideoContent = Nothing
                                               End Sub)
            End If
            WindowAPI.GetWindow.Dispatcher.Invoke(Sub()
                                                      Dim content = WindowAPI.GetRoot(Of Grid)()
                                                      VideoContent = New MediaElement
                                                      VideoContent.SetValue(Panel.ZIndexProperty, 10)
                                                      VideoContent.LoadedBehavior = MediaState.Manual
                                                      VideoContent.Source = New Uri(PathAPI.GetPath(PathAPI.Resource, fileName))
                                                      VideoContent.Height = content.Height
                                                      VideoContent.Width = content.Width
                                                      VideoContent.Margin = New Windows.Thickness(0)
                                                      AddHandler VideoContent.MediaEnded, AddressOf Video_Ended
                                                      AddHandler VideoContent.MouseLeftButtonDown, AddressOf Video_Click
                                                      content.Children.Add(VideoContent)
                                                      VideoContent.Play()
                                                  End Sub)
            IsPlayFinished = False
            MessageAPI.SendSync("MEDIA_VIDEO_PLAY")
        End Sub

        Protected Shared Sub Video_Click(sender As Object, e As Windows.Input.MouseButtonEventArgs)
            If ClickToSkip Then
                VideoContent.Close()
                Video_Ended(VideoContent, New Windows.RoutedEventArgs)
                MessageAPI.SendSync("MEDIA_VIDEO_SKIP")
            End If
            MessageAPI.SendSync("MEDIA_VIDEO_CLICK")
        End Sub

        Protected Shared Sub Video_Ended(sender As Object, e As Windows.RoutedEventArgs)
            VideoContent.Visibility = Windows.Visibility.Collapsed
            IsPlayFinished = True
            MessageAPI.SendSync("MEDIA_VIDEO_END")
        End Sub

    End Class

End Namespace