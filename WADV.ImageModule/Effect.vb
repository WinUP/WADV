Imports System.Windows.Threading
Imports System.Windows.Media.Imaging
Imports System.Windows
Imports System.Windows.Media
Imports WADV.ImageModule.Config

Namespace Effect

    ''' <summary>
    ''' 图像渐变效果的基类
    ''' </summary>
    Public MustInherit Class ImageEffect

        Protected bitmapImageContent As ImageCore.BitmapWithPixel
        Protected effectingDuration As Integer
        Protected complete As Boolean = False

        Protected Friend MustOverride Function GetNextImageState() As BitmapSource

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
        Protected Friend ReadOnly Property PixelImageContent As ImageCore.BitmapWithPixel
            Get
                Return bitmapImageContent
            End Get
        End Property

        ''' <summary>
        ''' 获取或设置动画时长
        ''' </summary>
        ''' <value>新的动画时长</value>
        Protected Friend Property Duration As Integer
            Get
                Return effectingDuration
            End Get
            Set(value As Integer)
                effectingDuration = value
            End Set
        End Property

        Protected Friend ReadOnly Property IsEffectComplete As Boolean
            Get
                Return complete
            End Get
        End Property

    End Class

    Public Class FadeInEffect : Inherits ImageEffect
        Private opacityPerFrame As Double
        Private pixelArray() As Byte

        Public Sub New(fileName As String, duration As Integer)
            MyBase.New(fileName, duration)
            opacityPerFrame = 255 / duration
            If opacityPerFrame < 1 Then opacityPerFrame = 1
            pixelArray = PixelImageContent.PixelInfomation
            Dim i = 3
            While i < pixelArray.Length
                pixelArray(i) = 0
                i += 4
            End While
        End Sub

        Protected Friend Overrides Function GetNextImageState() As BitmapSource
            Dim i = 3
            While i < pixelArray.Length
                If pixelArray(i) + opacityPerFrame < 256 Then
                    pixelArray(i) += opacityPerFrame
                Else
                    pixelArray(i) = 255
                End If
                i += 4
            End While
            If pixelArray(3) = 255 Then complete = True
            Dim width As Integer = PixelImageContent.ImageContent.Dispatcher.Invoke(Function() PixelImageContent.Width)
            Dim height As Integer = PixelImageContent.ImageContent.Dispatcher.Invoke(Function() PixelImageContent.Height)
            Return ImageCore.BitmapWithPixel.ConvertToImage(width, height, pixelArray)
        End Function

    End Class

End Namespace