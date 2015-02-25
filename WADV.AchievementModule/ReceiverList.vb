Imports WADV.AchievementModule.PluginInterface

Friend NotInheritable Class ReceiverList
    Private Shared _receiver As New ScriptmessageReceiver

    Friend Shared Sub RunReceiver()
        MessageAPI.AddSync(_receiver)
    End Sub

    Friend Shared Sub StopReceiver()
        MessageAPI.DeleteSync(_receiver)
    End Sub

End Class
