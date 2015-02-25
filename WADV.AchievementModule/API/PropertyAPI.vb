Namespace API

    Public NotInheritable Class PropertyAPI

        Public Shared Sub Add(name As String)
            PropertyList.Add(name)
        End Sub

        Public Shared Sub SetData(name As String, data As Integer)
            PropertyList.SetData(name, data)
        End Sub

        Public Shared Function GetData(name As String) As Integer
            Return PropertyList.GetData(name)
        End Function

        Public Shared Sub AddData(name As String, count As Integer)
            PropertyList.SetData(name, PropertyList.GetData(name) + count)
        End Sub

        Public Shared Sub Register(name As String, target As Achievement)
            PropertyList.Register(name, target)
        End Sub

        Public Shared Sub RegisterByName(propertyName As String, achievementName As String)
            Dim achievement = AchievementList.Item(achievementName)
            If achievement Is Nothing Then Return
            Register(propertyName, achievement)
        End Sub

        Public Shared Sub Unregister(name As String, target As Achievement)
            PropertyList.Unregister(name, target)
        End Sub

        Public Shared Sub UnregisterByName(propertyName As String, achievementName As String)
            Dim achievement = AchievementList.Item(achievementName)
            If achievement Is Nothing Then Return
            Unregister(propertyName, achievement)
        End Sub

        Public Shared Sub Delete(name As String)
            PropertyList.Delete(name)
        End Sub

        Public Shared Sub Save()
            PropertyList.Save(IO.Path.Combine(Config.SaveFileFolder, "achievement.p.save"))
        End Sub

        Public Shared Sub Load()
            PropertyList.Load(IO.Path.Combine(Config.SaveFileFolder, "achievement.p.save"))
        End Sub

    End Class

End Namespace
