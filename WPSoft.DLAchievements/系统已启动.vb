<Serializable> Public Class 系统已启动 : Inherits Achievement
    Public Sub New()
        MyBase.New("系统已启动", "等待游戏完成一次初始化")
    End Sub

    Public Overrides Sub Check()
        If API.GetData("游戏运行次数") = 1 Then
            SetEarn()
        End If
    End Sub

    Protected Overrides Sub Register()
        API.Register("游戏运行次数", Me)
    End Sub
End Class
