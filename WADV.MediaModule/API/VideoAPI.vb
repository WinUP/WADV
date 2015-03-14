Namespace API

    ''' <summary>
    ''' 视频API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class VideoAPI

        ''' <summary>
        ''' 播放视频(若已经有视频在播放则会暂停之前的)
        ''' </summary>
        ''' <param name="fileName">视频文件路径(Resource目录下)</param>
        ''' <param name="clickSkip">是否允许单击跳过</param>
        ''' <remarks></remarks>
        Public Shared Sub PlayVideo(fileName As String, clickSkip As Boolean)
            VideoConfig.ClickToSkip = clickSkip
            VideoConfig.GetNewContent(fileName)
        End Sub

        ''' <summary>
        ''' 播放视频并等待其播放完(若已经有视频在播放则会暂停之前的)
        ''' </summary>
        ''' <param name="fileName">视频文件路径(Resource目录下)</param>
        ''' <param name="clickSkip">是否允许单击跳过</param>
        ''' <remarks></remarks>
        Public Shared Sub PlayVideoSync(fileName As String, clickSkip As Boolean)
            PlayVideo(fileName, clickSkip)
            While True
                MessageAPI.WaitSync("[MEDIA]VIDEO_END")
                If VideoConfig.IsPlayFinished Then Exit While
            End While
        End Sub

        ''' <summary>
        ''' 停止视频
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopVideo()
            If VideoConfig.VideoContent IsNot Nothing Then VideoConfig.VideoContent.Dispatcher.BeginInvoke(Sub() VideoConfig.VideoContent.Close())
        End Sub

        ''' <summary>
        ''' 暂停视频
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub PauseVideo()
            If VideoConfig.VideoContent IsNot Nothing Then VideoConfig.VideoContent.Dispatcher.BeginInvoke(
                Sub()
                    VideoConfig.VideoContent.Pause()
                    MessageAPI.SendSync("[MEDIA]VIDEO_PAUSE")
                End Sub)
        End Sub

        ''' <summary>
        ''' 继续播放已暂停的视频
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ResumeVideo()
            If VideoConfig.VideoContent IsNot Nothing Then VideoConfig.VideoContent.Dispatcher.BeginInvoke(
                Sub()
                    VideoConfig.VideoContent.Play()
                    MessageAPI.SendSync("[MEDIA]VIDEO_RESUME")
                End Sub)
        End Sub

    End Class

End Namespace