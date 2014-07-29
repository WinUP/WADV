Imports System.Windows.Controls
Imports System.Windows.Media.Imaging

Namespace Tachie

    Public Class Core

        Private Shared egaList As New Dictionary(Of Integer, Image)
        Private Shared egaID As Integer = 0

        Protected Friend Shared Function AddTachie(fileName As String, x As Double, y As Double, width As Double, height As Double, z As Integer, effect As TachieEffect.BaseEffect) As Integer
            Dim tmpImage As New Image
            tmpImage.BeginInit()
            tmpImage.Height = height
            tmpImage.HorizontalAlignment = Windows.HorizontalAlignment.Left
            tmpImage.VerticalAlignment = Windows.VerticalAlignment.Top
            tmpImage.Margin = New Windows.Thickness(x, y, 0, 0)
            tmpImage.Visibility = Windows.Visibility.Collapsed
            tmpImage.Width = width
            tmpImage.Source = New BitmapImage(New Uri(PathAPI.GetPath(PathAPI.Resource, fileName)))
            tmpImage.SetValue(Grid.ZIndexProperty, z)
            tmpImage.EndInit()
            egaList.Add(egaID, tmpImage)
            egaID += 1
            WindowAPI.GetGrid.Children.Add(tmpImage)
            Return egaID - 1
        End Function

    End Class

End Namespace
