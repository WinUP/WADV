Imports WADV.Core
Imports WADV.Core.API

Namespace ParticleModel
    Friend NotInheritable Class ParticleModelList
        Private Shared ReadOnly List As New Dictionary(Of String, Type)

        ''' <summary>
        ''' 读取所有粒子模型
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Sub ReadModel()
            List.Clear()
            List.Add("NormalParticleModel", GetType(NormalParticleModel))
            For Each file In IO.Directory.GetFiles(Path.Combine(PathType.Resource, "ParticleModel\"), "*.dll") _
                .SelectMany(Function(e) Reflection.Assembly.LoadFrom(e).GetTypes()) _
                .Where(Function(e) e.BaseType.FullName = "WADV.ParticleModule.ParticleModel.NormalParticleModel" <> Nothing)
                List.Add(file.Name, file)
            Next
        End Sub

        ''' <summary>
        ''' 获取指定名称的粒子模型
        ''' </summary>
        ''' <param name="name">目标模型的名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function [Get](name As String) As Type
            If Not List.ContainsKey(name) Then Return Nothing
            Return List.Item(name)
        End Function
    End Class
End Namespace