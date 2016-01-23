﻿Namespace GameSystem
    ''' <summary>
    ''' 路径辅助类
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class PathFunction
        ''' <summary>
        ''' 获取文件的绝对路径
        ''' </summary>
        ''' <param name="type">路径类型</param>
        ''' <param name="filePath">从类型后开始的文件路径</param>
        ''' <returns>文件的绝对路径</returns>
        Friend Shared Function GetFullPath(type As PathType, Optional filePath As String = "") As String
            Dim typePath As String = ""
            Select Case type
                Case PathType.Plugin
                    typePath = Configuration.Path.PluginPath
                Case PathType.Resource
                    typePath = Configuration.Path.ResourcePath
                Case PathType.Script
                    typePath = Configuration.Path.ScriptPath
                Case PathType.Skin
                    typePath = Configuration.Path.SkinPath
                Case PathType.UserFile
                    typePath = Configuration.Path.UserFilePath
            End Select
            Return IO.Path.Combine(typePath, filePath)
        End Function

        ''' <summary>
        ''' 获取文件的绝对路径的URI表示形式
        ''' </summary>
        ''' <param name="type">路径类型</param>
        ''' <param name="filePath">从类型开始后的文件路径</param>
        ''' <returns>文件的绝对路径</returns>
        ''' <remarks></remarks>
        Friend Shared Function GetFullUri(type As PathType, Optional filePath As String = "") As Uri
            Return New Uri(GetFullPath(type, filePath))
        End Function
    End Class
End Namespace