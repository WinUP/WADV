Imports System.Windows.Controls

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
        Private ReadOnly _imageContent As Image
        Private ReadOnly _params() As String
        Private ReadOnly _id As Integer

        Protected Friend ReadOnly Property ImageContent As Image
            Get
                Return _imageContent
            End Get
        End Property

        Protected Friend ReadOnly Property Params As String()
            Get
                Return _params
            End Get
        End Property

        Public ReadOnly Property ID As Integer
            Get
                Return _id
            End Get
        End Property

        Public Sub New(id As Integer , Optional params As String() = Nothing)
            _imageContent = TEList.List.Item(id)
            _params = params
        End Sub

        Public Overridable Sub Rendering() Implements IEffect.Rendering
            ImageContent.Visibility = Windows.Visibility.Visible
        End Sub

        Public Sub Dispose()
            TEList.List.Delete(_id)
        End Sub

    End Class

End Namespace