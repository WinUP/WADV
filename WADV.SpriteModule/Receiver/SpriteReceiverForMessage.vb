Imports System.Windows

Namespace Receiver
    ''' <summary>
    ''' 精灵消息循环接收器
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class SpriteReceiverForMessage
        Private ReadOnly _target As FrameworkElement
        Private ReadOnly _onMessage As Func(Of FrameworkElement, String, Boolean)

        ''' <summary>
        ''' 获得一个精灵消息循环接收器
        ''' </summary>
        ''' <param name="target">目标精灵</param>
        ''' <param name="onMessage">更新函数</param>
        ''' <remarks></remarks>
        Friend Sub New(target As FrameworkElement, onMessage As Func(Of FrameworkElement, String, Boolean))
            _target = target
            _onMessage = onMessage
        End Sub

        ''' <summary>
        ''' 确定接收器对应的精灵是否为指定精灵
        ''' </summary>
        ''' <param name="target">要验证的精灵</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function TargetEquals(target As FrameworkElement) As Boolean
            Return _target.Name = target.Name
        End Function

        ''' <summary>
        ''' 获取关联精灵
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function GetTarget() As FrameworkElement
            Return _target
        End Function

        ''' <summary>
        ''' 执行更新
        ''' </summary>
        ''' <param name="message">收到的消息</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function OnMessage(message As String) As Boolean
            Return _onMessage.Invoke(_target, message)
        End Function
    End Class
End Namespace