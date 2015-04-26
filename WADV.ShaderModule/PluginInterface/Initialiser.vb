Imports System.Windows.Media.Effects
Imports WADV.Core.PluginInterface
Imports WADV.ShaderModule.API

Namespace PluginInterface

    Friend NotInheritable Class PluginInitialise : Implements IPluginInitialise

        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            ScriptAPI.RegisterInTableSync("shader", "init", New Action(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("shader", "getShader", New Func(Of String, ShaderEffect)(AddressOf ShaderAPI.GetShader))
            MessageAPI.SendSync("[SHADER]LOAD_FINISH")
            Return True
        End Function

    End Class

End Namespace