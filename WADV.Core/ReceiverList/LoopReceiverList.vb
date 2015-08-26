Imports WADV.Core.PluginInterface

Namespace ReceiverList

    ''' <summary>
    ''' 循环接收器列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class LoopReceiverList
        Private Shared ReadOnly List As New List(Of ILoopReceiver)

        ''' <summary>
        ''' 添加一个循环体
        ''' </summary>
        ''' <param name="target">要添加的循环函数</param>
        ''' <remarks></remarks>
        Friend Shared Function Add(target As ILoopReceiver) As Boolean
            If Not Contains(target) Then
                List.Add(target)
                Config.MessageService.SendMessage("[SYSTEM]LOOP_CONTENT_ADD")
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' 确定指定循环体是否已存在
        ''' </summary>
        ''' <param name="content">要检查的循环体</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function Contains(content As ILoopReceiver) As Boolean
            Return List.Contains(content)
        End Function

        ''' <summary>
        ''' 获得一个循环体
        ''' </summary>
        ''' <param name="i">目标索引</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function [Get](i As Integer) As ILoopReceiver
            Return List(i)
        End Function

        ''' <summary>
        ''' 删除一个循环体
        ''' </summary>
        ''' <param name="i">目标索引</param>
        ''' <remarks></remarks>
        Friend Shared Sub Delete(i As Integer)
            List.RemoveAt(i)
            Config.MessageService.SendMessage("[SYSTEM]LOOP_CONTENT_REMOVE")
        End Sub

        ''' <summary>
        ''' 获取已注册的循环体的数目
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function Count() As Integer
            Return List.Count
        End Function

    End Class

End Namespace
