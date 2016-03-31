Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 像素化
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class PixelateEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(PixelateEffect), 0)
    Public Shared ReadOnly PixelCountsProperty As DependencyProperty = DependencyProperty.Register("PixelCounts", GetType(Size), GetType(PixelateEffect), New UIPropertyMetadata(New Size(60.0, 40.0), PixelShaderConstantCallback(0)))
    Public Shared ReadOnly BrickOffsetProperty As DependencyProperty = DependencyProperty.Register("BrickOffset", GetType(Double), GetType(PixelateEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(1)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/PixelateEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(PixelCountsProperty)
        UpdateShaderValue(BrickOffsetProperty)
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
    ''' 获取或设置像素块数量[X: 20-100, Y: 20-100]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PixelCounts() As Size
        Get
            Return CType(GetValue(PixelCountsProperty), Size)
        End Get
        Set(value As Size)
            SetValue(PixelCountsProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置位块偏移[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BrickOffset() As Double
        Get
            Return CType(GetValue(BrickOffsetProperty), Double)
        End Get
        Set(value As Double)
            SetValue(BrickOffsetProperty, value)
        End Set
    End Property
End Class