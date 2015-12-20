Imports System.Windows.Navigation

Namespace PluginInterface
    ''' <summary>
    ''' 游戏转场接收器
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface INavigateReceiver
        ''' <summary>
        ''' 处理游戏转场
        ''' </summary>
        ''' <param name="e">可取消的转场数据</param>
        ''' <remarks></remarks>
        Sub ReceiveNavigate(e As NavigationParameter)
    End Interface
End Namespace

