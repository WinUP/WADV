Namespace Effect

    ''' <summary>
    ''' 精灵效果列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class EffectList
        Private Shared ReadOnly EffectList As New Dictionary(Of String, Type)

        ''' <summary>
        ''' 读取所有效果
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Sub ReadEffect()
            EffectList.Add("BaseEffect", GetType(BaseEffect))
            For Each file In IO.Directory.GetFiles(PathAPI.GetPath(PathType.Resource, "SpriteEffect\"), "*.dll") _
                .SelectMany(Function(e) Reflection.Assembly.LoadFrom(e).GetTypes()) _
                .Where(Function(e) e.GetInterface("WADV.SpriteModule.Effect.IEffect") <> Nothing)
                EffectList.Add(file.Name, file)
            Next
        End Sub

        ''' <summary>
        ''' 获取指定名称的效果
        ''' </summary>
        ''' <param name="name">目标效果的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function [Get](name As String) As Type
            If Not EffectList.ContainsKey(name) Then Return Nothing
            Return EffectList.Item(name)
        End Function
    End Class
End Namespace