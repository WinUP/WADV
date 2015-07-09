Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media.Animation
Imports WADV.SpriteModule.Effect

''' <summary>
''' 将精灵透明度从0%过渡到100%的动画
''' 参数列表：消耗帧数, [缓动类型], [缓动参数]
''' 缓动支持：circle, cubic, exp, quadratic, quartic, quintic, sine
''' 缓动参数：in, out, both
''' 循环动画：支持
''' </summary>
''' <remarks></remarks>
Public Class FadeIn : Inherits BaseEffect

    Public Sub New(target As FrameworkElement, variable As Object())
        MyBase.New(target, variable)
        Invoke(Sub() ImageContent.Opacity = 0.0)
    End Sub

    Public Overrides Sub Render()
        Dim animation As New DoubleAnimation(1.0, New Duration(ToTime(Params(0))))
        Dim ease As EasingFunctionBase
        Select Case CStr(Params(1))
            Case "circle"
                ease = New CircleEase
            Case "cubic"
                ease = New CubicEase
            Case "exp"
                ease = New ExponentialEase
            Case "quadratic"
                ease = New QuadraticEase
            Case "quartic"
                ease = New QuarticEase
            Case "quintic"
                ease = New QuinticEase
            Case "sine"
                ease = New SineEase
            Case Else
                ease = Nothing
        End Select
        Select Case CStr(Params(2))
            Case "in"
                ease.EasingMode = EasingMode.EaseIn
            Case "out"
                ease.EasingMode = EasingMode.EaseOut
            Case "both"
                ease.EasingMode = EasingMode.EaseInOut
        End Select
        animation.EasingFunction = ease
        AddHandler animation.Completed, AddressOf Animation_Finished
        ImageContent.BeginAnimation(Panel.OpacityProperty, animation)
    End Sub

    Protected Overrides Sub ReRender()
        ImageContent.Opacity = 0.0
        Render()
    End Sub

End Class
