Imports System.Windows.Controls

Public Class BaseEffect : Implements IEffect
    Protected ImageContent As Panel
    Protected Params() As Object
    Private _isCircle As Boolean
    Private _circleCount As Integer

    Public Sub New(name As String, params As Object())
        ImageContent = SpriteList.Item(name)
        Me.Params = params
    End Sub

    Public Sub SetCircle(isCircle As Boolean, count As Integer)
        _isCircle = isCircle
        _circleCount = count
    End Sub

    Public Overridable Sub Render() Implements IEffect.Render
        Animation_Finished(Me, New EventArgs)
    End Sub

    Protected Overridable Sub ReRender()
        Animation_Finished(Me, New EventArgs)
    End Sub

    Public Sub Dispose() Implements IEffect.Dispose
        ImageContent = Nothing
        Params = Nothing
    End Sub

    Protected Sub Animation_Finished(sender As Object, e As EventArgs)
        If _isCircle Then
            If _circleCount = 0 Then
                MessageAPI.SendSync("[SPRITE]EFFECT_PLAY_FINISH")
                Exit Sub
            End If
            MessageAPI.SendSync("[SPRITE]EFFECT_PLAY_CIRCLE")
            ReRender()
            If _circleCount <> -1 Then _circleCount -= 1
        Else
            MessageAPI.SendSync("[SPRITE]EFFECT_PLAY_FINISH")
        End If
    End Sub

    Public Overridable Sub Wait() Implements IEffect.Wait
        MessageAPI.WaitSync("[SPRITE]EFFECT_PLAY_FINISH")
    End Sub

End Class