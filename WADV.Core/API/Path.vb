Namespace API
    ''' <summary>
    ''' 路径API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Path
        ''' <summary>
        ''' 获取或设置程序资源文件的存放路径
        ''' </summary>
        ''' <returns>程序资源文件的存放路径，不需要设置的话不要传递字符</returns>
        ''' <remarks></remarks>
        Public Function Resource(Optional path As String = "") As String
            If path = "" Then Return Config.ResourcePath
            Config.ResourcePath = path
            Return path
        End Function

        ''' <summary>
        ''' 获取或设置程序皮肤文件的存放路径
        ''' </summary>
        ''' <returns>皮肤文件的存放路径，不需要设置的话不要传递字符</returns>
        ''' <remarks></remarks>
        Public Function Skin(Optional path As String = "") As String
            If path = "" Then Return Config.SkinPath
            Config.SkinPath = path
            Return path
        End Function

        ''' <summary>
        ''' 获取或设置程序插件的存放路径
        ''' </summary>
        ''' <returns>插件的存放路径，不需要设置的话不要传递字符</returns>
        ''' <remarks></remarks>
        Public Function Plugin(Optional path As String = "") As String
            If path = "" Then Return Config.PluginPath
            Config.PluginPath = path
            Return path
        End Function

        ''' <summary>
        ''' 获取或设置程序脚本文件的存放路径
        ''' </summary>
        ''' <returns>脚本文件的存放路径，不需要设置的话不要传递字符</returns>
        ''' <remarks></remarks>
        Public Function Script(Optional path As String = "") As String
            If path = "" Then Return Config.ScriptPath
            Config.ScriptPath = path
            Return path
        End Function

        ''' <summary>
        ''' 获取或设置程序用户个人文件的存放路径
        ''' </summary>
        ''' <returns>用户个人文件的存放路径，不需要设置的话不要传递字符</returns>
        ''' <remarks></remarks>
        Public Function UserFile(Optional path As String = "") As String
            If path = "" Then Return Config.UserFilePath
            Config.UserFilePath = path
            Return path
        End Function

        ''' <summary>
        ''' 获取程序主存储目录
        ''' </summary>
        ''' <returns>主存储目录</returns>
        ''' <remarks></remarks>
        Public Function Game() As String
            Return My.Application.Info.DirectoryPath
        End Function

        ''' <summary>
        ''' 获取完整路径
        ''' </summary>
        ''' <param name="pathType">资源路径类型</param>
        ''' <param name="filePath">从资源目录开始的文件相对路径</param>
        ''' <returns>文件的绝对路径</returns>
        ''' <remarks></remarks>
        Public Function Combine(pathType As PathType, Optional filePath As String = "") As String
            Return PathFunction.GetFullPath(pathType, filePath)
        End Function

        ''' <summary>
        ''' 获取完整路径的URI表示形式
        ''' </summary>
        ''' <param name="pathType">资源路径类型</param>
        ''' <param name="filePath">从资源目录开始的文件相对路径</param>
        ''' <returns>文件的绝对路径</returns>
        ''' <remarks></remarks>
        Public Function CombineUri(pathType As PathType, Optional filePath As String = "") As Uri
            Return PathFunction.GetFullUri(pathType, filePath)
        End Function
    End Module
End Namespace