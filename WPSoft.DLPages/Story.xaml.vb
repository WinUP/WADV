﻿Class Story

    Private Sub Story_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If Config.RoteConfig.DetectedRote <> "" Then
            WADV.Core.API.RunFileAsync("logic\" & Config.RoteConfig.DetectedRote & ".lua")
        End If
    End Sub
End Class
