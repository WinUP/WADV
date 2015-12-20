Namespace Exception
    ''' <summary>
    ''' 表示调用了不允许的方法的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MethodNotAllowedException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("你正在尝试调用不允许使用的方法。")
        End Sub
    End Class
End Namespace
