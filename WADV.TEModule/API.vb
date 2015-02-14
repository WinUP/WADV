Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports WADV.TEModule.Config

Namespace API

    Public Class ImageAPI

        ''' <summary>
        ''' 显示一张新图片
        ''' </summary>
        ''' <param name="fileName">图片路径</param>
        ''' <param name="width">宽度</param>
        ''' <param name="height">高度</param>
        ''' <param name="x">距离容器左上角的水平距离</param>
        ''' <param name="y">距离容器左上角的垂直距离</param>
        ''' <param name="showInBegin">声明完成后是否立即显示</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Show(fileName As String, width As Double, height As Double, x As Double, y As Double, Optional showInBegin As Boolean = True) As Integer
            If UIConfig.ImagePanel Is Nothing Then Return False
            Dim tmpImage As Canvas
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               tmpImage = New Canvas
                                               tmpImage.BeginInit()
                                               tmpImage.Width = width
                                               tmpImage.Height = height
                                               tmpImage.HorizontalAlignment = HorizontalAlignment.Left
                                               tmpImage.VerticalAlignment = VerticalAlignment.Top
                                               Dim brush = New ImageBrush(New BitmapImage(New Uri(PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Resource, fileName))))
                                               brush.Stretch = Stretch.Uniform
                                               brush.TileMode = TileMode.None
                                               tmpImage.Background = brush
                                               tmpImage.Margin = New Thickness(x, y, 0, 0)
                                               tmpImage.EndInit()
                                               If Not showInBegin Then tmpImage.Opacity = 0.0
                                               UIConfig.ImagePanel.Children.Add(tmpImage)
                                           End Sub)
            '!在实际执行过程中它一定不是空值，请忽略这个警告
            Return TEList.List.Add(tmpImage)
        End Function

        ''' <summary>
        ''' 将已有图片注册到模块中
        ''' </summary>
        ''' <param name="imageObject">图片对象</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Register(imageObject As Panel) As Integer
            Return TEList.List.Add(imageObject)
        End Function

        ''' <summary>
        ''' 对指定ID的图片应用动画效果
        ''' </summary>
        ''' <param name="id">图片ID</param>
        ''' <param name="effectName">效果名称</param>
        ''' <param name="params">效果参数</param>
        ''' <remarks></remarks>
        Public Shared Sub Effect(id As Integer, effectName As String, sync As Boolean, ParamArray params() As Object)
            If Not TEList.List.Contains(id) Then Return
            Dim effectType = TEModule.Effect.Initialiser.EffectList.Item(effectName)
            If effectType Is Nothing Then Return
            Dim effect As Effect.IEffect = Activator.CreateInstance(effectType, New Object() {id, params})
            MessageAPI.SendSync("TE_EFFECT_BEFORE")
            WindowAPI.GetDispatcher.Invoke(Sub() effect.Render())
            If sync Then effect.Wait()
            effect.Dispose()
            MessageAPI.SendSync("TE_EFFECT_AFTER")
        End Sub

        ''' <summary>
        ''' 注销指定ID的图片并从界面上删除它
        ''' </summary>
        ''' <param name="id"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Hide(id As Integer) As Boolean
            Dim tmpImage = TEList.List.Item(id)
            If tmpImage Is Nothing Then Return False
            UIConfig.ImagePanel.Dispatcher.Invoke(Sub() UIConfig.ImagePanel.Children.Remove(tmpImage))
            Return TEList.List.Delete(id)
        End Function

    End Class

    Public Class ConfigAPI

        Public Shared Sub Init(Optional contentName As String = "")
            Effect.Initialiser.LoadEffect()
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               If contentName = "" Then
                                                   UIConfig.ImagePanel = WindowAPI.GetRoot(Of Grid)()
                                               Else
                                                   UIConfig.ImagePanel = WindowAPI.SearchObject(Of Grid)(contentName)
                                               End If
                                           End Sub)
            MessageAPI.SendSync("TE_INIT_ALLFINISH")
        End Sub

    End Class

End Namespace
