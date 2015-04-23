Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 胶片效果
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class OldMovieEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(OldMovieEffect), 0)
    Public Shared ReadOnly ScratchAmountProperty As DependencyProperty = DependencyProperty.Register("ScratchAmount", GetType(Double), GetType(OldMovieEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly NoiseAmountProperty As DependencyProperty = DependencyProperty.Register("NoiseAmount", GetType(Double), GetType(OldMovieEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly RandomCoord1Property As DependencyProperty = DependencyProperty.Register("RandomCoord1", GetType(Point), GetType(OldMovieEffect), New UIPropertyMetadata(New Point(0.0, 0.0), PixelShaderConstantCallback(2)))
    Public Shared ReadOnly RandomCoord2Property As DependencyProperty = DependencyProperty.Register("RandomCoord2", GetType(Point), GetType(OldMovieEffect), New UIPropertyMetadata(New Point(0.0, 0.0), PixelShaderConstantCallback(3)))
    Public Shared ReadOnly FrameProperty As DependencyProperty = DependencyProperty.Register("Frame", GetType(Double), GetType(OldMovieEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(4)))
    Public Shared ReadOnly NoiseSamplerProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("NoiseSampler", GetType(OldMovieEffect), 1)

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/OldMovieEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(ScratchAmountProperty)
        UpdateShaderValue(NoiseAmountProperty)
        UpdateShaderValue(RandomCoord1Property)
        UpdateShaderValue(RandomCoord2Property)
        UpdateShaderValue(FrameProperty)
        UpdateShaderValue(NoiseSamplerProperty)
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
    ''' 获取或设置刮擦数量[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ScratchAmount() As Double
        Get
            Return CType(GetValue(ScratchAmountProperty), Double)
        End Get
        Set(value As Double)
            SetValue(ScratchAmountProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置噪点数量[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NoiseAmount() As Double
        Get
            Return CType(GetValue(NoiseAmountProperty), Double)
        End Get
        Set(value As Double)
            SetValue(NoiseAmountProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置随机噪点位置1[X: 0-1, Y: 0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RandomCoord1() As Point
        Get
            Return CType(GetValue(RandomCoord1Property), Point)
        End Get
        Set(value As Point)
            SetValue(RandomCoord1Property, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置随机噪点位置2[X: 0-1, Y: 0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RandomCoord2() As Point
        Get
            Return CType(GetValue(RandomCoord2Property), Point)
        End Get
        Set(value As Point)
            SetValue(RandomCoord2Property, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置帧进度[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Frame() As Double
        Get
            Return CType(GetValue(FrameProperty), Double)
        End Get
        Set(value As Double)
            SetValue(FrameProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置噪点样本
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NoiseSampler() As Brush
        Get
            Return CType(GetValue(NoiseSamplerProperty), Brush)
        End Get
        Set(value As Brush)
            SetValue(NoiseSamplerProperty, value)
        End Set
    End Property
End Class