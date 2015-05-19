Imports FarseerPhysics.Dynamics.Contacts

''' <summary>
''' 碰撞对象辅助类
''' </summary>
''' <remarks></remarks>
Public Class CollisionInstance
    Public Property Sprite1() As PhysicsSprite
    Public Property Sprite2() As PhysicsSprite
    Public Property Contact() As Contact

    Public Sub New(sprite1__1 As PhysicsSprite, sprite2__2 As PhysicsSprite, contact__3 As Contact)
        Sprite1 = sprite1__1
        Sprite2 = sprite2__2
        Contact = contact__3
    End Sub
End Class

''' <summary>
''' 碰撞存储池
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class CollisionStore
    Private Sub New()
    End Sub
    Public Shared ReadOnly Collisions As New List(Of CollisionInstance)()

    ''' <summary>
    ''' 添加一个碰撞事件
    ''' </summary>
    ''' <param name="sprite1">碰撞对象1</param>
    ''' <param name="sprite2">碰撞对象2</param>
    ''' <param name="contact">对象连接</param>
    ''' <remarks></remarks>
    Public Shared Sub AddCollision(sprite1 As PhysicsSprite, sprite2 As PhysicsSprite, contact As Contact)
        Collisions.Add(New CollisionInstance(sprite1, sprite2, contact))
    End Sub

End Class