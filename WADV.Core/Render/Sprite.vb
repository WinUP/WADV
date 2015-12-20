Imports System.Collections.ObjectModel
Imports WADV.Core.Component

Namespace Render
    ''' <summary>
    ''' 表示一个精灵
    ''' </summary>
    Public NotInheritable Class Sprite
        Private _parent As Sprite = Nothing

        Private Sub New()
        End Sub

        ''' <summary>
        ''' 获取精灵的名称
        ''' </summary>
        Public ReadOnly Property Name As String
        ''' <summary>
        ''' 获取精灵的组件列表
        ''' </summary>
        Public ReadOnly Property Components As ComponentList
        ''' <summary>
        ''' 获取精灵的所有子精灵
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Children As SpriteList
        ''' <summary>
        ''' 获取精灵所在的场景
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Scene As Scene
        ''' <summary>
        ''' 获取精灵的父精灵
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Parent As Sprite
            Get
                Return _parent
            End Get
        End Property

        ''' <summary>
        ''' 获得一个新的精灵
        ''' </summary>
        ''' <param name="name">精灵的名称</param>
        ''' <remarks></remarks>
        Friend Sub New(name As String, scene As Scene)
            Me.Name = name
            Me.Scene = scene
            Components = New ComponentList(Me)
            Children = New SpriteList(Me)
        End Sub

        '!触发顺序为：BeforeChangeParent->更改父精灵->AfterChangeParent
        '!BeforeChangeParent仅触发自身，AfterChangeParent将会链式传递到所有子精灵
        ''' <summary>
        ''' 设置精灵的父精灵
        ''' </summary>
        ''' <param name="target">要设置到的父精灵</param>
        Friend Sub SetParent(target As Sprite, Optional chain As List(Of Sprite) = Nothing)
            Dim list = chain
            If chain Is Nothing Then
                If target.Scene IsNot Scene Then Throw New Exception.SpriteCrossSceneAddingIlleagleException()
                RaiseEvent BeforeChangeParent(Me, target)
                _parent = target
                list = New List(Of Sprite)
                list.Add(target)
            End If
            Dim transmiteToChildren = True
            RaiseEvent AfterChangeParent(Me, list.AsReadOnly, transmiteToChildren) '这个列表不包含自身
            If Not transmiteToChildren Then Exit Sub
            list.Add(Me)
            Children.ForEach(Sub(e) e.SetParent(Me, list))
            list.Remove(list.Last)
        End Sub

        ''' <summary>
        ''' 触发场景解绑事件
        ''' </summary>
        ''' <param name="target">要解绑的场景</param>
        Friend Sub RaiseUnbindFromScene(target As Scene)
            RaiseEvent UnbindFromScene(Me, target)
        End Sub

        Public Event UnbindFromScene(sprite As Sprite, scene As Scene)
        Public Event BeforeChangeParent(sprite As Sprite, parentWanted As Sprite)
        Public Event AfterChangeParent(sprite As Sprite, parentList As ReadOnlyCollection(Of Sprite), ByRef transmiteToChildren As Boolean)
    End Class
End Namespace