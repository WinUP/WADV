Class Story
    Private Sub Story_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If RoadConfig.DetectedRoad <> "" Then
            Script.RunFileAsync("logic\" & RoadConfig.DetectedRoad & ".lua")
        End If
    End Sub
End Class
