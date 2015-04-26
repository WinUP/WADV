Imports WADV.SpriteModule.Effect

Namespace API

    Public NotInheritable Class ConfigAPI

        ''' <summary>
        ''' 初始化模块
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub Init()
            EffectList.ReadEffect()
            MessageAPI.SendSync("[SPRITE]INIT_FINISH")
        End Sub

    End Class

End Namespace