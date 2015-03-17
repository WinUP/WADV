Imports System.Windows.Controls
Imports System.Windows.Markup
Imports Neo.IronLua

Namespace API

    ''' <summary>
    ''' 选项API类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class ChoiceAPI

        Public Shared Function Show(choice() As String, showEffectName As String, hideEffectName As String, waitFrame As Integer, Optional waitEffectName As String = "BaseProgress") As String
            If (Config.ChoiceContent Is Nothing) OrElse (Config.ChoiceStyle = "") OrElse (Config.ChoiceZIndex < 1) Then Return ""
            If Not Initialiser.ShowEffectList.ContainsKey(showEffectName) Then Return ""
            If Not Initialiser.HideEffectList.ContainsKey(hideEffectName) Then Return ""
            If Not Initialiser.ProgressEffectList.ContainsKey(waitEffectName) Then Return ""
            Dim content = Config.ChoiceContent
            Dim dispatcher = WindowAPI.GetDispatcher
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
            Dim showEffect As IShowEffect = Activator.CreateInstance(Initialiser.ShowEffectList(showEffectName), New Object() {choiceList.ToArray})
            Dim hideEffect As IHideEffect = Activator.CreateInstance(Initialiser.HideEffectList(hideEffectName), New Object() {choiceList.ToArray})
            Dim waitEffect As IProgressEffect = Activator.CreateInstance(Initialiser.ProgressEffectList(waitEffectName), New Object() {choiceList.ToArray, waitFrame})
            Dim loopContent As New PluginInterface.LoopReceiver(waitEffect)
            MessageAPI.SendSync("[CHOICE]DSIPLAY_STANDBY")
            dispatcher.Invoke(Sub()
                                  content.Visibility = Windows.Visibility.Visible
                                  showEffect.Render()
                              End Sub)
            showEffect.Wait()
            LoopAPI.AddLoopSync(loopContent)
            LoopAPI.WaitLoopSync(loopContent)
            dispatcher.Invoke(Sub() hideEffect.Render())
            hideEffect.Wait()
            dispatcher.Invoke(Sub()
                                  For Each tmpPanel In choiceList
                                      content.Children.Remove(tmpPanel)
                                  Next
                                  content.Visibility = Windows.Visibility.Collapsed
                              End Sub)
            MessageAPI.SendSync("[CHOICE]DISPLAY_FINISH")
            Return waitEffect.GetAnswer
        End Function

        Public Shared Function ShowByLua(choice As LuaTable, showEffectName As String, hideEffectName As String, waitFrame As Integer, Optional waitEffectName As String = "BaseProgress") As String
            Dim choiceList() As String = (From tmpChoice In choice.ArrayList Select CStr(tmpChoice)).ToArray
            Return Show(choiceList, showEffectName, hideEffectName, waitFrame, waitEffectName)
        End Function

    End Class

End Namespace