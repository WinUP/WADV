Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Friend NotInheritable Class GameClose : Implements IGameDestructingReceiver

        Public Sub DestructuringGame(e As ComponentModel.CancelEventArgs) Implements IGameDestructingReceiver.DestructGame
            AchievementList.Save(IO.Path.Combine(ModuleConfig.SaveFileFolder, "achievement.a.save"))
            AchievementPropertyList.Save(IO.Path.Combine(ModuleConfig.SaveFileFolder, "achievement.p.save"))
        End Sub

    End Class

End Namespace