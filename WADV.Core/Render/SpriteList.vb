Namespace Render
    Public Class SpriteList : Inherits List(Of Sprite)
        Private ReadOnly Property SpriteDelegator As Sprite

        Public Sub New(target As Sprite)
            MyBase.New
            SpriteDelegator = target
        End Sub
        Public Shadows Sub Add(target As Sprite)
            If Contains(target) Then Exit Sub
            target.SetParent(SpriteDelegator)
            MyBase.Add(target)
        End Sub
        Public Shadows Sub AddRange(collection As IEnumerable(Of Sprite))
            collection.ToList.ForEach(Sub(e) e.SetParent(SpriteDelegator))
            MyBase.AddRange(collection)
        End Sub
        Public Shadows Sub Clear()
            ForEach(Sub(e) e.SetParent(Nothing))
            MyBase.Clear()
        End Sub
        ''' <summary>
        ''' 注意：重写这个方法是为了防止误调用。SpriteList不允许复制！千万不要使用这个方法！
        ''' <br></br>
        ''' WARNING: We overwrite this function to avoid the purpose that may use it by mistake.
        ''' <br></br>
        ''' SpriteList do not allowed copy! NEVER USER THIS FUNCTION PLEASE!
        ''' </summary>
        Public Shadows Sub CopyTo(array() As Sprite)
            Throw New Exception.MethodNotAllowedException
        End Sub
        ''' <summary>
        ''' 注意：重写这个方法是为了防止误调用。SpriteList不允许复制！千万不要使用这个方法！
        ''' <br></br>
        ''' WARNING: We overwrite this function to avoid the purpose that may use it by mistake.
        ''' <br></br>
        ''' SpriteList do not allowed copy! NEVER USER THIS FUNCTION PLEASE!
        ''' </summary>
        Public Shadows Sub CopyTo(array() As Sprite, arrayIndex As Integer)
            Throw New Exception.MethodNotAllowedException
        End Sub
        ''' <summary>
        ''' 注意：重写这个方法是为了防止误调用。SpriteList不允许复制！千万不要使用这个方法！
        ''' <br></br>
        ''' WARNING: We overwrite this function to avoid the purpose that may use it by mistake.
        ''' <br></br>
        ''' SpriteList do not allowed copy! NEVER USER THIS FUNCTION PLEASE!
        ''' </summary>
        Public Shadows Sub CopyTo(index As Integer, array() As Sprite, arrayIndex As Integer, count As Integer)
            Throw New Exception.MethodNotAllowedException
        End Sub
        Public Shadows Sub Insert(index As Integer, item As Sprite)
            If index >= Count Then Throw New ArgumentOutOfRangeException(NameOf(index))
            item.SetParent(SpriteDelegator)
            MyBase.Insert(index, item)
        End Sub
        Public Shadows Sub InsertRange(index As Integer, collection As IEnumerable(Of Sprite))
            collection.ToList.ForEach(Sub(e) e.SetParent(SpriteDelegator))
            MyBase.InsertRange(index, collection)
        End Sub
        Public Shadows Sub RemoveAt(index As Integer)
            If index >= Count Then Throw New ArgumentOutOfRangeException
            Item(index).SetParent(Nothing)
            MyBase.RemoveAt(index)
        End Sub
        Public Shadows Sub RemoveRange(index As Integer, count As Integer)
            If index < 0 OrElse count > Me.Count Then Throw New ArgumentOutOfRangeException()
            For Each e In Skip(index).Take(count)
                e.SetParent(Nothing)
            Next
            MyBase.RemoveRange(index, count)
        End Sub
        Public Shadows Function Remove(item As Sprite) As Boolean
            If Not Contains(item) Then Return False
            item.SetParent(Nothing)
            Return MyBase.Remove(item)
        End Function
        Public Shadows Function RemoveAll(match As Predicate(Of Sprite)) As Integer
            If match Is Nothing Then Throw New ArgumentNullException(NameOf(match))
            Where(Function(e) match(e)).ToList.ForEach(Sub(e) e.SetParent(Nothing))
            Return MyBase.RemoveAll(match)
        End Function
    End Class
End Namespace