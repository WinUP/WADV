Imports FarseerPhysics.Dynamics.Contacts

''' <summary>
''' 碰撞对象辅助类
''' </summary>
''' <remarks></remarks>
Friend Class CollisionInstance
    Friend Property SpriteA() As PhysicsSprite
    Friend Property SpriteB() As PhysicsSprite
    Friend Property Contact() As Contact

    Public Sub New(sprite1 As PhysicsSprite, sprite2 As PhysicsSprite, contact As Contact)
        SpriteA = sprite1
        SpriteB = sprite2
        Me.Contact = contact
    End Sub
End Class

''' <summary>
''' 碰撞存储池
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class CollisionStore
    Friend Shared ReadOnly Collisions As New List(Of CollisionInstance)()

    ''' <summary>
    ''' 添加一个碰撞事件
    ''' </summary>
    ''' <param name="sprite1">碰撞对象1</param>
    ''' <param name="sprite2">碰撞对象2</param>
    ''' <param name="contact">对象连接</param>
    ''' <remarks></remarks>
    Friend Shared Sub AddCollision(sprite1 As PhysicsSprite, sprite2 As PhysicsSprite, contact As Contact)
        Collisions.Add(New CollisionInstance(sprite1, sprite2, contact))
    End Sub
End Class