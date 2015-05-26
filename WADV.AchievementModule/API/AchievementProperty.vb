Namespace API
    Public Module AchievementProperty
        ''' <summary>
        ''' 添加一个新的属性(值为0)
        ''' </summary>
        ''' <param name="name">属性名称</param>
        ''' <remarks></remarks>
        Public Sub Add(name As String)
            AchievementPropertyList.Add(name)
        End Sub

        ''' <summary>
        ''' 设置属性的值
        ''' </summary>
        ''' <param name="name">属性名称</param>
        ''' <param name="data">属性值</param>
        ''' <remarks></remarks>
        Public Sub SetData(name As String, data As Integer)
            AchievementPropertyList.SetData(name, data)
        End Sub

        ''' <summary>
        ''' 获得属性的值
        ''' </summary>
        ''' <param name="name">属性名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetData(name As String) As Integer
            Return AchievementPropertyList.GetData(name)
        End Function

        ''' <summary>
        ''' 将目标属性的值增加指定数字
        ''' </summary>
        ''' <param name="name">属性名称</param>
        ''' <param name="count">要增加的值</param>
        ''' <remarks></remarks>
        Public Sub AddData(name As String, count As Integer)
            AchievementPropertyList.SetData(name, AchievementPropertyList.GetData(name) + count)
        End Sub

        ''' <summary>
        ''' 关联成就和属性
        ''' </summary>
        ''' <param name="name">属性名称</param>
        ''' <param name="target">要关联的成就</param>
        ''' <remarks></remarks>
        Public Sub Register(name As String, target As Achievement)
            AchievementPropertyList.Register(name, target)
        End Sub

        ''' <summary>
        ''' 关联成就和属性
        ''' </summary>
        ''' <param name="propertyName">属性名称</param>
        ''' <param name="achievementName">成就名称</param>
        ''' <remarks></remarks>
        Public Sub Register(propertyName As String, achievementName As String)
            Dim achievement = AchievementList.Item(achievementName)
            If achievement Is Nothing Then Return
            Register(propertyName, achievement)
        End Sub

        ''' <summary>
        ''' 解除成就和属性的关联
        ''' </summary>
        ''' <param name="name">属性名称</param>
        ''' <param name="target">要解除关联的成就</param>
        ''' <remarks></remarks>
        Public Sub Unregister(name As String, target As Achievement)
            AchievementPropertyList.Unregister(name, target)
        End Sub

        ''' <summary>
        ''' 解除成就和属性的关联
        ''' </summary>
        ''' <param name="propertyName">属性名称</param>
        ''' <param name="achievementName">成就名称</param>
        ''' <remarks></remarks>
        Public Sub Unregister(propertyName As String, achievementName As String)
            Dim achievement = AchievementList.Item(achievementName)
            If achievement Is Nothing Then Return
            Unregister(propertyName, achievement)
        End Sub

        ''' <summary>
        ''' 删除属性
        ''' </summary>
        ''' <param name="name">属性名称</param>
        ''' <remarks></remarks>
        Public Sub Delete(name As String)
            AchievementPropertyList.Delete(name)
        End Sub

        ''' <summary>
        ''' 保存属性信息
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Save()
            AchievementPropertyList.Save(IO.Path.Combine(ModuleConfig.SaveFileFolder, "achievement.p.save"))
        End Sub

        ''' <summary>
        ''' 读取属性信息
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Load()
            AchievementPropertyList.Load(IO.Path.Combine(ModuleConfig.SaveFileFolder, "achievement.p.save"))
        End Sub
    End Module
End Namespace
