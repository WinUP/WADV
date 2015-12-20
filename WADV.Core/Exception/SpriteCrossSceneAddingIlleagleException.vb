Namespace Exception
    ''' <summary>
    ''' 表示跨场景指定精灵层次的异常
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpriteCrossSceneAddingIlleagleException : Inherits System.Exception
        ''' <summary>
        ''' 声明一个新的异常
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New("精灵不允许跨场景指定父子关系。")
        End Sub
    End Class
End Namespace
