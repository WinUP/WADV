Namespace API
    Public Module Joint

        ''' <summary>
        ''' 建立一个空的物理关节
        ''' </summary>
        ''' <param name="name">物理关节的理想名称</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NewJoint(name As String) As PhysicsJoint
            Return New PhysicsJoint With {.Name = name}
        End Function

        ''' <summary>
        ''' 添加一个物理关节到正在模拟的物理世界
        ''' </summary>
        ''' <param name="target">要添加的物理关节</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AddJoint(target As PhysicsJoint) As PhysicsJoint
            If Config.TargetPysicsWorld Is Nothing Then Return Nothing
            Config.TargetPysicsWorld.AddPhysicsJoint(target)
            Return target
        End Function

        ''' <summary>
        ''' 删除一个物理关节
        ''' </summary>
        ''' <param name="name">要删除的物理关节的名称</param>
        ''' <remarks></remarks>
        Public Sub RemoveJoint(name As String)
            If Config.TargetPysicsWorld Is Nothing Then Exit Sub
            Config.TargetPysicsWorld.DeletePhysicsObject(name)
        End Sub
    End Module
End Namespace
