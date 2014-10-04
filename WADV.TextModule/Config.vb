Imports System.Xml
Imports System.Windows.Controls

Namespace Config

    ''' <summary>
    ''' 模块设置类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ModuleConfig

        Private Shared framsBetweenWord As Integer
        Private Shared autoMode As Boolean
        Private Shared framsBetweenSetence As Integer
        Private Shared ignoreRead As Boolean
        Private Shared ellipsisWord As Boolean
        Private Shared fastSpeed As Boolean

        ''' <summary>
        ''' 获取或设置快进状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Fast As Boolean
            Get
                Return fastSpeed
            End Get
            Set(value As Boolean)
                fastSpeed = value
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置略过效果状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Ellipsis As Boolean
            Get
                Return ellipsisWord
            End Get
            Set(value As Boolean)
                ellipsisWord = value
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置文字间隔帧
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property WordFrame As Integer
            Get
                Return framsBetweenWord
            End Get
            Set(value As Integer)
                framsBetweenWord = value
                WriteConfig()
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置自动播放状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Auto As Boolean
            Get
                Return autoMode
            End Get
            Set(value As Boolean)
                autoMode = value
                WriteConfig()
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置句子间隔帧
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property SetenceFrame As Integer
            Get
                Return framsBetweenSetence
            End Get
            Set(value As Integer)
                framsBetweenSetence = value
                WriteConfig()
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置略过状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Ignore As Boolean
            Get
                Return ignoreRead
            End Get
            Set(value As Boolean)
                ignoreRead = value
                WriteConfig()
            End Set
        End Property

        ''' <summary>
        ''' 读取配置文件
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub ReadConfigFile()
            Dim configFile As New XmlDocument
            configFile.Load(PathAPI.GetPath(AppCore.Path.PathFunction.PathType.UserFile, "WADV.TextModule.xml"))
            WordFrame = CInt(configFile.SelectSingleNode("/config/framsBetweenWord").InnerXml)
            SetenceFrame = CInt(configFile.SelectSingleNode("/config/framsBetweenSetence").InnerXml)
            Auto = If(configFile.SelectSingleNode("/config/autoMode").InnerXml = "True", True, False)
            Ignore = If(configFile.SelectSingleNode("/config/ignoreRead").InnerXml = "True", True, False)
        End Sub

        ''' <summary>
        ''' 保存配置
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared Sub WriteConfig()
            Dim configFile As New XmlDocument
            configFile.Load(PathAPI.GetPath(AppCore.Path.PathFunction.PathType.UserFile, "WADV.TextModule.xml"))
            configFile.SelectSingleNode("/config/framsBetweenWord").InnerXml = WordFrame
            configFile.SelectSingleNode("/config/framsBetweenSetence").InnerXml = SetenceFrame
            configFile.SelectSingleNode("/config/autoMode").InnerXml = If(Auto, "True", "False")
            configFile.SelectSingleNode("/config/ignoreRead").InnerXml = If(Ignore, "True", "False")
            configFile.Save(PathAPI.GetPath(AppCore.Path.PathFunction.PathType.UserFile, "WADV.TextModule.xml"))
        End Sub

    End Class

    ''' <summary>
    ''' 界面显示设置类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UIConfig

        ''' <summary>
        ''' 对话内容区域
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared TextArea As TextBlock = Nothing
        ''' <summary>
        ''' 说话者区域
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared CharacterArea As TextBlock = Nothing

        ''' <summary>
        ''' 主显示区域
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared FrameArea As Windows.FrameworkElement = Nothing

    End Class

End Namespace
