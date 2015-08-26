Imports System.Windows.Navigation
Imports WADV.Core.GameSystem

Namespace Config
    ''' <summary>
    ''' 游戏系统数据存储模块
    ''' </summary>
    Friend Module SystemConfig
        ''' <summary>
        ''' 获取或设置游戏主窗口
        ''' </summary>
        ''' <remarks></remarks>
        Friend BaseWindow As NavigationWindow

        ''' <summary>
        ''' 获取或设置插件路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Friend Property PluginPath As String

        ''' <summary>
        ''' 获取或设置资源路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Friend Property ResourcePath As String

        ''' <summary>
        ''' 获取或设置皮肤路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Friend Property SkinPath As String

        ''' <summary>
        ''' 获取或设置脚本路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Friend Property ScriptPath As String

        ''' <summary>
        ''' 获取或设置用户文件路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Friend Property UserFilePath As String

        ''' <summary>
        ''' 获取游戏主目录
        ''' </summary>
        ''' <returns></returns>
        Friend ReadOnly Property GamePath As String = My.Application.Info.DirectoryPath

        ''' <summary>
        ''' 获取脚本核心实例
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Property ScriptEngine As IScriptEngine

        ''' <summary>
        ''' 获取消息循环实例
        ''' </summary>
        ''' <returns></returns>
        Friend Property MessageService As MessageService

        ''' <summary>
        ''' 获取计时器实例
        ''' </summary>
        ''' <returns></returns>
        Friend Property MainTimer As MainTimer

        ''' <summary>
        ''' 获取游戏循环实例
        ''' </summary>
        ''' <returns></returns>
        Friend Property MainLoop As MainLoop

        ''' <summary>
        ''' 游戏引擎是否已经准备好了启动数据
        ''' </summary>
        ''' <returns></returns>
        Friend Property IsSystemPrepared As Boolean

        ''' <summary>
        ''' 游戏引擎是否已经加载过了Plugin目录下的所有插件
        ''' </summary>
        ''' <returns></returns>
        Friend Property IsPluginInitialised As Boolean

        ''' <summary>
        ''' 游戏引擎是否正在运行
        ''' </summary>
        ''' <returns></returns>
        Friend Property IsSystemRunning As Boolean
    End Module
End Namespace