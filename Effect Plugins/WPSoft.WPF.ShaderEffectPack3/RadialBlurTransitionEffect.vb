Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 径向模糊过渡
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class RadialBlurTransitionEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(RadialBlurTransitionEffect), 0)
    Public Shared ReadOnly Texture2Property As DependencyProperty = RegisterPixelShaderSamplerProperty("Texture2", GetType(RadialBlurTransitionEffect), 1)
    Public Shared ReadOnly ProgressProperty As DependencyProperty = DependencyProperty.Register("Progress", GetType(Double), GetType(RadialBlurTransitionEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack3;component/PixelShader/RadialBlurTransitionEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(Texture2Property)
        UpdateShaderValue(ProgressProperty)
    End Sub

    ''' <summary>
    ''' 获取或设置变换后的图像
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
    ''' 获取或设置变换前的图像
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Texture2() As Brush
        Get
            Return CType(GetValue(Texture2Property), Brush)
        End Get
        Set(value As Brush)
            SetValue(Texture2Property, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置变换程度[0-100]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Progress() As Double
        Get
            Return CType(GetValue(ProgressProperty), Double)
        End Get
        Set(value As Double)
            SetValue(ProgressProperty, value)
        End Set
    End Property
End Class
