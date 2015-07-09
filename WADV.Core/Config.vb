Imports System.Windows.Navigation

''' <summary>
''' 窗口设定类
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class Config

    ''' <summary>
    ''' 游戏主窗口
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared BaseWindow As NavigationWindow

    ''' <summary>
    ''' 获取或设置插件路径
    ''' </summary>
    ''' <value>新的路径</value>
    ''' <returns></returns>
    Friend Shared Property PluginPath As String

    ''' <summary>
    ''' 获取或设置资源路径
    ''' </summary>
    ''' <value>新的路径</value>
    ''' <returns></returns>
    Friend Shared Property ResourcePath As String

    ''' <summary>
    ''' 获取或设置皮肤路径
    ''' </summary>
    ''' <value>新的路径</value>
    ''' <returns></returns>
    Friend Shared Property SkinPath As String

    ''' <summary>
    ''' 获取或设置脚本路径
    ''' </summary>
    ''' <value>新的路径</value>
    ''' <returns></returns>
    Friend Shared Property ScriptPath As String

    ''' <summary>
    ''' 获取或设置用户文件路径
    ''' </summary>
    ''' <value>新的路径</value>
    ''' <returns></returns>
    Friend Shared Property UserFilePath As String

    ''' <summary>
    ''' 获取游戏主目录
    ''' </summary>
    ''' <returns></returns>
    Friend Shared ReadOnly Property GamePath As String
        Get
            Return My.Application.Info.DirectoryPath
        End Get
    End Property

    ''' <summary>
    ''' 脚本核心
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Property ScriptEngine As IScriptEngine = Nothing
End Class