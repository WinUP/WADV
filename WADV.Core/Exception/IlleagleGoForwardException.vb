Namespace Exception
    ''' <summary>
    ''' 表示试图在无法前进时前进到下一个场景的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class IlleagleGoForwardException : Inherits FrameworkException
        Public Sub New()
            MyBase.New("窗口当前不允许进行场景前进。", "IlleagleGoForward")
        End Sub
    End Class
End Namespace