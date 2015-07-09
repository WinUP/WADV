Imports System.Windows.Controls

Public Class CutdownBar : Inherits WADV.ChoiceModule.BaseProgress
    Private _bar As ProgressBar
    Private ReadOnly _parent As Panel
    Private _returns As Boolean

    Public Sub New(choices() As Button, waitFrame As Integer)
        MyBase.New(choices, waitFrame)
        _parent = choices(0).Parent
        _parent.Dispatcher.Invoke(Sub()
                                      _bar = New ProgressBar
                                      _bar.BeginInit()
                                      _bar.Width = 400
                                      _bar.Height = 20
                                      _bar.HorizontalAlignment = Windows.HorizontalAlignment.Center
                                      _bar.VerticalAlignment = Windows.VerticalAlignment.Bottom
                                      _bar.Maximum = waitFrame
                                      _bar.Value = waitFrame
                                      _bar.EndInit()
                                      _parent.Children.Add(_bar)
                                  End Sub)
    End Sub

    Public Overrides Function Logic() As Boolean
        _returns = MyBase.Logic()
        Return _returns
    End Function

    Public Overrides Sub Render()
        If _returns Then
            _bar.Value = WaitFrame
        Else
            _parent.Children.Remove(_bar)
        End If
    End Sub
End Class
