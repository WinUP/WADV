Imports System.Windows
Imports System.Windows.Controls

Namespace API

    Public Class TEAPI

        Public Function Show(fileName As String, effectName As String, width As Double, height As Double, x As Double, y As Double, param() As String) As Integer
            If Config.UIConfig.ImagePanel Is Nothing Then Return False
            If Not Effect.Initialiser.EffectList.ContainsKey(effectName) Then Return False
            Dim tmpImage As New Image
            tmpImage.BeginInit()
            tmpImage.Width = width
            tmpImage.Height = height
            tmpImage.Margin = New Thickness(x, y, 0, 0)
            tmpImage.EndInit()
            Dim id = TEList.List.Add(tmpImage)
            Dim tmpEffect As Effect.IEffect = Activator.CreateInstance(Effect.Initialiser.EffectList(effectName), New Object() {id, param})
            Config.UIConfig.ImagePanel.Dispatcher.Invoke(Sub() Config.UIConfig.ImagePanel.Children.Add(tmpImage))
            tmpEffect.Rendering()
            Return id
        End Function

        Public Function Hide(id As Integer) As Boolean
            Return TEList.List.Delete(id)
        End Function

        Public Function Change(id As Integer, effectName As String, params() As String) As Boolean
            If Not Effect.Initialiser.EffectList.ContainsKey(effectName) Then Return False

            Return False
        End Function

    End Class

    Public Class ConfigAPI

        Public Shared Sub Init(imagePanel As Panel)
            Config.UIConfig.ImagePanel = imagePanel
            MessageAPI.SendSync("TE_SCRIPT_INITFINISH")
        End Sub

    End Class

End Namespace
