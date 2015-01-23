Imports System.Windows.Controls
Imports System.Windows.Markup
Imports WADV.ChoiceModule.Config
Imports WADV.ChoiceModule.Effect

Namespace API

    ''' <summary>
    ''' 界面API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UIAPI

        ''' <summary>
        ''' 设置显示选项的默认容器
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <remarks></remarks>
        Public Shared Sub SetContent(content As Panel)
            Config.UIConfig.ChoiceContent = content
            MessageAPI.SendSync("CHOICE_CONTENT_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置选项默认样式所在的文件路径
        ''' </summary>
        ''' <param name="styleFile">样式文件(放置在Skin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetStyle(styleFile As String)
            Config.UIConfig.ChoiceStyle = My.Computer.FileSystem.ReadAllText(AppCore.API.PathAPI.GetPath(AppCore.API.PathAPI.Skin, styleFile), System.Text.Encoding.Default)
            MessageAPI.SendSync("CHOICE_STYLE_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置两个选项之间的默认间隔
        ''' </summary>
        ''' <param name="margin">新的间隔</param>
        ''' <remarks></remarks>
        Public Shared Sub SetMargin(margin As Double)
            Config.UIConfig.ChoiceMargin = margin
            MessageAPI.SendSync("CHOICE_MARGIN_CHANGE")
        End Sub

    End Class

    ''' <summary>
    ''' 选项API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ChoiceAPI

        ''' <summary>
        ''' 显示选项
        ''' </summary>
        ''' <param name="choice">选项内容</param>
        ''' <param name="waiteTime">等待选择时间(单位为帧)</param>
        ''' <param name="styleName">选项显示效果类的名字</param>
        ''' <param name="contentName">要显示选项的容器，不提供则使用默认容器</param>
        ''' <param name="optionStyle">选项的样式文件，不提供则使用默认样式文件</param>
        ''' <param name="optionMargin">选项之间的间隔，不提供则使用默认间隔</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Show(choice() As String, waiteTime As Integer, styleName As String,
                                           Optional contentName As String = "", Optional optionStyle As String = "", Optional optionMargin As Integer = 0) As String
            '窗口渲染线程
            Dim dispatcher = WindowAPI.GetDispatcher
            '确认显示区域
            Dim content As Panel
            If Config.UIConfig.ChoiceContent Is Nothing Then
                If contentName = "" Then Return ""
                content = WindowAPI.SearchObject(Of Panel)(contentName)
                If content Is Nothing Then Return ""
            Else
                content = Config.UIConfig.ChoiceContent
            End If
            '隐藏选项区域
            dispatcher.Invoke(Sub() content.Visibility = Windows.Visibility.Collapsed)
            '确认样式文件
            Dim style As String = If(optionStyle = "", Config.UIConfig.ChoiceStyle, optionStyle)
            '确认选项距离
            Dim margin As Integer = If(optionMargin = 0, Config.UIConfig.ChoiceMargin, optionMargin)
            '查找时间显示区
            Dim timeArea As TextBlock = WindowAPI.GetChildByName(Of TextBlock)(content, "TimeArea")
            '声明选项列表
            Dim choiceList As New List(Of Windows.FrameworkElement)
            '声明界面元素
            For i = 0 To choice.Length - 1
                Dim readIndex = i
                Dim totalMargin = i * margin
                dispatcher.Invoke(Sub()
                                      '获取元素
                                      Dim tmpPanel As TextBlock = XamlReader.Parse(Config.UIConfig.ChoiceStyle)
                                      '设置元素样式
                                      tmpPanel.Margin = New Windows.Thickness(tmpPanel.Margin.Left, totalMargin, tmpPanel.Margin.Right, tmpPanel.Margin.Bottom)
                                      tmpPanel.Text = choice(readIndex)
                                      '添加到列表和窗口
                                      choiceList.Add(tmpPanel)
                                      Config.UIConfig.ChoiceContent.Children.Add(tmpPanel)
                                  End Sub)
            Next
            '查找特效
            If Not Initialiser.EffectList.ContainsKey(styleName) Then Return ""
            Dim effect As Effect.IEffect = Activator.CreateInstance(Initialiser.EffectList(styleName), New Object() {choiceList.ToArray, waiteTime, timeArea})
            Dim loopContent As New PluginInterface.Looping(effect)
            '显示选项区域
            dispatcher.Invoke(Sub() content.Visibility = Windows.Visibility.Visible)
            '开始渲染
            MessageAPI.SendSync("CHOICE_SHOW_BEFORE")
            LoopingAPI.AddLoopSync(loopContent)
            '等待渲染结束
            LoopingAPI.WaitLoopSync(loopContent)
            MessageAPI.SendSync("CHOICE_SHOW_AFTER")
            '移除界面元素
            For Each tmpPanel In choiceList
                dispatcher.Invoke(Sub() content.Children.Remove(tmpPanel))
            Next
            '隐藏选项区域
            dispatcher.Invoke(Sub() content.Visibility = Windows.Visibility.Collapsed)
            '返回用户选择
            Return effect.GetAnswer
        End Function

    End Class

End Namespace