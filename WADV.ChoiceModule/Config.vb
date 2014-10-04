Imports System.Windows.Controls

Namespace Config

    Public Class UIConfig
        Protected Friend Shared ChoiceContent As Panel
        Protected Friend Shared ChoiceStyle As String
        Protected Friend Shared ChoiceMargin As Double

        Public Shared Sub TextBlock_Click(sender As Object, e As Windows.Input.MouseButtonEventArgs)
            DataConfig.Choice = TryCast(sender, TextBlock).Text
        End Sub

    End Class

    Public Class DataConfig

        Protected Friend Shared Choice As String

    End Class

End Namespace