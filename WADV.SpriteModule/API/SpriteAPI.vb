Imports System.Windows
Imports System.Windows.Controls

Namespace API
    Module Sprite
        ''' <summary>
        ''' 添加一个精灵
        ''' </summary>
        ''' <param name="name">精灵的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function [New](name As String) As Canvas
            Dim target As Canvas = WindowAPI.GetDispatcher.Invoke(Function() New Canvas With {.Name = name})
            Return SpriteList.Add(name, target)
        End Function

        ''' <summary>
        ''' 注册一个现有界面元素为精灵
        ''' </summary>
        ''' <param name="name">精灵的名称</param>
        ''' <param name="target">要注册的元素</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Register(name As String, target As FrameworkElement) As FrameworkElement
            Return SpriteList.Add(name, target)
        End Function

        ''' <summary>
        ''' 获得一个已注册的精灵
        ''' </summary>
        ''' <param name="name">精灵的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function [Get](name As String) As FrameworkElement
            Return SpriteList.Item(name)
        End Function

        ''' <summary>
        ''' 获取元素对应的精灵
        ''' </summary>
        ''' <param name="target">要检查的元素</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSprite(target As FrameworkElement) As SpriteModule.Sprite
            Return WindowAPI.InvokeFunction(Function(e) e.Tag, target)
        End Function

        ''' <summary>
        ''' 为指定精灵应用动画
        ''' </summary>
        ''' <param name="name">精灵名称</param>
        ''' <param name="effectName">动画名称</param>
        ''' <param name="sync">是否等待动画结束</param>
        ''' <param name="params">动画参数</param>
        ''' <remarks></remarks>
        Public Sub Effect(name As String, effectName As String, sync As Boolean, ParamArray params() As Object)
            Dim target = SpriteList.Item(name)
            If target Is Nothing Then Exit Sub
            DirectCast(target.Tag, SpriteModule.Sprite).Effect(effectName, sync, params)
        End Sub

        ''' <summary>
        ''' 注销指定名称的精灵并从界面上删除它
        ''' </summary>
        ''' <param name="name">精灵的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Delete(name As String) As Boolean
            Return SpriteList.Delete(name)
        End Function

        ''' <summary>
        ''' 注销指定的精灵并从界面上删除它
        ''' </summary>
        ''' <param name="target">精灵对象</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Delete(target As FrameworkElement) As Boolean
            Return SpriteList.Delete(target)
        End Function
    End Module
End Namespace
