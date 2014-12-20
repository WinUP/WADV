Imports System.Windows.Markup
Imports System.Xml
Imports System.Windows.Controls
Imports System.IO
Imports WADV.ChoiceModule.Config

Namespace API

    ''' <summary>
    ''' 界面API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UIAPI

        ''' <summary>
        ''' 获取显示选项的容器
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetContent() As Panel
            Return Config.UIConfig.ChoiceContent
        End Function

        ''' <summary>
        ''' 获取选项样式XAML
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetStyle() As String
            Return Config.UIConfig.ChoiceStyle
        End Function

        ''' <summary>
        ''' 获取两个选项之间的间隔
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetMargin() As Double
            Return Config.UIConfig.ChoiceMargin
        End Function

        ''' <summary>
        ''' 设置显示选项的容器
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <remarks></remarks>
        Public Shared Sub SetContent(content As Panel)
            Config.UIConfig.ChoiceContent = content
        End Sub

        ''' <summary>
        ''' 设置选项样式所在的文件路径
        ''' </summary>
        ''' <param name="styleFile">样式文件(放置在Skin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetStyle(styleFile As String)
            Config.UIConfig.ChoiceStyle = My.Computer.FileSystem.ReadAllText(AppCore.API.PathAPI.GetPath(AppCore.API.PathAPI.Skin, styleFile), System.Text.Encoding.Default)
        End Sub

        ''' <summary>
        ''' 设置两个选项之间的间隔
        ''' </summary>
        ''' <param name="margin">新的间隔</param>
        ''' <remarks></remarks>
        Public Shared Sub SetMargin(margin As Double)
            Config.UIConfig.ChoiceMargin = margin
        End Sub

    End Class

    ''' <summary>
    ''' 选项API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ChoiceAPI

        ''' <summary>
        ''' 获取上一个选项的结果
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Answer() As String
            Return Config.DataConfig.Choice
        End Function

        ''' <summary>
        ''' 显示选项
        ''' </summary>
        ''' <param name="choice">选项内容</param>
        ''' <param name="waitingTime">等待选择时间(单位为帧)</param>
        ''' <param name="styleName">选项显示效果类的名字</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ShowRegular(choice() As String, waitingTime As Integer, styleName As String) As Boolean
            '检查配置
            If Config.UIConfig.ChoiceContent Is Nothing Then Return False
            If Config.UIConfig.ChoiceStyle Is Nothing Then Return False
            Config.DataConfig.Choice = ""
            Dim choicePanel As New List(Of Windows.FrameworkElement)
            Dim dispatcher = WindowAPI.GetDispatcher
            '声明界面元素
            For i = 0 To choice.Length - 1
                Dim readIndex = i
                Dim margin = i * Config.UIConfig.ChoiceMargin
                dispatcher.Invoke(Sub()
                                      '获取元素
                                      Dim tmpPanel As Windows.FrameworkElement = XamlReader.Parse(Config.UIConfig.ChoiceStyle)
                                      '设置元素样式
                                      tmpPanel.Margin = New Windows.Thickness(tmpPanel.Margin.Left, margin, tmpPanel.Margin.Right, tmpPanel.Margin.Bottom)
                                      Dim tmpText = WindowAPI.GetChildByName(Of TextBlock)(tmpPanel, "")
                                      tmpText.Text = choice(readIndex)
                                      '绑定事件
                                      AddHandler tmpText.MouseLeftButtonDown, Sub() DataConfig.Choice = tmpText.Text
                                      '添加到窗口
                                      choicePanel.Add(tmpPanel)
                                      Config.UIConfig.ChoiceContent.Children.Add(tmpPanel)
                                  End Sub)
            Next
            '查找特效
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Name = styleName AndAlso tmpClass.Namespace = "WADV.ChoiceModule.Effect" Select tmpClass
            If classList.Count < 1 Then Return False
            Dim effect As Effect.StandardEffect = Activator.CreateInstance(classList.FirstOrDefault, New Object() {choicePanel.ToArray, waitingTime})
            Dim loopContent As New PluginInterface.Looping(effect)
            dispatcher.Invoke(Sub() Config.UIConfig.ChoiceContent.Visibility = Windows.Visibility.Visible)
            '开始渲染
            AppCore.API.LoopingAPI.AddLoopSync(loopContent)
            AppCore.API.LoopingAPI.WaitLoopSync(loopContent)
            '注销事件
            For Each tmpPanel In choicePanel
                RemoveHandler tmpPanel.MouseLeftButtonDown, AddressOf Config.UIConfig.TextBlock_Click
                dispatcher.Invoke(Sub() Config.UIConfig.ChoiceContent.Children.Remove(tmpPanel))
            Next
            '隐藏界面元素
            dispatcher.Invoke(Sub() Config.UIConfig.ChoiceContent.Visibility = Windows.Visibility.Collapsed)
            Return True
        End Function

        ''' <summary>
        ''' 用LUA表来显示选项
        ''' </summary>
        ''' <param name="choice">选项内容</param>
        ''' <param name="waitingTime">等待选择时间(单位为帧)</param>
        ''' <param name="styleName">选项显示效果类的名字</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Show(choice As LuaInterface.LuaTable, waitingTime As Integer, styleName As String) As Boolean
            Dim choice1(choice.Values.Count - 1) As String
            choice.Values.CopyTo(choice1, 0)
            Return ShowRegular(choice1, waitingTime, styleName)
        End Function

    End Class

End Namespace