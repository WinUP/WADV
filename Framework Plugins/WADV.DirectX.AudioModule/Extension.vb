Public Class Extension
    ''' <summary>
    ''' 播放音频
    ''' </summary>
    ''' <param name="filePath">文件路径(Resource目录下)</param>
    ''' <param name="cycle">是否循环</param>
    ''' <param name="count">循环次数(永久循环为-1)</param>
    ''' <remarks></remarks>
    Public Shared Function Play(filePath As String, cycle As Boolean, count As Integer) As Integer
        Dim tmpPlayer = AudioPlayerList.AddPlayer(filePath, cycle, count)
        If tmpPlayer Is Nothing Then Return -1
        tmpPlayer.Play()
        Return tmpPlayer.PlayerId
    End Function

    ''' <summary>
    ''' 获取音频
    ''' </summary>
    ''' <param name="id">音频ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function [Get](id As Integer) As AudioPlayer
        Return AudioPlayerList.GetPlayer(id)
    End Function

    ''' <summary>
    ''' 停止音频
    ''' </summary>
    ''' <param name="id">音频ID</param>
    ''' <remarks></remarks>
    Public Shared Sub [Stop](id As Integer)
        AudioPlayerList.DeletePlayer(id)
    End Sub

    ''' <summary>
    ''' 暂停音频
    ''' </summary>
    ''' <param name="id">音频ID</param>
    ''' <remarks></remarks>
    Public Shared Sub Pause(id As Integer)
        Dim soundContent = AudioPlayerList.GetPlayer(id)
        If soundContent IsNot Nothing Then
            soundContent.Pause()
            Message.Send("[AUDIO]SOUND_PAUSE", 4)
        End If
    End Sub

    ''' <summary>
    ''' 继续播放已暂停的音频
    ''' </summary>
    ''' <param name="id">音频ID</param>
    ''' <remarks></remarks>
    Public Shared Sub [Resume](id As Integer)
        Dim soundContent = AudioPlayerList.GetPlayer(id)
        If soundContent IsNot Nothing Then
            soundContent.Play()
            Message.Send("[AUDIO]SOUND_RESUME", 4)
        End If
    End Sub

    ''' <summary>
    ''' 获取或设置音频音量(0~10000)
    ''' </summary>
    ''' <param name="id">音频ID</param>
    ''' <param name="value">音量</param>
    ''' <remarks></remarks>
    Public Shared Function Volume(id As Integer, Optional value As Double = -1) As Double
        Dim soundContent = AudioPlayerList.GetPlayer(id)
        If value.Equals(-1.0) Then Return soundContent.Volume
        If soundContent IsNot Nothing Then
            soundContent.Volume = value
            Message.Send("[AUDIO]SOUND_VOLUME_CHANGE", 4)
        End If
        Return value
    End Function
End Class