Imports System.Windows.Threading
Imports System.Windows.Media.Imaging
Imports System.Windows
Imports System.Windows.Media
Imports WADV.ImageModule.Config
Imports WADV.AppCore.API

Namespace ImageEffect

    ''' <summary>
    ''' 图像渐变效果的基类
    ''' </summary>
    Public MustInherit Class BaseEffect
        Private effectingDuration As Integer
        Private imageWidth As Integer
        Private imageHeight As Integer
        Protected pixelArray() As Byte
        Protected arrayLength As Integer
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
            arrayLength = pixelArray.Length
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

    Public Class NoEffect : Inherits BaseEffect

        Public Sub New(fileName As String)
            MyBase.New(fileName, 0)
        End Sub

        Protected Friend Overrides Function GetNextImageState() As BitmapSource
            complete = True
            Return WindowAPI.GetDispatcher.Invoke(Function() ImageConfig.ConvertToImage(Width, Height, pixelArray))
        End Function

    End Class

    Public Class FadeInEffect : Inherits BaseEffect
        Private opacityPerFrame As Integer

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
            Dim blockNumber = arrayLength \ Config.ModuleConfig.BytePerPixel
            Dim threadMaximite As Integer = (blockNumber \ 2) * Config.ModuleConfig.BytePerPixel - 1
            Dim thread1 As New System.Threading.Thread(Sub()
                                                           Dim i = 3
                                                           While i < threadMaximite
                                                               If pixelArray(i) + opacityPerFrame < 256 Then
                                                                   pixelArray(i) += opacityPerFrame
                                                               Else
                                                                   pixelArray(i) = 255
                                                               End If
                                                               i += 4
                                                           End While
                                                       End Sub)
            Dim thread2 As New System.Threading.Thread(Sub()
                                                           Dim i = threadMaximite + 4
                                                           While i < arrayLength
                                                               If pixelArray(i) + opacityPerFrame < 256 Then
                                                                   pixelArray(i) += opacityPerFrame
                                                               Else
                                                                   pixelArray(i) = 255
                                                               End If
                                                               i += 4
                                                           End While
                                                       End Sub)
            thread1.IsBackground = True
            thread2.IsBackground = True
            thread1.Start()
            thread2.Start()
            thread1.Join()
            thread2.Join()
            If pixelArray(3) = 255 Then complete = True
            Return WindowAPI.GetDispatcher.Invoke(Function() ImageConfig.ConvertToImage(Width, Height, pixelArray))
        End Function

    End Class

    Public Class FadeOutEffect : Inherits BaseEffect
        Private opacityPerFrame As Integer

        Public Sub New(fileName As String, duration As Integer)
            MyBase.New(fileName, duration)
            opacityPerFrame = 255 / duration
            If opacityPerFrame < 1 Then opacityPerFrame = 1
            Dim i = 3
            While i < pixelArray.Length
                pixelArray(i) = 255
                i += 4
            End While
        End Sub

        Protected Friend Overrides Function GetNextImageState() As BitmapSource
            'BGRA32
            Dim blockNumber = arrayLength \ Config.ModuleConfig.BytePerPixel
            Dim threadMaximite As Integer = (blockNumber \ 2) * Config.ModuleConfig.BytePerPixel - 1
            Dim thread1 As New System.Threading.Thread(Sub()
                                                           Dim i = 3
                                                           While i < threadMaximite
                                                               If pixelArray(i) - opacityPerFrame > -1 Then
                                                                   pixelArray(i) -= opacityPerFrame
                                                               Else
                                                                   pixelArray(i) = 0
                                                               End If
                                                               i += 4
                                                           End While
                                                       End Sub)
            Dim thread2 As New System.Threading.Thread(Sub()
                                                           Dim i = threadMaximite + 4
                                                           While i < arrayLength
                                                               If pixelArray(i) - opacityPerFrame > -1 Then
                                                                   pixelArray(i) -= opacityPerFrame
                                                               Else
                                                                   pixelArray(i) = 0
                                                               End If
                                                               i += 4
                                                           End While
                                                       End Sub)
            thread1.IsBackground = True
            thread2.IsBackground = True
            thread1.Start()
            thread2.Start()
            thread1.Join()
            thread2.Join()
            If pixelArray(3) = 0 Then complete = True
            Return WindowAPI.GetDispatcher.Invoke(Function() ImageConfig.ConvertToImage(Width, Height, pixelArray))
        End Function

    End Class

    Public Class LtREffect : Inherits BaseEffect
        Private showLinePerFrame As Integer
        Private lineNow As Integer

        Public Sub New(fileName As String, duration As Integer)
            MyBase.New(fileName, duration)
            showLinePerFrame = Width / duration
            If showLinePerFrame = 0 Then showLinePerFrame = 1
            Dim i = 3
            While i < pixelArray.Length
                pixelArray(i) = 255
                i += 4
            End While
        End Sub

        Protected Friend Overrides Function GetNextImageState() As Windows.Media.Imaging.BitmapSource

        End Function

    End Class

End Namespace