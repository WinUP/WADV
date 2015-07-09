Namespace Exception

    ''' <summary>
    ''' 表示重复启动游戏的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class IllegalMultipleStartException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("游戏引擎核心被非法多次启动")
        End Sub
    End Class
End Namespace