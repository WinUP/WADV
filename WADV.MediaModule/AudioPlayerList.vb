''' <summary>
''' 音频播放器列表
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class AudioPlayerList
    Friend Shared ReadOnly SoundList As New Dictionary(Of Integer, AudioPlayer)
    Private Shared _nextId As Integer = 0

    ''' <summary>
    ''' 新建一个播放器
    ''' </summary>
    ''' <param name="fileName">音频文件路径</param>
    ''' <param name="cycle">是否循环播放</param>
    ''' <param name="count">循环次数(-1为无限循环)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function AddPlayer(fileName As String, cycle As Boolean, count As Integer) As AudioPlayer
        If fileName = "" Then Return Nothing
        Dim tmpPlayer As New AudioPlayer(fileName, cycle, count, _nextId)
        SoundList.Add(_nextId, tmpPlayer)
        _nextId += 1
        Message.Send("[AUDIO]SOUND_ADD")
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
        Message.Send("[AUDIO]SOUND_REMOVE")
    End Sub

    ''' <summary>
    ''' 获得一个播放器
    ''' </summary>
    ''' <param name="id">播放器ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function GetPlayer(id As Integer) As AudioPlayer
        If Not SoundList.ContainsKey(id) Then Return Nothing
        Return SoundList(id)
    End Function

    ''' <summary>
    ''' 进行播放器循环检查
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared Sub CheckEnding()
        Dim player As AudioPlayer
        For Each sound In SoundList
            player = sound.Value
            If player.Duration - player.Position <= 0.001 Then
                If (Not player.Cycle) OrElse (player.Cycle AndAlso player.CycleCount = 0) Then
                    DeletePlayer(player.ID)
                    Message.Send("[AUDIO]SOUND_END")
                Else
                    player.Position = 0.0
                    player.Play()
                    If player.CycleCount > 0 Then player.CycleCount -= 1
                    Message.Send("[AUDIO]SOUND_CYCLE")
                End If
            End If
        Next
    End Sub

End Class