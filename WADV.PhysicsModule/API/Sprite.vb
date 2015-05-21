Imports System.Windows.Controls
Imports System.Windows.Markup
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Dynamics.Contacts

Namespace API
    Public Module Sprite

        ''' <summary>
        ''' 建立一个空的物体
        ''' </summary>
        ''' <param name="name">物体的理想名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NewSprite(name As String) As PhysicsSprite
            Return New PhysicsSprite With {.Name = name, .InternalName = name}
        End Function

        ''' <summary>
        ''' 添加一个物体到正在模拟的物理世界
        ''' </summary>
        ''' <param name="target">物体的理想名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AddSprite(target As PhysicsSprite) As PhysicsSprite
            If Config.TargetPysicsWorld Is Nothing Then Return Nothing
            Config.TargetPysicsWorld.AddPhysicsSprite(target, True)
            Return target
        End Function

        ''' <summary>
        ''' 从正在模拟的物理世界中检索物体
        ''' </summary>
        ''' <param name="name">要检索的物体的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSprite(name As String) As PhysicsSprite
            If Config.TargetPysicsWorld Is Nothing Then Return Nothing
            Return Config.TargetPysicsWorld.FindName(name)
        End Function

        ''' <summary>
        ''' 从正在模拟的物理世界中删除物体
        ''' </summary>
        ''' <param name="name">要删除的物体的名称</param>
        ''' <remarks></remarks>
        Public Sub RemoveSprite(name As String)
            If Config.TargetPysicsWorld Is Nothing Then Exit Sub
            Config.TargetPysicsWorld.DeletePhysicsObject(name)
            Dim target As PhysicsSprite = Config.TargetPysicsWorld.FindName(name)
            If target Is Nothing Then Exit Sub
            DirectCast(target.Parent, Panel).Children.Remove(target)
        End Sub

        ''' <summary>
        ''' 从布局文件加载元素并作为物体加载
        ''' </summary>
        ''' <param name="filePath">要加载的布局文件的路径(Resource目录下)</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function LoadSprite(filePath As String) As PhysicsSprite
            Dim content = XamlReader.Parse(My.Computer.FileSystem.ReadAllText(PathAPI.GetPath(PathType.Resource, filePath), Text.Encoding.Default))
            Dim target As New PhysicsSprite
            target.Children.Add(content)
            Return target
        End Function

        ''' <summary>
        ''' 绑定委托到精灵的碰撞事件
        ''' </summary>
        ''' <param name="target">要绑定的精灵</param>
        ''' <param name="code">要绑定的委托</param>
        ''' <remarks></remarks>
        Public Sub HandleSpriteCollision(target As PhysicsSprite, code As Action(Of Fixture, Fixture, Contact))
            AddHandler target.FixtureCollision, code
        End Sub

        ''' <summary>
        ''' 从精灵的碰撞事件解绑委托
        ''' </summary>
        ''' <param name="target">要解绑的精灵</param>
        ''' <param name="code">要解绑的委托</param>
        ''' <remarks></remarks>
        Public Sub RemoveSpriteCollision(target As PhysicsSprite, code As Action(Of Fixture, Fixture, Contact))
            RemoveHandler target.FixtureCollision, code
        End Sub
    End Module
End Namespace
