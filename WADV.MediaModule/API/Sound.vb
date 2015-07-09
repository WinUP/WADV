
Namespace API

    ''' <summary>
    ''' 声音API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Sound

        ''' <summary>
        ''' 播放声音
        ''' </summary>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <param name="type">声音类型</param>
        ''' <param name="cycle">是否循环</param>
        ''' <param name="count">循环次数(永久循环为-1)</param>
        ''' <remarks></remarks>
        Public Function Play(fileName As String, type As SoundType, cycle As Boolean, count As Integer) As Integer
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
        Public Function PlayBgm(fileName As String) As Integer
            Message.Send("[MEDIA]BGM_PLAY_STANDBY")
            Dim id = Play(fileName, SoundType.Background, True, -1)
            SoundConfig.LastBgmId = id
            Return id
        End Function

        ''' <summary>
        ''' 播放对话
        ''' </summary>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Public Function PlayReading(fileName As String) As Integer
            Message.Send("[MEDIA]READ_PLAY_STANDBY")
            Dim id = Play(fileName, SoundType.Reading, False, 0)
            SoundConfig.LastReadingId = id
            Return id
        End Function

        ''' <summary>
        ''' 播放效果音
        ''' </summary>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Public Function PlayEffect(fileName As String) As Integer
            Message.Send("[MEDIA]EFFECT_PLAY_STANDBY")
            Return Play(fileName, SoundType.Effect, False, 0)
        End Function

        ''' <summary>
        ''' 播放效果音，并在播放完成后再返回
        ''' </summary>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Public Sub PlayEffectSync(fileName As String)
            Dim id = PlayEffect(fileName)
            While True
                Message.Wait("[MEDIA]SOUND_END")
                If Not PlayerList.SoundList.ContainsKey(id) Then Exit While
            End While
        End Sub

        ''' <summary>
        ''' 获取声音
        ''' </summary>
        ''' <param name="id">声音ID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function [Get](id As Integer) As Player
            Return PlayerList.GetPlayer(id)
        End Function

        ''' <summary>
        ''' 停止声音
        ''' </summary>
        ''' <param name="id">声音ID</param>
        ''' <remarks></remarks>
        Public Sub [Stop](id As Integer)
            PlayerList.DeletePlayer(id)
        End Sub

        ''' <summary>
        ''' 暂停声音
        ''' </summary>
        ''' <param name="id">声音ID</param>
        ''' <remarks></remarks>
        Public Sub Pause(id As Integer)
            Dim soundContent = PlayerList.GetPlayer(id)
            If soundContent IsNot Nothing Then
                soundContent.Pause()
                Message.Send("[MEDIA]SOUND_PAUSE")
            End If
        End Sub

        ''' <summary>
        ''' 继续播放已暂停的声音
        ''' </summary>
        ''' <param name="id">声音ID</param>
        ''' <remarks></remarks>
        Public Sub [Resume](id As Integer)
            Dim soundContent = PlayerList.GetPlayer(id)
            If soundContent IsNot Nothing Then
                soundContent.Play()
                Message.Send("[MEDIA]SOUND_RESUME")
            End If
        End Sub

        ''' <summary>
        ''' 改变声音音量
        ''' </summary>
        ''' <param name="id">声音ID</param>
        ''' <param name="volume">音量</param>
        ''' <remarks></remarks>
        Public Sub ChangeVolume(id As Integer, volume As Double)
            Dim soundContent = PlayerList.GetPlayer(id)
            If soundContent IsNot Nothing Then
                soundContent.Volume = volume
                Message.Send("[MEDIA]SOUND_VOLUME_CHANGE")
            End If
        End Sub

        ''' <summary>
        ''' 停止最近一个对话
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub StopNearlyReading()
            [Stop](SoundConfig.LastReadingId)
        End Sub

        ''' <summary>
        ''' 停止最近一个BGM
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub StopNearlyBgm()
            [Stop](SoundConfig.LastBgmId)
        End Sub

    End Module
End Namespace