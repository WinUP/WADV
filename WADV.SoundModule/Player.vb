Imports System.Windows.Controls
Imports WADV.MediaModule.API

Namespace Player

    '一开始用的MediaPlayer，因为它比这个轻量。不过GC似乎老是和我过不去，循环播放总是实现不了，只好用MediaElement了
    ''' <summary>
    ''' 带循环的媒体播放控件
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AdvancedPlayer : Inherits MediaElement
        Private _tagName As String
        Private _type As SoundType
        Private _cycle As Boolean
        Private _cycleCount As Integer

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
        ''' 获取标识名
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property TagName As String
            Get
                Return _tagName
            End Get
        End Property

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
        ''' 获取或声音循环状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Cycle As Boolean
            Get
                Return _cycle
            End Get
            Set(value As Boolean)
                _cycle = value
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置剩余循环次数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CycleCount As Integer
            Get
                Return _cycleCount
            End Get
            Set(value As Integer)
                _cycleCount = value
            End Set
        End Property

        ''' <summary>
        ''' 声明一个媒体
        ''' </summary>
        ''' <param name="name">标识名</param>
        ''' <param name="fileName">文件路径(Resource目录下)</param>
        ''' <param name="type">声音类型</param>
        ''' <param name="cycle">是否循环</param>
        ''' <param name="count">循环次数</param>
        ''' <remarks></remarks>
        Public Sub New(name As String, fileName As String, type As SoundType, cycle As Boolean, count As Integer)
            MyBase.New()
            Visibility = Windows.Visibility.Collapsed
            LoadedBehavior = MediaState.Manual
            _tagName = name
            _type = type
            _cycle = cycle
            _cycleCount = count
            If type = SoundType.Background Then
                Volume = ConfigAPI.GetBackgroundVolume
            ElseIf type = SoundType.Effect Then
                Volume = ConfigAPI.GetEffectVolume
            Else
                Volume = ConfigAPI.GetReadingVolume
            End If
            WindowAPI.GetGrid.Children.Add(Me)
            Source = New Uri(PathAPI.GetPath(PathAPI.Resource, fileName))
        End Sub

        Protected Sub Media_Ended(sender As Object, e As EventArgs) Handles Me.MediaEnded
            If (Not Cycle) OrElse (Cycle AndAlso CycleCount = 0) Then
                Config.SoundList.DeleteSound(Me)
            End If
            If Cycle AndAlso CycleCount = -1 Then
                Position = New TimeSpan(0)
                Play()
            End If
            CycleCount -= 1
            Position = New TimeSpan(0)
            Play()
        End Sub

        Protected Sub Media_Faild(sender As Object, e As Windows.ExceptionRoutedEventArgs) Handles Me.MediaFailed
            MsgBox(e.ErrorException.Message, "音频组件错误")
        End Sub

    End Class

End Namespace
