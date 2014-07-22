Imports System.Windows.Media
Imports WADV.MediaModule.Config
Imports WADV.MediaModule.Player

Namespace API

    ''' <summary>
    ''' 设置API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConfigAPI

        ''' <summary>
        ''' 获取背景音乐音量
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBackgroundVolume() As Double
            Return SoundConfig.Background
        End Function

        ''' <summary>
        ''' 获取对话音量
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetReadingVolume() As Double
            Return SoundConfig.Reading
        End Function

        ''' <summary>
        ''' 获取效果音量
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEffectVolume() As Double
            Return SoundConfig.Effect
        End Function

        ''' <summary>
        ''' 设置背景音乐音量
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetBackgroundVolume(value As Double)
            SoundConfig.Background = value
        End Sub

        ''' <summary>
        ''' 设置对话音量
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetReadingVolume(value As Double)
            SoundConfig.Reading = value
        End Sub

        ''' <summary>
        ''' 设置效果音量
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetEffectVolume(value As Double)
            SoundConfig.Effect = value
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
        ''' <param name="name">声音标识名</param>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <param name="type">声音类型</param>
        ''' <param name="cycle">是否循环</param>
        ''' <param name="count">循环次数(永久循环为-1)</param>
        ''' <remarks></remarks>
        Public Shared Sub PlaySound(name As String, fileName As String, type As AdvancedPlayer.SoundType, cycle As Boolean, count As Integer)
            Dim playerContent As AdvancedPlayer = AppCore.API.WindowAPI.GetWindowDispatcher.Invoke(Function() New AdvancedPlayer(name, fileName, type, cycle, count))
            SoundList.AddSound(playerContent)
            playerContent.Dispatcher.BeginInvoke(Sub() playerContent.Play())
        End Sub

        ''' <summary>
        ''' 播放背景音乐
        ''' </summary>
        ''' <param name="name">声音标识名</param>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub PlayBGM(name As String, fileName As String)
            PlaySound(name, fileName, AdvancedPlayer.SoundType.Background, True, -1)
        End Sub

        ''' <summary>
        ''' 播放对话
        ''' </summary>
        ''' <param name="name">声音标识名</param>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub PlayReading(name As String, fileName As String)
            PlaySound(name, fileName, AdvancedPlayer.SoundType.Reading, False, 0)
        End Sub

        ''' <summary>
        ''' 播放效果音
        ''' </summary>
        ''' <param name="name">声音标识名</param>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub PlayEffect(name As String, fileName As String)
            PlaySound(name, fileName, AdvancedPlayer.SoundType.Effect, False, 0)
        End Sub

        ''' <summary>
        ''' 获取声音
        ''' </summary>
        ''' <param name="name">声音标识名</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSound(name As String) As AdvancedPlayer
            Return SoundList.FindSound(name)
        End Function

        ''' <summary>
        ''' 停止声音
        ''' </summary>
        ''' <param name="name">声音标识名</param>
        ''' <remarks></remarks>
        Public Shared Sub StopSound(name As String)
            SoundList.DeleteSound(name)
        End Sub

        ''' <summary>
        ''' 暂停声音
        ''' </summary>
        ''' <param name="name">声音标识名</param>
        ''' <remarks></remarks>
        Public Shared Sub PauseSound(name As String)
            Dim soundContent = SoundList.FindSound(name)
            If soundContent IsNot Nothing Then soundContent.Dispatcher.BeginInvoke(Sub() soundContent.Pause())
        End Sub

        ''' <summary>
        ''' 继续播放已暂停的声音
        ''' </summary>
        ''' <param name="name">声音标识名</param>
        ''' <remarks></remarks>
        Public Shared Sub ResumeSound(name As String)
            Dim soundContent = SoundList.FindSound(name)
            If soundContent IsNot Nothing Then soundContent.Dispatcher.BeginInvoke(Sub() soundContent.Play())
        End Sub

        ''' <summary>
        ''' 改变声音音量
        ''' </summary>
        ''' <param name="name">声音标识名</param>
        ''' <param name="volume">音量</param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeSoundVolume(name As String, volume As Double)
            Dim soundContent = SoundList.FindSound(name)
            If soundContent IsNot Nothing Then soundContent.Dispatcher.BeginInvoke(Sub() soundContent.Volume = volume)
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
        Public Shared Sub PlayAndWait(fileName As String, clickSkip As Boolean)
            PlayVideo(fileName, clickSkip)
            While True
                If VideoConfig.IsPlayFinished Then
                    Exit While
                Else
                    Threading.Thread.Sleep(1000)
                End If
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
            If VideoConfig.VideoContent IsNot Nothing Then VideoConfig.VideoContent.Dispatcher.BeginInvoke(Sub() VideoConfig.VideoContent.Pause())
        End Sub

        ''' <summary>
        ''' 继续播放已暂停的视频
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ResumeVideo()
            If VideoConfig.VideoContent IsNot Nothing Then VideoConfig.VideoContent.Dispatcher.BeginInvoke(Sub() VideoConfig.VideoContent.Play())
        End Sub

    End Class

End Namespace