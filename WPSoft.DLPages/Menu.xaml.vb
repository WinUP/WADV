Imports WADV.WPF.Renderer

Class Menu
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        RoadConfig.DetectedRoad = "0"
        Dim scene As New XamlScene("Story", New Story)
        Game.Window.Go(scene)
    End Sub
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Dim brush As New ImageBrush(New BitmapImage(API.Path.CombineUri(PathType.Resource, "image\Title.png")))
        brush.Stretch = Stretch.Uniform
        Logo.Background = brush
        brush = New ImageBrush(New BitmapImage(API.Path.CombineUri(PathType.Resource, "image\chain.png")))
        brush.Stretch = Stretch.UniformToFill
        brush.TileMode = TileMode.Tile
        brush.ViewportUnits = BrushMappingMode.Absolute
        brush.Viewport = New Rect(0, 0, 96, 61)
        ChainImage.Background = brush
        brush = New ImageBrush(New BitmapImage(API.Path.CombineUri(PathType.Resource, "image\gear.png")))
        brush.Stretch = Stretch.Uniform
        brush.TileMode = TileMode.None
        GearImage.Background = brush
        MenuMainGrid.Background = New ImageBrush(New BitmapImage(API.Path.CombineUri(PathType.Resource, "image\setting.png")))
    End Sub
End Class
