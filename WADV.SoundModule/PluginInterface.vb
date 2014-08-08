Imports System.Reflection
Imports WADV.MediaModule.AudioCore

Namespace PluginInterface

    Public Class Initialise : Implements AppCore.Plugin.IInitialise

        Public Function StartInitialising() As Boolean Implements AppCore.Plugin.IInitialise.StartInitialising
            Config.SoundConfig.ReadConfigFile()
            Return True
        End Function

    End Class

    Public Class Script : Implements AppCore.Plugin.IScriptFunction

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements AppCore.Plugin.IScriptFunction.StartRegisting
            ScriptAPI.RegisterFunction(Reflection.Assembly.GetExecutingAssembly.GetTypes, "WADV.MediaModule.API", "MM")
        End Sub

    End Class

    Public Class Looping : Implements AppCore.Plugin.ILooping
        Protected Friend Shared isLooping As Boolean = False

        Public Function StartLooping() As Boolean Implements AppCore.Plugin.ILooping.StartLooping
            For Each tmpID In PlayerList.deleteList
                PlayerList.soundList.Remove(tmpID)
            Next
            Dim i As Integer
            Dim loopContent = PlayerList.soundList
            Dim loopCount = loopContent.Count
            Dim player As Player
            While i < loopCount
                player = loopContent(i)
                If player.Duration = player.Position Then
                    If (Not player.Cycle) OrElse (player.Cycle AndAlso player.CycleCount = 0) Then
                        PlayerList.soundList.Remove(player.ID)
                        player.Finish()
                        player.Dispose()
                        loopCount = loopContent.Count
                        Continue While
                    End If
                    If Not (player.Cycle AndAlso player.CycleCount = -1) Then player.CycleCount -= 1
                    player.Position = 0
                    player.Play()
                End If
                i += 1
            End While
            If PlayerList.soundList.Count = 0 Then
                isLooping = False
                Return False
            End If
            Return True
        End Function

        Public Sub StartRendering(window As Windows.Window) Implements AppCore.Plugin.ILooping.StartRendering

        End Sub

    End Class

End Namespace