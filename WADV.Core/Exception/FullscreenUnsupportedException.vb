Namespace Exception
    ''' <summary>
    ''' 表示试图全屏化不可全屏的窗口的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class FullscreenUnsupportedException : Inherits FrameworkException
        Public Sub New()
            MyBase.New("窗口当前不允许进入全屏模式。", "FullscreenUnsupported")
        End Sub
    End Class
End Namespace