Namespace API

    Public Class ConfigAPI

        Public Shared Sub Init(Optional saveFolder As String = "", Optional fileName As String = "achievement.lua")
            Config.SaveFileFolder = saveFolder
            Config.FileName = fileName
            AchievementList.Load(IO.Path.Combine(saveFolder, "achievement.a.save"))
            PropertyList.Load(IO.Path.Combine(saveFolder, "achievement.p.save"))
            ReceiverList.RunReceiver()
            MessageAPI.SendSync("[ACHIEVE]INIT_ALLFINISH")
        End Sub

    End Class

End Namespace
