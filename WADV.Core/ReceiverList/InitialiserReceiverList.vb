Imports WADV.Core.PluginInterface

Namespace ReceiverList

    ''' <summary>
    ''' 游戏启动接收器列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class InitialiserReceiverList
        Private Shared ReadOnly List As New List(Of IGameInitialiserReceiver)

        ''' <summary>
        ''' 添加一个游戏启动接收器
        ''' </summary>
        ''' <param name="target">要添加的游戏启动接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Add(target As IGameInitialiserReceiver)
            If Not Contains(target) Then List.Add(target)
        End Sub

        ''' <summary>
        ''' 确定指定游戏启动接收器是否已存在
        ''' </summary>
        ''' <param name="content">要检查的游戏启动接收器</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function Contains(content As IGameInitialiserReceiver) As Boolean
            Return List.Contains(content)
        End Function

        ''' <summary>
        ''' 删除一个游戏启动接收器
        ''' </summary>
        ''' <param name="content">目标游戏启动接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Delete(content As IGameInitialiserReceiver)
            If Contains(content) Then
                List.Remove(content)
            End If
        End Sub

        ''' <summary>
        ''' 传递事件到所有已注册的游戏启动接收器
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Sub InitialisingGame()
            For Each receiver In List
                receiver.InitialisingGame()
            Next
        End Sub
    End Class

End Namespace
