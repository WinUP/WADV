Namespace API
    Module Particle
        ''' <summary>
        ''' 获得一个新的粒子系统
        ''' </summary>
        ''' <param name="useEllipse">是否使用圆形粒子，否则将使用方形粒子</param>
        ''' <returns></returns>
        Public Function [New](useEllipse As Boolean) As ParticleSystem
            Return New ParticleSystem(useEllipse)
        End Function
    End Module
End Namespace
