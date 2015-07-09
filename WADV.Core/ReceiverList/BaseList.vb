Namespace ReceiverList

    Friend MustInherit Class BaseList(Of T)
        Protected Shared ReadOnly List As New List(Of T)

        ''' <summary>
        ''' 添加一个接收器
        ''' </summary>
        ''' <param name="target">要添加的接收器</param>
        ''' <remarks></remarks>
        Friend Shared Function Add(target As T) As Boolean
            If Not Contains(target) Then
                List.Add(target)
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' 确定指定接收器是否已存在
        ''' </summary>
        ''' <param name="content">要检查的接收器</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function Contains(content As T) As Boolean
            Return List.Contains(content)
        End Function

        ''' <summary>
        ''' 删除一个接收器
        ''' </summary>
        ''' <param name="content">要删除的接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Delete(content As T)
            If Contains(content) Then
                List.Remove(content)
            End If
        End Sub
    End Class
End Namespace
