Imports System.Windows.Controls

Public Class BaseEffect : Implements IEffect
    Protected ImageContent As Panel
    Protected Params() As Object
    Protected ReadOnly Id As Integer

    Public Sub New(id As Integer, params As Object())
        ImageContent = TeList.Item(id)
        Me.Id = id
        Me.Params = params
    End Sub

    Public Overridable Sub Render() Implements IEffect.Render
    End Sub

    Public Sub Dispose() Implements IEffect.Dispose
        ImageContent = Nothing
        Params = Nothing
    End Sub

    Protected Sub Animation_Finished(sender As Object, e As EventArgs)
        MessageAPI.SendSync("[TE]EFFECT_PLAY_FINISH")
    End Sub

    Public Overridable Sub Wait() Implements IEffect.Wait
        MessageAPI.WaitSync("[TE]EFFECT_PLAY_FINISH")
    End Sub

End Class