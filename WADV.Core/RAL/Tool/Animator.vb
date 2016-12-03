' 如何实现：RAL动画组件
' RAL动画组件是一个允许多重挂载的组件，只能挂载到已添加到Scene的Sprite级别节点上
' 继承自动画组件的动画需要在声明时将所有可用数字表示的动画参数传入，参数将储存在Paramaters属性中。
' Play()和PlayAndWait()会开始播放一个关联元素的动画，但对正在播放动画的元素无效。调用成功时会发送[COMPONENT]ANIMATION_STARTED -> 2
' Play_Implement()会在每次游戏循环的Render流程中以及每次接受到新消息时进行，每次调用Play()或PlayAndWait()成功时也会运行一次
' Stop()仅会在被调用时执行，作用是停止一个关联元素的动画，但对不在播放动画的元素无效。调用成功时会发送[COMPONENT]ANIMATION_FINISHED -> 2
' Stop_Implement()会在每次Stop()调用成功时执行
' 当一个Sprite成功挂在到组件时，它的IsPlaying和IsFinished都是False；动画播放过程中它的IsPlaying会是True；动画播放完成后，它的IsPlaying会回到False，但IsFinished会变成True
' 当一个Sprite从Scene解绑后，直到它再次绑定到Scene之前，动画不会进行，IsPlaying和IsFinished也会在再次绑定时都变成False

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
            If _isPlaying.ContainsKey(target) Then Exit Sub
            IsFinished(target) = False
            IsPlaying(target) = True
            Play_Implement(target,, True)
            Configuration.System.MessageService.SendMessage("[COMPONENT]ANIMATION_STARTED", 2)
        End Sub

        Protected MustOverride Sub Play_Implement(target As Sprite, Optional message As String = "", Optional firstCall As Boolean = False)

        ''' <summary>
        ''' 异步播放动画
        ''' </summary>
        ''' <param name="target">要播放动画的精灵</param>
        Public Sub PlayAndWait(target As Sprite)
            If _isPlaying.ContainsKey(target) Then Exit Sub
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
            If Not _isPlaying.ContainsKey(target) Then Exit Sub
            IsPlaying(target) = False
            IsFinished(target) = True
            Stop_Implement(target)
            Configuration.System.MessageService.SendMessage("[COMPONENT]ANIMATION_FINISHED", 2)
        End Sub

        Protected MustOverride Sub Stop_Implement(target As Sprite)

        Protected Friend Overrides Function BeforeBinding(sprite As Sprite) As Boolean
            If sprite.Scene Is Nothing Then Return False
            BindToScene(sprite, sprite.Scene)
            Return True
        End Function

        Protected Friend Overrides Function BeforeUnbinding(sprite As Sprite, Optional isFromClear As Boolean = False) As Boolean
            UnbindFromScene(sprite, sprite.Scene)
            Return True
        End Function

        Protected Friend Overrides Sub BindToScene(sprite As Sprite, scene As Scene)
            If Not _drawingList.Contains(sprite) Then
                _drawingList.Add(sprite)
                _isPlaying.TryAdd(sprite, False)
                _isFinished.TryAdd(sprite, False)
            End If
        End Sub

        Protected Friend Overrides Sub UnbindFromScene(sprite As Sprite, scene As Scene)
            If _drawingList.Contains(sprite) Then
                If IsPlaying(sprite) Then [Stop](sprite)
                _drawingList.TryTake(sprite)
                Dim tempAnswer As Boolean
                _isPlaying.TryRemove(sprite, tempAnswer)
                _isFinished.TryRemove(sprite, tempAnswer)
            End If
        End Sub

        Protected Friend Overrides Sub LoopOnRender()
            For Each e In _isPlaying.Keys
                SyncLock (e)
                    Play_Implement(e)
                End SyncLock
            Next
        End Sub

        Protected Friend Overrides Sub MessageOnReceiver(message As String)
            For Each e In _isPlaying.Keys
                SyncLock (e)
                    Play_Implement(e, message)
                End SyncLock
            Next
        End Sub
    End Class
End Namespace