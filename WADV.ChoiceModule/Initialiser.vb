Imports System.Reflection

Friend NotInheritable Class Initialiser
    ''' <summary>
    ''' 待实例化的图像效果列表
    ''' </summary>
    Friend Shared ShowEffectList, HideEffectList, ProgressEffectList As Dictionary(Of String, Type)

    ''' <summary>
    ''' 读取并缓存所有图像效果
    ''' </summary>
    Friend Shared Sub LoadEffect()
        ShowEffectList = New Dictionary(Of String, Type)
        HideEffectList = New Dictionary(Of String, Type)
        ProgressEffectList = New Dictionary(Of String, Type)
        ShowEffectList.Add("BaseShow", GetType(BaseShow))
        HideEffectList.Add("BaseHide", GetType(BaseHide))
        ProgressEffectList.Add("BaseProgress", GetType(BaseProgress))
        Dim basePath As String = PathAPI.GetPath(PathType.Resource, "ChoiceEffect\")
        For Each tmpType In From assemble In (IO.Directory.GetFiles(basePath, "*.dll").Select(Function(file) Assembly.LoadFrom(file)))
                            Select types = assemble.GetTypes
                            From tmpType1 In types Select tmpType1
            If tmpType.GetInterface("IShowEffect") IsNot Nothing Then ShowEffectList.Add(tmpType.Name, tmpType)
                If tmpType.GetInterface("IHideEffect") IsNot Nothing Then HideEffectList.Add(tmpType.Name, tmpType)
                If tmpType.GetInterface("IProgressEffect") IsNot Nothing Then ProgressEffectList.Add(tmpType.Name, tmpType)
        Next
        MessageAPI.SendSync("[CHOICE]INIT_EFFECT_FINISH")
    End Sub

End Class