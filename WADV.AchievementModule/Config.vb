Imports System.Windows.Controls
Imports System.Windows.Media.Animation
Imports System.Windows.Navigation

Friend NotInheritable Class Config
    ''' <summary>
    ''' 游戏主窗口
    ''' </summary>
    Friend Shared Window As NavigationWindow = Nothing
    ''' <summary>
    ''' 成就信息保存文件夹
    ''' </summary>
    Friend Shared SaveFileFolder As String = ""
    ''' <summary>
    ''' 成就处理脚本文件名
    ''' </summary>
    Friend Shared ReceiverFileName As String = ""
    ''' <summary>
    ''' 成就显示元素
    ''' </summary>
    Friend Shared Element As Border = Nothing
    ''' <summary>
    ''' 成就显示动画
    ''' </summary>
    Friend Shared Storyboard As Storyboard = Nothing
End Class
