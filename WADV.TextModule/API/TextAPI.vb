Imports Neo.IronLua
Imports WADV.TextModule.Config

Namespace API

    ''' <summary>
    ''' 文本API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TextAPI

        ''' <summary>
        ''' 显示文本
        ''' </summary>
        ''' <param name="name">句子组ID</param>
        ''' <param name="effectName">效果类的名字</param>
        ''' <remarks></remarks>
        Public Shared Sub Show(name As String, effectName As String)
            '检查状态
            If UiConfig.TextArea Is Nothing Then Exit Sub
            '查找特效
            If Not Initialiser.EffectList.ContainsKey(effectName) Then Exit Sub
            Dim effect As IEffect = Activator.CreateInstance(Initialiser.EffectList(effectName), New Object() {name})
            ModuleConfig.Clicked = False
            '生成循环体
            Dim loopContent As New PluginInterface.LoopReceiver(effect)
            '开始循环
            MessageAPI.SendSync("[TEXT]SHOW_STANDBY")
            LoopAPI.AddLoopSync(loopContent)
            '等待结束
            LoopAPI.WaitLoopSync(loopContent)
            MessageAPI.SendSync("[TEXT]SHOW_FINISH")
        End Sub

        Public Shared Sub AddSentences(name As String, content As LuaTable)
            If SentenceList.Contains(name) Then Exit Sub
            Dim tmpArray As New List(Of Sentence)
            For Each record As LuaTable In content.ArrayList
                Dim target As Sentence
                target.Content = record("content")
                target.Speaker = record("speaker")
                target.IsRead = record("isread")
                target.VoiceFile = record("voice")
                tmpArray.Add(target)
            Next
            SentenceList.DirectAdd(name, tmpArray.ToArray)
        End Sub

        Public Shared Sub AddSentences(name As String, content As Sentence())
            If Not SentenceList.Contains(name) Then
                SentenceList.DirectAdd(name, content)
            End If
        End Sub

    End Class

End Namespace