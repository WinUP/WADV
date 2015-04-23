Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 基于颜色的Alpha渐变
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class ColorKeyAlphaEffect: Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", GetType(ColorKeyAlphaEffect), 0)
    Public Shared ReadOnly ColorKeyProperty As DependencyProperty = DependencyProperty.Register("ColorKey", GetType(Color), GetType(ColorKeyAlphaEffect), New UIPropertyMetadata(Color.FromArgb(255, 0, 128, 0), PixelShaderConstantCallback(0)))
    Public Shared ReadOnly ToleranceProperty As DependencyProperty = DependencyProperty.Register("Tolerance", GetType(Double), GetType(ColorKeyAlphaEffect), New UIPropertyMetadata(CType(0.3R, Double), PixelShaderConstantCallback(1)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/ColorKeyAlphaEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(ColorKeyProperty)
        UpdateShaderValue(ToleranceProperty)
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
    ''' 获取或设置目标颜色
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ColorKey() As Color
        Get
            Return CType(GetValue(ColorKeyProperty), Color)
        End Get
        Set(value As Color)
            SetValue(ColorKeyProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置变换上限
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Tolerance() As Double
        Get
            Return CType(GetValue(ToleranceProperty), Double)
        End Get
        Set(value As Double)
            SetValue(ToleranceProperty, value)
        End Set
    End Property

End Class