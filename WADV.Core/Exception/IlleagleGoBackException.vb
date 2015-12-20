Namespace Exception
    ''' <summary>
    ''' 表示试图在无法后退时回到上一个场景的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class IlleagleGoBackException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("你尝试在无法后退的情况下要求回到上一个场景。")
        End Sub
    End Class
End Namespace