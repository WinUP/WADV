''' <summary>
''' 音频播放器列表
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class PlayerList
    Friend Shared ReadOnly SoundList As New Dictionary(Of Integer, Player)
    Private Shared _nextId As Integer = 0

    ''' <summary>
    ''' 新建一个播放器
    ''' </summary>
    ''' <param name="fileName">音频文件路径</param>
    ''' <param name="type">音频类型</param>
    ''' <param name="cycle">是否循环播放</param>
    ''' <param name="count">循环次数(-1为无限循环)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function AddPlayer(fileName As String, type As SoundType, cycle As Boolean, count As Integer) As Player
        Dim tmpPlayer As New Player(fileName, type, cycle, count, _nextId)
        SoundList.Add(_nextId, tmpPlayer)
        _nextId += 1
        MessageAPI.SendSync("[MEDIA]SOUND_ADD")
        Return tmpPlayer
    End Function

    ''' <summary>
    ''' 删除一个播放器
    ''' </summary>
    ''' <param name="id">播放器ID</param>
    ''' <remarks></remarks>
    Friend Shared Sub DeletePlayer(id As Integer)
        Dim player = GetPlayer(id)
        If player Is Nothing Then Exit Sub
        Try
            player.Finish()
            player.Dispose()
        Catch ex As Exception
            Debug.WriteLine("--ERROR: " & ex.Message)
        End Try
        SoundList.Remove(id)
        MessageAPI.SendSync("[MEDIA]SOUND_REMOVE")
    End Sub

    ''' <summary>
    ''' 获得一个播放器
    ''' </summary>
    ''' <param name="id">播放器ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function GetPlayer(id As Integer) As Player
        If Not SoundList.ContainsKey(id) Then Return Nothing
        Return SoundList(id)
    End Function

    ''' <summary>
    ''' 修改所有指定类型音频的音量
    ''' </summary>
    ''' <param name="type">音频类型</param>
    ''' <param name="value">目标音量</param>
    ''' <remarks></remarks>
    Friend Shared Sub ChangeVolume(type As SoundType, value As Integer)
        For Each tmpSound In (From singleSound In SoundList.Values Where singleSound.Type = type Select singleSound)
            tmpSound.Volume = value
        Next
        MessageAPI.SendSync("[MEDIA]SOUND_VOLUME_CHANGE")
    End Sub

    ''' <summary>
    ''' 进行播放器循环检查
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared Sub CheckEnding()
        Dim player As Player
        For Each sound In SoundList
            player = sound.Value
            If player.Duration - player.Position <= 0.001 Then
                If (Not player.Cycle) OrElse (player.Cycle AndAlso player.CycleCount = 0) Then
                    DeletePlayer(player.ID)
                    MessageAPI.SendSync("[MEDIA]SOUND_END")
                Else
                    player.Position = 0.0
                    player.Play()
                    If player.CycleCount > 0 Then player.CycleCount -= 1
                    MessageAPI.SendSync("[MEDIA]SOUND_CYCLE")
                End If
            End If
        Next
    End Sub

End Class