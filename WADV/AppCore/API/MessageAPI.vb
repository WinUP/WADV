Imports System.Threading
Imports WADV.AppCore.PluginInterface

Namespace AppCore.API

    ''' <summary>
    ''' 消息API类
    ''' </summary>
    Public NotInheritable Class MessageAPI

        ''' <summary>
        ''' 添加一个接收器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Public Shared Sub AddSync(receiver As IMessageReceiver)
            ReceiverList.Add(receiver)
        End Sub

        ''' <summary>
        ''' 删除一个接收器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Public Shared Sub DeleteSync(receiver As IMessageReceiver)
            ReceiverList.Delete(receiver)
        End Sub

        ''' <summary>
        ''' 发送一个消息
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="message">消息内容</param>
        Public Shared Sub SendSync(message As String)
            MessageService.GetInstance.SendMessage(message)
        End Sub

        ''' <summary>
        ''' 等待下一个指定消息的出现
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="message">消息内容</param>
        Public Shared Sub WaitSync(message As String)
            While True
                SyncLock MessageService.LastMessage
                    For i As Integer = 0 To message.Length - 1
                        If message(i) <> MessageService.LastMessage(i) Then
                            Monitor.Wait(MessageService.LastMessage)
                            Continue While
                        End If
                    Next
                    Exit While
                End SyncLock
            End While
        End Sub

        Public Shared Function LastMessage() As String
            Return New String(MessageService.LastMessage).ToString.Trim(ChrW(0))
        End Function

    End Class

End Namespace