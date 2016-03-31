Imports System.Runtime.Serialization

Friend NotInheritable Class DeserializationBinder : Inherits SerializationBinder
    Public Overrides Function BindToType(assemblyName As String, typeName As String) As Type
        If typeName = "System.Collections.Generic.Dictionary`2" Then typeName = "System.Collections.Generic.Dictionary"
        Return Type.GetType(String.Format("{0}, {1}", typeName, assemblyName))
    End Function
End Class