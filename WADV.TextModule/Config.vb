Imports System.Xml
Imports System.Windows.Controls

Namespace Config

    ''' <summary>
    ''' 模块设置类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ModuleConfig

        ''' <summary>
        ''' 获取或设置快进状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Fast As Boolean

        ''' <summary>
        ''' 获取或设置略过效果状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Clicked As Boolean

        ''' <summary>
        ''' 获取或设置文字间隔帧
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property WordFrame As Integer

        ''' <summary>
        ''' 获取或设置自动播放状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Auto As Boolean

        ''' <summary>
        ''' 获取或设置句子间隔帧
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property SetenceFrame As Integer

        ''' <summary>
        ''' 获取或设置略过状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Ignore As Boolean

    End Class

    ''' <summary>
    ''' 界面显示设置类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UIConfig

        ''' <summary>
        ''' 对话内容区域
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared TextArea As TextBlock = Nothing
        ''' <summary>
        ''' 说话者区域
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared SpeakerArea As TextBlock = Nothing

        ''' <summary>
        ''' 主显示区域
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared FrameArea As Windows.FrameworkElement = Nothing

    End Class

End Namespace
