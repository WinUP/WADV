Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 像素化过渡
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class PixelateTransitionEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(PixelateTransitionEffect), 0)
    Public Shared ReadOnly ProgressProperty As DependencyProperty = DependencyProperty.Register("Progress", GetType(Double), GetType(PixelateTransitionEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly Texture2Property As DependencyProperty = RegisterPixelShaderSamplerProperty("Texture2", GetType(PixelateTransitionEffect), 1)

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack3;component/PixelShader/PixelateTransitionEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(ProgressProperty)
        UpdateShaderValue(Texture2Property)
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
End Class
