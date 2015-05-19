Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Public Class Initialiser : Implements IPluginInitialise

        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            ScriptAPI.RegisterInTableSync("physics", "setWorld", New Action(Of PhysicsWorld)(AddressOf API.PhysicsAPI.SetWorld), True)
            ScriptAPI.RegisterInTableSync("physics", "startSimulation", New Action(AddressOf API.PhysicsAPI.StartSimulation))
            ScriptAPI.RegisterInTableSync("physics", "stopSimulation", New Action(AddressOf API.PhysicsAPI.StopSimulation))

            Return True
        End Function
    End Class
End Namespace
