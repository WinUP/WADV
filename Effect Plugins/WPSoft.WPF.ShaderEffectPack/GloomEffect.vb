Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 亮度降低
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class GloomEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(GloomEffect), 0)
    Public Shared ReadOnly GloomIntensityProperty As DependencyProperty = DependencyProperty.Register("GloomIntensity", GetType(Double), GetType(GloomEffect), New UIPropertyMetadata(1.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly BaseIntensityProperty As DependencyProperty = DependencyProperty.Register("BaseIntensity", GetType(Double), GetType(GloomEffect), New UIPropertyMetadata(0.5, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly GloomSaturationProperty As DependencyProperty = DependencyProperty.Register("GloomSaturation", GetType(Double), GetType(GloomEffect), New UIPropertyMetadata(0.2, PixelShaderConstantCallback(2)))
    Public Shared ReadOnly BaseSaturationProperty As DependencyProperty = DependencyProperty.Register("BaseSaturation", GetType(Double), GetType(GloomEffect), New UIPropertyMetadata(1.0, PixelShaderConstantCallback(3)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/GloomEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(GloomIntensityProperty)
        UpdateShaderValue(BaseIntensityProperty)
        UpdateShaderValue(GloomSaturationProperty)
        UpdateShaderValue(BaseSaturationProperty)
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
    ''' 获取或设置降亮强度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GloomIntensity() As Double
        Get
            Return CType(GetValue(GloomIntensityProperty), Double)
        End Get
        Set(value As Double)
            SetValue(GloomIntensityProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置基色强度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BaseIntensity() As Double
        Get
            Return CType(GetValue(BaseIntensityProperty), Double)
        End Get
        Set(value As Double)
            SetValue(BaseIntensityProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置降亮饱和度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GloomSaturation() As Double
        Get
            Return CType(GetValue(GloomSaturationProperty), Double)
        End Get
        Set(value As Double)
            SetValue(GloomSaturationProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置基色饱和度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BaseSaturation() As Double
        Get
            Return CType(GetValue(BaseSaturationProperty), Double)
        End Get
        Set(value As Double)
            SetValue(BaseSaturationProperty, value)
        End Set
    End Property

End Class