Friend NotInheritable Class EffectList
    Private Shared ReadOnly List As New Dictionary(Of String, Type)

    ''' <summary>
    ''' 读取所有效果
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared Sub ReadEffect()
        For Each file In IO.Directory.GetFiles(PathAPI.GetPath(PathType.Resource, "ShaderEffect\"), "*.dll") _
            .SelectMany(Function(e) Reflection.Assembly.LoadFrom(e).GetTypes()) _
            .Where(Function(e) e.BaseType.FullName = "System.Windows.Media.Effects.ShaderEffect" <> Nothing)
            List.Add(file.Name, file)
        Next
    End Sub

    ''' <summary>
    ''' 获取指定名称的效果
    ''' </summary>
    ''' <param name="name">目标效果的名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function [Get](name As String) As Type
        If Not List.ContainsKey(name) Then Return Nothing
        Return List.Item(name)
    End Function


End Class
