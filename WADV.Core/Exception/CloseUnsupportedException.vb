Namespace Exception
    ''' <summary>
    ''' 表示试图关闭不可关闭的窗口的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CloseUnsupportedException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("你正在要求不允许关闭的窗口进入关闭。")
        End Sub
    End Class
End Namespace
