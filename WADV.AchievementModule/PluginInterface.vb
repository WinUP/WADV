Imports WADV.AppCore.PluginInterface
Imports WADV.AchievementModule.API

Namespace PluginInterface

    Public Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
            ScriptAPI.RegisterInTableSync("api_achieve", "newAchieve", New Action(Of Achievement)(AddressOf AchieveAPI.NewAchieve), True)
            '!API没写完
            ScriptAPI.RegisterInTableSync("api_achieve", "init", New Action(Of String, String)(AddressOf ConfigAPI.Init))
            MessageAPI.SendSync("ACHIEVE_INIT_LOADFINISH")
            Return True
        End Function

    End Class

    Public Class GameClose : Implements IGameClose

        Public Sub DestructuringGame(e As ComponentModel.CancelEventArgs) Implements IGameClose.DestructuringGame
            AchievementList.Save(IO.Path.Combine(Config.SaveFileFolder, "achievement.a.save"))
            PropertyList.Save(IO.Path.Combine(Config.SaveFileFolder, "achievement.p.save"))
        End Sub

    End Class

End Namespace
