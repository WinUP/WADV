<Serializable> Public Class 设置狂人 : Inherits Achievement
    Public Sub New()
        MyBase.New("设置狂人", "改动并保存了50次系统设置")
    End Sub

    Public Overrides Sub Check()
        If API.GetData("设置修改次数") = 50 Then
            SetEarn()
        End If
    End Sub

    Protected Overrides Sub Register()
        API.Register("设置修改次数", Me)
    End Sub
End Class
