Namespace RAL.Tool

    ''' <summary>
    ''' RAL辅助工具 - 动画组件
    ''' </summary>
    Public MustInherit Class Animator : Inherits Component
        Private _isPlaying As Boolean
        Private ReadOnly _drawingList As New Concurrent.ConcurrentBag(Of Sprite)

        Public Sub New(ParamArray param As Double())
            Paramaters = param
            IsFinished = False
            IsPlaying = False
        End Sub

        ''' <summary>
        ''' 获取或设置动画的延迟时间
        ''' </summary>
        ''' <returns></returns>
        Public Property DelayTime As TimeSpan

        ''' <summary>
        ''' 获取或设置动画的播放时长
        ''' </summary>
        ''' <returns></returns>
        Public Property Duration As TimeSpan

        ''' <summary>
        ''' 获取或设置动画是否循环播放
        ''' </summary>
        ''' <returns></returns>
        Public Property Circle As Boolean

        ''' <summary>
        ''' 动画是否已经播放完成
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property IsFinished As Boolean

        ''' <summary>
        ''' 动画是否正在播放
        ''' </summary>
        ''' <returns></returns>
        Public Property IsPlaying As Boolean
            Get
                Return _isPlaying
            End Get
            Protected Set(value As Boolean)
                _isPlaying = value
            End Set
        End Property

        ''' <summary>
        ''' 获取动画的参数
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Paramaters As Double()

        ''' <summary>
        ''' 同步播放动画
        ''' </summary>
        ''' <param name="target">要播放动画的精灵</param>
        Public Sub Play(target As Sprite)
            IsPlaying = True
            Play_Implement(target)
        End Sub

        Protected MustOverride Sub Play_Implement(target As Sprite)

        ''' <summary>
        ''' 异步播放动画
        ''' </summary>
        ''' <param name="target">要播放动画的精灵</param>
        Public Sub PlayAndWait(target As Sprite)
            IsPlaying = True
            PlayAsync_Implement(target)
        End Sub

        Protected MustOverride Sub PlayAsync_Implement(target As Sprite)

        ''' <summary>
        ''' 立即停止动画
        ''' </summary>
        ''' <param name="target">要停止播放动画的精灵</param>
        Public Sub [Stop](target As Sprite)
            Stop_Implement(target)
            IsPlaying = False
        End Sub

        Protected MustOverride Sub Stop_Implement(target As Sprite)

        Protected Friend Overrides Function BeforeBinding(sprite As Sprite) As Boolean
            SyncLock (_drawingList)
                If Not _drawingList.Contains(sprite) Then _drawingList.Add(sprite)
            End SyncLock
            Return True
        End Function

        Protected Friend Overrides Function BeforeUnbinding(sprite As Sprite, Optional isFromClear As Boolean = False) As Boolean
            SyncLock (_drawingList)
                If _drawingList.Contains(sprite) Then _drawingList.TryTake(sprite)
            End SyncLock
            Return True
        End Function

        Protected Friend Overrides Sub BindToScene(sprite As Sprite, scene As Scene)
            SyncLock (_drawingList)
                If Not _drawingList.Contains(sprite) Then _drawingList.Add(sprite)
            End SyncLock
        End Sub

        Protected Friend Overrides Sub UnbindFromScene(sprite As Sprite, scene As Scene)
            SyncLock (_drawingList)
                If _drawingList.Contains(sprite) Then _drawingList.TryTake(sprite)
            End SyncLock
        End Sub

        Protected Friend Overrides Sub LoopOnRender()
            For Each e In _drawingList
                SyncLock (e)
                    Play(e)
                End SyncLock
            Next
        End Sub

        Protected Friend Overrides Sub MessageOnReceiver(message As String)
            For Each e In _drawingList
                SyncLock (e)
                    Play(e)
                End SyncLock
            Next
        End Sub

    End Class
End Namespace