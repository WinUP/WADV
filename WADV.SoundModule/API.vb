Imports System.Windows.Media
Imports WADV.SoundModule.Config

Namespace API

    Public Class ConfigAPI

        Public Shared Function GetBackgroundVolume() As Double
            Return SoundConfig.Background
        End Function

        Public Shared Function GetReadingVolume() As Double
            Return SoundConfig.Reading
        End Function

        Public Shared Function GetEffectVolume() As Double
            Return SoundConfig.Effect
        End Function

        Public Shared Sub SetBackgroundVolume(value As Double)
            SoundConfig.Background = value
        End Sub

        Public Shared Sub SetReadingVolume(value As Double)
            SoundConfig.Reading = value
        End Sub

        Public Shared Sub SetEffectVolume(value As Double)
            SoundConfig.Effect = value
        End Sub

    End Class

    Public Class SoundAPI

        Public Shared Sub PlaySound(name As String, fileName As String, type As Sound.SoundType, cycle As Boolean, count As Integer)
            Dim playerContent As New MediaPlayer
            playerContent.Open(New Uri(AppCore.API.URLAPI.CombineURL(AppCore.API.URLAPI.GetResourceURL, fileName)))
            If type = Sound.SoundType.Background Then
                playerContent.Volume = SoundConfig.Background
            ElseIf type = Sound.SoundType.Effect Then
                playerContent.Volume = SoundConfig.Effect
            Else
                playerContent.Volume = SoundConfig.Reading
            End If
            Dim soundContent As New Sound With {.Content = playerContent, .Cycle = cycle, .CycleCount = count, .Type = type, .Name = name}
            SoundList.AddSound(soundContent)
            playerContent.Play()
        End Sub

        Public Shared Sub PlayBGM(name As String, fileName As String)
            PlaySound(name, fileName, Sound.SoundType.Background, True, -1)
        End Sub

        Public Shared Sub PlayReading(name As String, fileName As String)
            PlaySound(name, fileName, Sound.SoundType.Reading, False, 0)
        End Sub

        Public Shared Sub PlayEffect(name As String, fileName As String)
            PlaySound(name, fileName, Sound.SoundType.Effect, False, 0)
        End Sub

        Public Shared Function GetSound(name As String) As Sound
            Return SoundList.FindSound(name)
        End Function

        Public Shared Sub StopSound(name As String)
            SoundList.DeleteSound(name)
        End Sub

        Public Shared Sub PauseSound(name As String)
            Dim soundContent = SoundList.FindSound(name)
            If soundContent IsNot Nothing Then soundContent.Content.Pause()
        End Sub

        Public Shared Sub ResumeSound(name As String)
            Dim soundContent = SoundList.FindSound(name)
            If soundContent IsNot Nothing Then soundContent.Content.Play()
        End Sub

        Public Shared Sub ChangeSoundVolume(name As String, volume As Double)
            Dim soundContent = SoundList.FindSound(name)
            If soundContent IsNot Nothing Then soundContent.Content.Volume = volume
        End Sub

    End Class

End Namespace