Imports System.Windows.Media.Imaging
Imports System.Windows.Media

Namespace ImageCore

    ''' <summary>
    ''' 一个带像素矩阵的位图
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BitmapWithPixel

        Private bitmapContent As FormatConvertedBitmap
        Private bitmapPixelContent(,) As Color

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
        ''' 获取图像的像素矩阵
        ''' </summary>
        Public ReadOnly Property PixelArray As Color(,)
            Get
                If bitmapPixelContent.Length < 1 Then
                    Dim data(Height * Width * Config.BytePerPixel) As Byte
                    bitmapContent.CopyPixels(data, Width * Config.BytePerPixel, 0)
                    ReDim bitmapPixelContent(Height, Width)
                    For i = 0 To Height - 1
                        For j = 0 To Width - 1
                            Dim startPoint = (i * Width + j) * Config.BytePerPixel
                            bitmapPixelContent(i, j) = Color.FromArgb(data(startPoint + 3), data(startPoint + 2), data(startPoint + 1), data(startPoint))
                        Next
                    Next
                End If
                Return bitmapPixelContent
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
        ''' 获得一个带像素矩阵的位图
        ''' </summary>
        ''' <param name="fileName">图像文件路径(Resource目录下)</param>
        ''' <remarks></remarks>
        Public Sub New(fileName As String)
            Me.New(New BitmapImage(New Uri(AppCore.API.URLAPI.CombineURL(AppCore.API.URLAPI.GetResourceURL, fileName))))
        End Sub

        ''' <summary>
        ''' 获得一个带像素矩阵的位图
        ''' </summary>
        ''' <param name="bitmapImageContent">图像的BitmapImage对象</param>
        ''' <remarks></remarks>
        Public Sub New(bitmapImageContent As BitmapSource)
            bitmapContent = New FormatConvertedBitmap()
            bitmapContent.BeginInit()
            bitmapContent.DestinationPalette = BitmapPalettes.WebPalette
            bitmapContent.DestinationFormat = PixelFormats.Bgra32
            bitmapContent.Source = bitmapImageContent
            bitmapContent.EndInit()
        End Sub

        Public Shared Function ConvertToImage(pixel(,) As Color) As BitmapSource


        End Function

    End Class

End Namespace
