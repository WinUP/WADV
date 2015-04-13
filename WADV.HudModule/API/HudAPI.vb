Namespace API

    Public NotInheritable Class HudAPI

        Public Shared Function Add(name As String, target As Hud)
            Return HudList.Add(name, target)
        End Function

        Public Shared Function Delete(name As String, type As ReceiverType)
            Return HudList.Delete(name, type)
        End Function

        Public Shared Function Delete(name As String, isLoop As Boolean)
            If isLoop Then
                Return HudList.Delete(name, ReceiverType.LoopOnly)
            Else
                Return HudList.Delete(name, ReceiverType.MessageOnly)
            End If
        End Function

    End Class

End Namespace
