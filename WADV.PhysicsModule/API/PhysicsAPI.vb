Namespace API

    Public Class PhysicsAPI

        ''' <summary>
        ''' 设置要进行模拟的物理世界对象
        ''' </summary>
        ''' <param name="target">要进行模拟的物理世界对象</param>
        ''' <remarks></remarks>
        Public Shared Sub SetWorld(target As PhysicsWorld)
            If Not Config.StopSimulation Then
                Config.StopSimulation = True
                MessageAPI.WaitSync("[PHYSICS]SIMULATION_END")
            End If
            Config.TargetPysicsWorld = target
            Config.StopSimulation = False
            LoopAPI.AddLoopSync(New PluginInterface.LoopReceiver)
            MessageAPI.SendSync("[PHYSICS]SIMULATION_START")
            MessageAPI.SendSync("[PHYSICS]WORLD_CHANGE")
        End Sub

        ''' <summary>
        ''' 停止物理模拟
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StopSimulation()
            Config.StopSimulation = True
        End Sub

        ''' <summary>
        ''' 开始物理模拟(若没有设置要进行模拟的物理世界则这个函数没有任何作用)
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub StartSimulation()
            If Config.TargetPysicsWorld Is Nothing Then Exit Sub
            If Not Config.StopSimulation Then Exit Sub
            Config.StopSimulation = False
            LoopAPI.AddLoopSync(New PluginInterface.LoopReceiver)
            MessageAPI.SendSync("[PHYSICS]SIMULATION_START")
        End Sub

        Public Shared Function AddSprite(name As String) As PhysicsSprite
            If Config.TargetPysicsWorld Is Nothing Then Return Nothing
            Dim target As New PhysicsSprite
            Config.TargetPysicsWorld.AddPhysicsSprite(target, True)
            Return target
        End Function
    End Class
End Namespace
