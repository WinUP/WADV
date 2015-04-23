Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 条带绘制
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class BandsEffect : Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(BandsEffect), 0)
    Public Shared ReadOnly BandDensityProperty As DependencyProperty = DependencyProperty.Register("BandDensity", GetType(Double), GetType(BandsEffect), New UIPropertyMetadata(65.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly BandIntensityProperty As DependencyProperty = DependencyProperty.Register("BandIntensity", GetType(Double), GetType(BandsEffect), New UIPropertyMetadata(0.056, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly IsRightSideBandProperty As DependencyProperty = DependencyProperty.Register("IsRightSideBand", GetType(Double), GetType(BandsEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(2)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack2;component/PixelShader/BandsEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(BandDensityProperty)
        UpdateShaderValue(BandIntensityProperty)
        UpdateShaderValue(IsRightSideBandProperty)
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
    ''' 获取或设置条带密度[0-150]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BandDensity() As Double
        Get
            Return CType(GetValue(BandDensityProperty), Double)
        End Get
        Set(value As Double)
            SetValue(BandDensityProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置条带宽度[0-0.5]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BandIntensity() As Double
        Get
            Return CType(GetValue(BandIntensityProperty), Double)
        End Get
        Set(value As Double)
            SetValue(BandIntensityProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置条带方向[0|1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsRightSideBand() As Double
        Get
            Return CType(GetValue(IsRightSideBandProperty), Double)
        End Get
        Set(value As Double)
            SetValue(IsRightSideBandProperty, value)
        End Set
    End Property
End Class