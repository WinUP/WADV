Namespace Exception
    ''' <summary>
    ''' 表示无法找到指定精灵的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class SpriteCannotFoundException : Inherits FrameworkException
        Public Sub New()
            MyBase.New("当前场景中找不到指定名称的精灵。", "SpriteCannotFound")
        End Sub
    End Class
End Namespace
