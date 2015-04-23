Namespace PluginInterface

    Public Class Initialise : Implements WADV.Core.PluginInterface.IPluginInitialise

        Public Function Initialising() As Boolean Implements WADV.Core.PluginInterface.IPluginInitialise.Initialising
            AddHandler WindowAPI.GetWindow.MouseLeftButtonDown, Sub() MessageAPI.SendSync("[WINDOW]MOUSE_CLICK")
            AddHandler WindowAPI.GetWindow.KeyDown, Sub(sender, e)
                                                        If e.Key = Windows.Input.Key.Left Then
                                                            MessageAPI.SendSync("[WINDOW]LEFT_KEY_PRESS")
                                                        ElseIf e.Key = Windows.Input.Key.Right Then
                                                            MessageAPI.SendSync("[WINDOW]RIGHT_KEY_PRESS")
                                                        End If
                                                    End Sub
            Config.TargetShip = New Ship
            LoopAPI.AddLoopSync(New LoopReceiver)
            Return True
        End Function

    End Class

End Namespace
