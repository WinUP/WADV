Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Dynamics.Contacts
Imports Neo.IronLua

Friend Class Config

    ''' <summary>
    ''' 要模拟的物理世界
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared TargetPysicsWorld As PhysicsWorld = Nothing

    ''' <summary>
    ''' 是否停止物理模拟
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared StopSimulation As Boolean = False

    ''' <summary>
    ''' 所有物理世界统一的碰撞处理事件
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared WorldCollisionHandle As Action(Of PhysicsSprite, PhysicsSprite, Contact)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared ReadOnly SpriteCollisionHandle As New Dictionary(Of PhysicsSprite, Func(Of Fixture, Fixture, Contact, LuaResult))
End Class
