Imports WADV.Core.Enumeration

Namespace API

    ''' <summary>
    ''' 界面API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Ui

        ''' <summary>
        ''' 初始化模块
        ''' </summary>
        ''' <param name="contentName">显示容器的名称</param>
        ''' <param name="styleFile">样式文件</param>
        ''' <param name="margin">选项间隔</param>
        ''' <remarks></remarks>
        Public Sub Init(contentName As String, styleFile As String, margin As Double, index As Integer)
            Initialiser.LoadEffect()
            Dim content = Window.Search(Of Windows.Controls.Panel)(contentName)
            If content Is Nothing Then Return
            Ui.Content(content)
            Style(styleFile)
            Ui.Margin(margin)
            ZIndex(index)
            Message.Send("[CHOICE]INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 设置显示选项的默认容器
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <remarks></remarks>
        Public Sub Content(content As Windows.Controls.Panel)
            Config.ChoiceContent = content
            Message.Send("[CHOICE]CONTENT_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置选项默认样式所在的文件路径
        ''' </summary>
        ''' <param name="styleFile">样式文件(放置在Skin目录下)</param>
        ''' <remarks></remarks>
        Public Sub Style(styleFile As String)
            Config.ChoiceStyle = My.Computer.FileSystem.ReadAllText(Path.Combine(PathType.Skin, styleFile), Text.Encoding.Default)
            Message.Send("[CHOICE]STYLE_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置两个选项之间的默认间隔
        ''' </summary>
        ''' <param name="margin">新的间隔</param>
        ''' <remarks></remarks>
        Public Sub Margin(margin As Double)
            Config.ChoiceMargin = margin
            Message.Send("[CHOICE]MARGIN_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置选项的纵轴显示优先级
        ''' </summary>
        ''' <param name="index">要设置到的值</param>
        ''' <remarks></remarks>
        Public Sub ZIndex(index As Integer)
            Config.ChoiceZIndex = index
            Message.Send("[CHOICE]ZINDEX_CHANGE")
        End Sub
    End Module
End Namespace