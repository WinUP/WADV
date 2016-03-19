Imports WADV.Core.PluginInterface

Namespace Utilities
    ''' <summary>
    ''' 辅助空循环
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class EmptyLoop : Implements ILoopReceiver
        Private _count As Integer

        Public Sub New(count As Integer)
            _count = count
        End Sub

        Public Function Logic(frame As Integer) As Boolean Implements ILoopReceiver.Logic
            If _count = 0 Then Return False
            _count -= 1
            Return True
        End Function

        Public Sub Render() Implements ILoopReceiver.Render
        End Sub
    End Class
End Namespace