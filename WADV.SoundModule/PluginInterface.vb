Imports System.Reflection
Imports WADV.MediaModule.AudioCore

Namespace PluginInterface

    Public Class Looping : Implements AppCore.Plugin.ILoopReceiver
        Protected Friend Shared isLooping As Boolean = False

        Public Function StartLooping(frame As Integer) As Boolean Implements AppCore.Plugin.ILoopReceiver.Logic
            For Each item In PlayerList.deleteList
                PlayerList.soundList(item).Dispose()
                PlayerList.soundList.Remove(item)
            Next
            PlayerList.deleteList.Clear()
            If PlayerList.soundList.Count = 0 Then
                isLooping = False
                Return False
            End If
            Dim player As Player
            For Each item In PlayerList.soundList
                player = item.Value
                If player.Duration = player.Position Then
                    If (Not player.Cycle) OrElse (player.Cycle AndAlso player.CycleCount = 0) Then PlayerList.DeletePlayer(item.Key)
                    If Not (player.Cycle AndAlso player.CycleCount = -1) Then player.CycleCount -= 1
                    player.Position = 0
                    player.Play()
                End If
            Next
            Return True
        End Function

        Public Sub StartRendering(window As Windows.Window) Implements AppCore.Plugin.ILoopReceiver.Render

        End Sub

    End Class

End Namespace