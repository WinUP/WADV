Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Effects

''' <summary>
''' 水下效果
''' </summary>
''' <remarks></remarks>
Public Class UnderwaterEffect:Inherits ShaderEffect
    Public Shared ReadOnly InputProperty As DependencyProperty = RegisterPixelShaderSamplerProperty("Input", GetType(UnderwaterEffect), 0)
    Public Shared ReadOnly TimerProperty As DependencyProperty = DependencyProperty.Register("Timer", GetType(Double), GetType(UnderwaterEffect), New UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)))
    Public Shared ReadOnly RefractonProperty As DependencyProperty = DependencyProperty.Register("Refracton", GetType(Double), GetType(UnderwaterEffect), New UIPropertyMetadata(50.0, PixelShaderConstantCallback(1)))
    Public Shared ReadOnly VerticalTroughWidthProperty As DependencyProperty = DependencyProperty.Register("VerticalTroughWidth", GetType(Double), GetType(UnderwaterEffect), New UIPropertyMetadata(23.0, PixelShaderConstantCallback(2)))
    Public Shared ReadOnly Wobble2Property As DependencyProperty = DependencyProperty.Register("Wobble2", GetType(Double), GetType(UnderwaterEffect), New UIPropertyMetadata(23.0, PixelShaderConstantCallback(3)))

    Public Sub New()
        MyBase.New()
        PixelShader = New PixelShader() With {.UriSource = New Uri("/WPSoft.ShaderEffectPack2;component/PixelShader/UnderwaterEffect.ps", UriKind.Relative)}
        UpdateShaderValue(InputProperty)
        UpdateShaderValue(TimerProperty)
        UpdateShaderValue(RefractonProperty)
        UpdateShaderValue(VerticalTroughWidthProperty)
        UpdateShaderValue(Wobble2Property)
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
    ''' 获取或设置时间进度[0-1+]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Timer() As Double
        Get
            Return CType(GetValue(TimerProperty), Double)
        End Get
        Set(value As Double)
            SetValue(TimerProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置反射率[20-60+]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Refracton() As Double
        Get
            Return CType(GetValue(RefractonProperty), Double)
        End Get
        Set(value As Double)
            SetValue(RefractonProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置水面深度[0-30+]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VerticalTroughWidth() As Double
        Get
            Return CType(GetValue(VerticalTroughWidthProperty), Double)
        End Get
        Set(value As Double)
            SetValue(VerticalTroughWidthProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置波动程度[+20-30+]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Wobble2() As Double
        Get
            Return CType(GetValue(Wobble2Property), Double)
        End Get
        Set(value As Double)
            SetValue(Wobble2Property, value)
        End Set
    End Property
End Class