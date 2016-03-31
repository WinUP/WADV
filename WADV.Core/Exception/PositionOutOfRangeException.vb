Namespace Exception
    ''' <summary>
    ''' 表示游戏循环理想执行周期设置超出可用范围的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class PositionOutOfRangeException : Inherits FrameworkException
        Public Sub New()
            MyBase.New("目标值不能用作播放位置。值必须在0到Duration之间（包括两端）。", "PositionOutOfRange")
        End Sub
    End Class
End Namespace