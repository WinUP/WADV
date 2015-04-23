Imports System.Windows.Controls

Namespace Receiver
    Public Interface ISpriteMessageReceiver

        Function ReceivingMessage(target As Panel, message As String) As Boolean

    End Interface
End Namespace