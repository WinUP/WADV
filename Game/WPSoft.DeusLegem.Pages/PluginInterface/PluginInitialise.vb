Imports WADV

Namespace PluginInterface
    Friend NotInheritable Class PluginInitialise : Implements Core.PluginInterface.IPluginInitialise
        Public Function Initialising() As Boolean Implements Core.PluginInterface.IPluginInitialise.Initialising
            PageList.LoadPage()
            Dim target As New Core.Script.Field With {.Name = "deuslegem"}
            target.Content.Add("page", New Core.Script.Field() With {.Name = "page"})
            target = target.Content.Item("page")
            target.Content.Add("get", New Func(Of String, Page)(AddressOf Extension.[Get]))
            Script.RegisterField(target)
            Return True
        End Function
    End Class
End Namespace