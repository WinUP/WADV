Imports System.Windows.Controls

Namespace API

    ''' <summary>
    ''' 界面API类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class ConfigAPI

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
            MessageAPI.SendSync("[CHOICE]INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 设置显示选项的默认容器
        ''' </summary>
        ''' <param name="content">目标容器</param>
        ''' <remarks></remarks>
        Public Shared Sub SetContent(content As Panel)
            UIConfig.ChoiceContent = content
            MessageAPI.SendSync("[CHOICE]CONTENT_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置选项默认样式所在的文件路径
        ''' </summary>
        ''' <param name="styleFile">样式文件(放置在Skin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetStyle(styleFile As String)
            UIConfig.ChoiceStyle = My.Computer.FileSystem.ReadAllText(PathAPI.GetPath(PathType.Skin, styleFile), Text.Encoding.Default)
            MessageAPI.SendSync("[CHOICE]STYLE_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置两个选项之间的默认间隔
        ''' </summary>
        ''' <param name="margin">新的间隔</param>
        ''' <remarks></remarks>
        Public Shared Sub SetMargin(margin As Double)
            UIConfig.ChoiceMargin = margin
            MessageAPI.SendSync("[CHOICE]MARGIN_CHANGE")
        End Sub

    End Class

End Namespace