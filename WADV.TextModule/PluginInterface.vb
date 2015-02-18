Imports System.Reflection
Imports WADV.TextModule.TextEffect
Imports WADV.AppCore.PluginInterface

Namespace PluginInterface

    Public Class Initialiser : Implements IInitialise

        Public Function Initialising() As Boolean Implements IInitialise.Initialising
            ScriptAPI.RunStringSync("api_text={}")
            For Each tmpApiClass In (From tmpClass In Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Namespace = "WADV.TextModule.API" AndAlso tmpClass.IsClass AndAlso tmpClass.Name.LastIndexOf("API", StringComparison.Ordinal) = tmpClass.Name.Length - 3 Select tmpClass)
                Dim registerName = tmpApiClass.Name.Substring(0, tmpApiClass.Name.Length - 3).ToLower()
                ScriptAPI.RunStringSync("api_text." + registerName + "={}")
                ScriptAPI.RegisterSync(tmpApiClass, "api_text." + registerName)
            Next
            MessageAPI.SendSync("TEXT_INIT_LOADFINISH")
            Return True
        End Function

    End Class

    Public Class LoopReceiver : Implements ILoopReceiver
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
            Dim text As ITextEffect.SentenceInfo
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
            If Config.ModuleConfig.Clicked Then
                If _effect.IsSentenceOver Then
                    Config.ModuleConfig.Clicked = False
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
            '!在实际执行过程中并不会出现空值的情况，请忽略这个警告
            _renderingText.Speaker = text.Speaker
            _renderingText.Text = text.Text
            Config.ModuleConfig.Clicked = False
            If _effect.IsAllOver Then Return False
            _waitingCount = Config.ModuleConfig.WordFrame
            Return True
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
            Config.UIConfig.TextArea.Text = _renderingText.Text
            Config.UIConfig.SpeakerArea.Text = _renderingText.Speaker
        End Sub

    End Class

End Namespace