Namespace PluginInterface
    Friend NotInheritable Class PluginInitialise : Implements Core.PluginInterface.IPluginInitialise
        Public Function Initialising() As Boolean Implements Core.PluginInterface.IPluginInitialise.Initialising
            Dim target As New Core.Script.Field With {.Name = "audio"}
            target.Content.Add("play", New Func(Of String, Boolean, Integer, Integer)(AddressOf Extension.Play))
            target.Content.Add("get", New Func(Of String, AudioPlayer)(AddressOf Extension.Get))
            target.Content.Add("stop", New Action(Of Integer)(AddressOf Extension.Stop))
            target.Content.Add("pause", New Action(Of Integer)(AddressOf Extension.Pause))
            target.Content.Add("resume", New Action(Of Integer)(AddressOf Extension.Resume))
            target.Content.Add("volume", New Func(Of Integer, Double, Double)(AddressOf Extension.Volume))
            Script.RegisterField(target)
            Return True
        End Function
    End Class
End Namespace