Imports Microsoft.DirectX.AudioVideoPlayback

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
Public NotInheritable Class Player
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
        _player = New Audio(PathAPI.GetPath(PathType.Resource, fileName), False)
        If type = SoundType.Background Then
            Volume = SoundConfig.Background
        ElseIf type = SoundType.Effect Then
            Volume = SoundConfig.Effect
        Else
            Volume = SoundConfig.Reading
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