''' <summary>
''' HUD状态更新接收器类型
''' </summary>
''' <remarks></remarks>
Public Enum ReceiverType
    ''' <summary>
    ''' 只使用消息循环更新
    ''' </summary>
    ''' <remarks></remarks>
    MessageOnly
    ''' <summary>
    ''' 只使用主循环更新
    ''' </summary>
    ''' <remarks></remarks>
    LoopOnly
    ''' <summary>
    ''' 既使用消息循环也使用主循环
    ''' </summary>
    ''' <remarks></remarks>
    Both
End Enum

''' <summary>
''' HUD基础类
''' </summary>
''' <remarks></remarks>
Public MustInherit Class Hud
    Private ReadOnly _type As ReceiverType
    Private ReadOnly _continue As Boolean

    ''' <summary>
    ''' 建立一个新的HUD
    ''' </summary>
    ''' <param name="type">接收器类型</param>
    ''' <param name="continue">是否在游戏转场后继续显示</param>
    ''' <remarks></remarks>
    Public Sub New(type As ReceiverType, [continue] As Boolean)
        _type = type
        _continue = [continue]
    End Sub

    ''' <summary>
    ''' 该HUD需要使用的接收器类型
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public MustOverride Function ReceiverType() As ReceiverType

    ''' <summary>
    ''' 初始化HUD
    ''' </summary>
    ''' <remarks></remarks>
    Public MustOverride Sub Init()

    ''' <summary>
    ''' 游戏转场后是否继续显示HUD
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsContinue() As Boolean
        Return _continue
    End Function

    ''' <summary>
    ''' 重新渲染HUD
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub ReRender()
    End Sub

End Class
