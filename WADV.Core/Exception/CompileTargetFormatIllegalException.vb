Namespace Exception
    ''' <summary>
    ''' 表示动态编译的目标文件格式不正确的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CompileTargetFormatIllegalException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("你正在尝试编译非VB.NET且非CSharp的代码文件，这目前是不受支持的。")
        End Sub
    End Class
End Namespace