Imports System.Windows.Controls
Imports System.Windows.Markup
Imports WADV.ChoiceModule.Config
Imports WADV.ChoiceModule.Effect

Namespace API

    ''' <summary>
    ''' 界面API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConfigAPI

        ''' <summary>
        ''' 初始化模块
        ''' </summary>
        ''' <param name="contentName">显示容器的名称</param>
        ''' <param name="styleFile">样式文件</param>
        ''' <param name="margin">选项间隔</param>
        ''' <remarks></remarks>
        Public Shared Sub Init(contentName As String, styleFile As String, margin As Double)
            Initialiser.LoadEffect()
            Dim content = WindowAPI.SearchObject(Of Panel)(contentName)
            If content Is Nothing Then Return
            SetContent(content)
            SetStyle(styleFile)
            SetMargin(margin)
            MessageAPI.SendSync("CHOICE_INIT_ALLFINISH")
        End Sub

        ''' <summary>
        ''' 设置显示选项的默认容器
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <remarks></remarks>
        Public Shared Sub SetContent(content As Panel)
            UIConfig.ChoiceContent = content
            MessageAPI.SendSync("CHOICE_CONTENT_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置选项默认样式所在的文件路径
        ''' </summary>
        ''' <param name="styleFile">样式文件(放置在Skin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetStyle(styleFile As String)
            UIConfig.ChoiceStyle = My.Computer.FileSystem.ReadAllText(PathAPI.GetPath(PathType.Skin, styleFile), Text.Encoding.Default)
            MessageAPI.SendSync("CHOICE_STYLE_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置两个选项之间的默认间隔
        ''' </summary>
        ''' <param name="margin">新的间隔</param>
        ''' <remarks></remarks>
        Public Shared Sub SetMargin(margin As Double)
            UIConfig.ChoiceMargin = margin
            MessageAPI.SendSync("CHOICE_MARGIN_CHANGE")
        End Sub

    End Class

    ''' <summary>
    ''' 选项API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ChoiceAPI

        Public Shared Function Show(choice() As String, showEffectName As String, hideEffectName As String, waitFrame As Integer, Optional waitEffectName As String = "BaseProgress") As String
            If (UIConfig.ChoiceContent Is Nothing) OrElse (UIConfig.ChoiceStyle = "") Then Return ""
            If Not Initialiser.ShowEffectList.ContainsKey(showEffectName) Then Return ""
            If Not Initialiser.HideEffectList.ContainsKey(hideEffectName) Then Return ""
            If Not Initialiser.ProgressEffectList.ContainsKey(waitEffectName) Then Return ""
            Dim content = UIConfig.ChoiceContent
            Dim dispatcher = WindowAPI.GetDispatcher
            Dim margin = UIConfig.ChoiceMargin
            Dim choiceList As New List(Of Button)
            Dim totalMargin As Integer
            dispatcher.Invoke(Sub()
                                  content.Visibility = Windows.Visibility.Collapsed
                                  For i = 0 To choice.Length - 1
                                      totalMargin = i * margin
                                      Dim tmpPanel As Button = XamlReader.Parse(UIConfig.ChoiceStyle)
                                      tmpPanel.Margin = New Windows.Thickness(tmpPanel.Margin.Left, totalMargin, tmpPanel.Margin.Right, tmpPanel.Margin.Bottom)
                                      tmpPanel.Content = choice(i)
                                      choiceList.Add(tmpPanel)
                                      content.Children.Add(tmpPanel)
                                  Next
                              End Sub)
            Dim showEffect As IShowEffect = Activator.CreateInstance(Initialiser.ShowEffectList(showEffectName), New Object() {choiceList.ToArray})
            Dim hideEffect As IHideEffect = Activator.CreateInstance(Initialiser.HideEffectList(hideEffectName), New Object() {choiceList.ToArray})
            Dim waitEffect As IProgressEffect = Activator.CreateInstance(Initialiser.ProgressEffectList(waitEffectName), New Object() {choiceList.ToArray, waitFrame})
            Dim loopContent As New PluginInterface.LoopReceiver(waitEffect)
            MessageAPI.SendSync("CHOICE_DSIPLAY_BEFORE")
            dispatcher.Invoke(Sub()
                                  content.Visibility = Windows.Visibility.Visible
                                  showEffect.Render()
                              End Sub)
            showEffect.Wait()
            LoopingAPI.AddLoopSync(loopContent)
            LoopingAPI.WaitLoopSync(loopContent)
            dispatcher.Invoke(Sub() hideEffect.Render())
            hideEffect.Wait()
            dispatcher.Invoke(Sub()
                                  For Each tmpPanel In choiceList
                                      content.Children.Remove(tmpPanel)
                                  Next
                                  content.Visibility = Windows.Visibility.Collapsed
                              End Sub)
            MessageAPI.SendSync("CHOICE_DISPLAY_AFTER")
            Return waitEffect.GetAnswer
        End Function

        Public Function ShowByLua(choice As NLua.LuaTable, showEffectName As String, hideEffectName As String, waitFrame As Integer, Optional waitEffectName As String = "BaseProgress") As String
            Dim choiceList() As String = (From tmpChoice In choice.Values Select CStr(tmpChoice)).ToArray
            Return Show(choiceList, showEffectName, hideEffectName, waitFrame, waitEffectName)
        End Function

    End Class

End Namespace