Imports System.Windows.Controls
Imports System.Windows.Media.Animation

Namespace Extension
    Public Class Plugin
        ''' <summary>
        ''' 初始化成就模块
        ''' </summary>
        ''' <param name="saveFolder">成就存储文件夹(UserFile目录下)</param>
        ''' <param name="receiverFileName">成就处理脚本路径(Script目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub Ready(saveFolder As String, receiverFileName As String)
            Config.Window = Renderer.Extension.WpfGameWindow.Window
            Config.SaveFileFolder = Path.Combine(PathType.UserFile, saveFolder)
            Config.ReceiverFileName = Path.Combine(PathType.Script, receiverFileName)
            ShowList.Run()
            AchievementList.Load(IO.Path.Combine(saveFolder, "achievement.a.save"))
            AchievementPropertyList.Load(IO.Path.Combine(saveFolder, "achievement.p.save"))
            ReceiverList.RunReceiver()
            Message.Send("[ACHIEVE]INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 设置成就显示样式
        ''' </summary>
        ''' <param name="key">样式在资源词典中的Key（这个样式必须能被解析为一个Border元素）</param>
        ''' <remarks></remarks>
        Public Shared Function Style(Optional key As String = "") As Border
            If key = "" Then Return Config.Element
            Config.Element = Config.Window.FindResource(key)
            Return Config.Element
        End Function

        ''' <summary>
        ''' 设置成就显示动画
        ''' </summary>
        ''' <param name="key">样式在资源词典中的Key（这个样式必须能被解析为一个Storyboard元素）</param>
        ''' <returns></returns>
        Public Shared Function Animation(Optional key As String = "") As Storyboard
            If key = "" Then Return Config.Storyboard
            Config.Storyboard = Config.Window.FindResource(key)
            Return Config.Storyboard
        End Function
    End Class
End Namespace
