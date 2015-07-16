﻿Namespace Exception
    ''' <summary>
    ''' 表示计时器时间间隔设置不合法的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TickOutOfRangeException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("对计时器计时间隔的设置超出了可用范围(1-∞)")
        End Sub
    End Class
End Namespace