Imports WADV.AchievementModule.PluginInterface

Friend NotInheritable Class ReceiverList
    Private Shared ReadOnly Receiver As New ScriptMessageReceiver

    Friend Shared Sub RunReceiver()
        Message.Listen(Receiver)
    End Sub

    Friend Shared Sub StopReceiver()
        Message.Remove(Receiver)
    End Sub

End Class
