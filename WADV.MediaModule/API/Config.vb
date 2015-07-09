Namespace API

    ''' <summary>
    ''' 设置API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Config
        ''' <summary>
        ''' 初始化模块
        ''' </summary>
        ''' <param name="bgmVolume">默认BGM音量(-5000~5000)</param>
        ''' <param name="readingVolume">默认语音音量(-5000~5000)</param>
        ''' <param name="effectVolume">默认效果音音量(-5000~5000)</param>
        ''' <remarks></remarks>
        Public Sub Init(Optional bgmVolume As Integer = 0, Optional readingVolume As Integer = 0, Optional effectVolume As Integer = 0)
            SetBackgroundVolume(bgmVolume)
            SetReadingVolume(readingVolume)
            SetEffectVolume(effectVolume)
            Message.Send("[MEDIA]INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 启动监听器
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub StartReceiver()
            If Not ModuleConfig.LoopOn Then
                ModuleConfig.LoopOn = True
                [Loop].Listen(New PluginInterface.LoopReceiver)
            End If
        End Sub

        ''' <summary>
        ''' 停止监听器
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub StopReceiver()
            ModuleConfig.LoopOn = False
        End Sub

        ''' <summary>
        ''' 获取背景音乐音量(-5000~5000)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetBackgroundVolume() As Double
            Return SoundConfig.Background + 5000
        End Function

        ''' <summary>
        ''' 获取对话音量(-5000~5000)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetReadingVolume() As Double
            Return SoundConfig.Reading + 5000
        End Function

        ''' <summary>
        ''' 获取效果音量(-5000~5000)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetEffectVolume() As Double
            Return SoundConfig.Effect + 5000
        End Function

        ''' <summary>
        ''' 设置背景音乐音量(-5000~5000)
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Sub SetBackgroundVolume(value As Double)
            SoundConfig.Background = value - 5000
            PlayerList.ChangeVolume(SoundType.Background, SoundConfig.Background)
            Message.Send("[MEDIA]BGM_VOLUME_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置对话音量(-5000~5000)
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Sub SetReadingVolume(value As Double)
            SoundConfig.Reading = value - 5000
            PlayerList.ChangeVolume(SoundType.Reading, SoundConfig.Reading)
            Message.Send("[MEDIA]READ_VOLUME_CHANGE")
        End Sub

        ''' <summary>
        ''' 设置效果音量(-5000~5000)
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Sub SetEffectVolume(value As Double)
            SoundConfig.Effect = value - 5000
            PlayerList.ChangeVolume(SoundType.Effect, SoundConfig.Effect)
            Message.Send("[MEDIA]EFFECT_VOLUME_CHANGE")
        End Sub
    End Module
End Namespace