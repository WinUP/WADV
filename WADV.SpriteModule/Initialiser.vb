Friend NotInheritable Class Initialiser
    Friend Shared EffectList As Dictionary(Of String, Type)

    Friend Shared Sub LoadEffect()
        EffectList = New Dictionary(Of String, Type)
        EffectList.Add("BaseEffect", GetType(BaseEffect))
        Dim basePath = PathAPI.GetPath(PathType.Resource, "SpriteEffect\")
        For Each file In IO.Directory.GetFiles(basePath, "*.dll")
            Dim assembly = Reflection.Assembly.LoadFrom(file).GetTypes()
            For Each type In From type1 In assembly Where type1.GetInterface("IEffect") <> Nothing
                EffectList.Add(type.Name, type)
            Next
        Next
        MessageAPI.SendSync("[SPRITE]INIT_EFFECT_FINISH")
    End Sub

End Class