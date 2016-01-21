Namespace Exception
    ''' <summary>
    ''' 表示试图在同一场景加入两个名称相同的精灵的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpriteNameAlreadyExistedException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("同一场景中已存在名称相同的精灵。")
        End Sub
    End Class
End Namespace
