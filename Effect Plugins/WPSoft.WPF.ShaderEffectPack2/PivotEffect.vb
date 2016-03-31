Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 中轴旋转
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class PivotEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(PivotEffect), 0)
    Public Shared ReadOnly PivotAmountProperty As DependencyProperty = DependencyProperty.Register("PivotAmount", GetType(Double), GetType(PivotEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly EdgeProperty As DependencyProperty = DependencyProperty.Register("Edge", GetType(Double), GetType(PivotEffect), New UIPropertyMetadata(0.5, PixelShaderConstantCallback(1)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack2;component/PixelShader/PivotEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(PivotAmountProperty)
        UpdateShaderValue(EdgeProperty)
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
    ''' 获取或设置中轴旋转量[0-0.5]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PivotAmount() As Double
        Get
            Return CType(GetValue(PivotAmountProperty), Double)
        End Get
        Set(value As Double)
            SetValue(PivotAmountProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置旋转边[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Edge() As Double
        Get
            Return CType(GetValue(EdgeProperty), Double)
        End Get
        Set(value As Double)
            SetValue(EdgeProperty, value)
        End Set
    End Property
End Class