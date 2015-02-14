Imports System.Reflection

Namespace PluginInterface

    Public Class Initialiser : Implements AppCore.Plugin.IInitialise

        Public Function Initialising() As Boolean Implements AppCore.Plugin.IInitialise.Initialising
            ScriptAPI.RunStringSync("api_achieve={}")
            For Each tmpApiClass In (From tmpClass In Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Namespace = "WADV.AchievementModule.API" AndAlso tmpClass.IsClass AndAlso tmpClass.Name.LastIndexOf("API", StringComparison.Ordinal) = tmpClass.Name.Length - 3 Select tmpClass)
                Dim registerName = tmpApiClass.Name.Substring(0, tmpApiClass.Name.Length - 3).ToLower()
                ScriptAPI.RunStringSync("api_achieve." + registerName + "={}")
                ScriptAPI.RegisterSync(tmpApiClass, "api_achieve." + registerName)
            Next
            MessageAPI.SendSync("ACHIEVE_INIT_LOADFINISH")
            Return True
        End Function

    End Class

    Public Class GameClose : Implements AppCore.Plugin.IGameClose

        Public Sub DestructuringGame(e As ComponentModel.CancelEventArgs) Implements AppCore.Plugin.IGameClose.DestructuringGame
            If Config.SaveFileName <> "" Then AchievementList.Save(Config.SaveFileName)
        End Sub

    End Class

End Namespace
