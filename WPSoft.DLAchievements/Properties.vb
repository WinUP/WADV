Public Class Properties

    Public Shared Sub Register()
        For Each tmpProperty In {"游戏运行次数", "CG显示次数", "BGM播放次数"}
            PropertyAPI.Add(tmpProperty)
        Next
    End Sub

End Class
