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
End Enum

''' <summary>
''' HUD基础类
''' </summary>
''' <remarks></remarks>
Public MustInherit Class Hud
    Private ReadOnly _continue As Boolean
    Private ReadOnly _type As ReceiverType

    ''' <summary>
    ''' 建立一个新的HUD
    ''' </summary>
    ''' <param name="continue">是否在游戏转场后继续显示</param>
    ''' <remarks></remarks>
    Public Sub New(type As ReceiverType, [continue] As Boolean)
        _type = type
        _continue = [continue]
    End Sub

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
    Public ReadOnly Property IsContinue As Boolean
        Get
            Return _continue
        End Get
    End Property

    ''' <summary>
    ''' 该HUD使用的接收器类型
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ReceiverType As ReceiverType
        Get
            Return _type
        End Get
    End Property

    ''' <summary>
    ''' 重新渲染HUD
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub ReRender()
    End Sub

End Class

''' <summary>
''' 基于消息更新的HUD基础类
''' </summary>
''' <remarks></remarks>
Public MustInherit Class MessageHud : Inherits Hud

    ''' <summary>
    ''' 建立一个新的HUD
    ''' </summary>
    ''' <param name="continue">是否在游戏转场后继续显示</param>
    ''' <remarks></remarks>
    Public Sub New([continue] As Boolean)
        MyBase.New(HudModule.ReceiverType.MessageOnly, [continue])
    End Sub

    ''' <summary>
    ''' 更新HUD
    ''' </summary>
    ''' <param name="message">被接收的消息</param>
    ''' <remarks></remarks>
    Public MustOverride Sub Render(message As String)

End Class

''' <summary>
''' 基于主循环更新的HUD基础类
''' </summary>
''' <remarks></remarks>
Public MustInherit Class LoopHud : Inherits Hud

    ''' <summary>
    ''' 建立一个新的HUD
    ''' </summary>
    ''' <param name="continue">是否在游戏转场后继续显示</param>
    ''' <remarks></remarks>
    Public Sub New([continue] As Boolean)
        MyBase.New(HudModule.ReceiverType.LoopOnly, [continue])
    End Sub

    Public MustOverride Sub Logic()

    ''' <summary>
    ''' 更新HUD
    ''' </summary>
    ''' <remarks></remarks>
    Public MustOverride Sub Render()

End Class
