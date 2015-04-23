Imports WADV.MediaModule.API
Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Friend NotInheritable Class PluginInitialise : Implements IPluginInitialise

        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            ScriptAPI.RegisterInTableSync("media", "init", New Action(Of Integer, Integer, Integer)(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("media", "startReceiver", New Action(AddressOf ConfigAPI.StartReceiver))
            ScriptAPI.RegisterInTableSync("media", "stopReceiver", New Action(AddressOf ConfigAPI.StopReceiver))
            ScriptAPI.RegisterInTableSync("media", "getBackgroundVolume", New Func(Of Double)(AddressOf ConfigAPI.GetBackgroundVolume))
            ScriptAPI.RegisterInTableSync("media", "getReadingVolume", New Func(Of Double)(AddressOf ConfigAPI.GetReadingVolume))
            ScriptAPI.RegisterInTableSync("media", "getEffectVolume", New Func(Of Double)(AddressOf ConfigAPI.GetEffectVolume))
            ScriptAPI.RegisterInTableSync("media", "setBackgroundVolume", New Action(Of Double)(AddressOf ConfigAPI.SetBackgroundVolume))
            ScriptAPI.RegisterInTableSync("media", "setReadingVolume", New Action(Of Double)(AddressOf ConfigAPI.SetReadingVolume))
            ScriptAPI.RegisterInTableSync("media", "setEffectVolume", New Action(Of Double)(AddressOf ConfigAPI.SetEffectVolume))
            ScriptAPI.RegisterInTableSync("media", "play", New Func(Of String, SoundType, Boolean, Integer, Integer)(AddressOf SoundAPI.Play))
            ScriptAPI.RegisterInTableSync("media", "playBgm", New Func(Of String, Integer)(AddressOf SoundAPI.PlayBgm))
            ScriptAPI.RegisterInTableSync("media", "playReading", New Func(Of String, Integer)(AddressOf SoundAPI.PlayReading))
            ScriptAPI.RegisterInTableSync("media", "playEffect", New Func(Of String, Integer)(AddressOf SoundAPI.PlayEffect))
            ScriptAPI.RegisterInTableSync("media", "playEffectSync", New Action(Of String)(AddressOf SoundAPI.PlayEffectSync))
            ScriptAPI.RegisterInTableSync("media", "get", New Func(Of Integer, Player)(AddressOf SoundAPI.Get))
            ScriptAPI.RegisterInTableSync("media", "stop", New Action(Of Integer)(AddressOf SoundAPI.Stop))
            ScriptAPI.RegisterInTableSync("media", "pause", New Action(Of Integer)(AddressOf SoundAPI.Pause))
            ScriptAPI.RegisterInTableSync("media", "resume", New Action(Of Integer)(AddressOf SoundAPI.Resume))
            ScriptAPI.RegisterInTableSync("media", "changeVolume", New Action(Of Integer, Double)(AddressOf SoundAPI.ChangeVolume))
            ScriptAPI.RegisterInTableSync("media", "stopNearlyReading", New Action(AddressOf SoundAPI.StopNearlyReading))
            ScriptAPI.RegisterInTableSync("media", "stopNearlyBgm", New Action(AddressOf SoundAPI.StopNearlyBgm))
            ScriptAPI.RegisterInTableSync("media", "playVideo", New Action(Of String, Boolean)(AddressOf VideoAPI.PlayVideo))
            ScriptAPI.RegisterInTableSync("media", "playVideoSync", New Action(Of String, Boolean)(AddressOf VideoAPI.PlayVideoSync))
            ScriptAPI.RegisterInTableSync("media", "stopVideo", New Action(AddressOf VideoAPI.StopVideo))
            ScriptAPI.RegisterInTableSync("media", "pauseVideo", New Action(AddressOf VideoAPI.PauseVideo))
            ScriptAPI.RegisterInTableSync("media", "resumeVideo", New Action(AddressOf VideoAPI.ResumeVideo))
            MessageAPI.SendSync("[MEDIA]INIT_LOAD_FINISH")
            Return True
        End Function

    End Class

End Namespace