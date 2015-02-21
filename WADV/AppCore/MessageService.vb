Imports System.Threading
Imports System.Collections.Concurrent
Imports WADV.AppCore.PluginInterface

Namespace AppCore

    ''' <summary>
    ''' 游戏消息循环
    ''' </summary>
    Public NotInheritable Class MessageService
        Friend Shared ReadOnly LastMessage(100) As Char
        Private Shared _self As MessageService
        Private ReadOnly _callList As List(Of IMessageReceiver)
        Private ReadOnly _receiverThread As Thread
        Private ReadOnly _messageList As ConcurrentQueue(Of String)

        ''' <summary>
        ''' 添加一个接收器
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Public Sub AddReceiver(receiver As IMessageReceiver)
            If _callList.Contains(receiver) Then Return
            _callList.Add(receiver)
        End Sub

        ''' <summary>
        ''' 删除一个接收器
        ''' </summary>
        ''' <param name="receiver">接收器实体</param>
        Public Sub RemoveReceiver(receiver As IMessageReceiver)
            If Not _callList.Contains(receiver) Then Return
            _callList.Remove(receiver)
        End Sub

        ''' <summary>
        ''' 发送一条消息
        ''' </summary>
        ''' <param name="message">消息内容</param>
        Public Sub SendMessage(message As String)
            Monitor.Enter(_messageList)
            _messageList.Enqueue(message)
            Monitor.Pulse(_messageList)
            Monitor.Exit(_messageList)
        End Sub

        Private Sub New()
            _callList = New List(Of IMessageReceiver)
            _messageList = New ConcurrentQueue(Of String)
            _receiverThread = New Thread(AddressOf MessageContent)
            _receiverThread.Name = "消息循环线程"
            _receiverThread.Priority = ThreadPriority.AboveNormal
            _receiverThread.IsBackground = True
            _receiverThread.Start()
        End Sub

        Private Sub MessageContent()
            Dim message As String = ""
            Monitor.Enter(_messageList)
            While True
                While Not _messageList.IsEmpty
                    _messageList.TryDequeue(message)
#If DEBUG Then
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss,fff ") & message)
#End If
                    For Each receiver In _callList
                        receiver.ReceivingMessage(message)
                    Next
                    SyncLock LastMessage
                        For i As Integer = 0 To message.Length - 1
                            LastMessage(i) = message(i)
                        Next
                        Monitor.PulseAll(LastMessage)
                    End SyncLock
                End While
                Monitor.Wait(_messageList)
            End While
        End Sub

        ''' <summary>
        ''' 获取消息循环的唯一实例
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetInstance() As MessageService
            If _self Is Nothing Then _self = New MessageService
            Return _self
        End Function

    End Class

    ''' <summary>
    ''' 消息循环辅助类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class WaitReceiver

        ''' <summary>
        ''' 等待指定消息的发出
        ''' </summary>
        ''' <param name="message">要等待的消息</param>
        ''' <remarks></remarks>
        Public Sub WaitMessage(message As String)
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

    End Class

End Namespace
