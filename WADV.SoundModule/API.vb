Imports WADV.MediaModule.Config
Imports WADV.MediaModule.AudioCore

Namespace API

    ''' <summary>
    ''' 设置API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConfigAPI

        ''' <summary>
        ''' 初始化模块
        ''' </summary>
        ''' <param name="bgmVolume">默认BGM音量(-5000~5000)</param>
        ''' <param name="readingVolume">默认语音音量(-5000~5000)</param>
        ''' <param name="effectVolume">默认效果音音量(-5000~5000)</param>
        ''' <remarks></remarks>
        Public Shared Sub Init(Optional bgmVolume As Integer = 0, Optional readingVolume As Integer = 0, Optional effectVolume As Integer = 0)
            SetBackgroundVolume(bgmVolume)
            SetReadingVolume(readingVolume)
            SetEffectVolume(effectVolume)
            MessageAPI.SendSync("[MEDIA]INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 启动监听器
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StartReceiver()
            If Not ModuleConfig.LoopOn Then
                ModuleConfig.LoopOn = True
                LoopAPI.AddLoopSync(New PluginInterface.LoopReceiver)
            End If
        End Sub

        ''' <summary>
        ''' 停止监听器
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopReceiver()
            ModuleConfig.LoopOn = False
        End Sub

        ''' <summary>
        ''' 获取背景音乐音量(-5000~5000)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBackgroundVolume() As Double
            Return SoundConfig.Background + 5000
        End Function

        ''' <summary>
        ''' 获取对话音量(-5000~5000)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetReadingVolume() As Double
            Return SoundConfig.Reading + 5000
        End Function

        ''' <summary>
        ''' 获取效果音量(-5000~5000)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEffectVolume() As Double
            Return SoundConfig.Effect + 5000
        End Function

        ''' <summary>
        ''' 设置背景音乐音量(-5000~5000)
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetBackgroundVolume(value As Double)
            SoundConfig.Background = value - 5000
            PlayerList.ChangeVolume(SoundType.Background, SoundConfig.Background)
            MessageAPI.SendSync("[MEDIA]BGM_VOLUME_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置对话音量(-5000~5000)
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetReadingVolume(value As Double)
            SoundConfig.Reading = value - 5000
            PlayerList.ChangeVolume(SoundType.Reading, SoundConfig.Reading)
            MessageAPI.SendSync("[MEDIA]READ_VOLUME_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置效果音量(-5000~5000)
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetEffectVolume(value As Double)
            SoundConfig.Effect = value - 5000
            PlayerList.ChangeVolume(SoundType.Effect, SoundConfig.Effect)
            MessageAPI.SendSync("[MEDIA]EFFECT_VOLUME_CHANGE")
        End Sub

    End Class

    ''' <summary>
    ''' 声音API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SoundAPI

        ''' <summary>
        ''' 播放声音
        ''' </summary>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <param name="type">声音类型</param>
        ''' <param name="cycle">是否循环</param>
        ''' <param name="count">循环次数(永久循环为-1)</param>
        ''' <remarks></remarks>
        Public Shared Function Play(fileName As String, type As SoundType, cycle As Boolean, count As Integer) As Integer
            Dim tmpPlayer = PlayerList.AddPlayer(fileName, type, cycle, count)
            If tmpPlayer Is Nothing Then Return -1
            tmpPlayer.Play()
            Return tmpPlayer.ID
        End Function

        ''' <summary>
        ''' 播放背景音乐
        ''' </summary>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Public Shared Function PlayBGM(fileName As String) As Integer
            MessageAPI.SendSync("[MEDIA]BGM_PLAY_STANDBY")
            Dim id = Play(fileName, SoundType.Background, True, -1)
            SoundConfig.LastBGMID = id
            Return id
        End Function

        ''' <summary>
        ''' 播放对话
        ''' </summary>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Public Shared Function PlayReading(fileName As String) As Integer
            MessageAPI.SendSync("[MEDIA]READ_PLAY_STANDBY")
            Dim id = Play(fileName, SoundType.Reading, False, 0)
            SoundConfig.LastReadingID = id
            Return id
        End Function

        ''' <summary>
        ''' 播放效果音
        ''' </summary>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Public Shared Function PlayEffect(fileName As String) As Integer
            MessageAPI.SendSync("[MEDIA]EFFECT_PLAY_STANDBY")
            Return Play(fileName, SoundType.Effect, False, 0)
        End Function

        ''' <summary>
        ''' 播放效果音，并在播放完成后再返回
        ''' </summary>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub PlayEffectSync(fileName As String)
            Dim id = PlayEffect(fileName)
            While True
                MessageAPI.WaitSync("[MEDIA]SOUND_END")
                If Not PlayerList.soundList.ContainsKey(id) Then Exit While
            End While
        End Sub

        ''' <summary>
        ''' 获取声音
        ''' </summary>
        ''' <param name="id">声音ID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function [Get](id As Integer) As Player
            Return PlayerList.GetPlayer(id)
        End Function

        ''' <summary>
        ''' 停止声音
        ''' </summary>
        ''' <param name="id">声音ID</param>
        ''' <remarks></remarks>
        Public Shared Sub [Stop](id As Integer)
            PlayerList.DeletePlayer(id)
        End Sub

        ''' <summary>
        ''' 暂停声音
        ''' </summary>
        ''' <param name="id">声音ID</param>
        ''' <remarks></remarks>
        Public Shared Sub Pause(id As Integer)
            Dim soundContent = PlayerList.GetPlayer(id)
            If soundContent IsNot Nothing Then
                soundContent.Pause()
                MessageAPI.SendSync("[MEDIA]SOUND_PAUSE")
            End If
        End Sub

        ''' <summary>
        ''' 继续播放已暂停的声音
        ''' </summary>
        ''' <param name="id">声音ID</param>
        ''' <remarks></remarks>
        Public Shared Sub [Resume](id As Integer)
            Dim soundContent = PlayerList.GetPlayer(id)
            If soundContent IsNot Nothing Then
                soundContent.Play()
                MessageAPI.SendSync("[MEDIA]SOUND_RESUME")
            End If
        End Sub

        ''' <summary>
        ''' 改变声音音量
        ''' </summary>
        ''' <param name="id">声音ID</param>
        ''' <param name="volume">音量</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeVolume(id As Integer, volume As Double)
            Dim soundContent = PlayerList.GetPlayer(id)
            If soundContent IsNot Nothing Then
                soundContent.Volume = volume
                MessageAPI.SendSync("[MEDIA]SOUND_VOLUME_CHANGE")
            End If
        End Sub

        ''' <summary>
        ''' 停止最近一个对话
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopNearlyReading()
            [Stop](SoundConfig.LastReadingID)
        End Sub

        ''' <summary>
        ''' 停止最近一个BGM
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopNearlyBGM()
            [Stop](SoundConfig.LastBGMID)
        End Sub

    End Class

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