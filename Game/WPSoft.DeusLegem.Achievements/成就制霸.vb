﻿<Serializable> Public Class 成就制霸 : Inherits Achievement
    Public Sub New()
        MyBase.New("成就制霸", "获得了游戏全部的成就")
    End Sub

    Public Overrides Sub Check()
        If Extension.Property.Get("成就获得个数") = Integer.MaxValue Then '!注意：完工后把这里改成游戏成就总数-1
            SetEarn()
        End If
    End Sub

    Protected Overrides Sub Register()
        Extension.Property.Register("成就获得个数", Me)
    End Sub
End Class