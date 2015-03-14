Imports WADV.MediaModule.API
Imports WADV.AppCore.PluginInterface

Namespace PluginInterface

    Friend NotInheritable Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
            ScriptAPI.RegisterInTableSync("api_media", "init", New Action(Of Integer, Integer, Integer)(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("api_media", "startReceiver", New Action(AddressOf ConfigAPI.StartReceiver))
            ScriptAPI.RegisterInTableSync("api_media", "stopReceiver", New Action(AddressOf ConfigAPI.StopReceiver))
            ScriptAPI.RegisterInTableSync("api_media", "getBackgroundVolume", New Func(Of Double)(AddressOf ConfigAPI.GetBackgroundVolume))
            ScriptAPI.RegisterInTableSync("api_media", "getReadingVolume", New Func(Of Double)(AddressOf ConfigAPI.GetReadingVolume))
            ScriptAPI.RegisterInTableSync("api_media", "getEffectVolume", New Func(Of Double)(AddressOf ConfigAPI.GetEffectVolume))
            ScriptAPI.RegisterInTableSync("api_media", "setBackgroundVolume", New Action(Of Double)(AddressOf ConfigAPI.SetBackgroundVolume))
            ScriptAPI.RegisterInTableSync("api_media", "setReadingVolume", New Action(Of Double)(AddressOf ConfigAPI.SetReadingVolume))
            ScriptAPI.RegisterInTableSync("api_media", "setEffectVolume", New Action(Of Double)(AddressOf ConfigAPI.SetEffectVolume))
            ScriptAPI.RegisterInTableSync("api_media", "play", New Func(Of String, SoundType, Boolean, Integer, Integer)(AddressOf SoundAPI.Play))
            ScriptAPI.RegisterInTableSync("api_media", "playBgm", New Func(Of String, Integer)(AddressOf SoundAPI.PlayBgm))
            ScriptAPI.RegisterInTableSync("api_media", "playReading", New Func(Of String, Integer)(AddressOf SoundAPI.PlayReading))
            ScriptAPI.RegisterInTableSync("api_media", "playEffect", New Func(Of String, Integer)(AddressOf SoundAPI.PlayEffect))
            ScriptAPI.RegisterInTableSync("api_media", "playEffectSync", New Action(Of String)(AddressOf SoundAPI.PlayEffectSync))
            ScriptAPI.RegisterInTableSync("api_media", "get", New Func(Of Integer, Player)(AddressOf SoundAPI.Get))
            ScriptAPI.RegisterInTableSync("api_media", "stop", New Action(Of Integer)(AddressOf SoundAPI.Stop))
            ScriptAPI.RegisterInTableSync("api_media", "pause", New Action(Of Integer)(AddressOf SoundAPI.Pause))
            ScriptAPI.RegisterInTableSync("api_media", "resume", New Action(Of Integer)(AddressOf SoundAPI.Resume))
            ScriptAPI.RegisterInTableSync("api_media", "changeVolume", New Action(Of Integer, Double)(AddressOf SoundAPI.ChangeVolume))
            ScriptAPI.RegisterInTableSync("api_media", "stopNearlyReading", New Action(AddressOf SoundAPI.StopNearlyReading))
            ScriptAPI.RegisterInTableSync("api_media", "stopNearlyBgm", New Action(AddressOf SoundAPI.StopNearlyBgm))
            ScriptAPI.RegisterInTableSync("api_media", "playVideo", New Action(Of String, Boolean)(AddressOf VideoAPI.PlayVideo))
            ScriptAPI.RegisterInTableSync("api_media", "playVideoSync", New Action(Of String, Boolean)(AddressOf VideoAPI.PlayVideoSync))
            ScriptAPI.RegisterInTableSync("api_media", "stopVideo", New Action(AddressOf VideoAPI.StopVideo))
            ScriptAPI.RegisterInTableSync("api_media", "pauseVideo", New Action(AddressOf VideoAPI.PauseVideo))
            ScriptAPI.RegisterInTableSync("api_media", "resumeVideo", New Action(AddressOf VideoAPI.ResumeVideo))
            MessageAPI.SendSync("[MEDIA]INIT_LOAD_FINISH")
            Return True
        End Function

    End Class

End Namespace