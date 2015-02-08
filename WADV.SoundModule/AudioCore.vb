Imports Microsoft.DirectX.AudioVideoPlayback
Imports WADV.MediaModule.API

Namespace AudioCore

    ''' <summary>
    ''' 声音类型
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SoundType
        Background
        Reading
        Effect
    End Enum

    ''' <summary>
    ''' 声音播放器
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Player
        Private ReadOnly _type As SoundType
        Private ReadOnly _player As Audio
        Private ReadOnly _id As Integer

        ''' <summary>
        ''' 获取声音类型
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Type As SoundType
            Get
                Return _type
            End Get
        End Property

        ''' <summary>
        ''' 获取声音ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ID As Integer
            Get
                Return _id
            End Get
        End Property

        ''' <summary>
        ''' 获取或设置声音循环状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Cycle As Boolean

        ''' <summary>
        ''' 获取或设置剩余循环次数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CycleCount As Integer

        ''' <summary>
        ''' 获取或设置音量
        ''' </summary>
        ''' <value>目标音量(-10000~0)</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Volume As Integer
            Get
                Return _player.Volume
            End Get
            Set(value As Integer)
                _player.Volume = value
            End Set
        End Property

        ''' <summary>
        ''' 获取音频文件时间长度
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Duration As Double
            Get
                Return _player.Duration
            End Get
        End Property

        ''' <summary>
        ''' 获取或设置声音播放到的位置
        ''' </summary>
        ''' <value>新的位置</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Position As Double
            Get
                Return _player.CurrentPosition
            End Get
            Set(value As Double)
                _player.CurrentPosition = value
            End Set
        End Property

        ''' <summary>
        ''' 声明一个媒体
        ''' </summary>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <param name="type">声音类型</param>
        ''' <param name="cycle">是否循环</param>
        ''' <param name="count">循环次数</param>
        ''' <remarks></remarks>
        Public Sub New(fileName As String, type As SoundType, cycle As Boolean, count As Integer, id As Integer)
            _player = New Audio(PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Resource, fileName), False)
            If type = SoundType.Background Then
                Volume = ConfigAPI.GetBackgroundVolume
            ElseIf type = SoundType.Effect Then
                Volume = ConfigAPI.GetEffectVolume
            Else
                Volume = ConfigAPI.GetReadingVolume
            End If
            _type = type
            CycleCount = count
            Me.Cycle = cycle
            _id = id
        End Sub

        Public Sub Play()
            _player.Play()
        End Sub

        Public Sub Pause()
            _player.Pause()
        End Sub

        Public Sub Finish()
            _player.Stop()
        End Sub

        Public Sub Dispose()
            _player.Dispose()
        End Sub

    End Class

    Public Class PlayerList
        Protected Friend Shared soundList As New Dictionary(Of Integer, Player)
        Private Shared nextId As Integer = 0

        Protected Friend Shared Function AddPlayer(fileName As String, type As SoundType, cycle As Boolean, count As Integer) As Player
            Dim tmpPlayer As New Player(fileName, type, cycle, count, nextId)
            soundList.Add(nextId, tmpPlayer)
            nextId += 1
            MessageAPI.SendSync("MEDIA_SOUND_ADD")
            Return tmpPlayer
        End Function

        Protected Friend Shared Sub DeletePlayer(id As Integer)
            Dim player = GetPlayer(id)
            If player Is Nothing Then Exit Sub
            Try
                player.Finish()
                player.Dispose()
            Catch ex As Exception
                Debug.WriteLine("--ERROR: " & ex.Message)
            End Try
            soundList.Remove(id)
            MessageAPI.SendSync("MEDIA_SOUND_REMOVE")
        End Sub

        Protected Friend Shared Function GetPlayer(id As Integer) As Player
            If Not soundList.ContainsKey(id) Then Return Nothing
            Return soundList(id)
        End Function

        Protected Friend Shared Sub ChangeVolume(type As SoundType, value As Integer)
            For Each tmpSound In (From singleSound In soundList.Values Where singleSound.Type = type Select singleSound)
                tmpSound.Volume = value
            Next
            MessageAPI.SendSync("MEDIA_SOUND_CHANGEVOLUME")
        End Sub

        Protected Friend Shared Sub CheckEnding()
            Dim player As Player
            For Each sound In soundList
                player = sound.Value
                If player.Duration = player.Position Then
                    If (Not player.Cycle) OrElse (player.Cycle AndAlso player.CycleCount = 0) Then
                        DeletePlayer(player.ID)
                        MessageAPI.SendSync("MEDIA_SOUND_END")
                    Else
                        player.Position = 0.0
                        player.Play()
                        If player.CycleCount > 0 Then player.CycleCount -= 1
                        MessageAPI.SendSync("MEDIA_SOUND_CYCLE")
                    End If
                End If
            Next
        End Sub

    End Class

End Namespace