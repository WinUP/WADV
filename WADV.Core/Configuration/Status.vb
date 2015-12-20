Namespace Configuration
    Friend Class Status
        ''' <summary>
        ''' 游戏引擎是否已经准备好了启动数据
        ''' </summary>
        Friend Shared IsSystemPrepared As Boolean

        ''' <summary>
        ''' 游戏引擎是否已经加载过了Plugin目录下的所有插件
        ''' </summary>
        Friend Shared IsPluginInitialised As Boolean

        ''' <summary>
        ''' 游戏引擎是否正在运行
        ''' </summary>
        Friend Shared IsSystemRunning As Boolean
    End Class
End Namespace
