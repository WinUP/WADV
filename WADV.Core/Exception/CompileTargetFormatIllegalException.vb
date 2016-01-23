Namespace Exception
    ''' <summary>
    ''' 表示动态编译的目标文件格式不正确的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class CompileTargetFormatIllegalException : Inherits FrameworkException

        Public Sub New()
            MyBase.New("无法编译非VB.NET或C#的代码文件。", "CompileTargetFormatIllegal")
        End Sub
    End Class
End Namespace