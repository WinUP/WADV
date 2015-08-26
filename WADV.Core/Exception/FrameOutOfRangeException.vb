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
            MyBase.New("你正在尝试设置游戏循环理想执行周期为不支持的值。它必须为正整数。")
        End Sub
    End Class
End Namespace
