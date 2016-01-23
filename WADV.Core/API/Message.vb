Imports System.Threading
Imports WADV.Core.PluginInterface

Namespace API
    ''' <summary>
    ''' 消息API
    ''' </summary>
    Public Class Message
        ''' <summary>
        ''' 添加一个接收器<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Public Shared Function Listen(receiver As IMessageReceiver) As Boolean
            Return Configuration.Receiver.MessageReceiver.Add(receiver)
        End Function

        ''' <summary>
        ''' 删除一个接收器<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Public Shared Sub Remove(receiver As IMessageReceiver)
            Configuration.Receiver.MessageReceiver.Delete(receiver)
        End Sub

        ''' <summary>
        ''' 发送一个消息<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="message">消息内容</param>
        ''' <param name="type">消息类型（请将需要接收的类型的二进制位置为1，其余置为0）</param>
        Public Shared Sub Send(message As String, type As Integer)
            Configuration.System.MessageService.SendMessage(message, type)
        End Sub

        ''' <summary>
        ''' 等待下一个指定消息的出现<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="message">消息内容</param>
        Public Shared Sub Wait(message As String)
            While True
                SyncLock Configuration.System.MessageService.LastMessage
                    For i As Integer = 0 To message.Length - 1
                        If message(i) <> Configuration.System.MessageService.LastMessage(i) Then
                            Monitor.Wait(Configuration.System.MessageService.LastMessage)
                            Continue While
                        End If
                    Next
                    Exit While
                End SyncLock
            End While
        End Sub

        ''' <summary>
        ''' 获取最近广播的一条信息<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Last() As String
            Return New String(Configuration.System.MessageService.LastMessage).ToString.Trim(ChrW(0))
        End Function

        ''' <summary>
        ''' 获取消息循环的状态<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="value">是否启用消息循环</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Status(Optional value As Object = Nothing) As Boolean
            If value Is Nothing Then Return Configuration.System.MessageService.Status
            Dim data = DirectCast(value, Boolean)
            If data Then
                Configuration.System.MessageService.Start()
            Else
                Configuration.System.MessageService.Stop()
            End If
            Return data
        End Function
    End Class
End Namespace