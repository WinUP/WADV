Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class Serializer

    Public Shared Sub SaveToFile(filePath As String)
        Dim stream As New FileStream(PathAPI.GetPath(WADV.AppCore.Path.PathFunction.PathType.UserFile, filePath), FileMode.OpenOrCreate)
        Dim content As New List(Of Object)
        content.Add(ScriptAPI.GetVm())
        Dim formatter As New BinaryFormatter
        formatter.Serialize(stream, content)
        stream.Close()
    End Sub


End Class
