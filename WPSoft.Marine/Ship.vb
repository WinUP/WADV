Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Friend NotInheritable Class Ship : Inherits WADV.HudModule.MessageHud
    Private _target As Canvas

    Public Sub New()
        MyBase.New(False)
    End Sub

    Public Overrides Sub Init()
        _target = SpriteAPI.[New]("SHIP")
        _target.Margin = New Windows.Thickness(300, 20, 0, 0)
        _target.Background = New ImageBrush(New BitmapImage(PathAPI.GetUri(WADV.Core.PathType.Resource, "ship.png")))
        WindowAPI.InvokeAsync(Sub() WindowAPI.GetRoot(Of Grid).Children.Add(_target))
    End Sub

    Private Sub Move(length As Double)
        Dim margin = _target.Margin
        margin.Left += length
        WindowAPI.InvokeAsync(Sub() _target.Margin = margin)
    End Sub

    Public Overrides Sub Render(message As String)
        If message = "[WINDOW]MOUSE_CLICK" Then
            BoomList.Send()
        ElseIf message = "[WINDOW]LEFT_KEY_PRESS" Then
            Move(-20)
        ElseIf message = "[WINDOW]RIGHT_KEY_PRESS" Then
            Move(20)
        End If
    End Sub

End Class
