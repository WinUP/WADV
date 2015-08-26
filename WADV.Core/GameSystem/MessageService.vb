Imports System.Threading
Imports System.Collections.Concurrent
Imports WADV.Core.ReceiverList

Namespace GameSystem
    ''' <summary>
    ''' 消息循环
    ''' </summary>
    Friend NotInheritable Class MessageService
        Private ReadOnly _receiver As Thread
        Private ReadOnly _list As New ConcurrentQueue(Of String)
        Friend ReadOnly LastMessage(49) As Char

        ''' <summary>
        ''' 获取消息循环的状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend ReadOnly Property Status As Boolean = False

        ''' <summary>
        ''' 发送一条消息
        ''' </summary>
        ''' <param name="message">消息内容</param>
        Friend Sub SendMessage(message As String)
            Monitor.Enter(_list)
            _list.Enqueue(message)
            Monitor.Pulse(_list)
            Monitor.Exit(_list)
        End Sub

        ''' <summary>
        ''' 获得一个消息循环实例
        ''' </summary>
        Friend Sub New()
            _receiver = New Thread(AddressOf MessageContent)
            _receiver.Name = "[系统]消息循环线程"
            _receiver.Priority = ThreadPriority.AboveNormal
            _receiver.IsBackground = True
        End Sub

        ''' <summary>
        ''' 消息循环主函数
        ''' </summary>
        Private Sub MessageContent()
            Dim message As String = Nothing
            SyncLock (_list)
                While _Status
                    While Not _list.IsEmpty
                        If Not _list.TryDequeue(message) Then Throw New Exception.MessageDequeueFailedException
#If DEBUG Then
                        Debug.WriteLine(Date.Now.ToString($"HH:mm:ss,fff {message}"))
#End If
                        MessageReceiverList.Broadcast(message)
                        SyncLock LastMessage
                            For i = 0 To message.Length - 1
                                LastMessage(i) = message(i)
                            Next
                            For i = message.Length To 49
                                LastMessage(i) = Nothing
                            Next
                            Monitor.PulseAll(LastMessage)
                        End SyncLock
                    End While
                    Monitor.Wait(_list)
                End While
            End SyncLock
        End Sub

        ''' <summary>
        ''' 启动消息循环（如果消息循环正在运行则不进行任何操作）
        ''' </summary>
        ''' <remarks></remarks>
        Friend Sub Start()
            If Status Then Exit Sub
            _Status = True
            _receiver.Start()
        End Sub

        ''' <summary>
        ''' 终止消息循环（如果消息循环本身并没有启动则不进行任何操作）
        ''' </summary>
        ''' <remarks></remarks>
        Friend Sub [Stop]()
            _Status = False
        End Sub
    End Class
End Namespace

