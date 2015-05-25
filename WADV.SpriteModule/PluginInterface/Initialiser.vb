Imports System.Windows
Imports System.Windows.Controls
Imports WADV.Core.PluginInterface

Namespace PluginInterface
    Friend NotInheritable Class PluginInitialise : Implements IPluginInitialise
        Public Function Initialising() As Boolean Implements IPluginInitialise.Initialising
            LoopAPI.AddLoopSync(New LoopReceiver)
            MessageAPI.AddSync(New MessageReceiver)
            ScriptAPI.RegisterInTableSync("sprite", "init", New Action(AddressOf API.Init), True)
            ScriptAPI.RegisterInTableSync("sprite", "new", New Func(Of String, Canvas)(AddressOf API.[New]))
            ScriptAPI.RegisterInTableSync("sprite", "register", New Func(Of String, FrameworkElement, FrameworkElement)(AddressOf API.Register))
            ScriptAPI.RegisterInTableSync("sprite", "effect", New Action(Of String, String, Boolean, Object())(AddressOf API.Effect))
            ScriptAPI.RegisterInTableSync("sprite", "get", New Func(Of String, FrameworkElement)(AddressOf API.Get))
            ScriptAPI.RegisterInTableSync("sprite", "getSprite", New Func(Of FrameworkElement, Sprite)(AddressOf API.GetSprite))
            ScriptAPI.RegisterInTableSync("sprite", "delete", New Func(Of String, Boolean)(AddressOf API.Delete))
            ScriptAPI.RegisterInTableSync("sprite", "deleteObject", New Func(Of FrameworkElement, Boolean)(AddressOf API.Delete))
            MessageAPI.SendSync("[SPRITE]LOAD_FINISH")
            Return True
        End Function
    End Class
End Namespace