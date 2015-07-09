Imports System.Windows.Media.Effects

Namespace API
    Public Module Shader
        ''' <summary>
        ''' 获取一个Shader效果
        ''' </summary>
        ''' <param name="name">目标效果名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function [Get](name As String) As ShaderEffect
            Dim target = EffectList.Get(name)
            If target Is Nothing Then Return Nothing
            Return DirectCast(Activator.CreateInstance(EffectList.Get(name)), ShaderEffect)
        End Function
    End Module
End Namespace