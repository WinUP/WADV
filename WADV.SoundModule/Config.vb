Imports System.Windows.Media
Imports System.Xml

Namespace Config

    Public Class SoundList
        Private Shared SoundList As New List(Of Sound)

        Protected Friend Shared Function GetList() As List(Of Sound)
            Return SoundList
        End Function

        Protected Friend Shared Sub AddSound(sound As Sound)
            SoundList.Add(sound)
            '不知道为什么MediaEnded事件总是不调用。暂时先用逻辑循环顶一下
        End Sub

        Protected Friend Shared Function FindSound(name As String) As Sound
            Dim sound = From tmpSound In SoundList Where tmpSound.Name = name Select tmpSound
            If sound.Count < 1 Then Return Nothing
            Return sound.FirstOrDefault
        End Function

        Protected Friend Shared Sub DeleteSound(name As String)
            Dim soundContent = FindSound(name)
            If soundContent Is Nothing Then Exit Sub
            SoundList.Remove(soundContent)
            soundContent.Content.Close()
        End Sub

        Protected Friend Shared Sub DeleteSound(content As Sound)
            SoundList.Remove(content)
            content.Content.Close()
        End Sub

        Protected Friend Shared Sub ChangeVolume(type As Sound.SoundType, volume As Double)
            For Each tmpSound In SoundList
                If tmpSound.Type = type Then tmpSound.Content.Volume = volume
            Next
        End Sub

    End Class

    Public Class Sound
        Public Enum SoundType
            Background
            Reading
            Effect
        End Enum

        Public Name As String
        Public Content As MediaPlayer
        Public Type As SoundType
        Public Cycle As Boolean
        Public CycleCount As Integer
    End Class

    Public Class SoundConfig

        Private Shared BackgroundVolume As Double
        Private Shared ReadingVolume As Double
        Private Shared EffectVolume As Double

        Protected Friend Shared Property Background As Double
            Get
                Return BackgroundVolume
            End Get
            Set(value As Double)
                BackgroundVolume = value
                SoundList.ChangeVolume(Sound.SoundType.Background, value)
                WriteConfig()
            End Set
        End Property

        Protected Friend Shared Property Reading As Double
            Get
                Return ReadingVolume
            End Get
            Set(value As Double)
                ReadingVolume = value
                SoundList.ChangeVolume(Sound.SoundType.Reading, value)
                WriteConfig()
            End Set
        End Property

        Protected Friend Shared Property Effect As Double
            Get
                Return EffectVolume
            End Get
            Set(value As Double)
                EffectVolume = value
                SoundList.ChangeVolume(Sound.SoundType.Effect, value)
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

End Namespace