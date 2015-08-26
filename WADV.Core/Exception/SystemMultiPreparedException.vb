Namespace Exception

    ''' <summary>
    ''' 表示插件初始化失败的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SystemMultiPreparedException : Inherits System.Exception

        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("你正在尝试要求引擎核心重复准备启动数据。这是不允许的。")
        End Sub
    End Class
End Namespace