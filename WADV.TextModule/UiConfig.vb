Imports System.Windows.Controls

''' <summary>
''' 界面显示设置类
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class UiConfig

    ''' <summary>
    ''' 对话内容区域
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared TextArea As TextBlock = Nothing
    ''' <summary>
    ''' 说话者区域
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared SpeakerArea As TextBlock = Nothing

    ''' <summary>
    ''' 主显示区域
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared FrameArea As Windows.FrameworkElement = Nothing

End Class