Imports System.Windows.Controls
Imports System.Windows

Namespace Effect

    ''' <summary>
    ''' 选项显示效果基类
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class StandardEffect
        Protected choices() As FrameworkElement
        Protected waitingTime As Integer
        Protected alreadyWaiting As Integer = 0

        Public Sub New(choices() As FrameworkElement, wait As Integer)
            Me.choices = choices
            waitingTime = wait
        End Sub

        ''' <summary>
        ''' 渲染下一个显示状态
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub RenderingUI()

        ''' <summary>
        ''' 对选择时间计时
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetNextUIStyle() As Boolean
            If waitingTime = -1 Then Return True
            If alreadyWaiting > 0 Then
                If alreadyWaiting > waitingTime Then
                    Return False
                Else
                    alreadyWaiting += 1
                    Return True
                End If
            End If
            Return True
        End Function

    End Class

    ''' <summary>
    ''' 渐显显示效果
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FadeInEffect : Inherits Effect.StandardEffect
        Private renderingPanelIndex As Integer = 0

        Public Sub New(choices() As FrameworkElement, wait As Integer)
            MyBase.New(choices, wait)
            For Each tmpPanel In choices
                tmpPanel.Dispatcher.Invoke(Sub() tmpPanel.Opacity = 0)
            Next
        End Sub

        Public Overrides Sub RenderingUI()
            If alreadyWaiting > 0 Then Exit Sub
            If renderingPanelIndex = choices.Length Then
                alreadyWaiting = 1
                Exit Sub
            End If
            Dim renderingPanel = choices(renderingPanelIndex)
            If renderingPanel.Opacity < 1 Then
                renderingPanel.Opacity += 0.2
            Else
                renderingPanelIndex += 1
            End If
        End Sub

    End Class

End Namespace