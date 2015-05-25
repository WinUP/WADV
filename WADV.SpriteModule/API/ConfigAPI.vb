Imports WADV.SpriteModule.Effect

Namespace API
    Module Config
        ''' <summary>
        ''' 初始化模块
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Init()
            EffectList.ReadEffect()
            MessageAPI.SendSync("[SPRITE]INIT_FINISH")
        End Sub
    End Module
End Namespace