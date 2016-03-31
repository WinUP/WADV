Friend NotInheritable Class Properties
    Friend Shared Sub Register()
        For Each tmpProperty In {"游戏运行次数",
                                 "成就获得个数",
                                 "CG显示次数",
                                 "选项点击次数",
                                 "选项超时次数",
                                 "选项显示次数",
                                 "BGM播放次数",
                                 "效果音播放次数",
                                 "对话播放次数",
                                 "声音播放次数",
                                 "视频播放次数",
                                 "视频跳过次数",
                                 "立绘显示次数",
                                 "立绘效果次数",
                                 "阅读句子条数",
                                 "手动模式点击次数",
                                 "自动模式点击次数",
                                 "快进模式点击次数",
                                 "设置页面进入次数",
                                 "设置修改次数",
                                 "路线选择次数"}
            Extension.Property.Add(tmpProperty)
        Next
    End Sub
End Class
