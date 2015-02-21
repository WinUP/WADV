Imports System.Windows.Controls
Imports WADV.ChoiceModule.API
Imports WADV.AppCore.PluginInterface

Namespace PluginInterface

    Public Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
            ScriptAPI.RegisterInTableSync("api_choice", "init", New Action(Of String, String, Double)(AddressOf ConfigAPI.Init), True)
            ScriptAPI.RegisterInTableSync("api_choice", "setContent", New Action(Of Panel)(AddressOf ConfigAPI.SetContent))
            ScriptAPI.RegisterInTableSync("api_choice", "setStyle", New Action(Of String)(AddressOf ConfigAPI.SetStyle))
            ScriptAPI.RegisterInTableSync("api_choice", "setMargin", New Action(Of Double)(AddressOf ConfigAPI.SetMargin))
            ScriptAPI.RegisterInTableSync("api_choice", "show", New Action(Of Neo.IronLua.LuaTable, String, String, Integer, String)(AddressOf ChoiceAPI.ShowByLua))
            MessageAPI.SendSync("CHOICE_INIT_LOADFINISH")
            Return True
        End Function

    End Class

    Friend NotInheritable Class LoopReceiver : Implements ILoopReceiver
        Private ReadOnly _style As Effect.IProgressEffect

        Public Sub New(style As Effect.IProgressEffect)
            _style = style
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            Return _style.Logic()
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
            _style.Render()
        End Sub

    End Class

End Namespace