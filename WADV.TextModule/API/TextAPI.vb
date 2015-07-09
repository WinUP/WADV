Imports WADV.TextModule.Config

Namespace API
    ''' <summary>
    ''' 对话API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Text
        ''' <summary>
        ''' 显示一组对话
        ''' </summary>
        ''' <param name="name">句子组ID</param>
        ''' <param name="effectName">效果类的名字</param>
        ''' <remarks></remarks>
        Public Sub Show(name As String, effectName As String)
            '检查状态
            If UiConfig.TextArea Is Nothing Then Exit Sub
            '查找特效
            If Not Initialiser.EffectList.ContainsKey(effectName) Then Exit Sub
            Dim effect As BaseEffect = Activator.CreateInstance(Initialiser.EffectList(effectName), New Object() {name})
            ModuleConfig.Clicked = False
            '生成循环体
            Dim loopContent As New PluginInterface.LoopReceiver(effect)
            '开始循环
            Send("[TEXT]SHOW_STANDBY")
            [Loop].Listen(loopContent)
            '等待结束
            WaitLoop(loopContent)
            Send("[TEXT]SHOW_FINISH")
        End Sub

        ''' <summary>
        ''' 添加一组对话
        ''' </summary>
        ''' <param name="name">对话名称</param>
        ''' <param name="content">对话内容</param>
        ''' <remarks></remarks>
        Public Sub Add(name As String, content As Sentence())
            If Not SentenceList.Contains(name) Then
                SentenceList.DirectAdd(name, content)
            End If
        End Sub
    End Module
End Namespace