Imports System.Windows.Media.Imaging
Imports System.Windows.Media
Imports WADV.AppCore.API
Imports WADV.ImageModule.Config

Namespace ImageCore

    ''' <summary>
    ''' 一个带像素信息导出功能的位图
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BitmapWithPixel

        Private bitmapContent As FormatConvertedBitmap
        Private bitmapPixelContent(,) As Color
        Private bitmapPixel() As Byte

        ''' <summary>
        ''' 获取图像的像素宽度
        ''' </summary>
        Public ReadOnly Property Width As Integer
            Get
                Return bitmapContent.PixelWidth
            End Get
        End Property

        ''' <summary>
        ''' 获取图像的像素高度
        ''' </summary>
        Public ReadOnly Property Height As Integer
            Get
                Return bitmapContent.PixelHeight
            End Get
        End Property

        ''' <summary>
        ''' 获取图像的像素数组
        ''' </summary>
        Public ReadOnly Property PixelArray As Color(,)
            Get
                If bitmapPixelContent Is Nothing Then
                    Dim data = PixelInfomation
                    ReDim bitmapPixelContent(Height - 1, Width - 1)
                    For i = 0 To Height - 1
                        For j = 0 To Width - 1
                            Dim startPoint = (i * Width + j) * ModuleConfig.BytePerPixel
                            bitmapPixelContent(i, j) = Color.FromArgb(data(startPoint + 3), data(startPoint + 2), data(startPoint + 1), data(startPoint))
                        Next
                    Next
                End If
                Return bitmapPixelContent
            End Get
        End Property

        ''' <summary>
        ''' 获取图像的像素信息
        ''' </summary>
        Public ReadOnly Property PixelInfomation As Byte()
            Get
                If bitmapPixel Is Nothing Then
                    ReDim bitmapPixel(Width * Height * ModuleConfig.BytePerPixel - 1)
                    bitmapContent.CopyPixels(bitmapPixel, Width * ModuleConfig.BytePerPixel, 0)
                End If
                Return bitmapPixel
            End Get
        End Property

        ''' <summary>
        ''' 获取图像原始数据
        ''' </summary>
        Public ReadOnly Property ImageContent As BitmapSource
            Get
                Return bitmapContent.Source
            End Get
        End Property

        ''' <summary>
        ''' 获得一个带像素信息导出功能的位图
        ''' </summary>
        ''' <param name="fileName">图像文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Public Sub New(fileName As String)
            Me.New(New BitmapImage(New Uri(URLAPI.CombineURL(URLAPI.GetResourceURL, fileName))))
        End Sub

        ''' <summary>
        ''' 获得一个带像素信息导出功能的位图
        ''' </summary>
        ''' <param name="bitmapImageContent">图像的BitmapImage对象</param>
        ''' <remarks></remarks>
        Public Sub New(bitmapImageContent As BitmapSource)
            bitmapContent = New FormatConvertedBitmap
            bitmapContent.BeginInit()
            bitmapContent.DestinationPalette = BitmapPalettes.WebPaletteTransparent
            bitmapContent.DestinationFormat = PixelFormats.Bgra32
            bitmapContent.Source = bitmapImageContent
            bitmapContent.EndInit()
        End Sub

        Public Shared Function ConvertToImage(width As Integer, height As Integer, pixel() As Byte) As BitmapSource
            Return BitmapFrame.Create(width, height, ModuleConfig.DPI, ModuleConfig.DPI, PixelFormats.Bgra32, Imaging.BitmapPalettes.WebPaletteTransparent, pixel, width * ModuleConfig.BytePerPixel)
        End Function

    End Class

End Namespace
