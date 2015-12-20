Namespace Exception
    ''' <summary>
    ''' 表示试图全屏化不可全屏的窗口的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FullscreenUnsupportedException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("你正在要求不允许全屏的窗口进入全屏模式。")
        End Sub
    End Class
End Namespace