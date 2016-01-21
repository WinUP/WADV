Namespace Exception

    ''' <summary>
    ''' 表示插件初始化失败的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PluginInitialiseFailedException : Inherits System.Exception
        Public ReadOnly Property PluginName As String

        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <param name="name">初始化失败的插件的名称</param>
        ''' <remarks></remarks>
        Public Sub New(name As String)
            MyBase.New("尝试加载的一个插件因为初始化失败而未能加载。")
            PluginName = name
        End Sub
    End Class
End Namespace