Imports Neo.IronLua

Namespace API
    ''' <summary>
    ''' 脚本API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Script

        ''' <summary>
        ''' 获取游戏脚本主机对象
        ''' </summary>
        ''' <returns>脚本主机</returns>
        ''' <remarks></remarks>
        Public Function Vm() As Lua
            Return ScriptCore.GetInstance.Vm
        End Function

        ''' <summary>
        ''' 获取游戏脚本执行环境对象
        ''' </summary>
        ''' <returns>脚本执行环境</returns>
        ''' <remarks></remarks>
        Public Function Environment() As LuaGlobal
            Return ScriptCore.GetInstance.Environment
        End Function
    End Module
End Namespace