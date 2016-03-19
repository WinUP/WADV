Imports System.Collections.ObjectModel

Namespace RAL.Tool
    ''' <summary>
    ''' RAL辅助工具 - 精灵渲染节点
    ''' </summary>
    Public MustInherit Class Sprite : Inherits RenderNode(Of Sprite)

        ''' <summary>
        ''' 获得一个新的精灵
        ''' </summary>
        ''' <param name="name">精灵的名称</param>
        ''' <remarks></remarks>
        Public Sub New(name As String)
            MyBase.New(name)
            Components = New ComponentList(Me)
        End Sub

        ''' <summary>
        ''' 获取精灵的组件列表
        ''' </summary>
        Public ReadOnly Property Components As ComponentList

        ''' <summary>
        ''' 获取精灵所在的场景
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Scene As Scene

        ''' <summary>
        ''' 获取精灵的父精灵
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Parent As Sprite = Nothing

        ''' <summary>
        ''' 获取设置精灵的显示区域
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride Property DisplayArea As Rect2

        ''' <summary>
        ''' 获取或设置精灵的显隐性
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride Property Visibility As Boolean

        '!触发顺序为：BeforeChangeParent->更改父精灵->AfterChangeParent
        '!BeforeChangeParent仅触发自身，AfterChangeParent将会链式传递到所有子精灵
        ''' <summary>
        ''' 设置精灵的父精灵
        ''' </summary>
        ''' <param name="target">要设置到的父精灵</param>
        Friend Sub SetParent(target As Sprite, Optional chain As List(Of Sprite) = Nothing)
            Dim list = chain
            If chain Is Nothing Then
                RaiseEvent BeforeChangeParent(Me, target)
                _Parent = target
                list = New List(Of Sprite)
                list.Add(target)
            End If
            Dim transmiteToChildren = True
            RaiseEvent AfterChangeParent(Me, list.AsReadOnly, transmiteToChildren) '这个列表不包含自身
            If Not transmiteToChildren Then Exit Sub
            list.Add(Me)
            For Each e In ChildList.Values
                e.SetParent(Me, list)
            Next
            list.Remove(list.Last)
        End Sub

        Friend Sub RemoveChildren(target As Sprite)
            ChildList.Remove(target.Name)
            target.SetParent(Nothing)
        End Sub

        Friend Sub AddChildren(target As Sprite)
            ChildList.Add(target.Name, target)
            target.SetParent(Me)
        End Sub

        ''' <summary>
        ''' 触发场景绑定事件
        ''' </summary>
        ''' <param name="target">要绑定的场景</param>
        Friend Sub RaiseBindToScene(target As Scene)
            _Scene = target
            Components.RaiseBindToScene(Me, target)
            RaiseEvent BindToScene(Me, target)
        End Sub

        ''' <summary>
        ''' 触发场景解绑事件
        ''' </summary>
        ''' <param name="target">要解绑的场景</param>
        Friend Sub RaiseUnbindFromScene(target As Scene)
            Components.RaiseUnbindFromScene(Me, target)
            RaiseEvent UnbindFromScene(Me, target)
        End Sub

        ''' <summary>
        ''' 表示精灵被绑定到一个场景后触发的事件
        ''' </summary>
        ''' <param name="sprite">被绑定的精灵</param>
        ''' <param name="scene">要绑定的场景</param>
        Public Event BindToScene(sprite As Sprite, scene As Scene)

        ''' <summary>
        ''' 表示精灵被从一个场景解绑后触发的事件
        ''' </summary>
        ''' <param name="sprite">被解绑的精灵</param>
        ''' <param name="scene">要解绑的场景</param>
        Public Event UnbindFromScene(sprite As Sprite, scene As Scene)
        ''' <summary>
        ''' 表示精灵的父精灵被更换前触发的事件
        ''' </summary>
        ''' <param name="sprite">目标精灵</param>
        ''' <param name="parentWanted">即将成为父精灵的精灵</param>
        Public Event BeforeChangeParent(sprite As Sprite, parentWanted As Sprite)

        ''' <summary>
        ''' 表示精灵的父精灵被更换后触发的事件
        ''' </summary>
        ''' <param name="sprite">目标精灵</param>
        ''' <param name="parentList">精灵的所有父精灵从最顶层开始构成的列表</param>
        ''' <param name="transmiteToChildren">该事件是否继续传递给当前精灵的所有子精灵</param>
        Public Event AfterChangeParent(sprite As Sprite, parentList As ReadOnlyCollection(Of Sprite), ByRef transmiteToChildren As Boolean)
    End Class
End Namespace