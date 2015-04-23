Imports System.Windows.Controls

Namespace Receiver

    ''' <summary>
    ''' 精灵消息接收器辅助类
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class MessageReceiverHelper : Implements ISpriteMessageReceiver
        Private ReadOnly _onReceiving As Func(Of Object, Object, Object)

        Friend Sub New(onReceiving As Func(Of Object, Object, Object))
            _onReceiving = onReceiving
        End Sub

        Public Function ReceivingMessage(target As Windows.Controls.Panel, message As String) As Boolean Implements ISpriteMessageReceiver.ReceivingMessage
            Return CBool(_onReceiving.Invoke(target, message))
        End Function

    End Class
End Namespace