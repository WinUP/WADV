Imports WADV.AchievementModule.PluginInterface

Friend NotInheritable Class ReceiverList
    Private Shared ReadOnly Receiver As New ScriptMessageReceiver

    Friend Shared Sub RunReceiver()
        MessageAPI.AddSync(Receiver)
    End Sub

    Friend Shared Sub StopReceiver()
        MessageAPI.DeleteSync(Receiver)
    End Sub

End Class
