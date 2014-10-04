Imports System.Threading
Imports System.Collections.Concurrent

Namespace AppCore.Message

    ''' <summary>
    ''' 消息循环
    ''' </summary>
    Public Class MessageService
        Private Shared self As MessageService
        Private callList As List(Of Plugin.IMessage)
        Private receiverThread As Thread
        Private messageList As ConcurrentQueue(Of String)
        Private lastMessage As String = ""

        ''' <summary>
        ''' 添加一个接收器
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Protected Friend Sub AddReceiver(receiver As Plugin.IMessage)
            If callList.Contains(receiver) Then Return
            callList.Add(receiver)
        End Sub

        ''' <summary>
        ''' 删除一个接收器
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Protected Friend Sub DeleteReceiver(receiver As Plugin.IMessage)
            If Not callList.Contains(receiver) Then Return
            callList.Remove(receiver)
        End Sub

        ''' <summary>
        ''' 发送一个消息
        ''' </summary>
        ''' <param name="message">消息内容</param>
        Protected Friend Sub SendMessage(message As String)
            Monitor.Enter(messageList)
            messageList.Enqueue(message)
            Monitor.Pulse(messageList)
            Monitor.Exit(messageList)
        End Sub

        Protected Friend Sub WaitMessage(message As String)
            While True
                SyncLock (lastMessage)
                    If lastMessage = message Then
                        Exit While
                    End If
                End SyncLock
            End While
        End Sub

        Private Sub New()
            callList = New List(Of Plugin.IMessage)
            messageList = New ConcurrentQueue(Of String)
            receiverThread = New Thread(
                Sub()
                    Dim message As String = ""
                    Monitor.Enter(messageList)
                    While True
                        While Not messageList.IsEmpty
                            messageList.TryDequeue(message)
                            For Each receiver In callList
                                receiver.ReceivingMessage(message)
                            Next
                        End While
                        SyncLock (lastMessage)
                            lastMessage = message
                        End SyncLock
                        Monitor.Wait(messageList)
                    End While
                    Monitor.Exit(messageList)
                End Sub)
            receiverThread.Name = "消息循环线程"
            receiverThread.Priority = ThreadPriority.AboveNormal
            receiverThread.IsBackground = True
            receiverThread.Start()
        End Sub

        ''' <summary>
        ''' 获取消息循环的唯一实例
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function GetInstance() As MessageService
            If self Is Nothing Then self = New MessageService
            Return self
        End Function

    End Class

End Namespace
