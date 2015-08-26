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
            MyBase.New("你正在尝试设置计时器计时间隔为不支持的值。它必须为正整数。")
        End Sub
    End Class
End Namespace
