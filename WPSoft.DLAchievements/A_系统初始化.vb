<Serializable> Public Class A_系统启动 : Inherits Achievement

    Public Sub New()
        MyBase.New("系统: <启动>", "等待游戏完成一次初始化")
    End Sub

    Public Overrides Sub Check()
        If PropertyAPI.GetData("游戏运行次数") = 1 Then
            SetEarn()
        End If
    End Sub

    Public Overrides Sub Register()
        PropertyAPI.Register("游戏运行次数", Me)
    End Sub

End Class
