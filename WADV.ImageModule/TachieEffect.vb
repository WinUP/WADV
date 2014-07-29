﻿Imports System.Windows.Controls
Imports System.Windows.Media.Animation
Imports System.Windows.Media

Namespace TachieEffect

    Public MustInherit Class BaseEffect
        Private imageContent As Image
        Private effectDuration As Integer
        Protected running As Boolean = False
        Protected ease As Boolean
        Protected complete As Boolean = False

        Public Sub New(image As Image, duration As Integer, ease As Boolean)
            imageContent = image
            Me.effectDuration = duration
            Me.ease = ease
        End Sub

        Protected Friend ReadOnly Property Image As Image
            Get
                Return imageContent
            End Get
        End Property

        Protected Friend ReadOnly Property Duration As Integer
            Get
                Return effectDuration
            End Get
        End Property

        Protected Friend ReadOnly Property IsComplete As Boolean
            Get
                Return complete
            End Get
        End Property

        Protected Friend ReadOnly Property IsEase As Boolean
            Get
                Return ease
            End Get
        End Property

        Protected Friend MustOverride Sub RenderingUI()

    End Class

    Public Class NoEffect : Inherits BaseEffect

        Public Sub New(image As Image, duration As Integer, Optional ease As Boolean = False, Optional params() As Object = Nothing)
            MyBase.New(image, duration, ease)
        End Sub

        Protected Friend Overrides Sub RenderingUI()
            Image.Opacity = 1
            Image.Visibility = Windows.Visibility.Visible
            complete = True
        End Sub

    End Class

    Public Class FadeInEffect : Inherits BaseEffect

        Public Sub New(image As Image, duration As Integer, ease As Boolean, Optional params() As Object = Nothing)
            MyBase.New(image, duration, ease)
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               image.Opacity = 0
                                               image.Visibility = Windows.Visibility.Visible
                                           End Sub)
        End Sub

        Protected Friend Overrides Sub RenderingUI()
            If running Then Exit Sub
            Dim animation As New DoubleAnimation(1.0, New Windows.Duration(New TimeSpan(0, 0, 0, 0, Duration / LoopingAPI.GetFrame * 1000)))
            If ease Then animation.EasingFunction = New QuadraticEase
            AddHandler animation.Completed, Sub() complete = True
            Image.BeginAnimation(Windows.Controls.Image.OpacityProperty, animation)
            running = True
        End Sub

    End Class

    Public Class FadeOutEffect : Inherits BaseEffect

        Public Sub New(image As Image, duration As Integer, ease As Boolean, Optional params() As Object = Nothing)
            MyBase.New(image, duration, ease)
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               image.Opacity = 1.0
                                               image.Visibility = Windows.Visibility.Visible
                                           End Sub)
        End Sub

        Protected Friend Overrides Sub RenderingUI()
            If running Then Exit Sub
            Dim animation As New DoubleAnimation(0, New Windows.Duration(New TimeSpan(0, 0, 0, 0, Duration / LoopingAPI.GetFrame * 1000)))
            If ease Then animation.EasingFunction = New QuadraticEase
            AddHandler animation.Completed, Sub() complete = True
            Image.BeginAnimation(Windows.Controls.Image.OpacityProperty, animation)
            running = True
        End Sub

    End Class

    Public Class MoveFromEffect : Inherits BaseEffect
        Private target As Windows.Thickness

        Public Sub New(image As Image, duration As Integer, ease As Boolean, params() As Object)
            MyBase.New(image, duration, ease)
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               target = New Windows.Thickness(image.Margin.Left, image.Margin.Top, 0, 0)
                                               image.Margin = New Windows.Thickness(params(0), params(1), 0, 0)
                                               image.Opacity = 1.0
                                               image.Visibility = Windows.Visibility.Visible
                                           End Sub)
        End Sub

        Protected Friend Overrides Sub RenderingUI()
            If running Then Exit Sub
            Dim animation As New ThicknessAnimation(target, New Windows.Duration(New TimeSpan(0, 0, 0, 0, Duration / LoopingAPI.GetFrame * 1000)))
            If ease Then animation.EasingFunction = New QuadraticEase
            AddHandler animation.Completed, Sub() complete = True
            Image.BeginAnimation(Windows.Controls.Image.MarginProperty, animation)
            running = True
        End Sub

    End Class

    Public Class MoveToEffect : Inherits BaseEffect
        Private target As Windows.Thickness

        Public Sub New(image As Image, duration As Integer, ease As Boolean, params() As Object)
            MyBase.New(image, duration, ease)
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               target = New Windows.Thickness(params(0), params(1), 0, 0)
                                               image.Opacity = 1.0
                                               image.Visibility = Windows.Visibility.Visible
                                           End Sub)
        End Sub

        Protected Friend Overrides Sub RenderingUI()
            If running Then Exit Sub
            Dim animation As New ThicknessAnimation(target, New Windows.Duration(New TimeSpan(0, 0, 0, 0, Duration / LoopingAPI.GetFrame * 1000)))
            If ease Then animation.EasingFunction = New QuadraticEase
            AddHandler animation.Completed, Sub() complete = True
            Image.BeginAnimation(Windows.Controls.Image.MarginProperty, animation)
            running = True
        End Sub

    End Class

    Public Class RotateEffect : Inherits BaseEffect
        Private transform As RotateTransform
        Private angel As Double

        Public Sub New(image As Image, duration As Integer, ease As Boolean, params() As Object)
            MyBase.New(image, duration, ease)
            Me.angel = params(0)
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               transform = New RotateTransform(0, params(1), params(2))
                                               image.RenderTransform = transform
                                           End Sub)
        End Sub

        Protected Friend Overrides Sub RenderingUI()
            Dim animation As New DoubleAnimation(angel, New Windows.Duration(New TimeSpan(0, 0, 0, 0, Duration / LoopingAPI.GetFrame * 1000)))
            If ease Then animation.EasingFunction = New QuadraticEase
            AddHandler animation.Completed, Sub() complete = True
            transform.BeginAnimation(RotateTransform.AngleProperty, animation)
            running = True
        End Sub

    End Class

End Namespace
