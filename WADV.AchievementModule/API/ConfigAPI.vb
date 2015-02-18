Namespace API

    Public Class ConfigAPI

        Public Sub Init(Optional saveFolder As String = "", Optional receiverFolder As String = "")
            Config.SaveFileFolder = saveFolder
            Config.ReceiverFolder = receiverFolder
            AchievementList.Load(IO.Path.Combine(saveFolder, "achievement.a.save"))
            PropertyList.Load(IO.Path.Combine(saveFolder, "achievement.p.save"))
            ReceiverList.LoadReceiver()
            ReceiverList.RunReceiver()
            MessageAPI.SendSync("ACHIEVE_INIT_ALLFINISH")
        End Sub

    End Class

End Namespace
