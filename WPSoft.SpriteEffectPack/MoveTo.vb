Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media.Animation
Imports WADV.SpriteModule.Effect

''' <summary>
''' 将精灵移动到指定位置的动画
''' 参数列表：目标X, 目标Y, 消耗帧数, [缓动类型], [缓动参数]
''' 缓动支持：circle, cubic, exp, quadratic, quartic, quintic, sine
''' 缓动参数：in, out, both
''' 循环动画：支持
''' </summary>
''' <remarks></remarks>
Public Class MoveTo : Inherits BaseEffect
    Private _original As Thickness

    Public Sub New(target As FrameworkElement, variable As Object())
        MyBase.New(target, variable)
    End Sub

    Public Overrides Sub Render()
        Dim animation As New ThicknessAnimation(New Thickness(Params(0), Params(1), 0, 0), New Duration(ToTime(Params(2))))
        Dim ease As EasingFunctionBase
        Select Case CStr(Params(3))
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
        Select Case CStr(Params(4))
            Case "in"
                ease.EasingMode = EasingMode.EaseIn
            Case "out"
                ease.EasingMode = EasingMode.EaseOut
            Case "both"
                ease.EasingMode = EasingMode.EaseInOut
        End Select
        animation.EasingFunction = ease
        AddHandler animation.Completed, AddressOf Animation_Finished
        _original = ImageContent.Margin
        ImageContent.BeginAnimation(Panel.MarginProperty, animation)
    End Sub

    Protected Overrides Sub ReRender()
        ImageContent.Margin = _original
        Render()
    End Sub

End Class
