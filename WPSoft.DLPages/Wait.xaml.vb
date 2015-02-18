Class Wait

    Private Sub Wait_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim image As New ImageBrush(New BitmapImage(New Uri(PathAPI.GetPath(PathType.Resource, "image\black_logo.png"))))
        image.Stretch = Stretch.Uniform
        CenterImage.Background = image
    End Sub
End Class
