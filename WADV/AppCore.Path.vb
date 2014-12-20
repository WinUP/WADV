Namespace AppCore.Path

    ''' <summary>
    ''' 路径设置类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PathConfig

        ''' <summary>
        ''' 获取或设置插件路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Protected Friend Shared Property Plugin As String

        ''' <summary>
        ''' 获取或设置资源路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Protected Friend Shared Property Resource As String

        ''' <summary>
        ''' 获取或设置皮肤路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Protected Friend Shared Property Skin As String

        ''' <summary>
        ''' 获取或设置脚本路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Protected Friend Shared Property Script As String

        ''' <summary>
        ''' 获取或设置用户文件路径
        ''' </summary>
        ''' <value>新的路径</value>
        ''' <returns></returns>
        Protected Friend Shared Property UserFile As String

    End Class

    ''' <summary>
    ''' 路径功能类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PathFunction

        Public Enum PathType
            Plugin
            Resource
            Skin
            Script
            UserFile
            Other
        End Enum

        ''' <summary>
        ''' 获取文件的绝对路径
        ''' </summary>
        ''' <param name="type">路径类型</param>
        ''' <param name="filePath">从类型后开始的文件路径</param>
        ''' <returns>文件的绝对路径</returns>
        Protected Friend Shared Function GetFullPath(type As PathType, Optional filePath As String = "") As String
            Dim typePath As String = ""
            Select Case type
                Case PathType.Plugin
                    typePath = PathConfig.Plugin
                Case PathType.Resource
                    typePath = PathConfig.Resource
                Case PathType.Script
                    typePath = PathConfig.Script
                Case PathType.Skin
                    typePath = PathConfig.Skin
                Case PathType.UserFile
                    typePath = PathConfig.UserFile
            End Select
            Return System.IO.Path.Combine(My.Application.Info.DirectoryPath, typePath, filePath)
        End Function

        ''' <summary>
        ''' 获取文件的绝对路径的URI表示形式
        ''' </summary>
        ''' <param name="type">路径类型</param>
        ''' <param name="filePath">从类型开始后的文件路径</param>
        ''' <returns>文件的绝对路径</returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function GetFullUri(type As PathType, Optional filePath As String = "") As Uri
            Return New Uri(GetFullPath(type, filePath))
        End Function

    End Class

End Namespace