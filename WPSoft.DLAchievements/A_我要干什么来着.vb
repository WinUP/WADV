<Serializable> Public Class A_我要干什么来着 : Inherits Achievement

    Public Sub New()
        MyBase.New("我要干什么来着？", "有20次虽然进入了设置页面，但是却什么都没做")
    End Sub

    Public Overrides Sub Check()
        If PropertyAPI.GetData("设置页面进入次数") - PropertyAPI.GetData("设置修改次数") = 20 Then
            SetEarn()
        End If
    End Sub

    Public Overrides Sub Register()
        PropertyAPI.Register("设置页面进入次数", Me)
        PropertyAPI.Register("设置修改次数", Me)
    End Sub

End Class
