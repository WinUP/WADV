Namespace Exception
    ''' <summary>
    ''' 表示游戏循环理想执行周期设置超出可用范围的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class FrameOutOfRangeException : Inherits FrameworkException
        Public Sub New()
            MyBase.New("目标值不能用作游戏循环理想执行周期。值必须是正整数。", "FrameOutOfRange")
        End Sub
    End Class
End Namespace
