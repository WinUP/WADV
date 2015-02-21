Imports System.Reflection
Imports System.Windows.Controls
Imports WADV.AppCore.PluginInterface
Imports WADV.TEModule.API

Namespace PluginInterface

    Public Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
            ScriptAPI.RegisterInTableSync("api_te", "init", New Action(Of String)(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("api_te", "show", New Func(Of String, Double, Double, Double, Double, Boolean, Integer)(AddressOf ImageAPI.Show))
            ScriptAPI.RegisterInTableSync("api_te", "register", New Func(Of Panel, Integer)(AddressOf ImageAPI.Register))
            ScriptAPI.RegisterInTableSync("api_te", "effect", New Action(Of Integer, String, Boolean, Object())(AddressOf ImageAPI.Effect))
            ScriptAPI.RegisterInTableSync("api_te", "hide", New Func(Of Integer, Boolean)(AddressOf ImageAPI.Hide))
            MessageAPI.SendSync("TE_INIT_LOADFINISH")
            Return True
        End Function

    End Class

End Namespace