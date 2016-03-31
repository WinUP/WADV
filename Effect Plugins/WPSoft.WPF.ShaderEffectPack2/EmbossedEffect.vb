Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 浮雕化
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class EmbossedEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(EmbossedEffect), 0)
    Public Shared ReadOnly EmbossedAmountProperty As DependencyProperty = DependencyProperty.Register("EmbossedAmount", GetType(Double), GetType(EmbossedEffect), New UIPropertyMetadata(0.5, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly WidthProperty As DependencyProperty = DependencyProperty.Register("Width", GetType(Double), GetType(EmbossedEffect), New UIPropertyMetadata(0.003, PixelShaderConstantCallback(1)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack2;component/PixelShader/EmbossedEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(EmbossedAmountProperty)
        UpdateShaderValue(WidthProperty)
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
    ''' 获取或设置雕刻深度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EmbossedAmount() As Double
        Get
            Return CType(GetValue(EmbossedAmountProperty), Double)
        End Get
        Set(value As Double)
            SetValue(EmbossedAmountProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置雕刻宽度[0-0.01]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Width() As Double
        Get
            Return CType(GetValue(WidthProperty), Double)
        End Get
        Set(value As Double)
            SetValue(WidthProperty, value)
        End Set
    End Property
End Class