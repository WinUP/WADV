Imports System.Reflection
Imports WADV.AppCore.PluginInterface

Namespace PluginInterface

    Public Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
            ScriptAPI.RunStringSync("api_te={}")
            For Each tmpApiClass In (From tmpClass In Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Namespace = "WADV.TEModule.API" AndAlso tmpClass.IsClass AndAlso tmpClass.Name.LastIndexOf("API", StringComparison.Ordinal) = tmpClass.Name.Length - 3 Select tmpClass)
                Dim registerName = tmpApiClass.Name.Substring(0, tmpApiClass.Name.Length - 3).ToLower()
                ScriptAPI.RunStringSync("api_te." + registerName + "={}")
                ScriptAPI.RegisterSync(tmpApiClass, "api_te." + registerName)
            Next
            MessageAPI.SendSync("TE_INIT_LOADFINISH")
            Return True
        End Function

    End Class

End Namespace