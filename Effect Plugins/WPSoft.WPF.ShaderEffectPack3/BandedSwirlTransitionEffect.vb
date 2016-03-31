Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 旋转扭曲过渡
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class BandedSwirlTransitionEffect : Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(BandedSwirlTransitionEffect), 0)
    Public Shared ReadOnly Texture2Property As DependencyProperty = RegisterPixelShaderSamplerProperty("Texture2", GetType(BandedSwirlTransitionEffect), 1)
    Public Shared ReadOnly ProgressProperty As DependencyProperty = DependencyProperty.Register("Progress", GetType(Double), GetType(BandedSwirlTransitionEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly TwistAmountProperty As DependencyProperty = DependencyProperty.Register("TwistAmount", GetType(Double), GetType(BandedSwirlTransitionEffect), New UIPropertyMetadata(1.0, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly FrequencyProperty As DependencyProperty = DependencyProperty.Register("Frequency", GetType(Double), GetType(BandedSwirlTransitionEffect), New UIPropertyMetadata(20.0, PixelShaderConstantCallback(2)))
    Public Shared ReadOnly CenterProperty As DependencyProperty = DependencyProperty.Register("Center", GetType(Point), GetType(BandedSwirlTransitionEffect), New UIPropertyMetadata(New Point(0.5, 0.5), PixelShaderConstantCallback(3)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack3;component/PixelShader/BandedSwirlTransitionEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(Texture2Property)
        UpdateShaderValue(ProgressProperty)
        UpdateShaderValue(TwistAmountProperty)
        UpdateShaderValue(FrequencyProperty)
        UpdateShaderValue(CenterProperty)
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
    ''' 获取或设置扭曲程度[0-10+]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TwistAmount() As Double
        Get
            Return CType(GetValue(TwistAmountProperty), Double)
        End Get
        Set(value As Double)
            SetValue(TwistAmountProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置旋转程度[0-100]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Frequency() As Double
        Get
            Return CType(GetValue(FrequencyProperty), Double)
        End Get
        Set(value As Double)
            SetValue(FrequencyProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置变换中心[X: 0-1, Y: 0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Center() As Point
        Get
            Return CType(GetValue(CenterProperty), Point)
        End Get
        Set(value As Point)
            SetValue(CenterProperty, value)
        End Set
    End Property
End Class