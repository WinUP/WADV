Namespace API
    Public Module Config
        ''' <summary>
        ''' 初始化成就模块
        ''' </summary>
        ''' <param name="style">成就显示样式文件(Skin目录下)</param>
        ''' <param name="saveFolder">成就存储文件夹(UserFile目录下)</param>
        ''' <param name="receiverFileName">成就处理脚本路径(Script目录下)</param>
        ''' <remarks></remarks>
        Public Sub Init(style As String, saveFolder As String, receiverFileName As String)
            SetStyle(style)
            ModuleConfig.SaveFileFolder = Path.Combine(PathType.UserFile, saveFolder)
            ModuleConfig.ReceiverFileName = Path.Combine(PathType.Script, receiverFileName)
            ShowList.Initialise()
            AchievementList.Load(IO.Path.Combine(saveFolder, "achievement.a.save"))
            AchievementPropertyList.Load(IO.Path.Combine(saveFolder, "achievement.p.save"))
            ReceiverList.RunReceiver()
            Message.Send("[ACHIEVE]INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 设置成就显示的样式
        ''' </summary>
        ''' <param name="filePath">样式文件路径(Skin目录下)</param>
        ''' <remarks></remarks>
        Public Sub SetStyle(filePath As String)
            ModuleConfig.WindowStyle = My.Computer.FileSystem.ReadAllText(Path.Combine(PathType.Skin, filePath), Text.Encoding.UTF8)
        End Sub
    End Module
End Namespace
