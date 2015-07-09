Namespace Exception
    ''' <summary>
    ''' 表示游戏循环理想执行周期设置超出可用范围的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FrameOutOfRangeException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("对游戏循环理想执行周期的设置超出了可用范围(1-∞)")
        End Sub
    End Class
End Namespace
