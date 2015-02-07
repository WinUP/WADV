Imports System.Reflection
Imports WADV.AppCore.Plugin

Namespace PluginInterface

    Public Class Initlizer : Implements IInitialise

        Public Function Initialising() As Boolean Implements WADV.AppCore.Plugin.IInitialise.Initialising
            ScriptAPI.RunStringSync("api_media={}")
            For Each tmpApiClass In (From tmpClass In Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Namespace = "WADV.MediaModule.API" AndAlso tmpClass.IsClass AndAlso tmpClass.Name.LastIndexOf("API", StringComparison.Ordinal) = tmpClass.Name.Length - 3 Select tmpClass)
                Dim registerName = tmpApiClass.Name.Substring(0, tmpApiClass.Name.Length - 3).ToLower()
                ScriptAPI.RunStringSync("api_media." + registerName + "={}")
                ScriptAPI.RegisterSync(tmpApiClass, "api_media." + registerName)
            Next
            MessageAPI.SendSync("MEDIA_INIT_FINISH")
            Return True
        End Function

    End Class

End Namespace