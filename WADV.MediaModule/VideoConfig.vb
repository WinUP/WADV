﻿Imports System.Windows.Controls

''' <summary>
''' 视频设置类
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class VideoConfig
    Friend Shared VideoContent As MediaElement = Nothing
    Friend Shared ClickToSkip As Boolean = False
    Friend Shared IsPlayFinished As Boolean = False

    ''' <summary>
    ''' 播放新的视频
    ''' </summary>
    ''' <param name="fileName">视频文件路径(Resource目录下)</param>
    ''' <remarks></remarks>
    Friend Shared Sub GetNewContent(fileName As String)
        If VideoContent IsNot Nothing Then
            VideoContent.Dispatcher.Invoke(Sub()
                                               Window.Root(Of Grid).Children.Remove(VideoContent)
                                               RemoveHandler VideoContent.MediaEnded, AddressOf Video_Ended
                                               RemoveHandler VideoContent.MouseLeftButtonDown, AddressOf Video_Click
                                               VideoContent.Close()
                                               VideoContent = Nothing
                                           End Sub)
        End If
        Window.Invoke(Sub()
                          Dim content = Window.Root(Of Grid)()
                          VideoContent = New MediaElement
                          VideoContent.SetValue(Panel.ZIndexProperty, 10)
                          VideoContent.LoadedBehavior = MediaState.Manual
                          VideoContent.Source = New Uri(Path.Combine(PathType.Resource, fileName))
                          VideoContent.Height = content.Height
                          VideoContent.Width = content.Width
                          VideoContent.Margin = New Windows.Thickness(0)
                          AddHandler VideoContent.MediaEnded, AddressOf Video_Ended
                          AddHandler VideoContent.MouseLeftButtonDown, AddressOf Video_Click
                          content.Children.Add(VideoContent)
                          VideoContent.Play()
                      End Sub)
        IsPlayFinished = False
        Message.Send("[MEDIA]VIDEO_PLAY")
    End Sub

    Private Shared Sub Video_Click(sender As Object, e As Windows.Input.MouseButtonEventArgs)
        If ClickToSkip Then
            Video_Ended(VideoContent, New Windows.RoutedEventArgs)
            Message.Send("[MEDIA]VIDEO_SKIP")
        End If
        Message.Send("[MEDIA]VIDEO_CLICK")
    End Sub

    Private Shared Sub Video_Ended(sender As Object, e As Windows.RoutedEventArgs)
        VideoContent.Visibility = Windows.Visibility.Collapsed
        IsPlayFinished = True
        Message.Send("[MEDIA]VIDEO_END")
        VideoContent.Close()
    End Sub

End Class