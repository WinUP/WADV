Imports System.Reflection
Imports WADV.AppCore.PluginInterface

Namespace PluginInterface

    Public Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
            ScriptAPI.RunStringSync("api_media={}")
            For Each tmpApiClass In (From tmpClass In Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Namespace = "WADV.MediaModule.API" AndAlso tmpClass.IsClass AndAlso tmpClass.Name.LastIndexOf("API", StringComparison.Ordinal) = tmpClass.Name.Length - 3 Select tmpClass)
                Dim registerName = tmpApiClass.Name.Substring(0, tmpApiClass.Name.Length - 3).ToLower()
                ScriptAPI.RunStringSync("api_media." + registerName + "={}")
                ScriptAPI.RegisterSync(tmpApiClass, "api_media." + registerName)
            Next
            MessageAPI.SendSync("MEDIA_INIT_LOADFINISH")
            Return True
        End Function

    End Class

    Public Class LoopReceiver : Implements ILoopReceiver

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            AudioCore.PlayerList.CheckEnding()
            Return Config.ModuleConfig.LoopOn
        End Function

        Public Sub Render() Implements ILoopReceiver.Render

        End Sub
    End Class

End Namespace