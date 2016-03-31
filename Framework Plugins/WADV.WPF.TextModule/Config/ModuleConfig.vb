Namespace Config
    ''' <summary>
    ''' 模块设置类
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class ModuleConfig

        ''' <summary>
        ''' 获取或设置快进状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Property FastMode As Boolean

        ''' <summary>
        ''' 获取或设置略过效果状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Property ClickedSkip As Boolean

        ''' <summary>
        ''' 获取或设置文字间隔帧
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Property WordFrame As Integer

        ''' <summary>
        ''' 获取或设置自动播放状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Property AutoMode As Boolean

        ''' <summary>
        ''' 获取或设置句子间隔帧
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Property SetenceFrame As Integer

        ''' <summary>
        ''' 获取或设置略过状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Property IgnoreRead As Boolean
    End Class
End Namespace