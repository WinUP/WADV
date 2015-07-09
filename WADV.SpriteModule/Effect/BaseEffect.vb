Imports System.Windows

Namespace Effect
    ''' <summary>
    ''' 精灵动画基础类，同时也是一个立即完成的无效果的动画
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BaseEffect : Implements IDisposable
        Protected ImageContent As FrameworkElement
        Protected Params() As Object
        Private _isCircle As Boolean
        Private _circleCount As Integer

        ''' <summary>
        ''' 获得一个新的精灵动画
        ''' </summary>
        ''' <param name="target">要应用动画的精灵</param>
        ''' <param name="params">动画参数</param>
        ''' <remarks></remarks>
        Public Sub New(target As FrameworkElement, params As Object())
            ImageContent = target
            Me.Params = params
        End Sub

        ''' <summary>
        ''' 设置动画的循环状态
        ''' </summary>
        ''' <param name="isCircle">是否循环播放</param>
        ''' <param name="count">循环次数(-1为永久循环)</param>
        ''' <remarks></remarks>
        Public Sub SetCircle(isCircle As Boolean, count As Integer)
            _isCircle = isCircle
            _circleCount = count
        End Sub

        ''' <summary>
        ''' 应用动画到精灵
        ''' </summary>
        ''' <remarks></remarks>
        Public Overridable Sub Render()
            Animation_Finished(Me, New EventArgs)
        End Sub

        ''' <summary>
        ''' 重新应用动画到精灵，一般用于动画循环时
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overridable Sub ReRender()
            Animation_Finished(Me, New EventArgs)
        End Sub

        ''' <summary>
        ''' 释放这个动画持有的资源
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Dispose() Implements IDisposable.Dispose
            ImageContent = Nothing
            Params = Nothing
        End Sub

        ''' <summary>
        ''' 动画播放完成的推荐关联事件
        ''' </summary>
        ''' <param name="sender">触发对象</param>
        ''' <param name="e">事件参数</param>
        ''' <remarks></remarks>
        Protected Sub Animation_Finished(sender As Object, e As EventArgs)
            If _isCircle Then
                If _circleCount = 0 Then
                    Message.Send("[SPRITE]EFFECT_PLAY_FINISH")
                    Exit Sub
                End If
                Message.Send("[SPRITE]EFFECT_PLAY_CIRCLE")
                ReRender()
                If _circleCount <> -1 Then _circleCount -= 1
            Else
                Message.Send("[SPRITE]EFFECT_PLAY_FINISH")
            End If
        End Sub

        ''' <summary>
        ''' 等待动画播放结束
        ''' </summary>
        ''' <remarks></remarks>
        Public Overridable Sub Wait()
            Message.Wait("[SPRITE]EFFECT_PLAY_FINISH")
        End Sub
    End Class
End Namespace