Namespace Effect
    ''' <summary>
    ''' 精灵效果列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class EffectList
        Private Shared ReadOnly List As New Dictionary(Of String, Type)

        ''' <summary>
        ''' 读取所有效果
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Sub ReadEffect()
            List.Add("BaseEffect", GetType(BaseEffect))
            For Each target In IO.Directory.GetFiles(Path.Combine(PathType.Resource, "SpriteEffect\"), "*.dll") _
                .SelectMany(Function(e) Reflection.Assembly.LoadFrom(e).GetTypes()) _
                .Where(Function(e) e.BaseType.FullName = "WADV.SpriteModule.Effect.BaseEffect")
                List.Add(target.Name, target)
            Next
        End Sub

        ''' <summary>
        ''' 获取指定名称的效果
        ''' </summary>
        ''' <param name="name">目标效果的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared ReadOnly Property Item(name As String) As Type
            Get
                If Not List.ContainsKey(name) Then Return Nothing
                Return List.Item(name)
            End Get
        End Property
    End Class
End Namespace