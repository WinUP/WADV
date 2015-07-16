Imports System.Windows

Namespace Component
    ''' <summary>
    ''' 组件基础类
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class Component : Implements IDisposable
        Private ReadOnly _elementList As New List(Of FrameworkElement)
        Private _receiverType As ComponentReceiverType = ComponentReceiverType.None

        ''' <summary>
        ''' 当与元素的连接断开时执行的操作
        ''' </summary>
        ''' <param name="sourceElement">针对的元素</param>
        ''' <param name="isFromClear">这个操作是否由全部清除操作发起</param>
        ''' <returns>允许断开绑定则返回True，否则返回False</returns>
        ''' <remarks></remarks>
        Protected Friend Overridable Function BeforeUnbinding(sourceElement As FrameworkElement, Optional isFromClear As Boolean = False) As Boolean
            Return True
        End Function

        ''' <summary>
        ''' 当与元素连接时执行的操作
        ''' </summary>
        ''' <param name="sourceElement">针对的元素</param>
        ''' <returns>允许继续绑定则返回True，否则返回False</returns>
        ''' <remarks></remarks>
        Protected Friend Overridable Function BeforeBinding(sourceElement As FrameworkElement) As Boolean
            Return True
        End Function

        ''' <summary>
        ''' 当与元素的连接被强制断开时执行的操作
        ''' </summary>
        ''' <param name="sourceElement">针对的元素</param>
        ''' <remarks></remarks>
        Protected Friend Overridable Sub ForceUnbinding(sourceElement As FrameworkElement)
        End Sub

        ''' <summary>
        ''' 当游戏循环执行逻辑更新时的操作
        ''' </summary>
        ''' <param name="frame">当前的帧计数</param>
        ''' <returns>是否在下次循环时继续执行这段代码</returns>
        ''' <remarks>只有当ReceiverType为LoopOnly或Both时该函数才会执行</remarks>
        Protected Friend Overridable Function LoopOnLogic(frame As Integer) As Boolean
            Return False
        End Function

        ''' <summary>
        ''' 当游戏循环执行界面更新时的操作
        ''' </summary>
        ''' <remarks>只有当ReceiverType为LoopOnly或Both时该函数才会执行</remarks>
        Protected Friend Overridable Sub LoopOnRender()
        End Sub

        ''' <summary>
        ''' 当消息循环接收到消息时的操作
        ''' </summary>
        ''' <param name="message">接收到的消息</param>
        ''' <remarks>只有当ReceiverType为MessageOnly或Both时该函数才会执行</remarks>
        Protected Friend Overridable Sub MessageOnReceiver(message As String)
        End Sub

        ''' <summary>
        ''' 当组件的资源被释放时执行的操作
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Overridable Sub Dispose() Implements IDisposable.Dispose
        End Sub

        ''' <summary>
        ''' 获取组件的关联对象
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property BindedElements As FrameworkElement()
            Get
                Return _elementList.ToArray
            End Get
        End Property

        ''' <summary>
        ''' 获取组件对于游戏系统的接收器类型
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ReceiverType As ComponentReceiverType
            Get
                Return _receiverType
            End Get
        End Property

        ''' <summary>
        ''' 设置这个组件的游戏循环更新响应方式
        ''' </summary>
        ''' <param name="canListen">是否响应</param>
        ''' <remarks></remarks>
        Public Sub ListenLoop(canListen As Boolean)
            If canListen Then
                ComponentLoopReceiver.Add(Me)
                If _receiverType = ComponentReceiverType.None Then
                    _receiverType = ComponentReceiverType.LoopOnly
                Else
                    _receiverType = ComponentReceiverType.Both
                End If
            Else
                ComponentLoopReceiver.Remove(Me)
                If _receiverType = ComponentReceiverType.Both Then
                    _receiverType = ComponentReceiverType.MessageOnly
                Else
                    _receiverType = ComponentReceiverType.None
                End If
            End If
        End Sub

        ''' <summary>
        ''' 设置这个组件的消息循环更新响应方式
        ''' </summary>
        ''' <param name="canListen">是否响应</param>
        ''' <remarks></remarks>
        Public Sub ListenMessage(canListen As Boolean)
            If canListen Then
                ComponentMessageReceiver.Add(Me)
                If _receiverType = ComponentReceiverType.None Then
                    _receiverType = ComponentReceiverType.MessageOnly
                Else
                    _receiverType = ComponentReceiverType.Both
                End If
            Else
                ComponentMessageReceiver.Remove(Me)
                If _receiverType = ComponentReceiverType.Both Then
                    _receiverType = ComponentReceiverType.LoopOnly
                Else
                    _receiverType = ComponentReceiverType.None
                End If
            End If
        End Sub

        ''' <summary>
        ''' 添加一个关联对象
        ''' </summary>
        ''' <param name="element">要设置到的对象</param>
        ''' <remarks></remarks>
        Friend Sub Bind(element As FrameworkElement)
            If Not _elementList.Contains(element) Then _elementList.Add(element)
        End Sub

        ''' <summary>
        ''' 删除一个关联对象
        ''' </summary>
        ''' <param name="element"></param>
        ''' <remarks></remarks>
        Friend Sub Unbind(element As FrameworkElement)
            If _elementList.Contains(element) Then _elementList.Remove(element)
        End Sub
    End Class
End Namespace