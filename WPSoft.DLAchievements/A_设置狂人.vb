<Serializable> Public Class A_设置狂人 : Inherits Achievement

    Public Sub New()
        MyBase.New("设置狂人", "改动并保存了50次系统设置")
    End Sub

    Public Overrides Sub Check()
        If PropertyAPI.GetData("设置修改次数") = 50 Then
            SetEarn()
        End If
    End Sub

    Public Overrides Sub Register()
        PropertyAPI.Register("设置修改次数", Me)
    End Sub

End Class
