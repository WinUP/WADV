Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 玻璃贴片
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class GlassTilesEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(GlassTilesEffect), 0)
    Public Shared ReadOnly TilesProperty As DependencyProperty = DependencyProperty.Register("Tiles", GetType(Double), GetType(GlassTilesEffect), New UIPropertyMetadata(5.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly BevelWidthProperty As DependencyProperty = DependencyProperty.Register("BevelWidth", GetType(Double), GetType(GlassTilesEffect), New UIPropertyMetadata(1.0, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly OffsetProperty As DependencyProperty = DependencyProperty.Register("Offset", GetType(Double), GetType(GlassTilesEffect), New UIPropertyMetadata(1.0, PixelShaderConstantCallback(3)))
    Public Shared ReadOnly GroutColorProperty As DependencyProperty = DependencyProperty.Register("GroutColor", GetType(Color), GetType(GlassTilesEffect), New UIPropertyMetadata(Color.FromArgb(255, 0, 0, 0), PixelShaderConstantCallback(2)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack2;component/PixelShader/GlassTilesEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(TilesProperty)
        UpdateShaderValue(BevelWidthProperty)
        UpdateShaderValue(OffsetProperty)
        UpdateShaderValue(GroutColorProperty)
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
    ''' 获取或设置贴片大小[0-20+]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Tiles() As Double
        Get
            Return CType(GetValue(TilesProperty), Double)
        End Get
        Set(value As Double)
            SetValue(TilesProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置边缘宽度[0-10+]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BevelWidth() As Double
        Get
            Return CType(GetValue(BevelWidthProperty), Double)
        End Get
        Set(value As Double)
            SetValue(BevelWidthProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置贴片偏移[0-3+]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Offset() As Double
        Get
            Return CType(GetValue(OffsetProperty), Double)
        End Get
        Set(value As Double)
            SetValue(OffsetProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置边缘颜色
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GroutColor() As Color
        Get
            Return CType(GetValue(GroutColorProperty), Color)
        End Get
        Set(value As Color)
            SetValue(GroutColorProperty, value)
        End Set
    End Property
End Class