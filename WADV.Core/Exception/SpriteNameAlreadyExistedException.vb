Namespace Exception
    ''' <summary>
    ''' 表示试图在同一场景加入两个名称相同的精灵的异常
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable> Public Class SpriteNameAlreadyExistedException : Inherits FrameworkException
        Public Sub New()
            MyBase.New("同一场景中已存在名称相同的精灵。", "SpriteNameAlreadyExistedException")
        End Sub
    End Class
End Namespace
