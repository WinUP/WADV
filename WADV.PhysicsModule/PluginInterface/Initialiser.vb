Imports FarseerPhysics.Dynamics
Imports Neo.IronLua
Imports FarseerPhysics.Dynamics.Contacts
Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Public Class Initialiser : Implements IPluginInitialise

        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            ScriptAPI.RegisterInTableSync("physics", "newWorld", New Func(Of PhysicsWorld)(AddressOf API.NewWorld), True)
            ScriptAPI.RegisterInTableSync("physics", "setWorld", New Action(Of PhysicsWorld)(AddressOf API.SetWorld))
            ScriptAPI.RegisterInTableSync("physics", "startSimulation", New Action(AddressOf API.StartSimulation))
            ScriptAPI.RegisterInTableSync("physics", "stopSimulation", New Action(AddressOf API.StopSimulation))
            ScriptAPI.RegisterInTableSync("physics", "handleWorldCollision", New Action(Of Func(Of PhysicsSprite, PhysicsSprite, Contact, LuaResult))(AddressOf API.ApiForScript.HandleWorldCollision))
            ScriptAPI.RegisterInTableSync("physics", "removeWorldCollision", New Action(AddressOf API.RemoveWorldCollision))
            ScriptAPI.RegisterInTableSync("physics", "newSprite", New Action(Of String)(AddressOf API.NewSprite))
            ScriptAPI.RegisterInTableSync("physics", "addSprite", New Action(Of PhysicsSprite)(AddressOf API.AddSprite))
            ScriptAPI.RegisterInTableSync("physics", "getSprite", New Func(Of String, PhysicsSprite)(AddressOf API.GetSprite))
            ScriptAPI.RegisterInTableSync("physics", "removeSprite", New Action(Of String)(AddressOf API.RemoveSprite))
            ScriptAPI.RegisterInTableSync("physics", "loadSprite", New Func(Of String, PhysicsSprite)(AddressOf API.LoadSprite))
            ScriptAPI.RegisterInTableSync("physics", "handleSpriteCollision", New Action(Of PhysicsSprite, Func(Of Fixture, Fixture, Contact, LuaResult))(AddressOf API.ApiForScript.HandleSpriteCollision))
            ScriptAPI.RegisterInTableSync("physics", "removeSpriteCollision", New Action(Of PhysicsSprite)(AddressOf API.ApiForScript.RemoveSpriteCollision))
            ScriptAPI.RegisterInTableSync("physics", "newJoint", New Func(Of String, PhysicsJoint)(AddressOf API.NewJoint))
            ScriptAPI.RegisterInTableSync("physics", "addJoint", New Func(Of PhysicsJoint, PhysicsJoint)(AddressOf API.AddJoint))
            ScriptAPI.RegisterInTableSync("physics", "removeJoint", New Action(Of String)(AddressOf API.RemoveJoint))
            Return True
        End Function
    End Class
End Namespace
