Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media.Animation

Public Class FadeOut : Inherits WADV.ChoiceModule.BaseHide
    Public Sub New(choices() As Button)
        MyBase.New(choices)
    End Sub

    Public Overrides Sub Render()
        Dim duration = New Duration(TimeSpan.FromMilliseconds(200))
        For i = 0 To Choices.Length - 1
            Dim animation As New DoubleAnimation(0.0, duration)
            animation.BeginTime = TimeSpan.FromMilliseconds(0 + 50 * i)
            If i = Choices.Length - 1 Then AddHandler animation.Completed, Sub()
                                                                               IsOver = True
                                                                               SendFinished()
                                                                           End Sub
            Choices(i).BeginAnimation(Button.OpacityProperty, animation)
        Next
    End Sub
End Class
