Imports System.Windows.Media
Imports System.Xml
Imports WADV.MediaModule.Player
Imports System.Windows.Controls
Imports WADV.AppCore.API

Namespace Config

    ''' <summary>
    ''' 声音列表
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SoundList
        Private Shared SoundList As New List(Of AdvancedPlayer)

        ''' <summary>
        ''' 获取声音列表
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function GetList() As List(Of AdvancedPlayer)
            Return SoundList
        End Function

        ''' <summary>
        ''' 添加声音
        ''' </summary>
        ''' <param name="sound">声音</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub AddSound(sound As AdvancedPlayer)
            SoundList.Add(sound)
        End Sub

        ''' <summary>
        ''' 查找声音
        ''' </summary>
        ''' <param name="name">声音标识名</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function FindSound(name As String) As AdvancedPlayer
            Dim sound = From tmpSound In SoundList Where tmpSound.TagName = name Select tmpSound
            If sound.Count < 1 Then Return Nothing
            Return sound.FirstOrDefault
        End Function

        ''' <summary>
        ''' 删除声音
        ''' </summary>
        ''' <param name="name">声音标识名</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub DeleteSound(name As String)
            Dim soundContent = FindSound(name)
            If soundContent Is Nothing Then Exit Sub
            DeleteSound(soundContent)
        End Sub

        ''' <summary>
        ''' 删除声音
        ''' </summary>
        ''' <param name="content">声音</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub DeleteSound(content As AdvancedPlayer)
            SoundList.Remove(content)
            content.Dispatcher.BeginInvoke(Sub() content.Close())
            AppCore.API.WindowAPI.GetMainGrid.Dispatcher.BeginInvoke(Sub() AppCore.API.WindowAPI.GetMainGrid.Children.Remove(content))
        End Sub

        ''' <summary>
        ''' 改变一类声音的音量
        ''' </summary>
        ''' <param name="type">声音类型</param>
        ''' <param name="volume">音量</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub ChangeVolume(type As AdvancedPlayer.SoundType, volume As Double)
            For Each tmpSound In SoundList
                If tmpSound.Type = type Then tmpSound.Dispatcher.BeginInvoke(Sub() tmpSound.Volume = volume)
            Next
        End Sub

    End Class

    ''' <summary>
    ''' 声音设置类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SoundConfig

        Private Shared BackgroundVolume As Double
        Private Shared ReadingVolume As Double
        Private Shared EffectVolume As Double

        ''' <summary>
        ''' 获取或设置背景音量
        ''' </summary>
        ''' <value>音量</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Background As Double
            Get
                Return BackgroundVolume
            End Get
            Set(value As Double)
                BackgroundVolume = value
                SoundList.ChangeVolume(AdvancedPlayer.SoundType.Background, value)
                WriteConfig()
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置对话音量
        ''' </summary>
        ''' <value>音量</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Reading As Double
            Get
                Return ReadingVolume
            End Get
            Set(value As Double)
                ReadingVolume = value
                SoundList.ChangeVolume(AdvancedPlayer.SoundType.Reading, value)
                WriteConfig()
            End Set
        End Property

        ''' <summary>
        ''' 获取或设置效果音量
        ''' </summary>
        ''' <value>音量</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Property Effect As Double
            Get
                Return EffectVolume
            End Get
            Set(value As Double)
                EffectVolume = value
                SoundList.ChangeVolume(AdvancedPlayer.SoundType.Effect, value)
                WriteConfig()
            End Set
        End Property

        ''' <summary>
        ''' 读取配置文件
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub ReadConfigFile()
            Dim configFile As New XmlDocument
            configFile.Load(AppCore.API.URLAPI.CombineURL(AppCore.API.URLAPI.GetUserFileURL, "WADV.SoundModule.xml"))
            BackgroundVolume = CDbl(configFile.SelectSingleNode("/config/background").InnerXml)
            ReadingVolume = CDbl(configFile.SelectSingleNode("/config/reading").InnerXml)
            EffectVolume = CDbl(configFile.SelectSingleNode("/config/effect").InnerXml)
        End Sub

        ''' <summary>
        ''' 保存配置
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared Sub WriteConfig()
            Dim configFile As New XmlDocument
            configFile.Load(AppCore.API.URLAPI.CombineURL(AppCore.API.URLAPI.GetUserFileURL, "WADV.SoundModule.xml"))
            configFile.SelectSingleNode("/config/background").InnerXml = BackgroundVolume
            configFile.SelectSingleNode("/config/reading").InnerXml = ReadingVolume
            configFile.SelectSingleNode("/config/effect").InnerXml = EffectVolume
            configFile.Save(AppCore.API.URLAPI.CombineURL(AppCore.API.URLAPI.GetUserFileURL, "WADV.SoundModule.xml"))
        End Sub

    End Class

    ''' <summary>
    ''' 视频设置类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class VideoConfig

        Protected Friend Shared VideoContent As MediaElement = Nothing
        Protected Friend Shared ClickToSkip As Boolean = False
        Protected Friend Shared IsPlayFinished As Boolean = False

        ''' <summary>
        ''' 播放新的视频
        ''' </summary>
        ''' <param name="fileName">视频文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub GetNewContent(fileName As String)
            If VideoContent IsNot Nothing Then
                VideoContent.Dispatcher.Invoke(Sub()
                                                   WindowAPI.GetMainGrid.Children.Remove(VideoContent)
                                                   RemoveHandler VideoContent.MediaEnded, AddressOf Video_Ended
                                                   RemoveHandler VideoContent.MouseLeftButtonDown, AddressOf Video_Click
                                                   VideoContent.Close()
                                                   VideoContent = Nothing
                                               End Sub)
            End If
            WindowAPI.GetMainGrid.Dispatcher.Invoke(Sub()
                                                        VideoContent = New MediaElement
                                                        VideoContent.SetValue(Panel.ZIndexProperty, 10)
                                                        VideoContent.LoadedBehavior = MediaState.Manual
                                                        VideoContent.Source = New Uri(URLAPI.CombineURL(URLAPI.GetResourceURL, fileName))
                                                        VideoContent.Height = WindowAPI.GetMainGrid.Height
                                                        VideoContent.Width = WindowAPI.GetMainGrid.Width
                                                        VideoContent.Margin = New Windows.Thickness(0)
                                                        AddHandler VideoContent.MediaEnded, AddressOf Video_Ended
                                                        AddHandler VideoContent.MouseLeftButtonDown, AddressOf Video_Click
                                                        WindowAPI.GetMainGrid.Children.Add(VideoContent)
                                                        VideoContent.Play()
                                                    End Sub)
            IsPlayFinished = False
        End Sub

        Protected Shared Sub Video_Click(sender As Object, e As Windows.Input.MouseButtonEventArgs)
            If ClickToSkip Then
                VideoContent.Close()
                Video_Ended(VideoContent, New Windows.RoutedEventArgs)
            End If
        End Sub

        Protected Shared Sub Video_Ended(sender As Object, e As Windows.RoutedEventArgs)
            VideoContent.Visibility = Windows.Visibility.Collapsed
            IsPlayFinished = True
        End Sub

    End Class

End Namespace