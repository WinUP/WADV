Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 缩放模糊
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class ZoomBlurEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(ZoomBlurEffect), 0)
    Public Shared ReadOnly CenterProperty As DependencyProperty = DependencyProperty.Register("Center", GetType(Point), GetType(ZoomBlurEffect), New UIPropertyMetadata(New Point(0.9, 0.6), PixelShaderConstantCallback(0)))
    Public Shared ReadOnly BlurAmountProperty As DependencyProperty = DependencyProperty.Register("BlurAmount", GetType(Double), GetType(ZoomBlurEffect), New UIPropertyMetadata(0.1, PixelShaderConstantCallback(1)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/ZoomBlurEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(CenterProperty)
        UpdateShaderValue(BlurAmountProperty)
    End Sub

    ''' <summary>
    ''' 获取或设置输入画刷
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Input() As Brush
        Get
            Return CType(GetValue(InputProperty), Brush)
        End Get
        Set(value As Brush)
            SetValue(InputProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置模糊中心[X: 0-1, Y: 0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Center() As Point
        Get
            Return CType(GetValue(CenterProperty), Point)
        End Get
        Set(value As Point)
            SetValue(CenterProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置模糊比例[0-0.2]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BlurAmount() As Double
        Get
            Return CType(GetValue(BlurAmountProperty), Double)
        End Get
        Set(value As Double)
            SetValue(BlurAmountProperty, value)
        End Set
    End Property

End Class