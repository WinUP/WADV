Imports System.Windows.Controls
Imports WADV.SpriteModule.Effect
Imports WADV.SpriteModule.PluginInterface

Namespace API

    Public Class SpriteAPI

        ''' <summary>
        ''' 添加一个精灵
        ''' </summary>
        ''' <param name="name">精灵的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function [New](name As String) As Canvas
            Return SpriteList.Add(name)
        End Function

        ''' <summary>
        ''' 注册一个现有界面元素为精灵
        ''' </summary>
        ''' <param name="name">精灵的名称</param>
        ''' <param name="target">要注册的元素</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Register(name As String, target As Panel) As Boolean
            Return SpriteList.Add(name, target)
        End Function

        ''' <summary>
        ''' 将精灵添加到界面
        ''' </summary>
        ''' <param name="name">要添加的精灵</param>
        ''' <param name="parent">精灵的目标父元素</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Show(name As String, parent As Panel) As Boolean
            Dim target = SpriteList.Item(name)
            If target Is Nothing Then Return False
            WindowAPI.InvokeSync(Sub() parent.Children.Add(target))
            MessageAPI.SendSync("[SPRITE]SPRITE_SHOW")
            Return True
        End Function

        ''' <summary>
        ''' 对指定名称的精灵应用动画效果
        ''' </summary>
        ''' <param name="name">精灵的名称</param>
        ''' <param name="effectName">效果名称</param>
        ''' <param name="sync">是否等待效果完成</param>
        ''' <param name="params">效果参数</param>
        ''' <remarks></remarks>
        Public Shared Sub Effect(name As String, effectName As String, sync As Boolean, ParamArray params() As Object)
            If Not SpriteList.Contains(name) Then Return
            Dim effectType = EffectList.Get(effectName)
            If effectType Is Nothing Then Return
            Dim effect As IEffect = Activator.CreateInstance(effectType, New Object() {name, params})
            MessageAPI.SendSync("[SPRITE]EFFECT_STANDBY")
            WindowAPI.InvokeSync(Sub() effect.Render())
            If sync Then effect.Wait()
            effect.Dispose()
            MessageAPI.SendSync("[SPRITE]EFFECT_FINISH")
        End Sub

        ''' <summary>
        ''' 获得一个已注册的精灵
        ''' </summary>
        ''' <param name="name">精灵的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function [Get](name As String) As Panel
            Return SpriteList.Item(name)
        End Function

        ''' <summary>
        ''' 注销指定名称的精灵并从界面上删除它
        ''' </summary>
        ''' <param name="name">精灵的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Delete(name As String) As Boolean
            Return SpriteList.Delete(name)
        End Function

        ''' <summary>
        ''' 注销指定的精灵并从界面上删除它
        ''' </summary>
        ''' <param name="target">精灵对象</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Delete(target As Panel) As Boolean
            Return SpriteList.Delete(target)
        End Function

        ''' <summary>
        ''' 对指定精灵注册循环接收器
        ''' </summary>
        ''' <param name="name">目标精灵的名称</param>
        ''' <param name="onLogic">逻辑运算代码</param>
        ''' <param name="onRender">渲染代码</param>
        ''' <remarks></remarks>
        Public Shared Sub SetLoopReceiver(name As String, onLogic As Func(Of Object, Object, Object), onRender As Func(Of Object, Object))
            Dim target = SpriteList.Item(name)
            If target Is Nothing Then Exit Sub
            Dim receiver As New Receiver.LoopReceiverHelper(onLogic, onRender)
            LoopReceiver.Add(target, receiver)
        End Sub

        ''' <summary>
        ''' 对指定精灵注册循环接收器
        ''' </summary>
        ''' <param name="name">目标精灵的名称</param>
        ''' <param name="receiver">目标消息接收器</param>
        ''' <remarks></remarks>
        Public Shared Sub SetLoopReceiver(name As String, receiver As Receiver.ISpriteLoopReceiver)
            Dim target = SpriteList.Item(name)
            If target Is Nothing Then Exit Sub
            LoopReceiver.Add(target, receiver)
        End Sub

        ''' <summary>
        ''' 对指定精灵注册消息接收器
        ''' </summary>
        ''' <param name="name">目标精灵的名称</param>
        ''' <param name="onReceiving">接收器代码</param>
        ''' <remarks></remarks>
        Public Shared Sub SetMessageReceiver(name As String, onReceiving As Func(Of Object, Object, Object))
            Dim target = SpriteList.Item(name)
            If target Is Nothing Then Exit Sub
            Dim receiver As New Receiver.MessageReceiverHelper(onReceiving)
            MessageReceiver.Add(target, receiver)
        End Sub

        ''' <summary>
        ''' 对指定精灵注册消息接收器
        ''' </summary>
        ''' <param name="name">目标精灵的名称</param>
        ''' <param name="receiver">目标消息接收器</param>
        ''' <remarks></remarks>
        Public Shared Sub SetMessageReceiver(name As String, receiver As Receiver.ISpriteMessageReceiver)
            Dim target = SpriteList.Item(name)
            If target Is Nothing Then Exit Sub
            MessageReceiver.Add(target, receiver)
        End Sub
    End Class
End Namespace
