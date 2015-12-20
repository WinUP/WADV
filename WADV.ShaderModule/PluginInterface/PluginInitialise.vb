Imports System.Windows.Media.Effects
Imports WADV.Core.PluginInterface

Namespace PluginInterface
    Public Class PluginInitialise : Implements IPluginInitialise
        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            Dim target As New Core.Script.Field With {.Name = "shader"}
            target.Content.Add("ready", New Action(AddressOf Extension.Ready))
            target.Content.Add("get", New Func(Of String, ShaderEffect)(AddressOf Extension.Get))
            Script.RegisterField(target)
            Return True
        End Function
    End Class
End Namespace