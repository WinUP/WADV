Imports WADV.Core.PluginInterface

Namespace ReceiverList
    ''' <summary>
    ''' 插件加载接收器
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class PluginLoadReceiverList : Inherits BaseList(Of IPluginLoadReceiver)
        ''' <summary>
        ''' 在插件被加载前进行一定处理
        ''' </summary>
        ''' <param name="types">插件包含的所有类型</param>
        ''' <remarks></remarks>
        Friend Shared Sub BeforeLoad(types As Type())
            For Each receiver In List
                receiver.BeforeLoad(types)
            Next
        End Sub
    End Class
End Namespace
