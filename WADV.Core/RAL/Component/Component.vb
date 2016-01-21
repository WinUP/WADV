Imports System.Collections.ObjectModel
Imports WADV.Core.RAL.Tool

Namespace RAL.Component
    ''' <summary>
    ''' 组件基础类
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class Component : Implements IDisposable
        Private ReadOnly _elementList As New List(Of Sprite)

        ''' <summary>
        ''' 当与精灵的连接断开时执行的操作
        ''' </summary>
        ''' <param name="sprite">目标精灵</param>
        ''' <param name="isFromClear">这个操作是否由全部清除操作发起</param>
        ''' <returns>允许断开绑定则返回True，否则返回False</returns>
        ''' <remarks></remarks>
        Protected Friend Overridable Function BeforeUnbinding(sprite As Sprite, Optional isFromClear As Boolean = False) As Boolean
            Return True
        End Function

        ''' <summary>
        ''' 当与精灵连接时执行的操作
        ''' </summary>
        ''' <param name="sprite">目标精灵</param>
        ''' <returns>允许继续绑定则返回True，否则返回False</returns>
        ''' <remarks></remarks>
        Protected Friend Overridable Function BeforeBinding(sprite As Sprite) As Boolean
            Return True
        End Function

        ''' <summary>
        ''' 当与精灵的连接被强制断开时执行的操作
        ''' </summary>
        ''' <param name="sprite">目标精灵</param>
        ''' <remarks></remarks>
        Protected Friend Overridable Sub ForceUnbinding(sprite As Sprite)
        End Sub

        ''' <summary>
        ''' 当某个连接到的精灵绑定到场景时执行的操作
        ''' </summary>
        ''' <param name="sprite">目标精灵</param>
        ''' <param name="scene">目标场景</param>
        Protected Friend Overridable Sub BindToScene(sprite As Sprite, scene As Scene)
        End Sub

        ''' <summary>
        ''' 当某个连接到的精灵从场景解除绑定时执行的操作
        ''' </summary>
        ''' <param name="sprite">目标精灵</param>
        ''' <param name="scene">目标场景</param>
        Protected Friend Overridable Sub UnbindFromScene(sprite As Sprite, scene As Scene)
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
        ''' <remarks>只有当ReceiverType为MessageServiceOnly或Both时该函数才会执行</remarks>
        Protected Friend Overridable Sub MessageOnReceiver(message As String)
        End Sub

        ''' <summary>
        ''' 当组件的资源被释放时执行的操作
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Overridable Sub Dispose() Implements IDisposable.Dispose
        End Sub

        ''' <summary>
        ''' 获取连接到该组件的所有精灵
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property BindedElements As ReadOnlyCollection(Of Sprite)
            Get
                Return _elementList.AsReadOnly
            End Get
        End Property

        ''' <summary>
        ''' 确定组件是否监听游戏循环或消息循环
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ListenerType As ComponentListenerType = ComponentListenerType.None

        ''' <summary>
        ''' 设置组件是否监听游戏循环或消息循环
        ''' </summary>
        ''' <param name="target">监听类型</param>
        ''' <remarks></remarks>
        Public Sub Listen(target As ComponentListenerType)
            Select Case target
                Case ComponentListenerType.Both
                    If ListenerType <> ComponentListenerType.Both Then
                        If ListenerType <> ComponentListenerType.LoopingOnly Then
                            ComponentLoopReceiver.Add(Me)
                        End If
                        If ListenerType <> ComponentListenerType.MessageServiceOnly Then
                            ComponentMessageReceiver.Add(Me)
                        End If
                    End If
                Case ComponentListenerType.LoopingOnly
                    If ListenerType = ComponentListenerType.Both OrElse ListenerType = ComponentListenerType.MessageServiceOnly Then
                        ComponentMessageReceiver.Remove(Me)
                    End If
                    If ListenerType = ComponentListenerType.None Then
                        ComponentLoopReceiver.Add(Me)
                    End If
                Case ComponentListenerType.MessageServiceOnly
                    If ListenerType = ComponentListenerType.Both OrElse ListenerType = ComponentListenerType.LoopingOnly Then
                        ComponentLoopReceiver.Remove(Me)
                    End If
                    If ListenerType = ComponentListenerType.None Then
                        ComponentMessageReceiver.Add(Me)
                    End If
                Case ComponentListenerType.None
                    If ListenerType = ComponentListenerType.Both OrElse ListenerType = ComponentListenerType.LoopingOnly Then
                        ComponentLoopReceiver.Remove(Me)
                    End If
                    If ListenerType = ComponentListenerType.Both OrElse ListenerType = ComponentListenerType.MessageServiceOnly Then
                        ComponentMessageReceiver.Remove(Me)
                    End If
            End Select
            _ListenerType = target
        End Sub

        ''' <summary>
        ''' 连接到一个精灵
        ''' </summary>
        ''' <param name="element">要连接的精灵</param>
        ''' <remarks></remarks>
        Friend Sub Bind(element As Sprite)
            If Not _elementList.Contains(element) Then _elementList.Add(element)
        End Sub

        ''' <summary>
        ''' 断开与一个精灵的连接
        ''' </summary>
        ''' <param name="element">要断开连接的精灵</param>
        ''' <remarks></remarks>
        Friend Sub Unbind(element As Sprite)
            If _elementList.Contains(element) Then _elementList.Remove(element)
        End Sub
    End Class
End Namespace