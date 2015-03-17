Imports System.Threading
Imports WADV.Core.PluginInterface
Imports WADV.Core.ReceiverList

Namespace API

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
            MessageReceiverList.Add(receiver)
        End Sub

        ''' <summary>
        ''' 删除一个接收器
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Public Shared Sub DeleteSync(receiver As IMessageReceiver)
            MessageReceiverList.Delete(receiver)
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

        ''' <summary>
        ''' 获取最近广播的一条信息
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function LastMessage() As String
            Return New String(MessageService.LastMessage).ToString.Trim(ChrW(0))
        End Function

        ''' <summary>
        ''' 启动消息循环
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StartSync()
            MessageService.GetInstance.Start()
        End Sub

        ''' <summary>
        ''' 终止消息循环
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopSync()
            MessageService.GetInstance.Stop()
        End Sub

        ''' <summary>
        ''' 获取消息循环的状态
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetStatus() As Boolean
            Return MessageService.GetInstance.Status
        End Function

    End Class

End Namespace