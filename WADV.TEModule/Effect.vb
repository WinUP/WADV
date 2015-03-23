Imports System.Windows.Controls
Imports System.Windows.Media.Imaging

Namespace Effect

    Public Interface IEffect

        Sub Rendering()

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
        Protected imageContent As Image

        Public Sub New(content As Image, Optional variable As String() = Nothing)
            imageContent = content
        End Sub

        Public Overridable Sub Rendering() Implements IEffect.Rendering

        End Sub

    End Class

End Namespace