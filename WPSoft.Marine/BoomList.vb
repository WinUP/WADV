Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging

Friend NotInheritable Class BoomList
    Friend Shared ReadOnly List As New List(Of Canvas)

    Friend Shared Sub Send()
        Dim target = SpriteAPI.[New](New Random().Next)
        target.Background = New ImageBrush(New BitmapImage(PathAPI.GetUri(WADV.Core.PathType.Resource, "boom.png")))
        target.Margin = New Thickness(0, New Random().Next(60, 100), 0, 0)
        WindowAPI.InvokeAsync(Sub() WindowAPI.GetRoot(Of Grid).Children.Add(target))
        List.Add(target)
        Dim animation As New ThicknessAnimation(New Thickness(260, target.Margin.Top, 0, 0), New Duration(TimeSpan.FromSeconds(10)))
        target.BeginAnimation(Canvas.MarginProperty, animation)
    End Sub

End Class
