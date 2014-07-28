Imports WADV.AppCore
Imports System.Reflection
Imports System.Windows

Namespace PluginInterface

    Public Class Initialise : Implements Plugin.IInitialise

        Public Function StartInitialising() As Boolean Implements Plugin.IInitialise.StartInitialising
            Config.ModuleConfig.ReadConfigFile()
            Config.ModuleConfig.Ellipsis = False
            Config.ModuleConfig.Fast = False
            Return True
        End Function

    End Class

    Public Class Script : Implements Plugin.IScriptFunction

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements Plugin.IScriptFunction.StartRegisting
            ScriptAPI.RegisterFunction(Reflection.Assembly.GetExecutingAssembly.GetTypes, "WADV.TextModule.API", "TM")
        End Sub

    End Class

    Public Class CustomizedLoop : Implements Plugin.ILooping
        Private effect As TextEffect.StandardEffect
        Private waitingCount As Integer = 0
        Private renderingText As TextEffect.StandardEffect.SentenceInfo

        Public Sub New(effect As TextEffect.StandardEffect)
            Me.effect = effect
        End Sub

        Public Function StartLooping() As Boolean Implements AppCore.Plugin.ILooping.StartLooping
            Dim text As TextEffect.StandardEffect.SentenceInfo
            If waitingCount > 0 Then
                waitingCount -= 1
                Return True
            End If
            '快进状态
            If Config.ModuleConfig.Fast Then
                text = effect.GetNextString
                While Not effect.IsSentenceReadOver
                    text = effect.GetNextString
                End While
                renderingText.Character = text.Character
                renderingText.Content = text.Content
                If effect.IsReadOver Then Return False
                waitingCount = 10
                Return True
            End If
            '自动状态
            If Config.ModuleConfig.Auto Then
                text = effect.GetNextString
                renderingText.Character = text.Character
                renderingText.Content = text.Content
                If effect.IsReadOver Then Return False
                If effect.IsSentenceReadOver Then
                    waitingCount = Config.ModuleConfig.SetenceFrame
                    Return True
                End If
                waitingCount = Config.ModuleConfig.WordFrame
                Return True
            End If
            '手动状态
            If Not Config.ModuleConfig.Ellipsis AndAlso effect.IsSentenceReadOver AndAlso Not effect.IsReadOver Then Return True
            If effect.IsSentenceReadOver Then Config.ModuleConfig.Ellipsis = False
            text = effect.GetNextString
            If Config.ModuleConfig.Ellipsis Then
                While Not effect.IsSentenceReadOver
                    text = effect.GetNextString
                End While
            End If
            renderingText.Character = text.Character
            renderingText.Content = text.Content
            Config.ModuleConfig.Ellipsis = False
            If effect.IsReadOver Then Return False
            waitingCount = Config.ModuleConfig.WordFrame
            Return True
        End Function

        Public Sub StartRendering(window As Window) Implements AppCore.Plugin.ILooping.StartRendering
            Config.UIConfig.TextArea.Text = renderingText.Content
            Config.UIConfig.CharacterArea.Text = renderingText.Character
        End Sub

    End Class

End Namespace