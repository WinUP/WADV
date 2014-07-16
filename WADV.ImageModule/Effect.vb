Imports System.Windows.Threading
Imports System.Windows.Media.Imaging
Imports System.Windows

Namespace Effect

    ''' <summary>
    ''' 图像渐变效果的基类
    ''' </summary>
    Public MustInherit Class FadeEffect

        Protected bitmapImageContent As ImageCore.BitmapWithPixel
        Protected effectingDuration As TimeSpan

        Public MustOverride Sub StartEffect()

        ''' <summary>
        ''' 获得一个图像渐变效果
        ''' </summary>
        ''' <param name="bitmapURI">图像URI路径</param>
        ''' <param name="bitmapUriKind">URI类型</param>
        ''' <param name="duration">动画时长</param>
        ''' <remarks></remarks>
        Public Sub New(bitmapURI As String, bitmapUriKind As UriKind, duration As TimeSpan)
            bitmapImageContent = New ImageCore.BitmapWithPixel(bitmapURI, bitmapUriKind)
            effectingDuration = duration
        End Sub

        ''' <summary>
        ''' 获得一个图像渐变效果
        ''' </summary>
        ''' <param name="bitmapImageContent">图像的BitmapImage对象</param>
        ''' <param name="duration">动画时长</param>
        ''' <remarks></remarks>
        Public Sub New(bitmapImageContent As BitmapImage, duration As TimeSpan)
            Me.bitmapImageContent = New ImageCore.BitmapWithPixel(bitmapImageContent)
            effectingDuration = duration
        End Sub

        ''' <summary>
        ''' 获取图像带像素信息的内容
        ''' </summary>
        Public ReadOnly Property PixelImageContent As ImageCore.BitmapWithPixel
            Get
                Return bitmapImageContent
            End Get
        End Property

        ''' <summary>
        ''' 获取或设置动画时长
        ''' </summary>
        ''' <value>新的动画时长</value>
        Public Property Duration As TimeSpan
            Get
                Return effectingDuration
            End Get
            Set(value As TimeSpan)
                effectingDuration = value
            End Set
        End Property

    End Class

    ''' <summary>
    ''' 旋转图像渐变效果
    ''' </summary>
    Public Class CircleRotateFadeEffect : Inherits FadeEffect

        Private rotatePoint As Point
        Private processingImage As BitmapImage

        ''' <summary>
        ''' 获得一个旋转旋转图像渐变效果
        ''' </summary>
        ''' <param name="bitmapURI">新图像URI路径</param>
        ''' <param name="rotatePoint">旋转中心点</param>
        ''' <param name="imageUriKind">URI类型</param>
        ''' <param name="duration">动画时长</param>
        ''' <remarks></remarks>
        Public Sub New(bitmapURI As String, rotatePoint As Point, imageUriKind As UriKind, duration As TimeSpan)
            MyBase.New(bitmapURI, imageUriKind, duration)
            Me.rotatePoint = rotatePoint
        End Sub

        ''' <summary>
        ''' 获得一个旋转图像渐变效果
        ''' </summary>
        ''' <param name="bitmapImageContent">新图像的BitmapImage对象</param>
        ''' <param name="rotatePoint">旋转中心点</param>
        ''' <param name="duration">动画时长</param>
        ''' <remarks></remarks>
        Public Sub New(bitmapImageContent As BitmapImage, rotatePoint As Point, duration As TimeSpan)
            MyBase.New(bitmapImageContent, duration)
            Me.rotatePoint = rotatePoint
        End Sub

        ''' <summary>
        ''' 开始旋转图像渐变
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides Sub StartEffect()
            Dim workingTimer As New DispatcherTimer
            workingTimer.Interval = TimeSpan.FromMilliseconds(Duration.TotalMilliseconds / 360)
            AddHandler workingTimer.Tick, AddressOf Timer_Tick
            workingTimer.Start()
        End Sub

        Private Sub Timer_Tick(sender As Object, e As EventArgs)

        End Sub



    End Class

End Namespace