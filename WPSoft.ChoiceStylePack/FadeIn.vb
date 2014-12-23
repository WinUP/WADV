Imports WADV.ChoiceModule
Imports System.Windows.Controls

''' <summary>
''' 渐显显示效果
''' </summary>
''' <remarks></remarks>
Public Class FadeInEffect : Inherits Effect.BaseEffect
    Private renderingPanelIndex As Integer = 0

    Public Sub New(choices() As TextBlock, wait As Integer)
        MyBase.New(choices, wait)
        For Each choice In choices
            choice.Dispatcher.Invoke(Sub() choice.Opacity = 0)
        Next
    End Sub

    Public Overrides Sub Render()
        If initFinished Then
            If countBlock IsNot Nothing AndAlso waitTime > -1 Then
                countBlock.Text = waitTime
            Else
                Exit Sub
            End If
        Else
            If renderingPanelIndex = choices.Length Then
                initFinished = True
                Exit Sub
            End If
            Dim renderingPanel = choices(renderingPanelIndex)
            If renderingPanel.Opacity < 1 Then
                renderingPanel.Opacity += 0.2
            Else
                renderingPanelIndex += 1
            End If
        End If 
    End Sub

End Class
