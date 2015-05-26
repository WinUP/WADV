<Serializable> Public Class 我要干什么来着 : Inherits Achievement

    Public Sub New()
        MyBase.New("我要干什么来着？", "有20次虽然进入了设置页面，但是却什么都没做")
    End Sub

    Public Overrides Sub Check()
        If API.GetData("设置页面进入次数") - API.GetData("设置修改次数") = 5 Then
            SetEarn()
        End If
    End Sub

    Protected Overrides Sub Register()
        API.Register("设置页面进入次数", Me)
        API.Register("设置修改次数", Me)
    End Sub

End Class
