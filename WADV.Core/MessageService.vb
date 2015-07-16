' WinUP Adventure Game Engine Core Framework
' Game Message Service Class
' This is the message service class

Imports System.Threading
Imports System.Collections.Concurrent
Imports WADV.Core.ReceiverList

''' <summary>
''' 游戏消息循环
''' </summary>
Friend NotInheritable Class MessageService
    Private _status As Boolean
    Private Shared _self As MessageService
    Private ReadOnly _receiverThread As Thread
    Private ReadOnly _messageList As ConcurrentQueue(Of String)
    Friend Shared ReadOnly LastMessage(49) As Char

    ''' <summary>
    ''' 获取消息循环的状态
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property Status As Boolean
        Get
            Return _status
        End Get
    End Property

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
        _receiverThread.Name = "[系统]消息循环线程"
        _receiverThread.Priority = ThreadPriority.AboveNormal
        _receiverThread.IsBackground = True
    End Sub

    Private Sub MessageContent()
        Dim message As String = Nothing
        SyncLock (_messageList)
            While _status
                While Not _messageList.IsEmpty
                    If Not _messageList.TryDequeue(message) Then Throw New System.Exception("从消息队列中获取消息失败")
#If DEBUG Then
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss,fff ") & message)
#End If
                    MessageReceiverList.Broadcast(message)
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

    ''' <summary>
    ''' 启动消息循环
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Start()
        _status = True
        _receiverThread.Start()
    End Sub

    ''' <summary>
    ''' 终止消息循环
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub [Stop]()
        _status = False
    End Sub

End Class
