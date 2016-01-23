Namespace Exception

    ''' <summary>
    ''' 表示插件初始化失败的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class SystemMultiPreparedException : Inherits FrameworkException
        Public Sub New()
            MyBase.New("不允许对游戏引擎进行多次初始化。", "SystemMultiPrepared")
        End Sub
    End Class
End Namespace