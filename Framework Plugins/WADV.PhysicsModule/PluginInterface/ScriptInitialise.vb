Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Dynamics.Contacts
Imports Neo.IronLua

Namespace PluginInterface
    Friend NotInheritable Class ScriptInitialise : Implements LuaSupport.IScriptInitialise
        Public Sub Register(vm As Lua, env As LuaGlobal) Implements LuaSupport.IScriptInitialise.Register
            Dim core As LuaTable = env("core")
            core("physics") = New LuaTable
            Dim table As LuaTable = core("physics")
            table("newWorld") = New Func(Of PhysicsWorld)(AddressOf API.NewWorld)
            table("setWorld") = New Action(Of PhysicsWorld)(AddressOf API.SetWorld)
            table("startSimulation") = New Action(AddressOf API.StartSimulation)
            table("stopSimulation") = New Action(AddressOf API.StopSimulation)
            table("handleWorldCollision") = New Action(Of Func(Of PhysicsSprite, PhysicsSprite, Contact, LuaResult))(AddressOf API.ApiForScript.HandleWorldCollision)
            table("removeWorldCollision") = New Action(AddressOf API.RemoveWorldCollision)
            table("newSprite") = New Action(Of String)(AddressOf API.NewSprite)
            table("addSprite") = New Action(Of PhysicsSprite)(AddressOf API.AddSprite)
            table("getSprite") = New Func(Of String, PhysicsSprite)(AddressOf API.GetSprite)
            table("removeSprite") = New Action(Of String)(AddressOf API.RemoveSprite)
            table("loadSprite") = New Func(Of String, PhysicsSprite)(AddressOf API.LoadSprite)
            table("handleSpriteCollision") = New Action(Of PhysicsSprite, Func(Of Fixture, Fixture, Contact, LuaResult))(AddressOf API.ApiForScript.HandleSpriteCollision)
            table("removeSpriteCollision") = New Action(Of PhysicsSprite)(AddressOf API.ApiForScript.RemoveSpriteCollision)
            table("newJoint") = New Func(Of String, PhysicsJoint)(AddressOf API.NewJoint)
            table("addJoint") = New Func(Of PhysicsJoint, PhysicsJoint)(AddressOf API.AddJoint)
            table("removeJoint") = New Action(Of String)(AddressOf API.RemoveJoint)
            Message.Send("[PHYSICS]INIT_LOAD_FINISH")
        End Sub
    End Class
End Namespace
