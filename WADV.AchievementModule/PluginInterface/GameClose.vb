Imports WADV.AppCore.PluginInterface

Namespace PluginInterface

    Friend NotInheritable Class GameClose : Implements IGameClose

        Public Sub DestructuringGame(e As ComponentModel.CancelEventArgs) Implements IGameClose.DestructuringGame
            AchievementList.Save(IO.Path.Combine(Config.SaveFileFolder, "achievement.a.save"))
            PropertyList.Save(IO.Path.Combine(Config.SaveFileFolder, "achievement.p.save"))
        End Sub

    End Class

End Namespace