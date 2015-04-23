Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 铅笔速写
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class SketchPencilStrokeEffect : Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(SketchPencilStrokeEffect), 0)
    Public Shared ReadOnly BrushSizeProperty As DependencyProperty = DependencyProperty.Register("BrushSize", GetType(Double), GetType(SketchPencilStrokeEffect), New UIPropertyMetadata(0.005, PixelShaderConstantCallback(0)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/SketchPencilStrokeEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(BrushSizeProperty)
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
    ''' 获取或设置画笔大小[0.001-0.019]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BrushSize() As Double
        Get
            Return CType(GetValue(BrushSizeProperty), Double)
        End Get
        Set(value As Double)
            SetValue(BrushSizeProperty, value)
        End Set
    End Property
End Class