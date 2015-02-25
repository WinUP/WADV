Imports System.Windows
Imports WADV.ChoiceModule
Imports System.Windows.Controls
Imports System.Windows.Media.Animation

''' <summary>
''' 渐显显示效果
''' </summary>
''' <remarks></remarks>
Public Class FadeIn : Inherits BaseShow


    Public Sub New(choices() As Button)
        MyBase.New(choices)
        choices(0).Dispatcher.Invoke(Sub()
                                         For Each choice In choices
                                             choice.Opacity = 0
                                         Next
                                     End Sub)
        IsOver = False
    End Sub

    Public Overrides Sub Render()
        Dim duration = New Duration(TimeSpan.FromMilliseconds(200))
        For i = 0 To Choices.Length - 1
            Dim animation As New DoubleAnimation(1.0, duration)
            animation.BeginTime = TimeSpan.FromMilliseconds(0 + 50 * i)
            If i = Choices.Length - 1 Then AddHandler animation.Completed, Sub()
                                                                               IsOver = True
                                                                               SendMessage()
                                                                           End Sub
            Choices(i).BeginAnimation(Button.OpacityProperty, animation)
        Next
    End Sub

End Class
