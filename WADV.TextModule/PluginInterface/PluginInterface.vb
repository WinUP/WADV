Imports WADV.Core.PluginInterface

Namespace PluginInterface

    Friend NotInheritable Class LoopReceiver : Implements ILoopReceiver
        Private ReadOnly _effect As ITextEffect
        Private _waitingCount As Integer = 0
        Private _renderingText As ITextEffect.SentenceInfo

        Public Sub New(effect As ITextEffect)
            _effect = effect
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            If _waitingCount > 0 Then
                _waitingCount -= 1
                Return True
            End If
            If _effect.IsAllOver Then Return False
            Dim text As ITextEffect.SentenceInfo = Nothing
            '快进状态或略过已读
            If ModuleConfig.Fast OrElse (ModuleConfig.Ignore AndAlso _effect.IsRead) Then
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
            If ModuleConfig.Auto Then
                text = _effect.GetNext
                _renderingText.Speaker = text.Speaker
                _renderingText.Text = text.Text
                If _effect.IsAllOver Then Return False
                If _effect.IsSentenceOver Then
                    _waitingCount = ModuleConfig.SetenceFrame
                    Return True
                End If
                _waitingCount = ModuleConfig.WordFrame
                Return True
            End If
            '手动状态
            If ModuleConfig.Clicked Then
                If _effect.IsSentenceOver Then
                    ModuleConfig.Clicked = False
                    text = _effect.GetNext
                Else
                    While Not _effect.IsSentenceOver
                        text = _effect.GetNext
                    End While
                End If
            Else
                If _effect.IsSentenceOver Then Return True
                text = _effect.GetNext
            End If
            _renderingText.Speaker = text.Speaker
            _renderingText.Text = text.Text
            ModuleConfig.Clicked = False
            If _effect.IsAllOver Then Return False
            _waitingCount = ModuleConfig.WordFrame
            Return True
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
            UiConfig.TextArea.Text = _renderingText.Text
            UiConfig.SpeakerArea.Text = _renderingText.Speaker
        End Sub

    End Class

End Namespace