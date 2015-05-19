Imports System.Windows.Media

Public Class Utilities

    ''' <summary>
    ''' 返回一个包含所有变换效果的变换组，其效果顺序为：平移、缩放、旋转、扭曲
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GenerateTransformGroup() As TransformGroup
        Dim target As New TransformGroup
        target.Children.Add(New TranslateTransform)
        target.Children.Add(New ScaleTransform With {.CenterX = 0.5, .CenterY = 0.5})
        target.Children.Add(New RotateTransform With {.CenterX = 0.5, .CenterY = 0.5})
        target.Children.Add(New SkewTransform With {.CenterX = 0.5, .CenterY = 0.5})
        Return target
    End Function

    ''' <summary>
    ''' 从变换组中获取第一个类型为指定类型的变换效果
    ''' </summary>
    ''' <typeparam name="T">目标类型</typeparam>
    ''' <param name="group">要检索的变换组</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTransformFromGroup(Of T As Transform)(group As TransformGroup) As T
        Return group.Children.OfType(Of T).FirstOrDefault()
    End Function

End Class
