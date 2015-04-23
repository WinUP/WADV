Imports System
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 径向模糊
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class DirectionalBlurEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(DirectionalBlurEffect), 0)
    Public Shared ReadOnly AngleProperty As DependencyProperty = DependencyProperty.Register("Angle", GetType(Double), GetType(DirectionalBlurEffect), New UIPropertyMetadata(0.0R, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly BlurAmountProperty As DependencyProperty = DependencyProperty.Register("BlurAmount", GetType(Double), GetType(DirectionalBlurEffect), New UIPropertyMetadata(0.003, PixelShaderConstantCallback(1)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/DirectionalBlurEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(AngleProperty)
        UpdateShaderValue(BlurAmountProperty)
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
    ''' 获取或设置模糊角度[0-360]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Angle() As Double
        Get
            Return CType(Me.GetValue(AngleProperty), Double)
        End Get
        Set(value As Double)
            Me.SetValue(AngleProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置模糊半径[0-0.01]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BlurAmount() As Double
        Get
            Return CType(Me.GetValue(BlurAmountProperty), Double)
        End Get
        Set(value As Double)
            Me.SetValue(BlurAmountProperty, value)
        End Set
    End Property
End Class