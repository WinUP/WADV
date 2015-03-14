Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media.Animation
Imports WADV.AppCore.API

''' <summary>
''' 渐显效果
''' fadeTime, ease[circle, cubic, exp, quartic, quintic, sine, noease], easeMode[in, out, both]
''' </summary>
''' <remarks></remarks>
Public Class FadeOut : Inherits WADV.SpriteModule.BaseEffect

    Public Sub New(name As String, variable As Object())
        MyBase.New(name, variable)
        WindowAPI.InvokeSync(Sub() ImageContent.Opacity = 1.0)
    End Sub

    Public Overrides Sub Render()
        Dim animation As New DoubleAnimation(0.0, New Duration(LoopAPI.TranslateToTime(Params(0))))
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

End Class
