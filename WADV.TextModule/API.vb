﻿Imports System.Windows.Controls
Imports WADV.TextModule.TextEffect
Imports NLua

Namespace API

    ''' <summary>
    ''' 设置API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConfigAPI

        Public Shared Sub Init(framsBetweenWord As Integer, framsBetweenSetence As Integer, auto As Boolean, ignoreReaded As Boolean)
            Config.ModuleConfig.Auto = auto
            Config.ModuleConfig.Clicked = False
            Config.ModuleConfig.Fast = False
            Config.ModuleConfig.Ignore = ignoreReaded
            SetWordFrame(framsBetweenWord)
            SetSentenceFrame(framsBetweenSetence)
            MessageAPI.SendSync("TEXT_SCRIPT_INIT")
        End Sub

        ''' <summary>
        ''' 获取文字之间的间隔帧
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetWordFrame() As Integer
            Return Config.ModuleConfig.WordFrame
        End Function

        ''' <summary>
        ''' 获取句子之间的间隔帧
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSetenceFrame() As Integer
            Return Config.ModuleConfig.SetenceFrame
        End Function

        ''' <summary>
        ''' 获取自动播放状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetAutoMode() As Boolean
            Return Config.ModuleConfig.Auto
        End Function

        ''' <summary>
        ''' 获取略过状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetIgnoreMode() As Boolean
            Return Config.ModuleConfig.Ignore
        End Function

        ''' <summary>
        ''' 修改文字之间的间隔帧
        ''' </summary>
        ''' <param name="frame">新的数值</param>
        ''' <remarks></remarks>
        Public Shared Sub SetWordFrame(frame As Integer)
            Config.ModuleConfig.WordFrame = frame
            MessageAPI.SendSync("TEXT_WORDFRAME_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改句子之间的间隔帧
        ''' </summary>
        ''' <param name="frame">新的数值</param>
        ''' <remarks></remarks>
        Public Shared Sub SetSentenceFrame(frame As Integer)
            Config.ModuleConfig.SetenceFrame = frame
            MessageAPI.SendSync("TEXT_SENTENCEFRAME_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改自动播放状态
        ''' </summary>
        ''' <param name="auto">新的状态</param>
        ''' <remarks></remarks>
        Public Shared Sub SetAutoMode(auto As Boolean)
            Config.ModuleConfig.Auto = auto
            MessageAPI.SendSync("TEXT_AUTOMODE_CHANGE")
        End Sub

        ''' <summary>
        ''' 修改略过状态
        ''' </summary>
        ''' <param name="ignore">新的状态</param>
        ''' <remarks></remarks>
        Public Shared Sub SetIgnoreMode(ignore As Boolean)
            Config.ModuleConfig.Ignore = ignore
            MessageAPI.SendSync("TEXT_IGNOREMODE_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置对话内容显示区域
        ''' </summary>
        ''' <param name="areaName">目标文本区域</param>
        ''' <remarks></remarks>
        Public Shared Sub SetTextArea(areaName As String)
            Dim area = WindowAPI.SearchObject(Of TextBlock)(areaName)
            Config.UIConfig.TextArea = area
            MessageAPI.SendSync("TEXT_TEXTAREA_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置说话者显示区域
        ''' </summary>
        ''' <param name="areaName">目标文本区域</param>
        ''' <remarks></remarks>
        Public Shared Sub SetSpeakerArea(areaName As String)
            Dim area = WindowAPI.SearchObject(Of TextBlock)(areaName)
            Config.UIConfig.SpeakerArea = area
            MessageAPI.SendSync("TEXT_SPEAKERAREA_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置主显示区域
        ''' </summary>
        ''' <param name="areaName">目标面板区域</param>
        ''' <remarks></remarks>
        Public Shared Sub SetMainArea(areaName As String)
            Dim area = WindowAPI.SearchObject(Of Windows.FrameworkElement)(areaName)
            Config.UIConfig.FrameArea = area
            MessageAPI.SendSync("TEXT_MAINAREA_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置对话区域的可见性
        ''' </summary>
        ''' <param name="visible">是否可见</param>
        ''' <remarks></remarks>
        Public Shared Sub SetVisibility(visible As Boolean)
            WindowAPI.GetDispatcher.Invoke(Sub() Config.UIConfig.FrameArea.Visibility = If(visible, Windows.Visibility.Visible, Windows.Visibility.Collapsed))
            MessageAPI.SendSync("TEXT_VISIBLE_CHANGE")
        End Sub

        ''' <summary>
        ''' 注册事件
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub RegisterEvent()
            AddHandler WindowAPI.GetWindow.KeyDown, AddressOf Core.TextCore.Ctrl_Down
            AddHandler WindowAPI.GetWindow.KeyUp, AddressOf Core.TextCore.Ctrl_Up
            AddHandler Config.UIConfig.TextArea.MouseLeftButtonDown, AddressOf Core.TextCore.TextArea_Click
            MessageAPI.SendSync("TEXT_EVENT_REGISTER")
        End Sub

        ''' <summary>
        ''' 注销事件
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub UnregisterEvent()
            RemoveHandler WindowAPI.GetWindow.KeyDown, AddressOf Core.TextCore.Ctrl_Down
            RemoveHandler WindowAPI.GetWindow.KeyUp, AddressOf Core.TextCore.Ctrl_Up
            RemoveHandler Config.UIConfig.TextArea.MouseLeftButtonDown, AddressOf Core.TextCore.TextArea_Click
            MessageAPI.SendSync("TEXT_EVENT_UNREGISTER")
        End Sub

    End Class

    ''' <summary>
    ''' 文本API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TextAPI

        ''' <summary>
        ''' 显示文本
        ''' </summary>
        ''' <param name="text">对话内容</param>
        ''' <param name="speaker">说话者</param>
        ''' <param name="effectName">效果类的名字</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Show(text() As String, speaker() As String, isRead() As Boolean, effectName As String) As Boolean
            '检查状态
            If Config.UIConfig.TextArea Is Nothing Then Return False
            '查找特效
            If Not Initialiser.EffectList.ContainsKey(effectName) Then Return ""
            Dim effect As StandardEffect = Activator.CreateInstance(Initialiser.EffectList(effectName), New Object() {text, speaker, isRead})
            Config.ModuleConfig.Clicked = False
            '生成循环体
            Dim loopContent As New PluginInterface.CustomizedLoop(effect)
            '开始循环
            MessageAPI.SendSync("TEXT_SHOW_BEFORE")
            LoopingAPI.AddLoopSync(loopContent)
            '等待结束
            LoopingAPI.WaitLoopSync(loopContent)
            MessageAPI.SendSync("TEXT_SHOW_AFTER")
            Return True
        End Function

        Public Shared Function ShowByLua(content As LuaTable, effectName As String) As Boolean
            Dim text, speaker As New List(Of String)
            Dim isRead As New List(Of Boolean)
            For Each record As LuaTable In content.Values
                text.Add(CStr(record("content")))
                speaker.Add(CStr(record("speaker")))
                isRead.Add(CBool(record("isread")))
            Next
            Return Show(text.ToArray, speaker.ToArray, isRead.ToArray, effectName)
        End Function

    End Class

End Namespace
