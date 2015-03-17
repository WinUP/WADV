Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Public Class MessageReceiver : Implements IMessageReceiver

        Public Sub ReceivingMessage(message As String) Implements IMessageReceiver.ReceivingMessage
            For Each target As MessageHud In HudList.MessageHudList.Values
                target.Render(message)
            Next
        End Sub
    End Class

End Namespace
