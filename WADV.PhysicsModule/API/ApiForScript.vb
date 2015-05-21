Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Dynamics.Contacts
Imports Neo.IronLua

Namespace API
    Friend Class ApiForScript
        Public Shared Sub HandleWorldCollision(code As Func(Of PhysicsSprite, PhysicsSprite, Contact, LuaResult))
            Config.WorldCollisionHandle = Sub(arg1 As PhysicsSprite, arg2 As PhysicsSprite, arg3 As Contact) code.Invoke(arg1, arg2, arg3)
        End Sub

        Public Shared Sub HandleSpriteCollision(target As PhysicsSprite, code As Func(Of Fixture, Fixture, Contact, LuaResult))
            If Config.SpriteCollisionHandle.ContainsKey(target) Then
                Config.SpriteCollisionHandle(target) = code
            Else
                Config.SpriteCollisionHandle.Add(target, code)
            End If
        End Sub

        Public Shared Sub RemoveSpriteCollision(target As PhysicsSprite)
            If Config.SpriteCollisionHandle.ContainsKey(target) Then
                Config.SpriteCollisionHandle.Remove(target)
            End If
        End Sub
    End Class
End Namespace
