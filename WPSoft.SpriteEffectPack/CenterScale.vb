﻿Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Animation

Public Class CenterScale : Inherits WADV.SpriteModule.BaseEffect

    Public Sub New(name As String, variable As Object())
        MyBase.New(name, variable)
    End Sub

    Public Overrides Sub Render()
        Dim target As New ScaleTransform(1.0, 1.0)
        target.CenterX = ImageContent.Width / 2.0
        target.CenterY = ImageContent.Height / 2.0
        ImageContent.RenderTransform = target
        Dim targetStoryBoard As New Storyboard
        Dim animationX As New DoubleAnimation(Params(0), New Duration(LoopAPI.TranslateToTime(Params(2))))
        Dim animationY As New DoubleAnimation(Params(1), New Duration(LoopAPI.TranslateToTime(Params(2))))
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
        animationX.EasingFunction = ease
        animationY.EasingFunction = ease
        animationX.SetValue(Storyboard.TargetProperty, ImageContent)
        animationX.SetValue(Storyboard.TargetPropertyProperty, New PropertyPath("(0).(1)", New DependencyProperty() {Panel.RenderTransformProperty, ScaleTransform.ScaleXProperty}))
        animationY.SetValue(Storyboard.TargetProperty, ImageContent)
        animationY.SetValue(Storyboard.TargetPropertyProperty, New PropertyPath("(0).(1)", New DependencyProperty() {Panel.RenderTransformProperty, ScaleTransform.ScaleYProperty}))
        targetStoryBoard.Children.Add(animationX)
        targetStoryBoard.Children.Add(animationY)
        AddHandler targetStoryBoard.Completed, AddressOf Animation_Finished
        targetStoryBoard.Begin()
    End Sub

End Class
