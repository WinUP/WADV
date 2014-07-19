Imports System.Reflection
Imports System.Windows.Controls

Namespace API

    Public Class ConfigAPI

        Public Shared Function GetWordFrame() As Integer
            Return Config.ModuleConfig.WordFrame
        End Function

        Public Shared Function GetSetenceFrame() As Integer
            Return Config.ModuleConfig.SetenceFrame
        End Function

        Public Shared Function GetAutoMode() As Boolean
            Return Config.ModuleConfig.Auto
        End Function

        Public Shared Function GetIgnoreMode() As Boolean
            Return Config.ModuleConfig.Ignore
        End Function

        Public Shared Sub ChangeWordFrame(frame As Integer)
            Config.ModuleConfig.WordFrame = frame
        End Sub

        Public Shared Sub ChangeSentenceFrame(frame As Integer)
            Config.ModuleConfig.SetenceFrame = frame
        End Sub

        Public Shared Sub ChangeAutoMode(auto As Boolean)
            Config.ModuleConfig.Auto = auto
        End Sub

        Public Shared Sub ChangeIgnoreMode(ignore As Boolean)
            Config.ModuleConfig.Ignore = ignore
        End Sub

        Public Shared Sub SetUITextArea(area As TextBlock)
            Config.UIConfig.TextArea = area
        End Sub

        Public Shared Sub SetCharacterArea(area As TextBlock)
            Config.UIConfig.CharacterArea = area
        End Sub

        Public Shared Sub RegisterEvent()
            AddHandler AppCore.API.WindowAPI.GetWindow.KeyDown, AddressOf Core.TextCore.Ctrl_Down
            AddHandler AppCore.API.WindowAPI.GetWindow.KeyUp, AddressOf Core.TextCore.Ctrl_Up
            AddHandler Config.UIConfig.TextArea.MouseLeftButtonDown, AddressOf Core.TextCore.TextArea_Click
        End Sub

        Public Shared Sub UnregisterEvent()
            RemoveHandler AppCore.API.WindowAPI.GetWindow.KeyDown, AddressOf Core.TextCore.Ctrl_Down
            RemoveHandler AppCore.API.WindowAPI.GetWindow.KeyUp, AddressOf Core.TextCore.Ctrl_Up
            RemoveHandler Config.UIConfig.TextArea.MouseLeftButtonDown, AddressOf Core.TextCore.TextArea_Click
        End Sub

    End Class

    Public Class TextAPI

        Public Shared Function ShowText(text() As String, character() As String, effectName As String) As Boolean
            If Config.UIConfig.TextArea Is Nothing Then Return False
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Name = effectName AndAlso tmpClass.Namespace = "WADV.TextModule.TextEffect" Select tmpClass
            If classList.Count < 1 Then Return False
            Dim effect As TextEffect.StandardEffect = Activator.CreateInstance(classList.FirstOrDefault, New Object() {text, character})
            Config.ModuleConfig.Ellipsis = False
            Dim loopContent As New PluginInterface.CustomizedLoop(effect)
            AppCore.API.LoopAPI.AddCustomizedLoop(loopContent)
            AppCore.API.LoopAPI.WaitCustomizedLoop(loopContent)
            Return True
        End Function

        Public Shared Function ShowTextByTable(text As LuaInterface.LuaTable, character As LuaInterface.LuaTable, effectName As String) As Boolean
            Dim text1(text.Values.Count - 1) As String
            text.Values.CopyTo(text1, 0)
            Dim character1(character.Values.Count - 1) As String
            character.Values.CopyTo(character1, 0)
            Return ShowText(text1, character1, effectName)
        End Function

    End Class

End Namespace
