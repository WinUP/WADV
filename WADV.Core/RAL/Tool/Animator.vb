Namespace RAL.Tool

    ''' <summary>
    ''' RAL辅助工具 - 动画组件
    ''' </summary>
    Public MustInherit Class Animator : Inherits Component
        Private ReadOnly _isPlaying As New Concurrent.ConcurrentDictionary(Of Sprite, Boolean)
        Private ReadOnly _isFinished As New Concurrent.ConcurrentDictionary(Of Sprite, Boolean)
        Private ReadOnly _drawingList As New Concurrent.ConcurrentBag(Of Sprite)

        Public Sub New(ParamArray param As Double())
            Paramaters = param
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
        Public Property IsFinished(target As Sprite) As Boolean
            Get
                Return _isFinished(target)
            End Get
            Private Set(value As Boolean)
                _isFinished(target) = value
            End Set
        End Property

        ''' <summary>
        ''' 动画是否正在播放
        ''' </summary>
        ''' <returns></returns>
        Public Property IsPlaying(target As Sprite) As Boolean
            Get
                Return _isPlaying(target)
            End Get
            Private Set(value As Boolean)
                _isPlaying(target) = value
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
            _isFinished(target) = False
            _isPlaying(target) = True
            Play_Implement(target)
        End Sub

        Protected MustOverride Sub Play_Implement(target As Sprite)

        ''' <summary>
        ''' 异步播放动画
        ''' </summary>
        ''' <param name="target">要播放动画的精灵</param>
        Public Sub PlayAndWait(target As Sprite)
            Play(target)
            While (IsPlaying(target))
                API.Message.Wait("[COMPONENT]ANIMATION_FINISHED")
            End While
        End Sub

        ''' <summary>
        ''' 立即停止动画
        ''' </summary>
        ''' <param name="target">要停止播放动画的精灵</param>
        Public Sub [Stop](target As Sprite)
            Stop_Implement(target)
            _isPlaying(target) = False
            _isFinished(target) = False
        End Sub

        Protected MustOverride Sub Stop_Implement(target As Sprite)

        Protected Sub FinishPlaying(target As Sprite)
            _isPlaying(target) = False
            _isFinished(target) = True
            Configuration.System.MessageService.SendMessage("[COMPONENT]ANIMATION_FINISHED", 2)
        End Sub

        Protected Friend Overrides Function BeforeBinding(sprite As Sprite) As Boolean
            SyncLock (_drawingList)
                If Not _drawingList.Contains(sprite) Then
                    _drawingList.Add(sprite)
                    _isPlaying.TryAdd(sprite, False)
                End If
            End SyncLock
            Return True
        End Function

        Protected Friend Overrides Function BeforeUnbinding(sprite As Sprite, Optional isFromClear As Boolean = False) As Boolean
            SyncLock (_drawingList)
                If _drawingList.Contains(sprite) Then
                    _drawingList.TryTake(sprite)
                    Dim tempAnswer As Boolean
                    _isPlaying.TryRemove(sprite, tempAnswer)
                End If
            End SyncLock
            Return True
        End Function

        Protected Friend Overrides Sub BindToScene(sprite As Sprite, scene As Scene)
            SyncLock (_drawingList)
                If Not _drawingList.Contains(sprite) Then
                    _drawingList.Add(sprite)
                    _isPlaying.TryAdd(sprite, False)
                End If
            End SyncLock
        End Sub

        Protected Friend Overrides Sub UnbindFromScene(sprite As Sprite, scene As Scene)
            SyncLock (_drawingList)
                If _drawingList.Contains(sprite) Then
                    _drawingList.TryTake(sprite)
                    Dim tempAnswer As Boolean
                    _isPlaying.TryRemove(sprite, tempAnswer)
                End If
            End SyncLock
        End Sub

        Protected Friend Overrides Sub LoopOnRender()
            For Each e In _drawingList
                SyncLock (e)
                    Play_Implement(e)
                End SyncLock
            Next
        End Sub

        Protected Friend Overrides Sub MessageOnReceiver(message As String)
            For Each e In _drawingList
                SyncLock (e)
                    Play_Implement(e)
                End SyncLock
            Next
        End Sub
    End Class
End Namespace