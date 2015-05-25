Imports System.Windows
Imports System.Windows.Controls
Imports Neo.IronLua
Imports WADV.SpriteModule.Effect

''' <summary>
''' 精灵接收器类型
''' </summary>
''' <remarks></remarks>
Public Enum SpriteReceiverType
    ''' <summary>
    ''' 没有接收器
    ''' </summary>
    ''' <remarks></remarks>
    None
    ''' <summary>
    ''' 循环接收器
    ''' </summary>
    ''' <remarks></remarks>
    LoopReceiver
    ''' <summary>
    ''' 消息接收器
    ''' </summary>
    ''' <remarks></remarks>
    MessageReceiver
End Enum

''' <summary>
''' 游戏精灵
''' </summary>
''' <remarks></remarks>
Public Class Sprite : Implements IDisposable
    Private ReadOnly _element As FrameworkElement

    ''' <summary>
    ''' 获得一个精灵
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(target As FrameworkElement)
        _element = target
    End Sub

    Private _receiverType As SpriteReceiverType = SpriteReceiverType.None
    ''' <summary>
    ''' 获取精灵的接收器类型
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ReceiverType As SpriteReceiverType
        Get
            Return _receiverType
        End Get
    End Property

    ''' <summary>
    ''' 在游戏界面上显示精灵
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Show()
        _element.Dispatcher.Invoke(Sub() _element.Visibility = Visibility.Visible)
    End Sub

    ''' <summary>
    ''' 从游戏界面中隐藏精灵
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Hide()
        _element.Dispatcher.Invoke(Sub() _element.Visibility = Visibility.Collapsed)
    End Sub

    ''' <summary>
    ''' 为精灵设置界面父元素
    ''' </summary>
    ''' <param name="parent">精灵的目标父元素</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AsChild(parent As Panel) As Boolean
        WindowAPI.InvokeSync(Sub() parent.Children.Add(_element))
        Return True
    End Function

    ''' <summary>
    ''' 对指定名称的精灵应用动画效果
    ''' </summary>
    ''' <param name="effectName">效果名称</param>
    ''' <param name="sync">是否等待效果完成</param>
    ''' <param name="params">效果参数</param>
    ''' <remarks></remarks>
    Public Sub Effect(effectName As String, sync As Boolean, ParamArray params() As Object)
        Dim effectType = EffectList.Item(effectName)
        If effectType Is Nothing Then Return
        Dim effect As BaseEffect = Activator.CreateInstance(effectType, New Object() {_element, params})
        MessageAPI.SendSync("[SPRITE]EFFECT_STANDBY")
        _element.Dispatcher.Invoke(Sub(e) e.Render(), effect)
        If sync Then effect.Wait()
        effect.Dispose()
        MessageAPI.SendSync("[SPRITE]EFFECT_FINISH")
    End Sub

    ''' <summary>
    ''' 注册循环接收器
    ''' </summary>
    ''' <param name="onLogic">逻辑更新代码</param>
    ''' <param name="onRender">渲染代码</param>
    ''' <remarks></remarks>
    Public Sub SetLoopReceiver(onLogic As Func(Of FrameworkElement, Integer, Boolean), onRender As Func(Of FrameworkElement, Boolean))
        If ReceiverType <> SpriteReceiverType.None Then RemoveReceiver()
        PluginInterface.LoopReceiver.Add(New Receiver.SpriteReceiverForLoop(_element, onLogic, onRender))
        _receiverType = SpriteReceiverType.LoopReceiver
    End Sub

    ''' <summary>
    ''' 注册循环接收器[适用于Lua]
    ''' </summary>
    ''' <param name="onLogic">逻辑更新代码</param>
    ''' <param name="onRender">渲染代码</param>
    ''' <remarks></remarks>
    Public Sub SetLoopReceiver(onLogic As Func(Of FrameworkElement, Integer, LuaResult), onRender As Func(Of FrameworkElement, LuaResult))
        If ReceiverType <> SpriteReceiverType.None Then RemoveReceiver()
        PluginInterface.LoopReceiver.Add(New Receiver.SpriteReceiverForLoop(_element, Function(target, frame) onLogic.Invoke(target, frame).ToBoolean, Function(target) onRender.Invoke(target).ToBoolean))
        _receiverType = SpriteReceiverType.LoopReceiver
    End Sub

    ''' <summary>
    ''' 注册消息接收器
    ''' </summary>
    ''' <param name="onMessage">状态更新代码</param>
    ''' <remarks></remarks>
    Public Sub SetMessageReceiver(onMessage As Func(Of FrameworkElement, String, Boolean))
        If ReceiverType <> SpriteReceiverType.None Then RemoveReceiver()
        PluginInterface.MessageReceiver.Add(New Receiver.SpriteReceiverForMessage(_element, onMessage))
        _receiverType = SpriteReceiverType.MessageReceiver
    End Sub

    ''' <summary>
    ''' 注册消息接收器[适用于Lua]
    ''' </summary>
    ''' <param name="onMessage">状态更新代码</param>
    ''' <remarks></remarks>
    Public Sub SetMessageReceiver(onMessage As Func(Of FrameworkElement, String, LuaResult))
        If ReceiverType <> SpriteReceiverType.None Then RemoveReceiver()
        PluginInterface.MessageReceiver.Add(New Receiver.SpriteReceiverForMessage(_element, Function(target, message) onMessage.Invoke(target, message).ToBoolean))
        _receiverType = SpriteReceiverType.MessageReceiver
    End Sub

    ''' <summary>
    ''' 清除精灵的接收器
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RemoveReceiver()
        If ReceiverType = SpriteReceiverType.LoopReceiver Then
            PluginInterface.LoopReceiver.Remove(_element)
        ElseIf ReceiverType = SpriteReceiverType.MessageReceiver Then
            PluginInterface.MessageReceiver.Remove(_element)
        End If
        _receiverType = SpriteReceiverType.None
    End Sub

    ''' <summary>
    ''' 删除这个精灵
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Delete()
        _element.Dispatcher.Invoke(Sub() SpriteList.Delete(_element))
    End Sub

    ''' <summary>
    ''' 释放这个精灵的所有资源，但不删除精灵列表引用
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Dispose() Implements IDisposable.Dispose
        RemoveReceiver()
        Hide()
        If _element.Parent IsNot Nothing Then
            _element.Dispatcher.Invoke(Sub() CType(_element.Parent, Panel).Children.Remove(_element))
        End If
    End Sub
End Class
