﻿''' <summary>
''' 路径类型
''' </summary>
''' <remarks></remarks>
Public Enum PathType
    Game
    Plugin
    Resource
    Skin
    Script
    UserFile
End Enum

''' <summary>
''' 组件绑定结果
''' </summary>
''' <remarks></remarks>
Public Enum BindingResult
    ''' <summary>
    ''' 绑定成功
    ''' </summary>
    ''' <remarks></remarks>
    Success
    ''' <summary>
    ''' 绑定已存在，不需要再次绑定
    ''' </summary>
    ''' <remarks></remarks>
    NoNeed
    ''' <summary>
    ''' 绑定被否决
    ''' </summary>
    ''' <remarks></remarks>
    Cancel
End Enum

''' <summary>
''' 组件解绑结果
''' </summary>
''' <remarks></remarks>
Public Enum UnbindingResult
    ''' <summary>
    ''' 解绑失败
    ''' </summary>
    ''' <remarks></remarks>
    Success
    ''' <summary>
    ''' 找不到需要解绑的组件
    ''' </summary>
    ''' <remarks></remarks>
    CannotFind
    ''' <summary>
    ''' 解绑被否决
    ''' </summary>
    ''' <remarks></remarks>
    Cancel
End Enum

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