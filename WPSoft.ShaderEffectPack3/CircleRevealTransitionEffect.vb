Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 中心扩散变换[这个效果的变换程度同时依赖于Progress和CircleSize]
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class CircleRevealTransitionEffect : Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(CircleRevealTransitionEffect), 0)
    Public Shared ReadOnly ProgressProperty As DependencyProperty = DependencyProperty.Register("Progress", GetType(Double), GetType(CircleRevealTransitionEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly FuzzyAmountProperty As DependencyProperty = DependencyProperty.Register("FuzzyAmount", GetType(Double), GetType(CircleRevealTransitionEffect), New UIPropertyMetadata(0.01, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly CircleSizeProperty As DependencyProperty = DependencyProperty.Register("CircleSize", GetType(Double), GetType(CircleRevealTransitionEffect), New UIPropertyMetadata(0.5, PixelShaderConstantCallback(2)))
    Public Shared ReadOnly CenterPointProperty As DependencyProperty = DependencyProperty.Register("CenterPoint", GetType(Point), GetType(CircleRevealTransitionEffect), New UIPropertyMetadata(New Point(0.5, 0.5), PixelShaderConstantCallback(3)))
    Public Shared ReadOnly Texture2Property As DependencyProperty = RegisterPixelShaderSamplerProperty("Texture2", GetType(CircleRevealTransitionEffect), 1)

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack3;component/PixelShader/CircleRevealTransitionEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(ProgressProperty)
        UpdateShaderValue(FuzzyAmountProperty)
        UpdateShaderValue(CircleSizeProperty)
        UpdateShaderValue(CenterPointProperty)
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
    ''' 获取或设置边缘羽化程度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FuzzyAmount() As Double
        Get
            Return CType(GetValue(FuzzyAmountProperty), Double)
        End Get
        Set(value As Double)
            SetValue(FuzzyAmountProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置中心半径[0-2+]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CircleSize() As Double
        Get
            Return CType(GetValue(CircleSizeProperty), Double)
        End Get
        Set(value As Double)
            SetValue(CircleSizeProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置变换中心[X: 0-1, Y: 0-1]
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
