Imports System.Windows.Threading
Imports System.Windows.Media.Imaging
Imports System.Windows
Imports System.Windows.Media
Imports WADV.ImageModule.Config
Imports WADV.AppCore.API

Namespace Effect

    ''' <summary>
    ''' 图像渐变效果的基类
    ''' </summary>
    Public MustInherit Class ImageEffect

        Private effectingDuration As Integer
        Private imageWidth As Integer
        Private imageHeight As Integer
        Protected pixelArray() As Byte
        Protected complete As Boolean = False

        Protected Friend MustOverride Function GetNextImageState() As BitmapSource

        ''' <summary>
        ''' 获得一个图像渐变效果
        ''' </summary>
        ''' <param name="filename">图像文件路径(Resource目录下)</param>
        ''' <param name="duration">动画时长(单位为帧)</param>
        ''' <remarks></remarks>
        Public Sub New(filename As String, duration As Integer)
            Dim bitmapContent As New FormatConvertedBitmap
            bitmapContent.BeginInit()
            bitmapContent.DestinationPalette = BitmapPalettes.WebPaletteTransparent
            bitmapContent.DestinationFormat = PixelFormats.Bgra32
            bitmapContent.Source = New BitmapImage(New Uri(PathAPI.GetPath(PathAPI.Resource, filename)))
            bitmapContent.EndInit()
            imageWidth = bitmapContent.PixelWidth
            imageHeight = bitmapContent.PixelHeight
            ReDim pixelArray(imageWidth * imageHeight * ModuleConfig.BytePerPixel - 1)
            bitmapContent.CopyPixels(pixelArray, imageWidth * ModuleConfig.BytePerPixel, 0)
            effectingDuration = duration
        End Sub

        ''' <summary>
        ''' 获取图像的像素信息
        ''' </summary>
        Protected Friend ReadOnly Property Pixels As Byte()
            Get
                Return pixelArray
            End Get
        End Property

        ''' <summary>
        ''' 获取图像的宽度
        ''' </summary>
        Protected Friend ReadOnly Property Width As Integer
            Get
                Return imageWidth
            End Get
        End Property

        ''' <summary>
        ''' 获取图像的高度
        ''' </summary>
        Protected Friend ReadOnly Property Height As Integer
            Get
                Return imageHeight
            End Get
        End Property

        ''' <summary>
        ''' 获取动画时长
        ''' </summary>
        Protected Friend ReadOnly Property Duration As Integer
            Get
                Return effectingDuration
            End Get
        End Property

        ''' <summary>
        ''' 获取效果播放状态
        ''' </summary>
        Protected Friend ReadOnly Property IsEffectComplete As Boolean
            Get
                Return complete
            End Get
        End Property

    End Class

    Public Class FadeInEffect : Inherits ImageEffect
        Private opacityPerFrame As Double

        Public Sub New(fileName As String, duration As Integer)
            MyBase.New(fileName, duration)
            opacityPerFrame = 255 / duration
            If opacityPerFrame < 1 Then opacityPerFrame = 1
            Dim i = 3
            While i < pixelArray.Length
                pixelArray(i) = 0
                i += 4
            End While
        End Sub

        Protected Friend Overrides Function GetNextImageState() As BitmapSource
            'BGRA32
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
            Return WindowAPI.GetDispatcher.Invoke(Function() ImageConfig.ConvertToImage(Width, Height, pixelArray))
        End Function

    End Class

End Namespace