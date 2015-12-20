<Serializable> Public Class 系统已启动 : Inherits Achievement
    Public Sub New()
        MyBase.New("系统已启动", "等待游戏完成一次初始化")
    End Sub
    Public Overrides Sub Check()
        If Extension.Property.Get("游戏运行次数") = 1 Then
            SetEarn()
        End If
    End Sub
    Protected Overrides Sub Register()
        Extension.Property.Register("游戏运行次数", Me)
    End Sub
End Class
