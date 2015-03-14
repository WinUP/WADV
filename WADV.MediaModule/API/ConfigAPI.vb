Namespace API

    ''' <summary>
    ''' 设置API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConfigAPI

        ''' <summary>
        ''' 初始化模块
        ''' </summary>
        ''' <param name="bgmVolume">默认BGM音量(-5000~5000)</param>
        ''' <param name="readingVolume">默认语音音量(-5000~5000)</param>
        ''' <param name="effectVolume">默认效果音音量(-5000~5000)</param>
        ''' <remarks></remarks>
        Public Shared Sub Init(Optional bgmVolume As Integer = 0, Optional readingVolume As Integer = 0, Optional effectVolume As Integer = 0)
            SetBackgroundVolume(bgmVolume)
            SetReadingVolume(readingVolume)
            SetEffectVolume(effectVolume)
            MessageAPI.SendSync("[MEDIA]INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 启动监听器
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StartReceiver()
            If Not ModuleConfig.LoopOn Then
                ModuleConfig.LoopOn = True
                LoopAPI.AddLoopSync(New PluginInterface.LoopReceiver)
            End If
        End Sub

        ''' <summary>
        ''' 停止监听器
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopReceiver()
            ModuleConfig.LoopOn = False
        End Sub

        ''' <summary>
        ''' 获取背景音乐音量(-5000~5000)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBackgroundVolume() As Double
            Return SoundConfig.Background + 5000
        End Function

        ''' <summary>
        ''' 获取对话音量(-5000~5000)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetReadingVolume() As Double
            Return SoundConfig.Reading + 5000
        End Function

        ''' <summary>
        ''' 获取效果音量(-5000~5000)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEffectVolume() As Double
            Return SoundConfig.Effect + 5000
        End Function

        ''' <summary>
        ''' 设置背景音乐音量(-5000~5000)
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetBackgroundVolume(value As Double)
            SoundConfig.Background = value - 5000
            PlayerList.ChangeVolume(SoundType.Background, SoundConfig.Background)
            MessageAPI.SendSync("[MEDIA]BGM_VOLUME_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置对话音量(-5000~5000)
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetReadingVolume(value As Double)
            SoundConfig.Reading = value - 5000
            PlayerList.ChangeVolume(SoundType.Reading, SoundConfig.Reading)
            MessageAPI.SendSync("[MEDIA]READ_VOLUME_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置效果音量(-5000~5000)
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Shared Sub SetEffectVolume(value As Double)
            SoundConfig.Effect = value - 5000
            PlayerList.ChangeVolume(SoundType.Effect, SoundConfig.Effect)
            MessageAPI.SendSync("[MEDIA]EFFECT_VOLUME_CHANGE")
        End Sub

    End Class

End Namespace