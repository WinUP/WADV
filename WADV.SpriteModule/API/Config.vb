Imports WADV.SpriteModule.Effect

Namespace API
    Public Module Config
        ''' <summary>
        ''' 初始化模块
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Init()
            EffectList.ReadEffect()
            Message.Send("[SPRITE]INIT_FINISH")
        End Sub
    End Module
End Namespace