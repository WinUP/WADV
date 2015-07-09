Imports FarseerPhysics.Dynamics.Contacts

Namespace API
    Public Module World

        ''' <summary>
        ''' 建立一个新的物理世界
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NewWorld() As PhysicsWorld
            Return New PhysicsWorld
        End Function

        ''' <summary>
        ''' 设置要进行模拟的物理世界对象
        ''' </summary>
        ''' <param name="target">要进行模拟的物理世界对象</param>
        ''' <remarks></remarks>
        Public Sub SetWorld(target As PhysicsWorld)
            If Not Config.StopSimulation Then
                Config.StopSimulation = True
                Message.Wait("[PHYSICS]SIMULATION_END")
            End If
            Config.TargetPysicsWorld = target
            Message.Send("[PHYSICS]WORLD_CHANGE")
        End Sub

        ''' <summary>
        ''' 停止物理模拟(物理模拟将会在最近一次更新完成后下载)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub StopSimulation()
            Config.StopSimulation = True
        End Sub

        ''' <summary>
        ''' 开始物理模拟(若没有设置要进行模拟的物理世界则这个函数没有任何作用)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub StartSimulation()
            If Config.TargetPysicsWorld Is Nothing Then Exit Sub
            If Not Config.StopSimulation Then Exit Sub
            Config.StopSimulation = False
            [Loop].Listen(New PluginInterface.LoopReceiver)
            Message.Send("[PHYSICS]SIMULATION_START")
        End Sub

        ''' <summary>
        ''' 挂载物理世界的自定义碰撞处理事件(在被模拟的物理世界改编后仍起作用)
        ''' </summary>
        ''' <param name="code"></param>
        ''' <remarks></remarks>
        Public Sub HandleWorldCollision(code As Action(Of PhysicsSprite, PhysicsSprite, Contact))
            Config.WorldCollisionHandle = code
        End Sub

        ''' <summary>
        ''' 卸载物理世界的自定义碰撞处理事件(在被模拟的物理世界改编后仍起作用)
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RemoveWorldCollision()
            Config.WorldCollisionHandle = Nothing
        End Sub
    End Module
End Namespace
