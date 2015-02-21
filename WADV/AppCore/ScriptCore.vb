Imports Neo.IronLua

Namespace AppCore

    ''' <summary>
    ''' 脚本引擎核心类
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class ScriptCore
        Private Shared _self As ScriptCore
        Private ReadOnly _vm As Lua
        Private ReadOnly _env As LuaGlobal
        Private ReadOnly _messanger As MessageService

        Private Sub New()
            _vm = New Lua
            _env = _vm.CreateEnvironment
            _messanger = MessageService.GetInstance
            _messanger.SendMessage("[SYSTEM]SCRIPT_INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 获取脚本核心的唯一实例
        ''' </summary>
        Friend Shared Function GetInstance() As ScriptCore
            If _self Is Nothing Then _self = New ScriptCore
            Return _self
        End Function

        ''' <summary>
        ''' 获取脚本主机实例
        ''' </summary>
        Friend ReadOnly Property Vm As Lua
            Get
                Return _vm
            End Get
        End Property

        ''' <summary>
        ''' 获取脚本执行环境实例
        ''' </summary>
        Friend ReadOnly Property Environment As LuaGlobal
            Get
                Return _env
            End Get
        End Property

        ''' <summary>
        ''' 执行一个脚本文件
        ''' </summary>
        ''' <param name="filePath">文件路径</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function RunFile(filePath As String) As LuaResult
            _messanger.SendMessage("[SYSTEM]SCRIPTFILE_STANDBY")
            Dim result = _env.DoChunk(filePath)
            _messanger.SendMessage("[SYSTEM]SCRIPTFILE_FINISH")
            Return result
        End Function

        ''' <summary>
        ''' 执行字符串脚本
        ''' </summary>
        ''' <param name="script">要执行的字符串</param>
        ''' <remarks></remarks>
        Friend Function RunString(script As String) As LuaResult
            _messanger.SendMessage("[SYSTEM]SCRIPT_STANDBY")
            Dim result = _env.DoChunk(script, "temp.lua")
            _messanger.SendMessage("[SYSTEM]SCRIPT_FINISH")
            Return result
        End Function

    End Class

End Namespace
