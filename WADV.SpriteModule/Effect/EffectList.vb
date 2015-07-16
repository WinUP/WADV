Imports System.Windows

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

        ''' <summary>
        ''' 根据名称、作用对象和参数获得效果实例
        ''' </summary>
        ''' <param name="name">目标效果的名称</param>
        ''' <param name="element">目标效果的作用对象</param>
        ''' <param name="params">目标效果的初始化参数</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function Create(name As String, element As FrameworkElement, ParamArray params() As Object) As BaseEffect
            Dim effectType = EffectList.Item(name)
            If effectType Is Nothing Then Return Nothing
            Return Activator.CreateInstance(effectType, New Object() {element, params})
        End Function
    End Class
End Namespace