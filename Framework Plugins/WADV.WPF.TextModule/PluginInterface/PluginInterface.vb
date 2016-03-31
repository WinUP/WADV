Imports WADV.Core.PluginInterface
Imports WADV.TextModule.Config

Namespace PluginInterface

    Friend NotInheritable Class LoopReceiver : Implements ILoopReceiver
        Private ReadOnly _effect As BaseEffect
        Private _waitingCount As Integer = 0
        Private _playVoice As Boolean

        Public Sub New(effect As BaseEffect)
            _effect = effect
            _playVoice = False
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            If _waitingCount > 0 Then
                _waitingCount -= 1
                Return True
            End If
            If _effect.IsAllOver Then Return False
            '快进状态或略过已读
            If ModuleConfig.FastMode OrElse (ModuleConfig.IgnoreRead AndAlso _effect.IsRead) Then
                _effect.NextState()
                While Not _effect.IsSentenceOver
                    _effect.NextState()
                End While
                _waitingCount = 10
                Return True
            End If
            '自动状态
            If ModuleConfig.AutoMode Then
                _effect.NextState()
                If _playVoice Then
                    PlayVoice(_effect.VoiceFile)
                    _playVoice = False
                End If
                If _effect.IsSentenceOver Then
                    _waitingCount = ModuleConfig.SetenceFrame
                    _playVoice = True
                Else
                    _waitingCount = ModuleConfig.WordFrame
                End If
                Return True
            End If
            '手动状态
            If ModuleConfig.ClickedSkip Then
                If _effect.IsSentenceOver Then
                    _effect.NextState()
                    PlayVoice(_effect.VoiceFile)
                Else
                    While Not _effect.IsSentenceOver
                        _effect.NextState()
                    End While
                End If
                ModuleConfig.ClickedSkip = False
            Else
                If _effect.IsSentenceOver Then Return True
                _effect.NextState()
            End If
            _waitingCount = ModuleConfig.WordFrame
            Return True
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
            UiConfig.TextArea.Text = _effect.Sentence
            UiConfig.SpeakerArea.Text = _effect.Speaker
        End Sub

        Private Sub PlayVoice(filePath As String)
            If filePath <> "" Then
                MediaModule.API.Sound.StopNearlyReading()
                MediaModule.API.Sound.PlayReading(filePath)
            End If
        End Sub

    End Class

End Namespace