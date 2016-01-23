Namespace Exception

    ''' <summary>
    ''' 表示插件初始化失败的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class PluginInitialiseFailedException : Inherits FrameworkException

        ''' <summary>
        ''' 初始化失败的插件的名称
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property PluginName As String

        Public Sub New(name As String)
            MyBase.New($"插件{name}初始化失败。", "PluginInitialiseFailed")
            PluginName = name
        End Sub
    End Class
End Namespace