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

    Public Class TachieConfig
        Private Shared egaList As New Dictionary(Of Integer, Image)
        Private Shared egaID As Integer = 0

        Protected Friend Shared Function AddTachie(fileName As String, x As Double, y As Double, width As Double, height As Double, z As Integer) As Integer
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               Dim tmpImage As New Image
                                               tmpImage.BeginInit()
                                               tmpImage.Name = "TachieImage_" & egaID
                                               WindowAPI.GetWindow.RegisterName(tmpImage.Name, tmpImage)
                                               tmpImage.Height = height
                                               tmpImage.HorizontalAlignment = Windows.HorizontalAlignment.Left
                                               tmpImage.VerticalAlignment = Windows.VerticalAlignment.Top
                                               tmpImage.Margin = New Windows.Thickness(x, y, 0, 0)
                                               tmpImage.Visibility = Windows.Visibility.Collapsed
                                               tmpImage.Width = width
                                               tmpImage.Source = New BitmapImage(New Uri(PathAPI.GetPath(PathAPI.Resource, fileName)))
                                               tmpImage.SetValue(Grid.ZIndexProperty, z)
                                               tmpImage.EndInit()
                                               egaList.Add(egaID, tmpImage)
                                               WindowAPI.GetGrid.Children.Add(tmpImage)
                                           End Sub)
            egaID += 1
            Return egaID - 1
        End Function

        Protected Friend Shared Sub ChangeTachie(id As Integer, fileName As String, width As Double, height As Double)
            If Not egaList.ContainsKey(id) Then Exit Sub
            Dim tmpImage = egaList(id)
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               tmpImage.Source = New BitmapImage(New Uri(PathAPI.GetPath(PathAPI.Resource, fileName)))
                                               If width > -1 Then tmpImage.Width = width
                                               If height > -1 Then tmpImage.Height = height
                                           End Sub)
        End Sub

        Protected Friend Shared Sub DeleteTachie(id As Integer)
            If Not egaList.ContainsKey(id) Then Exit Sub
            Dim tmpImage = egaList(id)
            WindowAPI.GetDispatcher.Invoke(Sub()
                                               WindowAPI.GetGrid.Children.Remove(tmpImage)
                                               WindowAPI.GetWindow.UnregisterName(tmpImage.Name)
                                           End Sub)
            egaList.Remove(id)
            GC.Collect()
        End Sub

        Protected Friend Shared Function GetTachie(id As Integer) As Image
            If Not egaList.ContainsKey(id) Then Return Nothing
            Return egaList(id)
        End Function


    End Class

End Namespace
