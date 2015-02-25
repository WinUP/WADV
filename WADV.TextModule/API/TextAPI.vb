Imports Neo.IronLua

Namespace API

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
            If UiConfig.TextArea Is Nothing Then Return False
            '查找特效
            If Not Initialiser.EffectList.ContainsKey(effectName) Then Return ""
            Dim effect As StandardEffect = Activator.CreateInstance(Initialiser.EffectList(effectName), New Object() {text, speaker, isRead})
            ModuleConfig.Clicked = False
            '生成循环体
            Dim loopContent As New PluginInterface.LoopReceiver(effect)
            '开始循环
            MessageAPI.SendSync("[TEXT]SHOW_STANDBY")
            LoopAPI.AddLoopSync(loopContent)
            '等待结束
            LoopAPI.WaitLoopSync(loopContent)
            MessageAPI.SendSync("[TEXT]SHOW_FINISH")
            Return True
        End Function

        Public Shared Function ShowByLua(content As LuaTable, effectName As String) As Boolean
            Dim text, speaker As New List(Of String)
            Dim isRead As New List(Of Boolean)
            For Each record As LuaTable In content.ArrayList
                text.Add(CStr(record("content")))
                speaker.Add(CStr(record("speaker")))
                isRead.Add(CBool(record("isread")))
            Next
            Return Show(text.ToArray, speaker.ToArray, isRead.ToArray, effectName)
        End Function

    End Class

End Namespace