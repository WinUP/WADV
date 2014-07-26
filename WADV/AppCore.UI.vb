Imports System.Windows.Threading

Namespace AppCore.UI

    ''' <summary>
    ''' 窗口设定类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WindowConfig

        ''' <summary>
        ''' 游戏主窗口
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared BaseWindow As Window = Application.Current.MainWindow

        ''' <summary>
        ''' 游戏主窗口的Grid
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared BaseGrid As Grid

        Protected Friend Shared Function GetDispatcher(dispatcher As Dispatcher) As Dispatcher
            Return If(dispatcher Is Nothing, API.WindowAPI.GetDispatcher, dispatcher)
        End Function

    End Class

End Namespace