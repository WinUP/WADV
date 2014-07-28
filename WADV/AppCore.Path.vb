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

        ''' <summary>
        ''' 获取文件的绝对路径
        ''' </summary>
        ''' <param name="type">路径类型</param>
        ''' <param name="fileURL">类型路径下的相对路径</param>
        ''' <returns>文件的绝对路径</returns>
        Protected Friend Shared Function GetFullPath(type As String, Optional fileURL As String = "") As String
            Return System.IO.Path.Combine(My.Application.Info.DirectoryPath, type, fileURL)
        End Function

    End Class


End Namespace