Namespace Exception
    ''' <summary>
    ''' 提供带错误日志记录功能的异常声明
    ''' </summary>
    <Serializable> Public MustInherit Class FrameworkException : Inherits System.Exception
        Friend Sub New(message As String, errorType As String)
            MyBase.New(message)
            If Configuration.Path.ErrorLogFilePath <> "" Then
                My.Computer.FileSystem.WriteAllText(
                    Configuration.Path.ErrorLogFilePath,
                    $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]{errorType}: {message}",
                    True,
                    Text.Encoding.UTF8)
            End If
        End Sub
    End Class
End Namespace