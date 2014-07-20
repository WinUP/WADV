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

        Protected MustOverride Function RenderingUI() As Boolean

        ''' <summary>
        ''' 更新显示样式并获取下一个显示状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetNextUIStyle() As Boolean
            If alreadyWaiting > 0 Then
                If alreadyWaiting > waitingTime Then
                    Return False
                Else
                    alreadyWaiting += 1
                    Return True
                End If
            End If
            Dim returnState As Boolean = AppCore.API.WindowAPI.GetWindowDispatcher.Invoke(Function() RenderingUI())
            If Not returnState Then
                alreadyWaiting = 1
            End If
            Return True
        End Function


    End Class

    ''' <summary>
    ''' 渐显显示效果
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FadeEffect : Inherits Effect.StandardEffect

        Private renderingPanelIndex As Integer = 0

        Public Sub New(choices() As FrameworkElement, wait As Integer)
            MyBase.New(choices, wait)
            For Each tmpPanel In choices
                tmpPanel.Dispatcher.Invoke(Sub() tmpPanel.Opacity = 0)
            Next
        End Sub

        Protected Overrides Function RenderingUI() As Boolean
            If renderingPanelIndex = choices.Length Then Return False
            Dim renderingPanel = choices(renderingPanelIndex)
            renderingPanel.Dispatcher.Invoke(Sub()
                                                 If renderingPanel.Opacity < 1 Then
                                                     renderingPanel.Opacity += 0.2
                                                 Else
                                                     renderingPanelIndex += 1
                                                 End If
                                             End Sub)
            Return True
        End Function

    End Class

End Namespace