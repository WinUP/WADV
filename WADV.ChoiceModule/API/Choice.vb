Imports System.Windows.Controls
Imports System.Windows.Markup

Namespace API
    ''' <summary>
    ''' 选项API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Choice
        ''' <summary>
        ''' 显示一个选择支
        ''' </summary>
        ''' <param name="choice">选择支内容</param>
        ''' <param name="showEffectName">进入效果的名称</param>
        ''' <param name="hideEffectName">退出效果的名称</param>
        ''' <param name="waitFrame">显示时间</param>
        ''' <param name="waitEffectName">等待效果的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Show(choice() As String, showEffectName As String, hideEffectName As String, waitFrame As Integer, Optional waitEffectName As String = "BaseProgress") As String
            If (Config.ChoiceContent Is Nothing) OrElse (Config.ChoiceStyle = "") OrElse (Config.ChoiceZIndex < 1) Then Return ""
            If Not Initialiser.ShowEffectList.ContainsKey(showEffectName) Then Return ""
            If Not Initialiser.HideEffectList.ContainsKey(hideEffectName) Then Return ""
            If Not Initialiser.ProgressEffectList.ContainsKey(waitEffectName) Then Return ""
            Dim content = Config.ChoiceContent
            Dim dispatcher = Window.Dispatcher
            Dim margin = Config.ChoiceMargin
            Dim choiceList As New List(Of Button)
            Dim totalMargin As Integer
            dispatcher.Invoke(Sub()
                                  content.Visibility = Windows.Visibility.Collapsed
                                  For i = 0 To choice.Length - 1
                                      totalMargin = i * margin
                                      Dim tmpPanel As Button = XamlReader.Parse(Config.ChoiceStyle)
                                      tmpPanel.Margin = New Windows.Thickness(tmpPanel.Margin.Left, totalMargin, tmpPanel.Margin.Right, tmpPanel.Margin.Bottom)
                                      tmpPanel.Content = choice(i)
                                      tmpPanel.SetValue(Panel.ZIndexProperty, Config.ChoiceZIndex)
                                      choiceList.Add(tmpPanel)
                                      content.Children.Add(tmpPanel)
                                  Next
                              End Sub)
            Dim showEffect As BaseShow = Activator.CreateInstance(Initialiser.ShowEffectList(showEffectName), New Object() {choiceList.ToArray})
            Dim hideEffect As BaseHide = Activator.CreateInstance(Initialiser.HideEffectList(hideEffectName), New Object() {choiceList.ToArray})
            Dim waitEffect As BaseProgress = Activator.CreateInstance(Initialiser.ProgressEffectList(waitEffectName), New Object() {choiceList.ToArray, waitFrame})
            Dim loopContent As New PluginInterface.LoopReceiver(waitEffect)
            Message.Send("[CHOICE]DSIPLAY_STANDBY")
            dispatcher.Invoke(Sub()
                                  content.Visibility = Windows.Visibility.Visible
                                  showEffect.Render()
                              End Sub)
            showEffect.Wait()
            [Loop].Listen(loopContent)
            [Loop].WaitLoop(loopContent)
            dispatcher.Invoke(Sub() hideEffect.Render())
            hideEffect.Wait()
            dispatcher.Invoke(Sub()
                                  For Each tmpPanel In choiceList
                                      content.Children.Remove(tmpPanel)
                                  Next
                                  content.Visibility = Windows.Visibility.Collapsed
                              End Sub)
            Message.Send("[CHOICE]DISPLAY_FINISH")
            Return waitEffect.GetAnswer
        End Function
    End Module
End Namespace