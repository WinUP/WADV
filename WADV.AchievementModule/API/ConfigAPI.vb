Namespace API

    Public NotInheritable Class ConfigAPI

        Public Shared Sub Init(style As String, saveFolder As String, fileName As String)
            SetStyle(style)
            Config.SaveFileFolder = saveFolder
            Config.ReceiverFileName = fileName
            ShowList.Initialise()
            AchievementList.Load(IO.Path.Combine(saveFolder, "achievement.a.save"))
            PropertyList.Load(IO.Path.Combine(saveFolder, "achievement.p.save"))
            ReceiverList.RunReceiver()
            MessageAPI.SendSync("[ACHIEVE]INIT_FINISH")
        End Sub

        Public Shared Sub SetStyle(filePath As String)
            Config.WindowStyle = My.Computer.FileSystem.ReadAllText(PathAPI.GetPath(PathType.Skin, filePath), Text.Encoding.UTF8)
        End Sub

    End Class

End Namespace
