Imports System.Windows.Controls

Namespace Receiver

    Public Interface ISpriteLoopReceiver

        Function Logic(target As Panel, frame As Integer) As Boolean

        Function Render(target As Panel) As Boolean

    End Interface
End Namespace