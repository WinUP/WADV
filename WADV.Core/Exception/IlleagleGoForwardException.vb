Namespace Exception
    ''' <summary>
    ''' 表示试图在无法前进时前进到下一个场景的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class IlleagleGoForwardException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("你尝试在无法前进的情况下要求前进到下一个场景。")
        End Sub
    End Class
End Namespace