Imports WADV.Core.RAL.Tool

Namespace RAL

    ''' <summary>
    ''' 游戏场景信息描述类
    ''' </summary>
    Public MustInherit Class Scene : Inherits RenderNode(Of Sprite)

        ''' <summary>
        ''' 获得一个新的场景
        ''' </summary>
        ''' <param name="name">场景名称</param>
        Public Sub New(name As String)
            MyBase.New(name)
        End Sub

        ''' <summary>
        ''' 添加一个具有指定名称的精灵到场景<br></br>
        ''' 同一场景中不允许精灵重名
        ''' </summary>
        ''' <param name="target">要添加的精灵</param>
        Public Function Add(target As Sprite) As Sprite
            If ChildList.ContainsKey(target.Name) Then Throw New Exception.SpriteNameAlreadyExistedException
            ChildList.Add(target.Name, target)
            target.RaiseBindToScene(Me)
            Return target
        End Function

        ''' <summary>
        ''' 添加一个具有指定名称的精灵到场景，并指定一个父精灵<br></br>
        ''' 同一场景中不允许精灵重名
        ''' </summary>
        ''' <param name="target">要添加的精灵</param>
        ''' <param name="parent">精灵的父精灵</param>
        ''' <returns></returns>
        Public Function Add(target As Sprite, parent As Sprite)
            If ChildList.ContainsKey(target.Name) Then Throw New Exception.SpriteNameAlreadyExistedException
            If Not ChildList.ContainsValue(parent) Then Throw New Exception.SpriteCannotBeFoundException
            ChildList.Add(target.Name, target)
            target.RaiseBindToScene(Me)
            parent.AddChildren(target)
            Return target
        End Function

        ''' <summary>
        ''' 添加一个具有指定名称的精灵到场景，并指定一个父精灵<br></br>
        ''' 同一场景中不允许精灵重名
        ''' </summary>
        ''' <param name="target">要添加的精灵</param>
        ''' <param name="parentName">精灵的父精灵的名称</param>
        ''' <returns></returns>
        Public Function Add(target As Sprite, parentName As String)
            If ChildList.ContainsKey(target.Name) Then Throw New Exception.SpriteNameAlreadyExistedException
            If Not ChildList.ContainsKey(parentName) Then Throw New Exception.SpriteCannotBeFoundException
            ChildList.Add(target.Name, target)
            target.RaiseBindToScene(Me)
            ChildList(parentName).AddChildren(target)
            Return target
        End Function

        ''' <summary>
        ''' 递归查找具有指定名称的精灵
        ''' </summary>
        ''' <param name="spriteName">要查找的名称</param>
        ''' <returns></returns>
        Public Function Find(spriteName As String) As Sprite
            If ChildList.ContainsKey(spriteName) Then Return ChildList(spriteName)
            Return Nothing
        End Function

        Private Shared Function SearchSprite(target As String, root As Sprite) As Sprite
            Dim answer = root.AllChildren.Where(Function(e) e.Name = target).FirstOrDefault
            If answer IsNot Nothing Then Return answer
            For Each e In root.AllChildren
                answer = SearchSprite(target, e)
                If answer IsNot Nothing Then Return answer
            Next
            Return Nothing
        End Function

        ''' <summary>
        ''' 从场景中删除一个精灵
        ''' </summary>
        ''' <param name="target">要删除的精灵的名称</param>
        Public Sub RemoveSprite(target As String)
            Dim sprite = Find(target)
            If sprite Is Nothing Then Throw New Exception.SpriteCannotBeFoundException
            If sprite.Parent IsNot Nothing Then sprite.Parent.RemoveChildren(sprite)
            sprite.RaiseUnbindFromScene(Me)
            ChildList.Remove(target)
        End Sub

        ''' <summary>
        ''' 从场景中删除一个精灵
        ''' </summary>
        ''' <param name="target">要删除的精灵</param>
        Public Sub RemoveSprite(target As Sprite)
            If Not ChildList.ContainsKey(target.Name) Then Throw New Exception.SpriteCannotBeFoundException
            If target.Parent IsNot Nothing Then target.Parent.RemoveChildren(target)
            target.RaiseUnbindFromScene(Me)
            ChildList.Remove(target.Name)
        End Sub

        ''' <summary>
        ''' 获取场景的内容对象
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride Function Content() As Object

        ''' <summary>
        ''' 进入场景后触发的事件
        ''' </summary>
        ''' <param name="param">提供的导航参数</param>
        Public Sub Scene_Entered(param As Object)
            Scene_Entered_Implement(param)
            RaiseEvent SceneEntered(param)
        End Sub

        Protected MustOverride Sub Scene_Entered_Implement(param As Object)
        Public Event SceneEntered(param As Object)

        ''' <summary>
        ''' 场景进入前触发的事件
        ''' </summary>
        ''' <param name="lastScene">当前的场景</param>
        ''' <param name="param">提供的导航参数</param>
        ''' <returns>允许进入则返回True，否则返回False</returns>
        ''' <remarks>该事件晚于上一个场景的Scene_Exit</remarks>
        Public Overridable Function Scene_Enter(lastScene As Scene, param As Object) As Boolean
            Return True
        End Function

        ''' <summary>
        ''' 场景退出时触发的事件
        ''' </summary>
        ''' <param name="nextScene">下一个场景</param>
        ''' <param name="param">提供的导航参数</param>
        ''' <returns>允许退出则返回True，否则返回False</returns>
        ''' <remarks>该事件早于下一个场景的Scene_Enter</remarks>
        Public Overridable Function Scene_Exit(nextScene As Scene, param As Object) As Boolean
            Return True
        End Function
    End Class
End Namespace