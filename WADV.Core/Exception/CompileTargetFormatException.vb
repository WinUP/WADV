Namespace Exception
    ''' <summary>
    ''' 表示动态编译的目标文件格式不正确的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CompileTargetFormatException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("编译目标必须是VB.NET或CSharp的代码文件，且不能包含代码错误")
        End Sub
    End Class
End Namespace