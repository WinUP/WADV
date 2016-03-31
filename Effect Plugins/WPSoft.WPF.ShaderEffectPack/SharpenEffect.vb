Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 锐化
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class SharpenEffect : Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(SharpenEffect), 0)
    Public Shared ReadOnly AmountProperty As DependencyProperty = DependencyProperty.Register("Amount", GetType(Double), GetType(SharpenEffect), New UIPropertyMetadata(1.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly InputSizeProperty As DependencyProperty = DependencyProperty.Register("InputSize", GetType(Size), GetType(SharpenEffect), New UIPropertyMetadata(New Size(800.0, 600.0), PixelShaderConstantCallback(1)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/SharpenEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(AmountProperty)
        UpdateShaderValue(InputSizeProperty)
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
    ''' 获取或设置锐化比例[0-2]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Amount() As Double
        Get
            Return CType(GetValue(AmountProperty), Double)
        End Get
        Set(value As Double)
            SetValue(AmountProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置锐化大小[X: 1-1000, Y: 1-1000]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InputSize() As Size
        Get
            Return CType(GetValue(InputSizeProperty), Size)
        End Get
        Set(value As Size)
            SetValue(InputSizeProperty, value)
        End Set
    End Property

End Class