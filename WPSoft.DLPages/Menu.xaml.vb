Imports System.Windows.Media.Animation

Class Menu

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Config.RoteConfig.DetectedRote = "0"
        WindowAPI.LoadObjectAsync(New Story)
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Dim brush As New ImageBrush(New BitmapImage(New Uri(PathAPI.GetPath(WADV.AppCore.Path.PathFunction.PathType.Resource, "image\Title.png"))))
        brush.Stretch = Stretch.Uniform
        Logo.Background = brush
        brush = New ImageBrush(New BitmapImage(New Uri(PathAPI.GetPath(WADV.AppCore.Path.PathFunction.PathType.Resource, "image\chain.png"))))
        brush.Stretch = Stretch.UniformToFill
        brush.TileMode = TileMode.Tile
        brush.ViewportUnits = BrushMappingMode.Absolute
        brush.Viewport = New Rect(0, 0, 96, 61)
        ChainImage.Background = brush
        brush = New ImageBrush(New BitmapImage(New Uri(PathAPI.GetPath(WADV.AppCore.Path.PathFunction.PathType.Resource, "image\gear.png"))))
        brush.Stretch = Stretch.Uniform
        brush.TileMode = TileMode.None
        GearImage.Background = brush
    End Sub
End Class
