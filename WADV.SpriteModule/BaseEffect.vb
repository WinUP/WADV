Imports System.Windows.Controls

Public Class BaseEffect : Implements IEffect
    Protected ImageContent As Panel
    Protected Params() As Object

    Public Sub New(name As String, params As Object())
        ImageContent = SpriteList.Item(name)
        Me.Params = params
    End Sub

    Public Overridable Sub Render() Implements IEffect.Render
    End Sub

    Public Sub Dispose() Implements IEffect.Dispose
        ImageContent = Nothing
        Params = Nothing
    End Sub

    Protected Sub Animation_Finished(sender As Object, e As EventArgs)
        MessageAPI.SendSync("[SPRITE]EFFECT_PLAY_FINISH")
    End Sub

    Public Overridable Sub Wait() Implements IEffect.Wait
        MessageAPI.WaitSync("[SPRITE]EFFECT_PLAY_FINISH")
    End Sub

End Class