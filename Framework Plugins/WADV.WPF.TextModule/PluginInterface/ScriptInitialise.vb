Imports Neo.IronLua

Namespace PluginInterface
    Friend NotInheritable Class ScriptInitialise : Implements LuaSupport.IScriptInitialise
        Public Sub Register(vm As Lua, env As LuaGlobal) Implements LuaSupport.IScriptInitialise.Register
            Dim core As LuaTable = env("core")
            core("text") = New LuaTable
            Dim table As LuaTable = core("text")
            table("init") = New Action(Of Integer, Integer, Boolean, Boolean)(AddressOf API.Init)
            table("wordFrame") = New Func(Of Integer, Integer)(AddressOf API.WordFrame)
            table("sentenceFrame") = New Func(Of Integer, Integer)(AddressOf API.SentenceFrame)
            table("getIgnoreMode") = New Func(Of Boolean)(AddressOf API.GetIgnoreMode)
            table("getAutoMode") = New Func(Of Boolean)(AddressOf API.GetAutoMode)
            table("setIgnoreMode") = New Action(Of Boolean)(AddressOf API.SetIgnoreMode)
            table("setAutoMode") = New Action(Of Boolean)(AddressOf API.SetAutoMode)
            table("setTextArea") = New Action(Of String)(AddressOf API.SetTextArea)
            table("setSpeakerArea") = New Action(Of String)(AddressOf API.SetSpeakerArea)
            table("setMainArea") = New Action(Of String)(AddressOf API.SetMainArea)
            table("setVisibility") = New Action(Of Boolean)(AddressOf API.SetVisibility)
            table("register") = New Action(AddressOf API.Register)
            table("unregister") = New Action(AddressOf API.Unregister)
            table("show") = New Action(Of String, String)(AddressOf API.Show)
            table("add") = New Action(Of String, Neo.IronLua.LuaTable)(AddressOf API.ApiForScript.Add)
            Message.Send("[TEXT]LOAD_FINISH")
        End Sub
    End Class
End Namespace
