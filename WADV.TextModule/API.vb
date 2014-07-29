Imports System.Reflection
Imports System.Windows.Controls

Namespace API

    ''' <summary>
    ''' 设置API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConfigAPI

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
        End Sub

        ''' <summary>
        ''' 修改句子之间的间隔帧
        ''' </summary>
        ''' <param name="frame">新的数值</param>
        ''' <remarks></remarks>
        Public Shared Sub SetSentenceFrame(frame As Integer)
            Config.ModuleConfig.SetenceFrame = frame
        End Sub

        ''' <summary>
        ''' 修改自动播放状态
        ''' </summary>
        ''' <param name="auto">新的状态</param>
        ''' <remarks></remarks>
        Public Shared Sub SetAutoMode(auto As Boolean)
            Config.ModuleConfig.Auto = auto
        End Sub

        ''' <summary>
        ''' 修改略过状态
        ''' </summary>
        ''' <param name="ignore">新的状态</param>
        ''' <remarks></remarks>
        Public Shared Sub SetIgnoreMode(ignore As Boolean)
            Config.ModuleConfig.Ignore = ignore
        End Sub

        ''' <summary>
        ''' 设置对话内容显示区域
        ''' </summary>
        ''' <param name="area">目标文本区域</param>
        ''' <remarks></remarks>
        Public Shared Sub SetUITextArea(area As TextBlock)
            Config.UIConfig.TextArea = area
        End Sub

        ''' <summary>
        ''' 设置说话者显示区域
        ''' </summary>
        ''' <param name="area">目标文本区域</param>
        ''' <remarks></remarks>
        Public Shared Sub SetCharacterArea(area As TextBlock)
            Config.UIConfig.CharacterArea = area
        End Sub

        ''' <summary>
        ''' 注册事件
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub RegisterEvent()
            AddHandler WindowAPI.GetWindow.KeyDown, AddressOf Core.TextCore.Ctrl_Down
            AddHandler WindowAPI.GetWindow.KeyUp, AddressOf Core.TextCore.Ctrl_Up
            AddHandler Config.UIConfig.TextArea.MouseLeftButtonDown, AddressOf Core.TextCore.TextArea_Click
        End Sub

        ''' <summary>
        ''' 注销事件
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub UnregisterEvent()
            RemoveHandler WindowAPI.GetWindow.KeyDown, AddressOf Core.TextCore.Ctrl_Down
            RemoveHandler WindowAPI.GetWindow.KeyUp, AddressOf Core.TextCore.Ctrl_Up
            RemoveHandler Config.UIConfig.TextArea.MouseLeftButtonDown, AddressOf Core.TextCore.TextArea_Click
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
        ''' <param name="character">说话者</param>
        ''' <param name="effectName">效果类的名字</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ShowReular(text() As String, character() As String, effectName As String) As Boolean
            '检查状态
            If Config.UIConfig.TextArea Is Nothing Then Return False
            '查找特效
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Name = effectName AndAlso tmpClass.Namespace = "WADV.TextModule.TextEffect" Select tmpClass
            If classList.Count < 1 Then Return False
            '生成特效
            Dim effect As TextEffect.StandardEffect = Activator.CreateInstance(classList.FirstOrDefault, New Object() {text, character})
            Config.ModuleConfig.Ellipsis = False
            '生成循环体
            Dim loopContent As New PluginInterface.CustomizedLoop(effect)
            '开始循环
            AppCore.API.LoopingAPI.AddLoop(loopContent)
            '等待结束
            AppCore.API.LoopingAPI.WaitLoop(loopContent)
            Return True
        End Function

        ''' <summary>
        ''' 显示文本
        ''' </summary>
        ''' <param name="text">对话内容</param>
        ''' <param name="character">说话者</param>
        ''' <param name="effectName">效果类的名字</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Show(text As LuaInterface.LuaTable, character As LuaInterface.LuaTable, effectName As String) As Boolean
            Dim text1(text.Values.Count - 1) As String
            text.Values.CopyTo(text1, 0)
            Dim character1(character.Values.Count - 1) As String
            character.Values.CopyTo(character1, 0)
            Return ShowReular(text1, character1, effectName)
        End Function

    End Class

End Namespace
