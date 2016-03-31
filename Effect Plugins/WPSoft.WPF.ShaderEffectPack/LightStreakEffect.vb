Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 光线追踪
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class LightStreakEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(LightStreakEffect), 0)
    Public Shared ReadOnly BrightThresholdProperty As DependencyProperty = DependencyProperty.Register("BrightThreshold", GetType(Double), GetType(LightStreakEffect), New UIPropertyMetadata(0.5, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly ScaleProperty As DependencyProperty = DependencyProperty.Register("Scale", GetType(Double), GetType(LightStreakEffect), New UIPropertyMetadata(0.5, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly AttenuationProperty As DependencyProperty = DependencyProperty.Register("Attenuation", GetType(Double), GetType(LightStreakEffect), New UIPropertyMetadata(0.25, PixelShaderConstantCallback(2)))
    Public Shared ReadOnly DirectionProperty As DependencyProperty = DependencyProperty.Register("Direction", GetType(Vector), GetType(LightStreakEffect), New UIPropertyMetadata(New Vector(0.5, 1.0), PixelShaderConstantCallback(3)))
    Public Shared ReadOnly InputSizeProperty As DependencyProperty = DependencyProperty.Register("InputSize", GetType(Size), GetType(LightStreakEffect), New UIPropertyMetadata(New Size(800.0, 600.0), PixelShaderConstantCallback(4)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/LightStreakEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(BrightThresholdProperty)
        UpdateShaderValue(ScaleProperty)
        UpdateShaderValue(AttenuationProperty)
        UpdateShaderValue(DirectionProperty)
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
    ''' 获取或设置追踪阈值[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BrightThreshold() As Double
        Get
            Return CType(GetValue(BrightThresholdProperty), Double)
        End Get
        Set(value As Double)
            SetValue(BrightThresholdProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置高光幅度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Scale() As Double
        Get
            Return CType(GetValue(ScaleProperty), Double)
        End Get
        Set(value As Double)
            SetValue(ScaleProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置衰减幅度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Attenuation() As Double
        Get
            Return CType(GetValue(AttenuationProperty), Double)
        End Get
        Set(value As Double)
            SetValue(AttenuationProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置光照方向[X: -1-1, Y: -1-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Direction() As Vector
        Get
            Return CType(GetValue(DirectionProperty), Vector)
        End Get
        Set(value As Vector)
            SetValue(DirectionProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置输入大小[X: 1-1000, Y: 1-1000]
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