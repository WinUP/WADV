Namespace Configuration
    Friend NotInheritable Class Path
        ''' <summary>
        ''' 获取或设置插件路径
        ''' </summary>
        Friend Shared PluginPath As String

        ''' <summary>
        ''' 获取或设置资源路径
        ''' </summary>
        Friend Shared ResourcePath As String

        ''' <summary>
        ''' 获取或设置皮肤路径
        ''' </summary>
        Friend Shared SkinPath As String

        ''' <summary>
        ''' 获取或设置脚本路径
        ''' </summary>
        Friend Shared ScriptPath As String

        ''' <summary>
        ''' 获取或设置用户文件路径
        ''' </summary>
        Friend Shared UserFilePath As String

        ''' <summary>
        ''' 获取或设置系统错误日志文件路径
        ''' </summary>
        Friend Shared ErrorLogFilePath As String

        ''' <summary>
        ''' 获取游戏主目录
        ''' </summary>
        Friend Shared ReadOnly GamePath As String = My.Application.Info.DirectoryPath
    End Class
End Namespace
