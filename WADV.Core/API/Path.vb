Namespace API
    ''' <summary>
    ''' 路径API
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Path
        ''' <summary>
        ''' 获取或设置程序资源文件的存放路径<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <returns>程序资源文件的存放路径，不需要设置的话不要传递字符</returns>
        ''' <remarks></remarks>
        Public Shared Function Resource(Optional path As String = "") As String
            If path = "" Then Return Configuration.Path.ResourcePath
            Configuration.Path.ResourcePath = path
            Return path
        End Function

        ''' <summary>
        ''' 获取或设置程序皮肤文件的存放路径<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <returns>皮肤文件的存放路径，不需要设置的话不要传递字符</returns>
        ''' <remarks></remarks>
        Public Shared Function Skin(Optional path As String = "") As String
            If path = "" Then Return Configuration.Path.SkinPath
            Configuration.Path.SkinPath = path
            Return path
        End Function

        ''' <summary>
        ''' 获取或设置程序插件的存放路径<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <returns>插件的存放路径，不需要设置的话不要传递字符</returns>
        ''' <remarks></remarks>
        Public Shared Function Plugin(Optional path As String = "") As String
            If path = "" Then Return Configuration.Path.PluginPath
            Configuration.Path.PluginPath = path
            Return path
        End Function

        ''' <summary>
        ''' 获取或设置程序脚本文件的存放路径<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <returns>脚本文件的存放路径，不需要设置的话不要传递字符</returns>
        ''' <remarks></remarks>
        Public Shared Function Script(Optional path As String = "") As String
            If path = "" Then Return Configuration.Path.ScriptPath
            Configuration.Path.ScriptPath = path
            Return path
        End Function

        ''' <summary>
        ''' 获取或设置程序用户个人文件的存放路径<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <returns>用户个人文件的存放路径，不需要设置的话不要传递字符</returns>
        ''' <remarks></remarks>
        Public Shared Function UserFile(Optional path As String = "") As String
            If path = "" Then Return Configuration.Path.UserFilePath
            Configuration.Path.UserFilePath = path
            Return path
        End Function

        ''' <summary>
        ''' 获取程序主存储目录<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <returns>主存储目录</returns>
        ''' <remarks></remarks>
        Public Shared Function Game() As String
            Return Configuration.Path.GamePath
        End Function

        ''' <summary>
        ''' 获取完整路径<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="pathType">资源路径类型</param>
        ''' <param name="filePath">从资源目录开始的文件相对路径</param>
        ''' <returns>文件的绝对路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Combine(pathType As PathType, Optional filePath As String = "") As String
            Return PathFunction.GetFullPath(pathType, filePath)
        End Function

        ''' <summary>
        ''' 获取完整路径的URI表示形式<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="pathType">资源路径类型</param>
        ''' <param name="filePath">从资源目录开始的文件相对路径</param>
        ''' <returns>文件的绝对路径</returns>
        ''' <remarks></remarks>
        Public Shared Function CombineUri(pathType As PathType, Optional filePath As String = "") As Uri
            Return PathFunction.GetFullUri(pathType, filePath)
        End Function
    End Class
End Namespace