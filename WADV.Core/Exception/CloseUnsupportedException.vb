Namespace Exception
    ''' <summary>
    ''' 表示试图关闭不可关闭的窗口的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class CloseUnsupportedException : Inherits FrameworkException
        Public Sub New()
            MyBase.New("窗口当前不允许被关闭。", "CloseUnsupported")
        End Sub
    End Class
End Namespace
