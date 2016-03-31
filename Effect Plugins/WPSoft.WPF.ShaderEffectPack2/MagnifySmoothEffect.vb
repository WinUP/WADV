Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 中心凸出缩放
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class MagnifySmoothEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(MagnifySmoothEffect), 0)
    Public Shared ReadOnly CenterPointProperty As DependencyProperty = DependencyProperty.Register("CenterPoint", GetType(Point), GetType(MagnifySmoothEffect), New UIPropertyMetadata(New Point(0.5, 0.5), PixelShaderConstantCallback(0)))
    Public Shared ReadOnly InnerRadiusProperty As DependencyProperty = DependencyProperty.Register("InnerRadius", GetType(Double), GetType(MagnifySmoothEffect), New UIPropertyMetadata(0.2, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly OuterRadiusProperty As DependencyProperty = DependencyProperty.Register("OuterRadius", GetType(Double), GetType(MagnifySmoothEffect), New UIPropertyMetadata(0.4, PixelShaderConstantCallback(2)))
    Public Shared ReadOnly MagnificationAmountProperty As DependencyProperty = DependencyProperty.Register("MagnificationAmount", GetType(Double), GetType(MagnifySmoothEffect), New UIPropertyMetadata(2.0, PixelShaderConstantCallback(3)))
    Public Shared ReadOnly AspectRatioProperty As DependencyProperty = DependencyProperty.Register("AspectRatio", GetType(Double), GetType(MagnifySmoothEffect), New UIPropertyMetadata(1.5, PixelShaderConstantCallback(4)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack2;component/PixelShader/MagnifySmoothEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(CenterPointProperty)
        UpdateShaderValue(InnerRadiusProperty)
        UpdateShaderValue(OuterRadiusProperty)
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
    ''' 获取或设置内缩放半径[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InnerRadius() As Double
        Get
            Return CType(GetValue(InnerRadiusProperty), Double)
        End Get
        Set(value As Double)
            SetValue(InnerRadiusProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置外缩放半径[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OuterRadius() As Double
        Get
            Return CType(GetValue(OuterRadiusProperty), Double)
        End Get
        Set(value As Double)
            SetValue(OuterRadiusProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置缩放大小[1-5+]
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