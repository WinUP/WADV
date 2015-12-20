Imports System.Collections.ObjectModel

Namespace Render

    ''' <summary>
    ''' 游戏场景信息描述类
    ''' </summary>
    Public MustInherit Class Scene
        ''' <summary>
        ''' 获取场景的名称
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Name As String
        Private ReadOnly Property Sprites As Dictionary(Of String, Sprite)

        ''' <summary>
        ''' 获得一个新的场景描述
        ''' </summary>
        ''' <param name="name">场景名称</param>
        Public Sub New(name As String)
            Me.Name = name
            Sprites = New Dictionary(Of String, Sprite)
        End Sub
        ''' <summary>
        ''' 获取所有的精灵
        ''' </summary>
        ''' <returns></returns>
        Public Function AllSprites() As Sprite()
            Return Sprites.Values.ToArray
        End Function
        ''' <summary>
        ''' 添加一个精灵到场景<br></br>
        ''' 同一场景中不允许精灵重名
        ''' </summary>
        ''' <param name="name">要添加的精灵的名称</param>
        Public Function AddSprite(name As String) As Sprite
            If Sprites.ContainsKey(name) Then Return Nothing
            Dim target = New Sprite(name, Me)
            Sprites.Add(name, target)
            Return target
        End Function
        ''' <summary>
        ''' 添加一个精灵到场景<br></br>
        ''' 同一场景中不允许精灵重名
        ''' </summary>
        ''' <param name="name">要添加的精灵的名称</param>
        ''' <param name="parentSprite">在此场景中作为精灵父精灵的精灵</param>
        Public Function AddSprite(name As String, parentSprite As Sprite) As Sprite
            If Sprites.ContainsKey(name) Then Return Nothing
            Dim target = New Sprite(name, Me)
            Sprites.Add(name, target)
            If parentSprite IsNot Nothing Then
                If Not Sprites.ContainsValue(parentSprite) Then Return Nothing
                parentSprite.Children.Add(target)
            End If
            Return target
        End Function
        ''' <summary>
        ''' 添加一个精灵到场景<br></br>
        ''' 同一场景中不允许精灵重名
        ''' </summary>
        ''' <param name="name">要添加的精灵的名称</param>
        ''' <param name="parentSprite">在此场景中作为精灵父精灵的精灵</param>
        Public Function AddSprite(name As String, parentSprite As String) As Sprite
            If Sprites.ContainsKey(name) Then Return Nothing
            Dim target = New Sprite(name, Me)
            Sprites.Add(name, target)
            If parentSprite <> "" Then
                Dim parent = SearchSprite(parentSprite)
                If parent Is Nothing Then Return Nothing
                parent.Children.Add(target)
            End If
            Return target
        End Function
        ''' <summary>
        ''' 递归查找具有指定名称的精灵
        ''' </summary>
        ''' <param name="target">要查找的名称</param>
        ''' <returns></returns>
        Public Function SearchSprite(target As String) As Sprite
            If Sprites.ContainsKey(target) Then
                Return Sprites(target)
            Else
                For Each e In Sprites.Values
                    Dim answer = SearchSprite(target, e)
                    If answer IsNot Nothing Then Return answer
                Next
            End If
            Return Nothing
        End Function
        Private Function SearchSprite(target As String, root As Sprite) As Sprite
            Dim answer = root.Children.Where(Function(e) e.Name = target).FirstOrDefault
            If answer IsNot Nothing Then Return answer
            For Each e In root.Children
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
            Dim sprite = SearchSprite(target)
            If sprite Is Nothing Then Throw New Exception.SpriteCannotFindException
            sprite.RaiseUnbindFromScene(Me)
            Sprites.Remove(target)
        End Sub
        ''' <summary>
        ''' 从场景中删除一个精灵
        ''' </summary>
        ''' <param name="target">要删除的精灵</param>
        Public Sub RemoveSprite(target As Sprite)
            If Not Sprites.ContainsKey(target.Name) Then Throw New Exception.SpriteCannotFindException
            target.RaiseUnbindFromScene(Me)
            Sprites.Remove(target.Name)
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
        Public Overridable Sub Scene_Entered(param As Object)
        End Sub

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