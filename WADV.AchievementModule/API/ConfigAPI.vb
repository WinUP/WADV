Namespace API

    Public Class ConfigAPI

        Public Sub Init(saveFile As String, Optional receiverFolder As String = "")
            Config.SaveFileFolder = saveFile
            Config.ReceiverFolder = receiverFolder
            AchievementList.Load(saveFile)
            MessageAPI.SendSync("ACHIEVE_INIT_ALLFINISH")
        End Sub

    End Class

End Namespace
