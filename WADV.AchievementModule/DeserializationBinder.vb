Imports System.Runtime.Serialization

Friend NotInheritable Class DeserializationBinder : Inherits SerializationBinder

    Public Overrides Function BindToType(assemblyName As String, typeName As String) As Type
        Dim typeToDeserialize As Type = Nothing
        If typeName = "System.Collections.Generic.Dictionary`2" Then typeName = "System.Collections.Generic.Dictionary"
        typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName))
        Return typeToDeserialize
    End Function

End Class