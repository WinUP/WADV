Imports System.Windows.Controls
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
            RenderOptions.SetBitmapScalingMode(Image, BitmapScalingMode.Linear)
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
            RenderOptions.SetBitmapScalingMode(Image, BitmapScalingMode.Linear)
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
            RenderOptions.SetBitmapScalingMode(Image, BitmapScalingMode.Linear)
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
            RenderOptions.SetBitmapScalingMode(Image, BitmapScalingMode.Linear)
            Image.BeginAnimation(Windows.Controls.Image.MarginProperty, animation)
            running = True
        End Sub

    End Class

    Public Class RotateEffect : Inherits BaseEffect
        Private transform As RotateTransform
        Private angel, cx, cy As Double
        Private completeCount As Integer = 0
        Private needAngel, needCX, needCY As Boolean

        Public Sub New(image As Image, duration As Integer, ease As Boolean, params() As Object)
            MyBase.New(image, duration, ease)
            angel = params(0)
            cx = params(1)
            cy = params(2)
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               If TypeOf image.RenderTransform Is RotateTransform Then
                                                   transform = image.RenderTransform
                                                   If transform.Angle <> angel Then needAngel = True
                                                   If transform.CenterX <> image.Width * cx Then needCX = True
                                                   If transform.CenterY <> image.Height * cy Then needCY = True
                                               Else
                                                   transform = New RotateTransform(0, image.Width * cx, image.Height * cy)
                                                   needAngel = True
                                                   image.RenderTransform = transform
                                               End If
                                           End Sub)
        End Sub

        Protected Friend Overrides Sub RenderingUI()
            Dim board As New Storyboard
            If needAngel Then
                Dim animationAngel As New DoubleAnimation(angel, New Windows.Duration(New TimeSpan(0, 0, 0, 0, Duration / LoopingAPI.GetFrame * 1000)))
                If ease Then animationAngel.EasingFunction = New QuadraticEase
                AddHandler animationAngel.Completed, AddressOf SetComplete
                board.Children.Add(animationAngel)
                Storyboard.SetTargetName(animationAngel, Image.Name)
                Storyboard.SetTargetProperty(animationAngel, New Windows.PropertyPath("(0).(1)", New Windows.DependencyProperty() {Windows.Controls.Image.RenderTransformProperty, RotateTransform.AngleProperty}))
            End If
            If needCX Then
                Dim animationCX As New DoubleAnimation(cx, New Windows.Duration(New TimeSpan(0, 0, 0, 0, Duration / LoopingAPI.GetFrame * 1000)))
                If ease Then animationCX.EasingFunction = New QuadraticEase
                AddHandler animationCX.Completed, AddressOf SetComplete
                board.Children.Add(animationCX)
                Storyboard.SetTargetName(animationCX, Image.Name)
                Storyboard.SetTargetProperty(animationCX, New Windows.PropertyPath("(0).(1)", New Windows.DependencyProperty() {Windows.Controls.Image.RenderTransformProperty, RotateTransform.CenterXProperty}))
            End If
            If needCY Then
                Dim animationCY As New DoubleAnimation(cy, New Windows.Duration(New TimeSpan(0, 0, 0, 0, Duration / LoopingAPI.GetFrame * 1000)))
                If ease Then animationCY.EasingFunction = New QuadraticEase
                AddHandler animationCY.Completed, AddressOf SetComplete
                board.Children.Add(animationCY)
                Storyboard.SetTargetName(animationCY, Image.Name)
                Storyboard.SetTargetProperty(animationCY, New Windows.PropertyPath("(0).(1)", New Windows.DependencyProperty() {Windows.Controls.Image.RenderTransformProperty, RotateTransform.CenterYProperty}))
            End If
            RenderOptions.SetBitmapScalingMode(Image, BitmapScalingMode.NearestNeighbor)
            board.Begin(WindowAPI.GetWindow)
            running = True
        End Sub

        Private Sub SetComplete()
            completeCount += 1
            If completeCount = 3 Then
                running = False
                complete = True
            End If
        End Sub

    End Class

    Public Class ScaleEffect : Inherits BaseEffect
        Private transform As ScaleTransform
        Private x, y, cx, cy As Double
        Private completeCount As Integer = 0
        Private needX, needY, needCX, needCY As Boolean

        Public Sub New(image As Image, duration As Integer, ease As Boolean, params() As Object)
            MyBase.New(image, duration, ease)
            x = params(0)
            y = params(1)
            cx = params(2)
            cy = params(3)
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               If TypeOf image.RenderTransform Is ScaleTransform Then
                                                   transform = image.RenderTransform
                                                   If transform.CenterX <> image.Width * cx Then needCX = True
                                                   If transform.CenterY <> image.Height * cy Then needCY = True
                                                   If transform.ScaleX <> x Then needX = True
                                                   If transform.ScaleY <> y Then needY = True
                                               Else
                                                   transform = New ScaleTransform(1, 1, image.Width * cx, image.Height * cy)
                                                   needX = True
                                                   needY = True
                                                   image.RenderTransform = transform
                                               End If
                                           End Sub)
        End Sub

        Protected Friend Overrides Sub RenderingUI()
            Dim board As New Storyboard
            If needX Then
                Dim animationX As New DoubleAnimation(x, New Windows.Duration(New TimeSpan(0, 0, 0, 0, Duration / LoopingAPI.GetFrame * 1000)))
                If ease Then animationX.EasingFunction = New QuadraticEase
                AddHandler animationX.Completed, AddressOf SetComplete
                board.Children.Add(animationX)
                Storyboard.SetTargetName(animationX, Image.Name)
                Storyboard.SetTargetProperty(animationX, New Windows.PropertyPath("(0).(1)", New Windows.DependencyProperty() {Windows.Controls.Image.RenderTransformProperty, ScaleTransform.ScaleXProperty}))
            End If
            If needY Then
                Dim animationY As New DoubleAnimation(y, New Windows.Duration(New TimeSpan(0, 0, 0, 0, Duration / LoopingAPI.GetFrame * 1000)))
                If ease Then animationY.EasingFunction = New QuadraticEase
                AddHandler animationY.Completed, AddressOf SetComplete
                board.Children.Add(animationY)
                Storyboard.SetTargetName(animationY, Image.Name)
                Storyboard.SetTargetProperty(animationY, New Windows.PropertyPath("(0).(1)", New Windows.DependencyProperty() {Windows.Controls.Image.RenderTransformProperty, ScaleTransform.ScaleYProperty}))
            End If
            If needCX Then
                Dim animationCX As New DoubleAnimation(cx, New Windows.Duration(New TimeSpan(0, 0, 0, 0, Duration / LoopingAPI.GetFrame * 1000)))
                If ease Then animationCX.EasingFunction = New QuadraticEase
                AddHandler animationCX.Completed, AddressOf SetComplete
                board.Children.Add(animationCX)
                Storyboard.SetTargetName(animationCX, Image.Name)
                Storyboard.SetTargetProperty(animationCX, New Windows.PropertyPath("(0).(1)", New Windows.DependencyProperty() {Windows.Controls.Image.RenderTransformProperty, ScaleTransform.CenterXProperty}))
            End If
            If needCY Then
                Dim animationCY As New DoubleAnimation(cy, New Windows.Duration(New TimeSpan(0, 0, 0, 0, Duration / LoopingAPI.GetFrame * 1000)))
                If ease Then animationCY.EasingFunction = New QuadraticEase
                AddHandler animationCY.Completed, AddressOf SetComplete
                board.Children.Add(animationCY)
                Storyboard.SetTargetName(animationCY, Image.Name)
                Storyboard.SetTargetProperty(animationCY, New Windows.PropertyPath("(0).(1)", New Windows.DependencyProperty() {Windows.Controls.Image.RenderTransformProperty, ScaleTransform.CenterYProperty}))
            End If
            RenderOptions.SetBitmapScalingMode(Image, BitmapScalingMode.NearestNeighbor)
            board.Begin(WindowAPI.GetWindow)
            running = True
        End Sub

        Private Sub SetComplete()
            completeCount += 1
            If completeCount = 4 Then
                running = False
                complete = True
            End If
        End Sub

    End Class

End Namespace
