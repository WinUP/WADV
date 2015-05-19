Namespace Exception

    ''' <summary>
    ''' 表示插件初始化失败的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PluginInitialiseFailedException : Inherits System.Exception

        Private ReadOnly _pluginName As String
        Public ReadOnly Property PluginName As String
            Get
                Return _pluginName
            End Get
        End Property

        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <param name="name">初始化失败的插件的名称</param>
        ''' <remarks></remarks>
        Public Sub New(name As String)
            MyBase.New("插件初始化失败")
            _pluginName = name
        End Sub
    End Class
End Namespace