Imports System.Xml
Imports System.Windows.Controls
Imports System.Windows.Media.Imaging
Imports System.Windows.Media

Namespace Config

    ''' <summary>
    ''' 模块设置类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ModuleConfig

        Private Shared _bytePerPixel As Integer
        Private Shared _dpi As Integer

        Protected Friend Shared Property BytePerPixel As Integer
            Get
                Return _bytePerPixel
            End Get
            Set(value As Integer)
                _bytePerPixel = value
                WriteConfig()
            End Set
        End Property

        Protected Friend Shared Property DPI As Integer
            Get
                Return _dpi
            End Get
            Set(value As Integer)
                _dpi = value
                WriteConfig()
            End Set
        End Property


        ''' <summary>
        ''' 读取配置文件
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub ReadConfigFile()
            Dim configFile As New XmlDocument
            configFile.Load(PathAPI.GetPath(PathAPI.UserFile, "WADV.ImageModule.xml"))
            _bytePerPixel = CInt(configFile.SelectSingleNode("/config/bytePerPixel").InnerXml)
            _dpi = CInt(configFile.SelectSingleNode("/config/dpi").InnerXml)
        End Sub

        ''' <summary>
        ''' 保存配置
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared Sub WriteConfig()
            Dim configFile As New XmlDocument
            configFile.Load(PathAPI.GetPath(PathAPI.UserFile, "WADV.ImageModule.xml"))
            configFile.SelectSingleNode("/config/bytePerPixel").InnerXml = BytePerPixel
            configFile.SelectSingleNode("/config/dpi").InnerXml = DPI
            configFile.Save(PathAPI.GetPath(PathAPI.UserFile, "WADV.ImageModule.xml"))
        End Sub

    End Class

    Public Class ImageConfig

        Public Shared Function ConvertToImage(width As Integer, height As Integer, pixel() As Byte) As BitmapSource
            Return BitmapSource.Create(width, height, ModuleConfig.DPI, ModuleConfig.DPI, PixelFormats.Bgra32, BitmapPalettes.WebPaletteTransparent, pixel, width * ModuleConfig.BytePerPixel)
        End Function

    End Class

End Namespace
