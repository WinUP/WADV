Namespace Exception
    ''' <summary>
    ''' 表示动态编译失败的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class CompileFailedException : Inherits FrameworkException

        ''' <summary>
        ''' 编译错误的文件的路径
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property FilePath As String

        Public Sub New(filePath As String)
            MyBase.New($"文件{filePath}没有通过编译。", "CompileFailed")
            Me.FilePath = filePath
        End Sub
    End Class
End Namespace
