Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media.Animation

Public Class MoveTo : Inherits WADV.SpriteModule.BaseEffect

    Public Sub New(name As String, variable As Object())
        MyBase.New(name, variable)
    End Sub

    Public Overrides Sub Render()
        Dim animation As New ThicknessAnimation(New Thickness(Params(0), Params(1), 0, 0), New Duration(LoopAPI.TranslateToTime(Params(2))))
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
        ImageContent.BeginAnimation(Panel.MarginProperty, animation)
    End Sub

End Class
