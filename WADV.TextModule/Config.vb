Imports System.Xml
Imports System.Windows.Controls

Namespace Config

    Public Class ModuleConfig

        Private Shared framsBetweenWord As Integer
        Private Shared autoMode As Boolean
        Private Shared framsBetweenSetence As Integer
        Private Shared ignoreRead As Boolean
        Private Shared ellipsisWord As Boolean
        Private Shared fastSpeed As Boolean

        Protected Friend Shared Property Fast As Boolean
            Get
                Return fastSpeed
            End Get
            Set(value As Boolean)
                fastSpeed = value
            End Set
        End Property

        Protected Friend Shared Property Ellipsis As Boolean
            Get
                Return ellipsisWord
            End Get
            Set(value As Boolean)
                ellipsisWord = value
            End Set
        End Property

        Protected Friend Shared Property WordFrame As Integer
            Get
                Return framsBetweenWord
            End Get
            Set(value As Integer)
                framsBetweenWord = value
                WriteConfig()
            End Set
        End Property

        Protected Friend Shared Property Auto As Boolean
            Get
                Return autoMode
            End Get
            Set(value As Boolean)
                autoMode = value
                WriteConfig()
            End Set
        End Property

        Protected Friend Shared Property SetenceFrame As Integer
            Get
                Return framsBetweenSetence
            End Get
            Set(value As Integer)
                framsBetweenSetence = value
                WriteConfig()
            End Set
        End Property

        Protected Friend Shared Property Ignore As Boolean
            Get
                Return ignoreRead
            End Get
            Set(value As Boolean)
                ignoreRead = value
                WriteConfig()
            End Set
        End Property

        Protected Friend Shared Sub ReadConfigFile()
            Dim configFile As New XmlDocument
            configFile.Load(AppCore.API.URLAPI.CombineURL(AppCore.API.URLAPI.GetUserFileURL, "WADV.TextModule.xml"))
            WordFrame = CInt(configFile.SelectSingleNode("/config/framsBetweenWord").InnerXml)
            SetenceFrame = CInt(configFile.SelectSingleNode("/config/framsBetweenSetence").InnerXml)
            Auto = If(configFile.SelectSingleNode("/config/autoMode").InnerXml = "True", True, False)
            Ignore = If(configFile.SelectSingleNode("/config/ignoreRead").InnerXml = "True", True, False)
        End Sub

        Private Shared Sub WriteConfig()
            Dim configFile As New XmlDocument
            configFile.Load(AppCore.API.URLAPI.CombineURL(AppCore.API.URLAPI.GetUserFileURL, "WADV.TextModule.xml"))
            configFile.SelectSingleNode("/config/framsBetweenWord").InnerXml = WordFrame
            configFile.SelectSingleNode("/config/framsBetweenSetence").InnerXml = SetenceFrame
            configFile.SelectSingleNode("/config/autoMode").InnerXml = If(Auto, "True", "False")
            configFile.SelectSingleNode("/config/ignoreRead").InnerXml = If(Ignore, "True", "False")
            configFile.Save(AppCore.API.URLAPI.CombineURL(AppCore.API.URLAPI.GetUserFileURL, "WADV.TextModule.xml"))
        End Sub

    End Class

    Public Class UIConfig

        Protected Friend Shared TextArea As TextBlock = Nothing
        Protected Friend Shared CharacterArea As TextBlock = Nothing

    End Class

End Namespace
