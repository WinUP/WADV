Imports Neo.IronLua
Imports WADV.MediaModule.API

Namespace PluginInterface
    Friend NotInheritable Class ScriptInitialise : Implements LuaSupport.IScriptInitialise
        Public Sub Register(vm As Lua, env As LuaGlobal) Implements LuaSupport.IScriptInitialise.Register
            Dim core As LuaTable = env("core")
            core("media") = New LuaTable
            Dim table As LuaTable = core("media")
            table("init") = New Action(Of Integer, Integer, Integer)(AddressOf Config.Init)
            table("startReceiver") = New Action(AddressOf Config.StartReceiver)
            table("stopReceiver") = New Action(AddressOf Config.StopReceiver)
            table("getBackgroundVolume") = New Func(Of Double)(AddressOf Config.GetBackgroundVolume)
            table("getReadingVolume") = New Func(Of Double)(AddressOf Config.GetReadingVolume)
            table("getEffectVolume") = New Func(Of Double)(AddressOf Config.GetEffectVolume)
            table("setBackgroundVolume") = New Action(Of Double)(AddressOf Config.SetBackgroundVolume)
            table("setReadingVolume") = New Action(Of Double)(AddressOf Config.SetReadingVolume)
            table("setEffectVolume") = New Action(Of Double)(AddressOf Config.SetEffectVolume)
            table("play") = New Func(Of String, SoundType, Boolean, Integer, Integer)(AddressOf Sound.Play)
            table("playBgm") = New Func(Of String, Integer)(AddressOf Sound.PlayBgm)
            table("playReading") = New Func(Of String, Integer)(AddressOf Sound.PlayReading)
            table("playEffect") = New Func(Of String, Integer)(AddressOf Sound.PlayEffect)
            table("playEffectSync") = New Action(Of String)(AddressOf Sound.PlayEffectSync)
            table("get") = New Func(Of Integer, Player)(AddressOf Sound.Get)
            table("stop") = New Action(Of Integer)(AddressOf Sound.Stop)
            table("pause") = New Action(Of Integer)(AddressOf Sound.Pause)
            table("resume") = New Action(Of Integer)(AddressOf Sound.Resume)
            table("changeVolume") = New Action(Of Integer, Double)(AddressOf Sound.ChangeVolume)
            table("stopNearlyReading") = New Action(AddressOf Sound.StopNearlyReading)
            table("stopNearlyBgm") = New Action(AddressOf Sound.StopNearlyBgm)
            table("playVideo") = New Action(Of String, Boolean)(AddressOf Video.PlayVideo)
            table("playVideoSync") = New Action(Of String, Boolean)(AddressOf Video.PlayVideoSync)
            table("stopVideo") = New Action(AddressOf Video.StopVideo)
            table("pauseVideo") = New Action(AddressOf Video.PauseVideo)
            table("resumeVideo") = New Action(AddressOf Video.ResumeVideo)
            Message.Send("[MEDIA]INIT_LOAD_FINISH")
        End Sub
    End Class
End Namespace
