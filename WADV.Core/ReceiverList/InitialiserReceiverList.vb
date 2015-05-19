Imports WADV.Core.PluginInterface

Namespace ReceiverList

    ''' <summary>
    ''' 游戏启动接收器列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class InitialiserReceiverList : Inherits BaseList(Of IGameInitialiserReceiver)
        
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
