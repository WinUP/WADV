Imports System.Threading
Imports System.Collections.Concurrent

Namespace GameSystem
    ''' <summary>
    ''' 消息循环
    ''' </summary>
    Friend NotInheritable Class MessageService
        Private ReadOnly _receiver As Thread
        Private ReadOnly _list As New ConcurrentQueue(Of Message)
        Private _status As Boolean = False
        Private Shared _instance As MessageService
        Friend ReadOnly LastMessage(49) As Char

        ''' <summary>
        ''' 获取消息服务的唯一实例
        ''' </summary>
        ''' <returns></returns>
        Friend Shared Function GetInstance() As MessageService
            If _instance Is Nothing Then _instance = New MessageService
            Return _instance
        End Function

        ''' <summary>
        ''' 获得一个消息循环实例
        ''' </summary>
        Private Sub New()
            _receiver = New Thread(AddressOf MessageContent)
            _receiver.Name = "[系统]消息循环线程"
            _receiver.Priority = ThreadPriority.AboveNormal
            _receiver.IsBackground = True
        End Sub

        ''' <summary>
        ''' 获取消息循环的状态
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Property Status As Boolean
            Get
                Return _status
            End Get
            Set(value As Boolean)
                If value = _status Then Exit Property
                _status = value
                If value Then _receiver.Start()
            End Set
        End Property

        ''' <summary>
        ''' 发送一条消息<br></br>
        ''' 消息类型采按位比较，系统消息为1B，内置组件消息为10B，其余消息系统未规定，可自由选择
        ''' </summary>
        ''' <param name="content">消息内容</param>
        Friend Sub SendMessage(content As String, type As Integer)
            Monitor.Enter(_list)
            _list.Enqueue(New Message With {.Content = content, .Type = type})
            Monitor.Pulse(_list)
            Monitor.Exit(_list)
        End Sub

        ''' <summary>
        ''' 消息循环主函数
        ''' </summary>
        Private Sub MessageContent()
            Dim message As Message = Nothing
            SyncLock (_list)
                While _Status
                    While Not _list.IsEmpty
                        If Not _list.TryDequeue(message) Then Throw New Exception.MessageDequeueFailedException
#If DEBUG Then
                        Debug.WriteLine(Date.Now.ToString($"HH:mm:ss,fff {message}"))
#End If
                        Configuration.Receiver.MessageReceiver.Broadcast(message)
                        SyncLock LastMessage
                            For i = 0 To message.Content.Length - 1
                                LastMessage(i) = message.Content(i)
                            Next
                            For i = message.Content.Length To 49
                                LastMessage(i) = Nothing
                            Next
                            Monitor.PulseAll(LastMessage)
                        End SyncLock
                    End While
                    Monitor.Wait(_list)
                End While
            End SyncLock
        End Sub
    End Class
End Namespace