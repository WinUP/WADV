Namespace API
    ''' <summary>
    ''' 计时器API
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Timer

        ''' <summary>
        ''' 获取或设置计时器的计时间隔<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL<br></br>
        ''' 异常：<br></br>
        '''  TickOutOfRangeException
        ''' </summary>
        ''' <param name="value">目标间隔(毫秒)</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Tick(Optional value As Integer = -1) As Integer
            If value = -1 Then Return Configuration.System.MainTimer.Span
            If value < 1 Then Throw New Exception.TickOutOfRangeException
            Configuration.System.MainTimer.Span = value
            Return value
        End Function

        ''' <summary>
        ''' 获取或设置计时器的状态<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="value">是否启用计时器</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Status(Optional value As Object = Nothing) As Boolean
            If value Is Nothing Then Return Configuration.System.MainTimer.Status
            Dim data = DirectCast(value, Boolean)
            If data Then
                Configuration.System.MainTimer.Start()
            Else
                Configuration.System.MainTimer.Stop()
            End If
            Return value
        End Function
    End Class
End Namespace