Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 对比调整
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class ContrastAdjustEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(ContrastAdjustEffect), 0)
    Public Shared ReadOnly BrightnessProperty As DependencyProperty = DependencyProperty.Register("Brightness", GetType(Double), GetType(ContrastAdjustEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly ContrastProperty As DependencyProperty = DependencyProperty.Register("Contrast", GetType(Double), GetType(ContrastAdjustEffect), New UIPropertyMetadata(1.5, PixelShaderConstantCallback(1)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/ContrastAdjustEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(BrightnessProperty)
        UpdateShaderValue(ContrastProperty)
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
    ''' 获取或设置亮度加成[-1-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Brightness() As Double
        Get
            Return CType(GetValue(BrightnessProperty), Double)
        End Get
        Set(value As Double)
            SetValue(BrightnessProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置对比度加成[0-2]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Contrast() As Double
        Get
            Return CType(GetValue(ContrastProperty), Double)
        End Get
        Set(value As Double)
            SetValue(ContrastProperty, value)
        End Set
    End Property

End Class