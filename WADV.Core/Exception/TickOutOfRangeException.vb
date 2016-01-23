Namespace Exception
    ''' <summary>
    ''' 表示计时器时间间隔设置不合法的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class TickOutOfRangeException : Inherits FrameworkException
        Public Sub New()
            MyBase.New("目标值不能用作计时器时间间隔。值必须是正整数。", "TickOutOfRange")
        End Sub
    End Class
End Namespace
