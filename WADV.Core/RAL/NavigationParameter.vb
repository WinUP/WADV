Namespace RAL

    ''' <summary>
    ''' 表示游戏转场请求
    ''' </summary>
    Public NotInheritable Class NavigationParameter
        ''' <summary>
        ''' 转场前的场景
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property PreviousScene As Scene
        ''' <summary>
        ''' 转场后的场景
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property NextScene As Scene
        ''' <summary>
        ''' 是否取消这次转场
        ''' </summary>
        ''' <returns></returns>
        Public Property Canceled As Boolean
        ''' <summary>
        ''' 本次请求的额外数据
        ''' </summary>
        ''' <returns></returns>
        Public Property ExtraData As Object

        ''' <summary>
        ''' 声明一个转场请求
        ''' </summary>
        ''' <param name="previous">转场前的场景</param>
        ''' <param name="next">转场后的场景</param>
        Public Sub New(previous As Scene, [next] As Scene)
            PreviousScene = previous
            NextScene = [next]
            Canceled = False
            ExtraData = Nothing
        End Sub

    End Class
End Namespace