Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 中心叠加缩放
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class MagnifyEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(MagnifyEffect), 0)
    Public Shared ReadOnly CenterPointProperty As DependencyProperty = DependencyProperty.Register("CenterPoint", GetType(Point), GetType(MagnifyEffect), New UIPropertyMetadata(New Point(0.5, 0.5), PixelShaderConstantCallback(0)))
    Public Shared ReadOnly RadiusProperty As DependencyProperty = DependencyProperty.Register("Radius", GetType(Double), GetType(MagnifyEffect), New UIPropertyMetadata(0.25, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly MagnificationAmountProperty As DependencyProperty = DependencyProperty.Register("MagnificationAmount", GetType(Double), GetType(MagnifyEffect), New UIPropertyMetadata(2.0, PixelShaderConstantCallback(2)))
    Public Shared ReadOnly AspectRatioProperty As DependencyProperty = DependencyProperty.Register("AspectRatio", GetType(Double), GetType(MagnifyEffect), New UIPropertyMetadata(1.5, PixelShaderConstantCallback(4)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack2;component/PixelShader/MagnifyEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(CenterPointProperty)
        UpdateShaderValue(RadiusProperty)
        UpdateShaderValue(MagnificationAmountProperty)
        UpdateShaderValue(AspectRatioProperty)
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
    ''' 获取或设置缩放中心坐标[X: 0-1, Y: 0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CenterPoint() As Point
        Get
            Return CType(GetValue(CenterPointProperty), Point)
        End Get
        Set(value As Point)
            SetValue(CenterPointProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置缩放半径[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Radius() As Double
        Get
            Return CType(GetValue(RadiusProperty), Double)
        End Get
        Set(value As Double)
            SetValue(RadiusProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置缩放比例[+1-5+]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MagnificationAmount() As Double
        Get
            Return CType(GetValue(MagnificationAmountProperty), Double)
        End Get
        Set(value As Double)
            SetValue(MagnificationAmountProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置缩放半轴比例[+0.5-10]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AspectRatio() As Double
        Get
            Return CType(GetValue(AspectRatioProperty), Double)
        End Get
        Set(value As Double)
            SetValue(AspectRatioProperty, value)
        End Set
    End Property
End Class