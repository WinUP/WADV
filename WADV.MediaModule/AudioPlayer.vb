Imports Microsoft.DirectX.AudioVideoPlayback
Imports WADV.Core.Enumeration

''' <summary>
''' 声音播放器
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class AudioPlayer
    Private ReadOnly _player As Audio
    Private ReadOnly _id As Integer

    ''' <summary>
    ''' 获取音频ID
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
    ''' 获取音频的托管DirectX对象
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DirectXObject As Audio
        Get
            Return _player
        End Get
    End Property

    ''' <summary>
    ''' 获取或设置音频的循环状态
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Cycle As Boolean

    ''' <summary>
    ''' 获取或设置音频的剩余循环次数
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CycleCount As Integer

    ''' <summary>
    ''' 获取或设置音量(0~10000)
    ''' </summary>
    ''' <value>目标音量</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Volume As Integer
        Get
            Return _player.Volume + 10000
        End Get
        Set(value As Integer)
            _player.Volume = value - 10000
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
    ''' 获取或设置音频播放到的位置
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
    ''' 声明一个音频
    ''' </summary>
    ''' <param name="fileName">文件路径(Resource目录下)</param>
    ''' <param name="cycle">是否循环</param>
    ''' <param name="count">循环次数</param>
    ''' <remarks></remarks>
    Public Sub New(fileName As String, cycle As Boolean, count As Integer, id As Integer)
        _player = New Audio(Path.Combine(PathType.Resource, fileName), False)
        CycleCount = count
        Me.Cycle = cycle
        _id = id
    End Sub

    ''' <summary>
    ''' 播放
    ''' </summary>
    Public Sub Play()
        _player.Play()
    End Sub

    ''' <summary>
    ''' 暂停
    ''' </summary>
    Public Sub Pause()
        _player.Pause()
    End Sub

    ''' <summary>
    ''' 结束
    ''' </summary>
    Public Sub Finish()
        _player.Stop()
    End Sub

    ''' <summary>
    ''' 释放音频占有的所有资源
    ''' </summary>
    Public Sub Dispose()
        _player.Dispose()
    End Sub
End Class