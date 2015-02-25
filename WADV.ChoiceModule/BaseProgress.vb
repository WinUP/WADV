Imports System.Windows
Imports System.Windows.Controls

Public Class BaseProgress : Implements IProgressEffect
    Protected ReadOnly Choices() As Button
    Protected WaitFrame As Integer
    Private _answer As String

    Public Sub New(choices() As Button, waitFrame As Integer)
        Me.Choices = choices
        Me.WaitFrame = waitFrame
        _answer = ""
        choices(0).Dispatcher.Invoke(Sub()
                                         For Each choice In choices
                                             AddHandler choice.Click, AddressOf Button_Click
                                         Next
                                     End Sub)
    End Sub

    Public Overridable Function Logic() As Boolean Implements IProgressEffect.Logic
        If WaitFrame > 0 Then WaitFrame -= 1
        If WaitFrame = 0 OrElse _answer <> "" Then Return False
        Return True
    End Function

    Public Overridable Sub Render() Implements IProgressEffect.Render
    End Sub

    Public Function GetAnswer() As String Implements IProgressEffect.GetAnswer
        Return _answer
    End Function

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        _answer = DirectCast(sender, Button).Content
        MessageAPI.SendSync("[CHOICE]USER_CLICKED")
    End Sub

End Class