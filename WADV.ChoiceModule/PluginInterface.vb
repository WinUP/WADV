Imports System.Reflection
Imports System.Windows

Namespace PluginInterface

    Public Class Initialiser : Implements AppCore.Plugin.IInitialise

        Public Function Initialising() As Boolean Implements AppCore.Plugin.IInitialise.Initialising
            Effect.Initialiser.LoadEffect()
            ScriptAPI.RunStringSync("api_choice={}")
            For Each tmpApiClass In (From tmpClass In Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Namespace = "WADV.ChoiceModule.API" AndAlso tmpClass.IsClass AndAlso tmpClass.Name.LastIndexOf("API", StringComparison.Ordinal) = tmpClass.Name.Length - 3 Select tmpClass)
                Dim registerName = tmpApiClass.Name.Substring(0, tmpApiClass.Name.Length - 3).ToLower()
                ScriptAPI.RunStringSync("api_choice." + registerName + "={}")
                ScriptAPI.RegisterSync(tmpApiClass, "api_choice." + registerName)
            Next
            MessageAPI.SendSync("CHOICE_INIT_FINISH")
            Return True
        End Function

    End Class

    Public Class Looping : Implements AppCore.Plugin.ILoopReceiver
        Private ReadOnly _style As Effect.IProgressEffect

        Public Sub New(style As Effect.IProgressEffect)
            Me._style = style
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements AppCore.Plugin.ILoopReceiver.Logic
            Return _style.Logic()
        End Function

        Public Sub Render(window As Window) Implements AppCore.Plugin.ILoopReceiver.Render
            _style.Render()
        End Sub

    End Class

End Namespace