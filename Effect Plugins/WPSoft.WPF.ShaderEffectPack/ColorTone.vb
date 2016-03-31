Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 专色提取
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class ColorToneEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(ColorToneEffect), 0)
    Public Shared ReadOnly DesaturationProperty As DependencyProperty = DependencyProperty.Register("Desaturation", GetType(Double), GetType(ColorToneEffect), New UIPropertyMetadata(0.5, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly TonedProperty As DependencyProperty = DependencyProperty.Register("Toned", GetType(Double), GetType(ColorToneEffect), New UIPropertyMetadata(0.5, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly LightColorProperty As DependencyProperty = DependencyProperty.Register("LightColor", GetType(Color), GetType(ColorToneEffect), New UIPropertyMetadata(Color.FromArgb(255, 255, 255, 0), PixelShaderConstantCallback(2)))
    Public Shared ReadOnly DarkColorProperty As DependencyProperty = DependencyProperty.Register("DarkColor", GetType(Color), GetType(ColorToneEffect), New UIPropertyMetadata(Color.FromArgb(255, 0, 0, 128), PixelShaderConstantCallback(3)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/ColorToneEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(DesaturationProperty)
        UpdateShaderValue(TonedProperty)
        UpdateShaderValue(LightColorProperty)
        UpdateShaderValue(DarkColorProperty)
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
    ''' 获取或设置冲淡程度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Desaturation() As Double
        Get
            Return CType(GetValue(DesaturationProperty), Double)
        End Get
        Set(value As Double)
            SetValue(DesaturationProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置暗色比例[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Toned() As Double
        Get
            Return CType(GetValue(TonedProperty), Double)
        End Get
        Set(value As Double)
            SetValue(TonedProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置亮区颜色
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LightColor() As Color
        Get
            Return CType(GetValue(LightColorProperty), Color)
        End Get
        Set(value As Color)
            SetValue(LightColorProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置暗区颜色
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DarkColor() As Color
        Get
            Return CType(GetValue(DarkColorProperty), Color)
        End Get
        Set(value As Color)
            SetValue(DarkColorProperty, value)
        End Set
    End Property

End Class