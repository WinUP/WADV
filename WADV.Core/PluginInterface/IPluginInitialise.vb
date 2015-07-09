Namespace PluginInterface

    ''' <summary>
    ''' 插件自动初始化接收器
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IPluginInitialise
        ''' <summary>
        ''' 开始初始化
        ''' </summary>
        ''' <returns>若插件初始化失败返回False，其余情况下返回True</returns>
        Function Initialising() As Boolean
    End Interface
End Namespace