Imports WADV.Core.API

Namespace API
    Module Config
        ''' <summary>
        ''' 初始化粒子模块
        ''' </summary>
        Public Sub Init()
            ParticleModel.ParticleModelList.ReadModel()
            Send("[PARTICLE]INIT_FINISH")
        End Sub
    End Module
End Namespace
