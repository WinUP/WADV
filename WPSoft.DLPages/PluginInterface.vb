Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Public Class Initialiser : Implements IPluginInitialise

        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            PageList.List.LoadPage()
            ScriptAPI.RegisterInTableSync("api_page", "getPage", New Func(Of String, Page)(AddressOf API.ObjectAPI.GetPage), True)
            Return True
        End Function

    End Class

End Namespace
