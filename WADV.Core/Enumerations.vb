' WinUP Adventure Game Engine Core Framework
' Enumerations
' This file include all enumerations of core framework

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
''' <remarks>NavigateOperation会在导航时传递给被导航的窗体，是否处理则由该窗体决定</remarks>
Public Enum NavigateOperation
    ''' <summary>
    ''' 在无特效的情况下完成导航
    ''' </summary>
    ''' <remarks></remarks>
    NoEffect
    ''' <summary>
    ''' 不进行导航，只使用退出效果
    ''' </summary>
    ''' <remarks></remarks>
    OnlyOut
    ''' <summary>
    ''' 不进行导航，只使用进入效果
    ''' </summary>
    ''' <remarks></remarks>
    OnlyIn
    ''' <summary>
    ''' 使用退出效果然后导航，并保持新页面透明度为0
    ''' </summary>
    ''' <remarks></remarks>
    OutAndNavigate
    ''' <summary>
    ''' 使用退出效果然后导航，并直接显示新页面
    ''' </summary>
    ''' <remarks></remarks>
    OutAndShow
    ''' <summary>
    ''' 直接导航，然后使用进入效果
    ''' </summary>
    ''' <remarks></remarks>
    NavigateAndIn
    ''' <summary>
    ''' 直接导航，并保持新页面透明度为0
    ''' </summary>
    ''' <remarks></remarks>
    NavigateAndHide
    ''' <summary>
    ''' 使用默认配置，即使用所有效果且进行导航
    ''' </summary>
    ''' <remarks></remarks>
    Normal
End Enum
