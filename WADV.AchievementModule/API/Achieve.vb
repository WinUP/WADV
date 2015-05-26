Namespace API
    Public Module Achieve
        ''' <summary>
        ''' 添加一个新成就
        ''' </summary>
        ''' <param name="target">要添加的成就</param>
        ''' <remarks></remarks>
        Public Sub Add(target As Achievement)
            AchievementList.Add(target)
        End Sub

        ''' <summary>
        ''' 删除一个成就
        ''' </summary>
        ''' <param name="name">要删除的成就</param>
        ''' <remarks></remarks>
        Public Sub Delete(name As String)
            AchievementList.Delete(name)
        End Sub

        ''' <summary>
        ''' 获取一个成就
        ''' </summary>
        ''' <param name="name">目标成就的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function [Get](name As String) As Achievement
            Return AchievementList.Item(name)
        End Function

        ''' <summary>
        ''' 获取已注册的成就的列表
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function List() As Achievement()
            Return AchievementList.GetList
        End Function

        ''' <summary>
        ''' 保存成就信息
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Save()
            AchievementList.Save(IO.Path.Combine(ModuleConfig.SaveFileFolder, "achievement.a.save"))
        End Sub

        ''' <summary>
        ''' 读取成就信息
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Load()
            AchievementList.Load(IO.Path.Combine(ModuleConfig.SaveFileFolder, "achievement.a.save"))
        End Sub
    End Module
End Namespace
