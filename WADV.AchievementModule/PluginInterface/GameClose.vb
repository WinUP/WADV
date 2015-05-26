Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Friend NotInheritable Class GameClose : Implements IGameDestructorReceiver

        Public Sub DestructuringGame(e As ComponentModel.CancelEventArgs) Implements IGameDestructorReceiver.DestructuringGame
            AchievementList.Save(IO.Path.Combine(ModuleConfig.SaveFileFolder, "achievement.a.save"))
            AchievementPropertyList.Save(IO.Path.Combine(ModuleConfig.SaveFileFolder, "achievement.p.save"))
        End Sub

    End Class

End Namespace