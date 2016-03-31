Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 泛光
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class BloomEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(BloomEffect), 0)
    Public Shared ReadOnly BloomIntensityProperty As DependencyProperty = DependencyProperty.Register("BloomIntensity", GetType(Double), GetType(BloomEffect), New UIPropertyMetadata(1.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly BaseIntensityProperty As DependencyProperty = DependencyProperty.Register("BaseIntensity", GetType(Double), GetType(BloomEffect), New UIPropertyMetadata(0.5, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly BloomSaturationProperty As DependencyProperty = DependencyProperty.Register("BloomSaturation", GetType(Double), GetType(BloomEffect), New UIPropertyMetadata(1.0, PixelShaderConstantCallback(2)))
    Public Shared ReadOnly BaseSaturationProperty As DependencyProperty = DependencyProperty.Register("BaseSaturation", GetType(Double), GetType(BloomEffect), New UIPropertyMetadata(0.5, PixelShaderConstantCallback(3)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/BloomEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(BloomIntensityProperty)
        UpdateShaderValue(BaseIntensityProperty)
        UpdateShaderValue(BloomSaturationProperty)
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
    ''' 获取或设置泛光强度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BloomIntensity() As Double
        Get
            Return CType(GetValue(BloomIntensityProperty), Double)
        End Get
        Set(value As Double)
            SetValue(BloomIntensityProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置基础光强[0-1]
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
    ''' 获取或设置泛光饱和度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BloomSaturation() As Double
        Get
            Return CType(GetValue(BloomSaturationProperty), Double)
        End Get
        Set(value As Double)
            SetValue(BloomSaturationProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置基础饱和度[0-1]
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