Namespace Script
    Public NotInheritable Class Field
        Public Property Name As String

        Public Property Content As Dictionary(Of String, Object)

        Public Sub New()
            Name = ""
            Content = New Dictionary(Of String, Object)
        End Sub
    End Class
End Namespace
