Imports Neo.IronLua
Imports WADV.Core.API.Script
Imports WADV.Core.API.Path
Imports WADV.Core.Enumeration

Public Class Extension
    ''' <summary>
    ''' 编译脚本代码文件
    ''' </summary>
    ''' <param name="targetFile">要编译的文件（Script目录下）</param>
    ''' <param name="[option]">编译选项</param>
    ''' <returns></returns>
    Public Shared Function Compile(targetFile As String, [option] As LuaCompileOptions) As LuaChunk
        Dim core = TryCast(VM(), ScriptCore)
        If core Is Nothing Then Return Nothing
        Return core.Vm.CompileChunk(Combine(PathType.Script, targetFile), [option])
    End Function

    ''' <summary>
    ''' 获取脚本主机
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetVm() As Lua
        Dim core = TryCast(VM(), ScriptCore)
        If core Is Nothing Then Return Nothing
        Return core.Vm
    End Function

    ''' <summary>
    ''' 获取脚本执行环境
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetEnvironment() As LuaGlobal
        Dim core = TryCast(VM(), ScriptCore)
        If core Is Nothing Then Return Nothing
        Return core.Environment
    End Function
End Class