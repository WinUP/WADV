Imports System.Windows.Controls

Public Class CutdownBar : Inherits WADV.ChoiceModule.BaseProgress
    Private _bar As ProgressBar
    Private ReadOnly _parent As Panel

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
        Dim returns = MyBase.Logic()
        If returns Then
            _bar.Dispatcher.Invoke(Sub() _bar.Value = WaitFrame)
        Else
            _bar.Dispatcher.Invoke(Sub() _parent.Children.Remove(_bar))
        End If
        Return returns
    End Function

End Class
