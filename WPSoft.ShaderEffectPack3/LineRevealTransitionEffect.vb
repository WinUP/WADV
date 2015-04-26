Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 线性变换
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class LineRevealTransitionEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", GetType(LineRevealTransitionEffect), 0)
    Public Shared ReadOnly ProgressProperty As DependencyProperty = DependencyProperty.Register("Progress", GetType(Double), GetType(LineRevealTransitionEffect), New UIPropertyMetadata(CType(30.0R, Double), PixelShaderConstantCallback(0)))
    Public Shared ReadOnly LineOriginProperty As DependencyProperty = DependencyProperty.Register("LineOrigin", GetType(Point), GetType(LineRevealTransitionEffect), New UIPropertyMetadata(New Point(1.0R, 0.0R), PixelShaderConstantCallback(1)))
    Public Shared ReadOnly LineNormalProperty As DependencyProperty = DependencyProperty.Register("LineNormal", GetType(Point), GetType(LineRevealTransitionEffect), New UIPropertyMetadata(New Point(1.0R, 1.0R), PixelShaderConstantCallback(2)))
    Public Shared ReadOnly LineOffsetProperty As DependencyProperty = DependencyProperty.Register("LineOffset", GetType(Point), GetType(LineRevealTransitionEffect), New UIPropertyMetadata(New Point(1.0R, 1.0R), PixelShaderConstantCallback(3)))
    Public Shared ReadOnly FuzzyAmountProperty As DependencyProperty = DependencyProperty.Register("FuzzyAmount", GetType(Double), GetType(LineRevealTransitionEffect), New UIPropertyMetadata(CType(0.05R, Double), PixelShaderConstantCallback(4)))
    Public Shared ReadOnly Texture2Property As DependencyProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Texture2", GetType(LineRevealTransitionEffect), 1)

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack3;component/PixelShader/LineRevealTransitionEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(ProgressProperty)
        UpdateShaderValue(LineOriginProperty)
        UpdateShaderValue(LineNormalProperty)
        UpdateShaderValue(LineOffsetProperty)
        UpdateShaderValue(FuzzyAmountProperty)
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
    ''' 获取或设置变换起点[X: 0-1, Y: 0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LineOrigin() As Point
        Get
            Return CType(GetValue(LineOriginProperty), Point)
        End Get
        Set(value As Point)
            SetValue(LineOriginProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置变换线偏转值[X: 0-1, Y: 0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LineNormal() As Point
        Get
            Return CType(GetValue(LineNormalProperty), Point)
        End Get
        Set(value As Point)
            SetValue(LineNormalProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置变换终点[X: 0-1, Y: 0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LineOffset() As Point
        Get
            Return CType(GetValue(LineOffsetProperty), Point)
        End Get
        Set(value As Point)
            SetValue(LineOffsetProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置变换线羽化程度
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
