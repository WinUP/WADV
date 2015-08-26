Namespace Exception

    ''' <summary>
    ''' 表示重复要求游戏引擎加载Plugin目录下所有插件的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PluginsMultiInitialiseException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("你正在尝试重复加载Plugin目录下的所有插件。这是不允许的。")
        End Sub
    End Class
End Namespace