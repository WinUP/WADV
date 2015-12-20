Imports System.Windows.Media.Effects

Public Class Extension
    ''' <summary>
    ''' 初始化模块
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Ready()
        EffectList.ReadEffect()
        Message.Send("[SHADER]INIT_FINISH")
    End Sub

    ''' <summary>
    ''' 获取一个Shader效果
    ''' </summary>
    ''' <param name="name">目标效果名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function [Get](name As String) As ShaderEffect
        Dim target = EffectList.Get(name)
        If target Is Nothing Then Return Nothing
        Return DirectCast(Activator.CreateInstance(EffectList.Get(name)), ShaderEffect)
    End Function
End Class