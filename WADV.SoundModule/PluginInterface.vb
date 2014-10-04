Imports System.Reflection
Imports WADV.MediaModule.AudioCore

Namespace PluginInterface

    Public Class Initialise : Implements AppCore.Plugin.IInitialise

        Public Function StartInitialising() As Boolean Implements AppCore.Plugin.IInitialise.StartInitialising
            Config.SoundConfig.ReadConfigFile()
            Return True
        End Function

    End Class

    Public Class Script : Implements AppCore.Plugin.IScript

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements AppCore.Plugin.IScript.StartRegisting
            ScriptAPI.RegisterFunction(Assembly.GetExecutingAssembly.GetTypes, "WADV.MediaModule.API", "MM")
        End Sub

    End Class

    Public Class Looping : Implements AppCore.Plugin.ILooping
        Protected Friend Shared isLooping As Boolean = False

        Public Function StartLooping(frame As Integer) As Boolean Implements AppCore.Plugin.ILooping.StartLooping
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

        Public Sub StartRendering(window As Windows.Window) Implements AppCore.Plugin.ILooping.StartRendering

        End Sub

    End Class

End Namespace