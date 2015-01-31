Imports System.Windows.Controls
Imports System.Windows.Media.Animation

''' <summary>
''' 渐显效果
''' x,y,width,height,image,fadeTime
''' </summary>
''' <remarks></remarks>
Public Class FadeIn : Inherits WADV.TEModule.Effect.BaseEffect

    Public Sub New(content As Image, variable As String())
        MyBase.New(content, variable)
    End Sub

    Public Overrides Sub Rendering()


    End Sub

End Class
