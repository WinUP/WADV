Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 平铺重复
''' </summary>
''' <remarks>来自Shazzam (C)2008-2012 Walt Ritscher</remarks>
Public Class TilerEffect : Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(TilerEffect), 0)
    Public Shared ReadOnly VerticalTileCountProperty As DependencyProperty = DependencyProperty.Register("VerticalTileCount", GetType(Double), GetType(TilerEffect), New UIPropertyMetadata(1.0, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly HorizontalTileCountProperty As DependencyProperty = DependencyProperty.Register("HorizontalTileCount", GetType(Double), GetType(TilerEffect), New UIPropertyMetadata(1.0, PixelShaderConstantCallback(2)))
    Public Shared ReadOnly HorizontalOffsetProperty As DependencyProperty = DependencyProperty.Register("HorizontalOffset", GetType(Double), GetType(TilerEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(3)))
    Public Shared ReadOnly VerticalOffsetProperty As DependencyProperty = DependencyProperty.Register("VerticalOffset", GetType(Double), GetType(TilerEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(4)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack2;component/PixelShader/TilerEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(VerticalTileCountProperty)
        UpdateShaderValue(HorizontalTileCountProperty)
        UpdateShaderValue(HorizontalOffsetProperty)
        UpdateShaderValue(VerticalOffsetProperty)
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
    ''' 获取或设置纵向重复量[0-20+]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VerticalTileCount() As Double
        Get
            Return CType(GetValue(VerticalTileCountProperty), Double)
        End Get
        Set(value As Double)
            SetValue(VerticalTileCountProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置横向平铺量[0-20+]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HorizontalTileCount() As Double
        Get
            Return CType(GetValue(HorizontalTileCountProperty), Double)
        End Get
        Set(value As Double)
            SetValue(HorizontalTileCountProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置横向偏移量[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HorizontalOffset() As Double
        Get
            Return CType(GetValue(HorizontalOffsetProperty), Double)
        End Get
        Set(value As Double)
            SetValue(HorizontalOffsetProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置纵向偏移量[0-1]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VerticalOffset() As Double
        Get
            Return CType(GetValue(VerticalOffsetProperty), Double)
        End Get
        Set(value As Double)
            SetValue(VerticalOffsetProperty, value)
        End Set
    End Property
End Class