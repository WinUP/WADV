Imports System.Windows.Threading
Imports System.Windows.Media.Imaging
Imports System.Windows
Imports System.Windows.Media

Namespace Effect

    ''' <summary>
    ''' 图像渐变效果的基类
    ''' </summary>
    Public MustInherit Class ImageEffect

        Protected bitmapImageContent As ImageCore.BitmapWithPixel
        Protected effectingDuration As Integer

        Public MustOverride Function GetNextImageState() As BitmapSource

        ''' <summary>
        ''' 获得一个图像渐变效果
        ''' </summary>
        ''' <param name="filename">图像文件路径(Resource目录下)</param>
        ''' <param name="duration">动画时长(单位为帧)</param>
        ''' <remarks></remarks>
        Public Sub New(filename As String, duration As Integer)
            bitmapImageContent = New ImageCore.BitmapWithPixel(filename)
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
        Public Property Duration As Integer
            Get
                Return effectingDuration
            End Get
            Set(value As Integer)
                effectingDuration = value
            End Set
        End Property

    End Class

    Public Class FadeEffect : Inherits ImageEffect
        Private opacityPerFrame As Double

        Public Sub New(fileName As String, duration As Integer)
            MyBase.New(fileName, duration)
            opacityPerFrame = 1.0 / duration

        End Sub

        Public Overrides Function GetNextImageState() As BitmapSource

        End Function

    End Class

End Namespace