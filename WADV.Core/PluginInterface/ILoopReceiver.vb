Namespace PluginInterface
    ''' <summary>
    ''' 游戏循环接收器
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ILoopReceiver
        ''' <summary>
        ''' 执行一次循环
        ''' </summary>
        Function Logic(frame As Integer) As Boolean
        ''' <summary>
        ''' 执行一次渲染
        ''' </summary>
        ''' <remarks></remarks>
        Sub Render()
    End Interface
End Namespace
