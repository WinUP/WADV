Namespace API

    Public Class AchieveAPI

        Public Function NewAchieve(name As String, substance As String) As Boolean
            If AchievementList.Contains(name) Then Return False
            Dim achieve As New Achievement
            achieve.Name = name
            achieve.Substance = substance
            achieve.IsEarn = False
            achieve.Data = ""
            AchievementList.Add(achieve)
            Return True
        End Function

        Public Function LoadData(name As String) As String
            If Not AchievementList.Contains(name) Then Return False
            Return AchievementList.Item(name).Data
        End Function

        Public Sub SaveData(name As String, data As String)
            If Not AchievementList.Contains(name) Then Return
            Dim achieve = AchievementList.Item(name)
            achieve.Data = data
            AchievementList.Change(name, achieve)
        End Sub

        Public Sub Earn(name As String)
            If Not AchievementList.Contains(name) Then Return
            Dim achieve = AchievementList.Item(name)
            If achieve._IsEarn = True Then Return
            achieve._IsEarn = True
            AchievementList.Change(name, achieve)

            '!在这里加入动画特效

        End Sub

    End Class

End Namespace
