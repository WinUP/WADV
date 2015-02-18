Imports System.Reflection
Imports WADV.AppCore.PluginInterface

Namespace PluginInterface

    Public Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
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

    Public Class GameClose : Implements IGameClose

        Public Sub DestructuringGame(e As ComponentModel.CancelEventArgs) Implements IGameClose.DestructuringGame
            AchievementList.Save(IO.Path.Combine(Config.SaveFileFolder, "achievement.a.save"))
            PropertyList.Save(IO.Path.Combine(Config.SaveFileFolder, "achievement.p.save"))
        End Sub

    End Class

End Namespace
