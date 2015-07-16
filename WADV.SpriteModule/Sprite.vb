Imports System.Windows
Imports System.Windows.Controls
Imports Neo.IronLua
Imports WADV.SpriteModule.Effect

''' <summary>
''' 游戏精灵 [Component]
''' </summary>
''' <remarks>这个Component被关联到多个元素时操作的始终是最后一个元素</remarks>
Public Class Sprite : Inherits Core.Component.Component
    Private _element As FrameworkElement

    Protected Overrides Function BeforeBinding(sourceElement As FrameworkElement) As Boolean
        _element = sourceElement
        Return True
    End Function

    Protected Overrides Function BeforeUnbinding(sourceElement As FrameworkElement, Optional isForce As Boolean = False) As Boolean
        Dim elements = BindedElements
        If elements.Length > 0 Then _element = elements(elements.Length - 1)
        Return True
    End Function

    ''' <summary>
    ''' 在游戏界面上显示精灵
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Show()
        Invoke(Sub() _element.Visibility = Visibility.Visible)
    End Sub

    ''' <summary>
    ''' 从游戏界面中隐藏精灵
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Hide()
        Invoke(Sub() _element.Visibility = Visibility.Collapsed)
    End Sub

    ''' <summary>
    ''' 为精灵设置界面父元素
    ''' </summary>
    ''' <param name="parent">精灵的目标父元素</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AsChild(parent As Panel) As Boolean
        Invoke(Sub()
                   If _element.Parent IsNot Nothing Then DirectCast(_element.Parent, Panel).Children.Remove(_element)
                   parent.Children.Add(_element)
               End Sub)
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
        Dim effect = EffectList.Create(effectName, _element, New Object() {_element, params})
        If effect Is Nothing Then Exit Sub
        Send("[SPRITE]EFFECT_STANDBY")
        _element.Dispatcher.Invoke(Sub(e) e.Render(), effect)
        If sync Then effect.Wait()
        effect.Dispose()
        Send("[SPRITE]EFFECT_FINISH")
    End Sub
End Class
