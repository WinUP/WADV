Imports Microsoft.DirectX.AudioVideoPlayback
Imports WADV.Core.Enumeration

''' <summary>
''' 声音播放器
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class AudioPlayer : Inherits RAL.Tool.AudioPlayer
    Private ReadOnly _player As Audio

    ''' <summary>
    ''' 获取音频的托管DirectX对象
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DirectXObject As Audio
        Get
            Return _player
        End Get
    End Property

    Public Overrides ReadOnly Property Duration As Double
        Get
            Return _player.Duration
        End Get
    End Property

    Protected Overrides Sub SetVolume(value As Integer)
        _player.Volume = value - 10000
    End Sub

    Protected Overrides Sub SetPosition(value As Integer)
        _player.CurrentPosition = value
    End Sub

    ''' <summary>
    ''' 声明一个音频
    ''' </summary>
    ''' <param name="fileName">文件路径(Resource目录下)</param>
    ''' <param name="cycle">是否循环</param>
    ''' <param name="count">循环次数</param>
    ''' <remarks></remarks>
    Public Sub New(fileName As String, cycle As Boolean, count As Integer, id As Integer)
        MyBase.New(id)
        _player = New Audio(Path.Combine(PathType.Resource, fileName), False)
        CycleCount = count
        Me.Cycle = cycle
    End Sub

    ''' <summary>
    ''' 播放
    ''' </summary>
    Public Overrides Sub Play()
        _player.Play()
    End Sub

    ''' <summary>
    ''' 暂停
    ''' </summary>
    Public Overrides Sub Pause()
        _player.Pause()
    End Sub

    ''' <summary>
    ''' 结束
    ''' </summary>
    Public Overrides Sub Finish()
        _player.Stop()
    End Sub

    ''' <summary>
    ''' 释放音频占有的所有资源
    ''' </summary>
    Public Overrides Sub Dispose()
        _player.Dispose()
    End Sub
End Class