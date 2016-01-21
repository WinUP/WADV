Namespace Exception
    ''' <summary>
    ''' 表示动态编译失败的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CompileFailedException : Inherits System.Exception

        Public ReadOnly Property FilePath As String

        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(filePath As String)
            MyBase.New("动态编译错误，一个文件没有通过编译。")
            Me.FilePath = filePath
        End Sub
    End Class
End Namespace
