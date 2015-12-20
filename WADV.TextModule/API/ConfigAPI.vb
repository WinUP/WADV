Imports System.Windows.Controls
Imports WADV.TextModule.Config

Namespace API

    ''' <summary>
    ''' 设置API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Config
        ''' <summary>
        ''' 初始化模块
        ''' </summary>
        ''' <param name="framsBetweenWord">文字间隔帧</param>
        ''' <param name="framsBetweenSetence">句子间隔帧</param>
        ''' <param name="auto">是否自动播放</param>
        ''' <param name="ignoreReaded">是否忽略已读</param>
        ''' <remarks></remarks>
        Public Sub Init(framsBetweenWord As Integer, framsBetweenSetence As Integer, auto As Boolean, ignoreReaded As Boolean)
            Initialiser.LoadEffect()
            ModuleConfig.AutoMode = auto
            ModuleConfig.ClickedSkip = False
            ModuleConfig.FastMode = False
            ModuleConfig.IgnoreRead = ignoreReaded
            WordFrame(framsBetweenWord)
            SentenceFrame(framsBetweenSetence)
            Send("[TEXT]INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 获取或设置文字之间的间隔帧
        ''' </summary>
        ''' <param name="value">目标值</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WordFrame(Optional value As Integer = -1) As Integer
            If value < 0 Then Return ModuleConfig.WordFrame
            ModuleConfig.SetenceFrame = value
            Send("[TEXT]SENTENCEFRAME_CHANGE")
            Return value
        End Function

        ''' <summary>
        ''' 获取句子之间的间隔帧
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SentenceFrame(Optional value As Integer = -1) As Integer
            If value < 0 Then Return ModuleConfig.SetenceFrame
            ModuleConfig.SetenceFrame = value
            Send("[TEXT]SENTENCEFRAME_CHANGE")
            Return value
        End Function

        ''' <summary>
        ''' 获取自动播放状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetAutoMode() As Boolean
            Return ModuleConfig.AutoMode
        End Function

        ''' <summary>
        ''' 获取略过状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetIgnoreMode() As Boolean
            Return ModuleConfig.IgnoreRead
        End Function

        ''' <summary>
        ''' 修改自动播放状态
        ''' </summary>
        ''' <param name="auto">新的状态</param>
        ''' <remarks></remarks>
        Public Sub SetAutoMode(auto As Boolean)
            ModuleConfig.AutoMode = auto
            Send("[TEXT]AUTOMODE_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改略过状态
        ''' </summary>
        ''' <param name="ignore">新的状态</param>
        ''' <remarks></remarks>
        Public Sub SetIgnoreMode(ignore As Boolean)
            ModuleConfig.IgnoreRead = ignore
            Send("[TEXT]IGNOREMODE_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置对话内容显示区域
        ''' </summary>
        ''' <param name="areaName">目标文本区域</param>
        ''' <remarks></remarks>
        Public Sub SetTextArea(areaName As String)
            Dim area = Search(Of TextBlock)(areaName)
            UiConfig.TextArea = area
            Send("[TEXT]TEXTAREA_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置讲话者显示区域
        ''' </summary>
        ''' <param name="areaName">目标文本区域</param>
        ''' <remarks></remarks>
        Public Sub SetSpeakerArea(areaName As String)
            Dim area = Search(Of TextBlock)(areaName)
            UiConfig.SpeakerArea = area
            Send("[TEXT]SPEAKERAREA_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置主显示区域
        ''' </summary>
        ''' <param name="areaName">目标面板区域</param>
        ''' <remarks></remarks>
        Public Sub SetMainArea(areaName As String)
            Dim area = Search(Of Windows.FrameworkElement)(areaName)
            UiConfig.FrameArea = area
            Send("[TEXT]MAINAREA_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置对话区域的可见性
        ''' </summary>
        ''' <param name="visible">是否可见</param>
        ''' <remarks></remarks>
        Public Sub SetVisibility(visible As Boolean)
            Invoke(Sub() UiConfig.FrameArea.Visibility = If(visible, Windows.Visibility.Visible, Windows.Visibility.Collapsed))
            Send("[TEXT]VISIBILITY_CHANGE")
        End Sub

        ''' <summary>
        ''' 注册事件
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            AddHandler Window.Window.KeyDown, AddressOf Events.Ctrl_Down
            AddHandler Window.Window.KeyUp, AddressOf Events.Ctrl_Up
            AddHandler UiConfig.TextArea.MouseLeftButtonDown, AddressOf Events.TextArea_Click
            Send("[TEXT]EVENT_REGISTER")
        End Sub

        ''' <summary>
        ''' 注销事件
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Unregister()
            RemoveHandler Window.Window.KeyDown, AddressOf Events.Ctrl_Down
            RemoveHandler Window.Window.KeyUp, AddressOf Events.Ctrl_Up
            RemoveHandler UiConfig.TextArea.MouseLeftButtonDown, AddressOf Events.TextArea_Click
            Send("[TEXT]EVENT_UNREGISTER")
        End Sub
    End Module
End Namespace
