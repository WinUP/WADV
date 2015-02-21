Namespace API

    Public Class AchieveAPI

        Public Shared Function NewAchieve(target As Achievement) As Boolean
            AchievementList.Add(target)
            Return True
        End Function

    End Class

End Namespace
