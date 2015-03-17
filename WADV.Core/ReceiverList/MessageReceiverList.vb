Imports WADV.Core.PluginInterface

Namespace ReceiverList

    ''' <summary>
    ''' 消息接收器列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class MessageReceiverList
        Private Shared ReadOnly List As New List(Of IMessageReceiver)

        ''' <summary>
        ''' 添加一个消息接收器
        ''' </summary>
        ''' <param name="target">要添加的消息接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Add(target As IMessageReceiver)
            If Not Contains(target) Then List.Add(target)
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
        Friend Shared Sub Boardcast(ByRef message As String)
            For Each receiver In List
                receiver.ReceivingMessage(message)
            Next
        End Sub
    End Class

End Namespace
