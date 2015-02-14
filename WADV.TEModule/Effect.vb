Imports System.Windows.Controls

Namespace Effect

    Public Interface IEffect

        Sub Render()

        Sub Dispose()

        Sub Wait()


    End Interface

    Public Class Initialiser
        Protected Friend Shared EffectList As Dictionary(Of String, Type)

        Protected Friend Shared Sub LoadEffect()
            EffectList = New Dictionary(Of String, Type)
            EffectList.Add("BaseEffect", GetType(BaseEffect))
            Dim basePath = PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Resource, "TEEffect\")
            For Each file In IO.Directory.GetFiles(basePath, "*.dll")
                Dim assembly = Reflection.Assembly.LoadFrom(file).GetTypes()
                For Each type In assembly
                    If type.GetInterface("IEffect") <> Nothing Then EffectList.Add(type.Name, type)
                Next
            Next
        End Sub

    End Class

    Public Class BaseEffect : Implements IEffect
        Protected ImageContent As Panel
        Protected Params() As Object
        Protected ReadOnly Id As Integer

        Public Sub New(id As Integer, params As Object())
            ImageContent = TEList.List.Item(id)
            Me.Id = id
            Me.Params = params
        End Sub

        Public Overridable Sub Render() Implements IEffect.Render
        End Sub

        Public Sub Dispose() Implements IEffect.Dispose
            ImageContent = Nothing
            Params = Nothing
        End Sub

        Protected Sub Animation_Finished(sender As Object, e As EventArgs)
            MessageAPI.SendSync("TE_EFFECT_FINISH")
        End Sub

        Public Overridable Sub Wait() Implements IEffect.Wait
            MessageAPI.WaitSync("TE_EFFECT_FINISH")
        End Sub

    End Class

End Namespace