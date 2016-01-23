Namespace Exception

    ''' <summary>
    ''' 表示重复加载同一个插件的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class PluginMultiInitialisesException : Inherits FrameworkException
        Public Sub New()
            MyBase.New("要加载的插件早已加载成功，不能重复加载。", "PluginMultiInitialises")
        End Sub
    End Class
End Namespace