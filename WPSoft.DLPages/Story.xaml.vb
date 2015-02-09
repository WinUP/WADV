Class Story

    Private Sub Story_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        WADV.MediaModule.API.SoundAPI.StopNearlyBGM()
        If Config.RoteConfig.DetectedRote <> "" Then
            ScriptAPI.RunFileAsync("logic\" & Config.RoteConfig.DetectedRote & ".lua")
        End If
    End Sub
End Class
