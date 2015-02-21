Imports System.Reflection
Imports WADV.AppCore.PluginInterface

Namespace PluginInterface

    Public Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
            PageList.List.LoadPage()
            ScriptAPI.RegisterInTableSync("api_page", "getPage", New Func(Of String, Page)(AddressOf API.ObjectAPI.GetPage), True)
            Return True
        End Function

    End Class

End Namespace
