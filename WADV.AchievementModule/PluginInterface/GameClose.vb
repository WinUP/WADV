Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Friend NotInheritable Class GameClose : Implements IGameDestructorReceiver

        Public Sub DestructuringGame(e As ComponentModel.CancelEventArgs) Implements IGameDestructorReceiver.DestructuringGame
            AchievementList.Save(IO.Path.Combine(Config.SaveFileFolder, "achievement.a.save"))
            PropertyList.Save(IO.Path.Combine(Config.SaveFileFolder, "achievement.p.save"))
        End Sub

    End Class

End Namespace