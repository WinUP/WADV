Namespace API

    Public Class AchieveAPI

        Public Function NewAchieve(target As Achievement) As Boolean
            AchievementList.Add(target)
            Return True
        End Function

    End Class

End Namespace
