Namespace PluginInterface
    ''' <summary>
    ''' 插件加载接收器
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IPluginLoadReceiver
        ''' <summary>
        ''' 在插件被加载前进行一定处理
        ''' </summary>
        ''' <param name="types">插件包含的所有类型</param>
        ''' <remarks></remarks>
        Sub BeforeLoad(types As Type())
    End Interface
End Namespace