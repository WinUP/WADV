Imports System.Windows.Markup
Imports System.Xml
Imports System.Windows.Controls

Namespace API

    Public Class UIAPI

        Public Function GetChoiceContent() As Panel
            Return Config.UIConfig.ChoiceContent
        End Function

        Public Function GetChoiceStyle() As Panel
            Return Config.UIConfig.ChoiceStyle
        End Function

        Public Function GetChoiceAreaName() As String
            Return Config.UIConfig.ChoiceTextAreaName
        End Function

        Public Sub SetChoiceContent(content As Panel)
            Config.UIConfig.ChoiceContent = content
        End Sub

        Public Sub SetChoiceStyle(styleFile As String)
            Config.UIConfig.ChoiceStyle = XamlReader.Load(XmlTextReader.Create(AppCore.API.URLAPI.CombineURL(AppCore.API.URLAPI.GetSkinURL, styleFile)))
        End Sub

        Public Sub SetChoiceTextArea(areaName As String)
            Config.UIConfig.ChoiceTextAreaName = areaName
        End Sub

    End Class

    Public Class ChoiceAPI

        Public Sub ShowChoice(choice() As String)

        End Sub

    End Class

End Namespace