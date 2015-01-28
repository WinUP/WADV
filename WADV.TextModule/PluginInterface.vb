Imports WADV.AppCore
Imports WADV.TextModule.TextEffect
Imports System.Windows

Namespace PluginInterface

    Public Class Initialise : Implements Plugin.IInitialise

        Public Function StartInitialising() As Boolean Implements Plugin.IInitialise.Initialising
            Config.ModuleConfig.Clicked = False
            Config.ModuleConfig.Fast = False
            Initialiser.LoadEffect()
            MessageAPI.SendSync("TEXT_INIT_FINISH")
            Return True
        End Function

    End Class

    Public Class CustomizedLoop : Implements Plugin.ILoopReceiver
        Private ReadOnly _effect As ITextEffect
        Private _waitingCount As Integer = 0
        Private _renderingText As ITextEffect.SentenceInfo

        Public Sub New(effect As ITextEffect)
            _effect = effect
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements Plugin.ILoopReceiver.Logic
            Dim text As ITextEffect.SentenceInfo
            If _waitingCount > 0 Then
                _waitingCount -= 1
                Return True
            End If
            '快进状态或略过已读
            If Config.ModuleConfig.Fast OrElse (Config.ModuleConfig.Ignore AndAlso _effect.IsRead) Then
                text = _effect.GetNext
                While Not _effect.IsSentenceOver
                    text = _effect.GetNext
                End While
                _renderingText.Speaker = text.Speaker
                _renderingText.Text = text.Text
                If _effect.IsAllOver Then Return False
                _waitingCount = 10
                Return True
            End If
            '自动状态
            If Config.ModuleConfig.Auto Then
                text = _effect.GetNext
                _renderingText.Speaker = text.Speaker
                _renderingText.Text = text.Text
                If _effect.IsAllOver Then Return False
                If _effect.IsSentenceOver Then
                    _waitingCount = Config.ModuleConfig.SetenceFrame
                    Return True
                End If
                _waitingCount = Config.ModuleConfig.WordFrame
                Return True
            End If
            '手动状态
            If Not Config.ModuleConfig.Clicked AndAlso _effect.IsSentenceOver AndAlso Not _effect.IsAllOver Then Return True
            If _effect.IsSentenceOver Then Config.ModuleConfig.Clicked = False
            text = _effect.GetNext
            If Config.ModuleConfig.Clicked Then
                While Not _effect.IsSentenceOver
                    text = _effect.GetNext
                End While
            End If
            _renderingText.Speaker = text.Speaker
            _renderingText.Text = text.Text
            Config.ModuleConfig.Clicked = False
            If _effect.IsAllOver Then Return False
            _waitingCount = Config.ModuleConfig.WordFrame
            Return True
        End Function

        Public Sub Render(window As Window) Implements Plugin.ILoopReceiver.Render
            Config.UIConfig.TextArea.Text = _renderingText.Text
            Config.UIConfig.SpeakerArea.Text = _renderingText.Speaker
        End Sub

    End Class

End Namespace