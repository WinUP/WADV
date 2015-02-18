Namespace AppCore.API

    ''' <summary>
    ''' 路径API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PathAPI

        ''' <summary>
        ''' 获取程序资源文件的存放路径
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>程序资源文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Resource() As String
            Return Config.ResourcePath
        End Function

        ''' <summary>
        ''' 获取程序皮肤文件的存放路径
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>皮肤文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Skin() As String
            Return Config.SkinPath
        End Function

        ''' <summary>
        ''' 获取程序插件的存放路径
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>插件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Plugin() As String
            Return Config.PluginPath
        End Function

        ''' <summary>
        ''' 获取程序脚本文件的存放路径
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>脚本文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function Script() As String
            Return Config.ScriptPath
        End Function

        ''' <summary>
        ''' 获取程序用户个人文件的存放路径
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>用户个人文件的存放路径</returns>
        ''' <remarks></remarks>
        Public Shared Function UserFile() As String
            Return Config.UserFilePath
        End Function

        ''' <summary>
        ''' 获取程序主存储目录
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>主存储目录</returns>
        ''' <remarks></remarks>
        Public Shared Function Game() As String
            Return My.Application.Info.DirectoryPath
        End Function

        ''' <summary>
        ''' 获取完整路径
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="urlType">资源路径类型</param>
        ''' <param name="fileURL">从资源目录开始的文件相对路径</param>
        ''' <returns>文件的绝对路径</returns>
        ''' <remarks></remarks>
        Public Shared Function GetPath(urlType As PathType, Optional fileURL As String = "") As String
            Return PathFunction.GetFullPath(urlType, fileURL)
        End Function

        ''' <summary>
        ''' 获取完整路径的URI表示形式
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="urlType">资源路径类型</param>
        ''' <param name="fileURL">从资源目录开始的文件相对路径</param>
        ''' <returns>文件的绝对路径</returns>
        ''' <remarks></remarks>
        Public Shared Function GetUri(urlType As PathType, Optional fileURL As String = "") As Uri
            Return New Uri(PathFunction.GetFullPath(urlType, fileURL))
        End Function

    End Class

End Namespace