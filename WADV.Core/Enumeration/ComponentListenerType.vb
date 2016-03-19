Namespace Enumeration
    ''' <summary>
    ''' 渲染节点对于游戏系统的接收器类型
    ''' </summary>
    Public Enum ComponentListenerType
        ''' <summary>
        ''' 没有接收器
        ''' </summary>
        ''' <remarks></remarks>
        None
        ''' <summary>
        ''' 有游戏循环接收器
        ''' </summary>
        ''' <remarks></remarks>
        LoopingOnly
        ''' <summary>
        ''' 有消息循环接收器
        ''' </summary>
        ''' <remarks></remarks>
        MessageServiceOnly
        ''' <summary>
        ''' 两种循环都有接收器
        ''' </summary>
        ''' <remarks></remarks>
        Both
    End Enum
End Namespace