Imports System.Windows

Namespace Receiver
    ''' <summary>
    ''' 精灵游戏循环接收器
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class SpriteReceiverForLoop
        Private ReadOnly _target As FrameworkElement
        Private ReadOnly _onLogic As Func(Of FrameworkElement, Integer, Boolean)
        Private ReadOnly _onRender As Func(Of FrameworkElement, Boolean)

        ''' <summary>
        ''' 获得一个精灵游戏循环接收器
        ''' </summary>
        ''' <param name="target">目标精灵</param>
        ''' <param name="onLogic">逻辑更新函数</param>
        ''' <param name="onRender">渲染函数</param>
        ''' <remarks></remarks>
        Friend Sub New(target As FrameworkElement, onLogic As Func(Of FrameworkElement, Integer, Boolean), onRender As Func(Of FrameworkElement, Boolean))
            _target = target
            _onLogic = onLogic
            _onRender = onRender
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
        ''' 执行逻辑更新
        ''' </summary>
        ''' <param name="frame">当前总帧数</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function OnLogic(frame As Integer) As Boolean
            Return _onLogic.Invoke(_target, frame)
        End Function

        ''' <summary>
        ''' 执行渲染
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function OnRender() As Boolean
            Return _onRender.Invoke(_target)
        End Function
    End Class
End Namespace
