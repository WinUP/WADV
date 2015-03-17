''' <summary>
''' 路径类型
''' </summary>
''' <remarks></remarks>
Public Enum PathType
    Game
    Plugin
    Resource
    Skin
    Script
    UserFile
End Enum

''' <summary>
''' 转场类型
''' </summary>
''' <remarks></remarks>
Public Enum NavigateOperation
    ''' <summary>
    ''' 在无特效的情况下完成导航
    ''' </summary>
    ''' <remarks></remarks>
    NoEffect
    ''' <summary>
    ''' 不进行导航，只淡出当前页面
    ''' </summary>
    ''' <remarks></remarks>
    OnlyFadeOut
    ''' <summary>
    ''' 不进行导航，只淡入当前页面
    ''' </summary>
    ''' <remarks></remarks>
    OnlyFadeIn
    ''' <summary>
    ''' 淡出然后导航，并保持新页面透明度为0
    ''' </summary>
    ''' <remarks></remarks>
    FadeOutAndNavigate
    ''' <summary>
    ''' 淡出然后导航，并直接显示新页面
    ''' </summary>
    ''' <remarks></remarks>
    FadeOutAndShow
    ''' <summary>
    ''' 直接导航，然后淡入新页面
    ''' </summary>
    ''' <remarks></remarks>
    NavigateAndFadeIn
    ''' <summary>
    ''' 直接导航，并保持新页面透明度为0
    ''' </summary>
    ''' <remarks></remarks>
    NavigateAndHide
    ''' <summary>
    ''' 使用默认配置进行导航
    ''' </summary>
    ''' <remarks></remarks>
    Normal
End Enum
