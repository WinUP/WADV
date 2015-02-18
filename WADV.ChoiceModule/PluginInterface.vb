Imports System.Reflection
Imports WADV.AppCore.PluginInterface

Namespace PluginInterface

    Public Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
            ScriptAPI.RunStringSync("api_choice={}")
            For Each tmpApiClass In (From tmpClass In Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Namespace = "WADV.ChoiceModule.API" AndAlso tmpClass.IsClass AndAlso tmpClass.Name.LastIndexOf("API", StringComparison.Ordinal) = tmpClass.Name.Length - 3 Select tmpClass)
                Dim registerName = tmpApiClass.Name.Substring(0, tmpApiClass.Name.Length - 3).ToLower()
                ScriptAPI.RunStringSync("api_choice." + registerName + "={}")
                ScriptAPI.RegisterSync(tmpApiClass, "api_choice." + registerName)
            Next
            MessageAPI.SendSync("CHOICE_INIT_LOADFINISH")
            Return True
        End Function

    End Class

    Friend NotInheritable Class LoopReceiver : Implements ILoopReceiver
        Private ReadOnly _style As Effect.IProgressEffect

        Public Sub New(style As Effect.IProgressEffect)
            Me._style = style
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            Return _style.Logic()
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
            _style.Render()
        End Sub

    End Class

End Namespace