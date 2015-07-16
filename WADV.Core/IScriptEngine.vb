' WinUP Adventure Game Engine Core Framework
' Script Engine Interface
' This is the only one necessary interface that a suitable script engine should be implemented

Public Interface IScriptEngine
    Sub RunFileAsync(filePath As String)
    Function RunFile(filePath As String) As Object
    Sub RunStringAsync(content As String)
    Function RunString(content As String) As Object
    Sub [Set](name As String, value As Object)
    Function [Get](name As String) As Object
End Interface