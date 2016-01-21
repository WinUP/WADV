Namespace Exception
    ''' <summary>
    ''' 表示无法找到指定精灵的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpriteCannotBeFoundException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("你提供的名称或对象无法定位到这个场景中的精灵。你确定它在这个场景中吗？")
        End Sub
    End Class
End Namespace
