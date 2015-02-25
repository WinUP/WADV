Imports System.Threading
Imports System.Collections.Concurrent
Imports WADV.AppCore.PluginInterface

Namespace AppCore

    ''' <summary>
    ''' 消息接收器列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class ReceiverList
        Private Shared ReadOnly List As New List(Of IMessageReceiver)

        ''' <summary>
        ''' 添加一个消息接收器
        ''' </summary>
        ''' <param name="target">要添加的消息接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Add(target As IMessageReceiver)
            If Not Contains(target) Then  List.Add(target)
        End Sub

        ''' <summary>
        ''' 确定指定消息接收器是否已存在
        ''' </summary>
        ''' <param name="content">要检查的消息接收器</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function Contains(content As IMessageReceiver) As Boolean
            Return List.Contains(content)
        End Function

        ''' <summary>
        ''' 删除一个消息接收器
        ''' </summary>
        ''' <param name="content">目标消息接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Delete(content As IMessageReceiver)
            If Contains(content) Then
                List.Remove(content)
            End If
        End Sub

        ''' <summary>
        ''' 传递消息到所有已注册的消息接收器
        ''' </summary>
        ''' <param name="message">要传递的消息</param>
        ''' <remarks></remarks>
        Friend Shared Sub [Call](ByRef message As String)
            For Each receiver In List
                receiver.ReceivingMessage(message)
            Next
        End Sub

    End Class

    ''' <summary>
    ''' 游戏消息循环
    ''' </summary>
    Friend NotInheritable Class MessageService
        Friend Shared ReadOnly LastMessage(49) As Char
        Private Shared _self As MessageService
        Private ReadOnly _receiverThread As Thread
        Private ReadOnly _messageList As ConcurrentQueue(Of String) '!注意：ConcurrentQueue存在内存泄漏，是否迁移到.NET 4.5?

        ''' <summary>
        ''' 发送一条消息
        ''' </summary>
        ''' <param name="message">消息内容</param>
        Friend Sub SendMessage(message As String)
            Monitor.Enter(_messageList)
            _messageList.Enqueue(message)
            Monitor.Pulse(_messageList)
            Monitor.Exit(_messageList)
        End Sub

        Private Sub New()
            _messageList = New ConcurrentQueue(Of String)
            _receiverThread = New Thread(AddressOf MessageContent)
            _receiverThread.Name = "消息循环线程"
            _receiverThread.Priority = ThreadPriority.AboveNormal
            _receiverThread.IsBackground = True
            _receiverThread.Start()
        End Sub

        Private Sub MessageContent()
            Dim message As String
            SyncLock (_messageList)
                While True
                    While Not _messageList.IsEmpty
                        '!已进行异常处理，此处不会引用空值，请忽略警告
                        If Not _messageList.TryDequeue(message) Then Throw New Exception("从消息队列中获取消息失败")
#If DEBUG Then
                        Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss,fff ") & message)
#End If
                        ReceiverList.Call(message)
                        SyncLock LastMessage
                            For i As Integer = 0 To message.Length - 1
                                LastMessage(i) = message(i)
                            Next
                            For i As Integer = message.Length To 49
                                LastMessage(i) = Nothing
                            Next
                            Monitor.PulseAll(LastMessage)
                        End SyncLock
                    End While
                    Monitor.Wait(_messageList)
                End While
            End SyncLock
        End Sub

        ''' <summary>
        ''' 获取消息循环的唯一实例
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function GetInstance() As MessageService
            If _self Is Nothing Then _self = New MessageService
            Return _self
        End Function

        Friend Sub [Stop]()
            _receiverThread.Abort()
        End Sub

    End Class

End Namespace
