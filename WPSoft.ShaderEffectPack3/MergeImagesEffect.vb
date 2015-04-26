Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 图像混合
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class MergeImagesEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(MergeImagesEffect), 0)
    Public Shared ReadOnly Texture2Property As DependencyProperty = RegisterPixelShaderSamplerProperty("Texture2", GetType(MergeImagesEffect), 1)
    Public Shared ReadOnly RatioProperty As DependencyProperty = DependencyProperty.Register("Ratio", GetType(Double), GetType(MergeImagesEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack3;component/PixelShader/MergeImagesEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(Texture2Property)
        UpdateShaderValue(RatioProperty)
    End Sub

    ''' <summary>
    ''' 获取或设置混合后的图像
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
    ''' 获取或设置要混合前的图像
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
    ''' 获取或设置混合程度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Ratio() As Double
        Get
            Return CType(GetValue(RatioProperty), Double)
        End Get
        Set(value As Double)
            SetValue(RatioProperty, value)
        End Set
    End Property
End Class