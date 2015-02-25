Namespace API

    Public NotInheritable Class AchieveAPI

        Public Shared Sub Add(target As Achievement)
            AchievementList.Add(target)
        End Sub

        Public Shared Sub Delete(name As String)
            AchievementList.Delete(name)
        End Sub

        Public Shared Function [Get](name As String) As Achievement
            Return AchievementList.Item(name)
        End Function

        Public Shared Function List() As Achievement()
            Return AchievementList.GetList
        End Function

        Public Shared Sub Save()
            AchievementList.Save(IO.Path.Combine(Config.SaveFileFolder, "achievement.a.save"))
        End Sub

        Public Shared Sub Load()
            AchievementList.Load(IO.Path.Combine(Config.SaveFileFolder, "achievement.a.save"))
        End Sub

    End Class

End Namespace
