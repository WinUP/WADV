<Serializable> Public Class 我要干什么来着 : Inherits Achievement
    Public Sub New()
        MyBase.New("我要干什么来着？", "有20次虽然进入了设置页面，但是却什么都没做")
    End Sub
    Public Overrides Sub Check()
        If Extension.Property.Get("设置页面进入次数") - Extension.Property.Get("设置修改次数") = 5 Then
            SetEarn()
        End If
    End Sub
    Protected Overrides Sub Register()
        Extension.Property.Register("设置页面进入次数", Me)
        Extension.Property.Register("设置修改次数", Me)
    End Sub
End Class
