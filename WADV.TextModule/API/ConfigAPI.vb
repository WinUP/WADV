Imports System.Windows.Controls

Namespace API

    ''' <summary>
    ''' 设置API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConfigAPI

        Public Shared Sub Init(framsBetweenWord As Integer, framsBetweenSetence As Integer, auto As Boolean, ignoreReaded As Boolean)
            Initialiser.LoadEffect()
            ModuleConfig.Auto = auto
            ModuleConfig.Clicked = False
            ModuleConfig.Fast = False
            ModuleConfig.Ignore = ignoreReaded
            SetWordFrame(framsBetweenWord)
            SetSentenceFrame(framsBetweenSetence)
            MessageAPI.SendSync("[TEXT]INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 获取文字之间的间隔帧
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetWordFrame() As Integer
            Return ModuleConfig.WordFrame
        End Function

        ''' <summary>
        ''' 获取句子之间的间隔帧
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSetenceFrame() As Integer
            Return ModuleConfig.SetenceFrame
        End Function

        ''' <summary>
        ''' 获取自动播放状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetAutoMode() As Boolean
            Return ModuleConfig.Auto
        End Function

        ''' <summary>
        ''' 获取略过状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetIgnoreMode() As Boolean
            Return ModuleConfig.Ignore
        End Function

        ''' <summary>
        ''' 修改文字之间的间隔帧
        ''' </summary>
        ''' <param name="frame">新的数值</param>
        ''' <remarks></remarks>
        Public Shared Sub SetWordFrame(frame As Integer)
            ModuleConfig.WordFrame = frame
            MessageAPI.SendSync("[TEXT]WORDFRAME_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改句子之间的间隔帧
        ''' </summary>
        ''' <param name="frame">新的数值</param>
        ''' <remarks></remarks>
        Public Shared Sub SetSentenceFrame(frame As Integer)
            ModuleConfig.SetenceFrame = frame
            MessageAPI.SendSync("[TEXT]SENTENCEFRAME_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改自动播放状态
        ''' </summary>
        ''' <param name="auto">新的状态</param>
        ''' <remarks></remarks>
        Public Shared Sub SetAutoMode(auto As Boolean)
            ModuleConfig.Auto = auto
            MessageAPI.SendSync("[TEXT]AUTOMODE_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改略过状态
        ''' </summary>
        ''' <param name="ignore">新的状态</param>
        ''' <remarks></remarks>
        Public Shared Sub SetIgnoreMode(ignore As Boolean)
            ModuleConfig.Ignore = ignore
            MessageAPI.SendSync("[TEXT]IGNOREMODE_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置对话内容显示区域
        ''' </summary>
        ''' <param name="areaName">目标文本区域</param>
        ''' <remarks></remarks>
        Public Shared Sub SetTextArea(areaName As String)
            Dim area = WindowAPI.SearchObject(Of TextBlock)(areaName)
            UiConfig.TextArea = area
            MessageAPI.SendSync("[TEXT]TEXTAREA_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置说话者显示区域
        ''' </summary>
        ''' <param name="areaName">目标文本区域</param>
        ''' <remarks></remarks>
        Public Shared Sub SetSpeakerArea(areaName As String)
            Dim area = WindowAPI.SearchObject(Of TextBlock)(areaName)
            UiConfig.SpeakerArea = area
            MessageAPI.SendSync("[TEXT]SPEAKERAREA_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置主显示区域
        ''' </summary>
        ''' <param name="areaName">目标面板区域</param>
        ''' <remarks></remarks>
        Public Shared Sub SetMainArea(areaName As String)
            Dim area = WindowAPI.SearchObject(Of Windows.FrameworkElement)(areaName)
            UiConfig.FrameArea = area
            MessageAPI.SendSync("[TEXT]MAINAREA_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置对话区域的可见性
        ''' </summary>
        ''' <param name="visible">是否可见</param>
        ''' <remarks></remarks>
        Public Shared Sub SetVisibility(visible As Boolean)
            WindowAPI.InvokeSync(Sub() UiConfig.FrameArea.Visibility = If(visible, Windows.Visibility.Visible, Windows.Visibility.Collapsed))
            MessageAPI.SendSync("[TEXT]VISIBILITY_CHANGE")
        End Sub

        ''' <summary>
        ''' 注册事件
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub RegisterEvent()
            AddHandler WindowAPI.GetWindow.KeyDown, AddressOf TextCore.Ctrl_Down
            AddHandler WindowAPI.GetWindow.KeyUp, AddressOf TextCore.Ctrl_Up
            AddHandler UiConfig.TextArea.MouseLeftButtonDown, AddressOf TextCore.TextArea_Click
            MessageAPI.SendSync("[TEXT]EVENT_REGISTER")
        End Sub

        ''' <summary>
        ''' 注销事件
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub UnregisterEvent()
            RemoveHandler WindowAPI.GetWindow.KeyDown, AddressOf TextCore.Ctrl_Down
            RemoveHandler WindowAPI.GetWindow.KeyUp, AddressOf TextCore.Ctrl_Up
            RemoveHandler UiConfig.TextArea.MouseLeftButtonDown, AddressOf TextCore.TextArea_Click
            MessageAPI.SendSync("[TEXT]EVENT_UNREGISTER")
        End Sub

    End Class

End Namespace
