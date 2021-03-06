﻿Imports WADV.Core.Enumeration

Friend NotInheritable Class Initialiser
    ''' <summary>
    ''' 待实例化的文字效果列表
    ''' </summary>
    Friend Shared EffectList As Dictionary(Of String, Type)

    ''' <summary>
    ''' 读取并缓存所有文字效果
    ''' </summary>
    Friend Shared Sub LoadEffect()
        EffectList = New Dictionary(Of String, Type)
        Dim basePath As String = Path.Combine(PathType.Resource, "TextEffect\")
        For Each tmpType In From assemble In (IO.Directory.GetFiles(basePath, "*.dll").Select(Function(file) Reflection.Assembly.LoadFrom(file)))
                            Select types = assemble.GetTypes.Where(Function(e) e.BaseType.FullName = "WADV.TextModule.BaseEffect")
                            From tmpType1 In types Select tmpType1
            EffectList.Add(tmpType.Name, tmpType)
        Next
        Message.Send("[TEXT]INIT_EFFECT_FINISH")
    End Sub

End Class