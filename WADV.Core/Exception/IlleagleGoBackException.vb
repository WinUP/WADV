Namespace Exception
    ''' <summary>
    ''' 表示试图在无法后退时回到上一个场景的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class IlleagleGoBackException : Inherits FrameworkException
        Public Sub New()
            MyBase.New("窗口当前不允许进行场景后退。", "IlleagleGoBackException")
        End Sub
    End Class
End Namespace