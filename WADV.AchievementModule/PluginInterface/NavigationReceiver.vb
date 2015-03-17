'Imports System.Threading
'Imports System.Windows
'Imports System.Windows.Media.Animation
'Imports System.Windows.Navigation
'Imports WADV.AppCore.PluginInterface

'Namespace PluginInterface

'    Friend Class NavigationReceiver : Implements INavigationReceiver

'        Public Sub RecevingNavigate(e As NavigatingCancelEventArgs) Implements INavigationReceiver.RecevingNavigate
'            If ShowList.IsShowing Then
'                Dim tmpThread As New Thread(AddressOf Thread_Running)
'                tmpThread.Name = "成就显示监听线程"
'                tmpThread.Priority = ThreadPriority.BelowNormal
'                tmpThread.IsBackground = True
'                e.Cancel = True
'                tmpThread.Start(e)
'            End If
'        End Sub

'        Private Sub Thread_Running(e As NavigatingCancelEventArgs)
'            WindowAPI.InvokeAsync(Sub()
'                                      Dim fadeOut As New DoubleAnimation(0.0, New Duration(TimeSpan.FromMilliseconds(600)))
'                                      fadeOut.EasingFunction = New QuinticEase With {.EasingMode = EasingMode.EaseOut}
'                                      WindowAPI.GetWindow.BeginAnimation(NavigationWindow.OpacityProperty, fadeOut)
'                                  End Sub)
'            While ShowList.IsShowing
'                MessageAPI.WaitSync("[ACHIEVE]SHOW_FINISH")
'            End While
'            Select Case e.NavigationMode
'                Case NavigationMode.Back
'                    WindowAPI.GoBackSync()
'                Case NavigationMode.Forward
'                    WindowAPI.GoForwardSync()
'                Case NavigationMode.New
'                    If e.Uri Is Nothing Then
'                        WindowAPI.LoadObjectAsync(e.Content, NavigateOperation.NavigateAndFadeIn)
'                    Else
'                        WindowAPI.LoadUriAsync(e.Uri, NavigateOperation.NavigateAndFadeIn)
'                    End If
'            End Select
'        End Sub

'    End Class

'End Namespace