Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media.Animation

Namespace PluginInterface

    Public Class LoopReceiver : Implements WADV.Core.PluginInterface.ILoopReceiver
        Private ReadOnly _boomList As New List(Of Canvas)
        Private ReadOnly _submarineList As New List(Of Canvas)

        '!-QUALITY LOW
        Public Function Logic(frame As Integer) As Boolean Implements WADV.Core.PluginInterface.ILoopReceiver.Logic
            For Each boom In BoomList.List
                Dim boomMargin = boom.Margin
                For Each submarine In SubmarineList.List
                    Dim subMargin = submarine.Margin
                    If subMargin.Left < boomMargin.Left Then
                        If boomMargin.Left - subMargin.Left < submarine.Width AndAlso boomMargin.Top - subMargin.Top < submarine.Height Then
                            If Not _boomList.Contains(boom) Then _boomList.Add(boom)
                            If Not _submarineList.Contains(submarine) Then _submarineList.Add(submarine)
                            If BoomList.List.Contains(boom) Then BoomList.List.Remove(boom)
                            If SubmarineList.List.Contains(submarine) Then SubmarineList.List.Remove(submarine)
                        End If
                    Else
                        If subMargin.Left - boomMargin.Left < boom.Width AndAlso subMargin.Top - boomMargin.Top < boom.Height Then
                            If Not _boomList.Contains(boom) Then _boomList.Add(boom)
                            If Not _submarineList.Contains(submarine) Then _submarineList.Add(submarine)
                            If BoomList.List.Contains(boom) Then BoomList.List.Remove(boom)
                            If SubmarineList.List.Contains(submarine) Then SubmarineList.List.Remove(submarine)
                        End If
                    End If
                Next
            Next
            Return True
        End Function

        Public Sub Render() Implements WADV.Core.PluginInterface.ILoopReceiver.Render
            _boomList.ForEach(Sub(e) SpriteAPI.Delete(e))
            _submarineList.ForEach(Sub(e)
                                       Dim animation As New DoubleAnimation(0.0, New Duration(TimeSpan.FromSeconds(1)))
                                       AddHandler animation.Completed, Sub() SpriteAPI.Delete(e)
                                       e.BeginAnimation(Canvas.OpacityProperty, animation)
                                   End Sub)
        End Sub

    End Class


End Namespace