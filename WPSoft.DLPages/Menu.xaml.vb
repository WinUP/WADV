Class Menu

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Config.RoteConfig.DetectedRote = "0"
        WindowAPI.LoadObjectAsync(New Story)
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Dim brush As New ImageBrush(New BitmapImage(New Uri(PathAPI.GetPath(WADV.AppCore.Path.PathFunction.PathType.Resource, "image\Title.png"))))
        brush.Stretch = Stretch.Uniform
        Logo.Background = brush
        'Dim menuBrush As New ImageBrush(New BitmapImage(New Uri(PathAPI.GetPath(WADV.AppCore.Path.PathFunction.PathType.Resource, "image\back_shape.png"))))
        'menuBrush.Stretch = Stretch.UniformToFill
        'menuBrush.TileMode = TileMode.Tile
        'menuBrush.ViewportUnits = BrushMappingMode.Absolute
        'menuBrush.Viewport = New Rect(0, 0, 62, 54)
        'MenuGrid.Background = menuBrush
    End Sub
End Class
