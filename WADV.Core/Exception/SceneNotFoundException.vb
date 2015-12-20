Namespace Exception
    ''' <summary>
    ''' 表示无法找到目标场景的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SceneNotFoundException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("你正在尝试操作不存在的场景。")
        End Sub
    End Class
End Namespace