Imports WADV.Core.PluginInterface

Namespace ReceiverList

    ''' <summary>
    ''' 循环接收器列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class LoopReceiverList : Inherits BaseList(Of ILoopReceiver)

        ''' <summary>
        ''' 添加一个循环体
        ''' </summary>
        ''' <param name="target">要添加的循环函数</param>
        ''' <remarks></remarks>
        Friend Overrides Function Add(target As ILoopReceiver) As Boolean
            Dim answer = MyBase.Add(target)
            If answer Then Configuration.System.MessageService.SendMessage("[SYSTEM]LOOP_CONTENT_ADD", 1)
            Return answer
        End Function

        ''' <summary>
        ''' 删除一个循环体
        ''' </summary>
        ''' <param name="index">目标索引</param>
        ''' <remarks></remarks>
        Friend Overrides Function Delete(index As Integer) As Boolean
            Dim answer = MyBase.Delete(index)
            If answer Then Configuration.System.MessageService.SendMessage("[SYSTEM]LOOP_CONTENT_REMOVE", 1)
            Return answer
        End Function

        ''' <summary>
        ''' 删除一个循环体
        ''' </summary>
        ''' <param name="content">目标循环体</param>
        ''' <returns></returns>
        Friend Overrides Function Delete(content As ILoopReceiver) As Boolean
            Dim answer = MyBase.Delete(content)
            If answer Then Configuration.System.MessageService.SendMessage("[SYSTEM]LOOP_CONTENT_REMOVE", 1)
            Return answer
        End Function

        ''' <summary>
        ''' 应用所有删除和添加操作
        ''' </summary>
        Friend Sub Update()
            UpdateRemove()
            UpdateAdd()
        End Sub
    End Class
End Namespace
