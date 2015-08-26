Namespace Exception

    ''' <summary>
    ''' 表示重复加载同一个插件的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PluginMultiInitialiseException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("你正在尝试重复加载同一个插件。这是不允许的。")
        End Sub
    End Class
End Namespace