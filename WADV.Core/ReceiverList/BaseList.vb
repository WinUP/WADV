Namespace ReceiverList

    Friend MustInherit Class BaseList(Of T)
        Protected ReadOnly List As New List(Of T)
        Private ReadOnly _addList As New List(Of T)
        Private ReadOnly _removeList As New List(Of T)

        ''' <summary>
        ''' 添加一个接收器
        ''' </summary>
        ''' <param name="target">要添加的接收器</param>
        ''' <remarks></remarks>
        Friend Overridable Function Add(target As T) As Boolean
            SyncLock _addList
                If Not Contains(target) Then
                    _addList.Add(target)

                    Return True
                Else
                    Return False
                End If
            End SyncLock
        End Function

        ''' <summary>
        ''' 获取接收器的数目
        ''' </summary>
        ''' <returns></returns>
        Friend ReadOnly Property Count() As Integer
            Get
                SyncLock List
                    SyncLock _addList
                        SyncLock _removeList
                            Return List.Count + _addList.Count - _removeList.Count
                        End SyncLock
                    End SyncLock
                End SyncLock
            End Get
        End Property

        ''' <summary>
        ''' 确定指定接收器是否已存在
        ''' </summary>
        ''' <param name="content">要检查的接收器</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Overridable Function Contains(content As T) As Boolean
            Return List.Contains(content) OrElse _addList.Contains(content) OrElse _removeList.Contains(content)
        End Function

        Default Friend ReadOnly Property Item(index As Integer) As T
            Get
                Return List(index)
            End Get
        End Property

        ''' <summary>
        ''' 删除一个接收器
        ''' </summary>
        ''' <param name="content">要删除的接收器</param>
        ''' <remarks></remarks>
        Friend Overridable Function Delete(content As T) As Boolean
            SyncLock _removeList
                If Contains(content) Then
                    _removeList.Add(content)
                    Return True
                Else
                    Return False
                End If
            End SyncLock
        End Function

        ''' <summary>
        ''' 删除一个接收器
        ''' </summary>
        ''' <param name="index">要删除的接收器的Index</param>
        ''' <returns></returns>
        Friend Overridable Function Delete(index As Integer) As Boolean
            SyncLock _removeList
                Dim content = List(index)
                If Contains(content) Then
                    _removeList.Add(content)
                    Return True
                Else
                    Return False
                End If
            End SyncLock
        End Function

        ''' <summary>
        ''' 应用所有添加操作
        ''' </summary>
        Protected Sub UpdateAdd()
            SyncLock _addList
                SyncLock List
                    List.AddRange(_addList)
                    _addList.Clear()
                End SyncLock
            End SyncLock
        End Sub

        ''' <summary>
        ''' 应用所有删除操作
        ''' </summary>
        Protected Sub UpdateRemove()
            SyncLock _removeList
                SyncLock List
                    _removeList.ForEach(Sub(e) List.Remove(e))
                    _removeList.Clear()
                End SyncLock
            End SyncLock
        End Sub
    End Class
End Namespace
