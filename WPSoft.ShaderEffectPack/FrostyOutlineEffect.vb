Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 边缘蚀刻
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class FrostyOutlineEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", GetType(FrostyOutlineEffect), 0)
    Public Shared ReadOnly WidthProperty As DependencyProperty = DependencyProperty.Register("Width", GetType(Double), GetType(FrostyOutlineEffect), New UIPropertyMetadata(500.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly HeightProperty As DependencyProperty = DependencyProperty.Register("Height", GetType(Double), GetType(FrostyOutlineEffect), New UIPropertyMetadata(300.0, PixelShaderConstantCallback(1)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack;component/PixelShader/FrostyOutlineEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(WidthProperty)
        UpdateShaderValue(HeightProperty)
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
    ''' 蚀刻宽度
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Width() As Double
        Get
            Return CType(GetValue(WidthProperty), Double)
        End Get
        Set(value As Double)
            SetValue(WidthProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 蚀刻高度
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Height() As Double
        Get
            Return CType(GetValue(HeightProperty), Double)
        End Get
        Set(value As Double)
            SetValue(HeightProperty, value)
        End Set
    End Property

End Class