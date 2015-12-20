Imports System.Threading
Imports Neo.IronLua
Imports WADV.Core
Imports WADV.Core.Script

''' <summary>
''' 脚本引擎核心类
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class ScriptCore : Implements IScriptEngine
    Private _vm As Lua
    Private _env As LuaGlobal

    ''' <summary>
    ''' 初始化脚本核心
    ''' </summary>
    Friend Sub Initialise() Implements IScriptEngine.Initialise
        _vm = New Lua
        _env = _vm.CreateEnvironment(Of LuaGlobal)
    End Sub

    ''' <summary>
    ''' 释放脚本核心的资源
    ''' </summary>
    Friend Sub Dispose() Implements IScriptEngine.Dispose
        _env.ArrayList.Clear()
        _env.Members.Clear()
        _env = Nothing
        _vm.Dispose()
        _vm = Nothing
    End Sub

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

    Friend Function RunFile(filePath As String) As Object Implements IScriptEngine.RunFile
        Message.Send("[LUA]SCRIPTFILE_STANDBY")
        Dim result = _env.DoChunk(Path.Combine(PathType.Script, filePath))
        Message.Send("[LUA]SCRIPTFILE_FINISH")
        Return result
    End Function

    Public Sub RunFileAsync(filePath As String) Implements IScriptEngine.RunFileAsync
        Dim tmpThread As New Thread(CType(Sub() RunFile(filePath), ThreadStart))
        tmpThread.Name = "[系统]脚本文件执行线程"
        tmpThread.IsBackground = True
        tmpThread.Priority = ThreadPriority.Normal
        Message.Send("[LUA]ASYNC_SCRIPTFILE_STANDBY")
        tmpThread.Start()
    End Sub

    Friend Function RunString(script As String) As Object Implements IScriptEngine.RunString
        Message.Send("[LUA]SCRIPT_STANDBY")
        Dim result = _env.DoChunk(script, "temp.lua")
        Message.Send("[LUA]SCRIPT_FINISH")
        Return result
    End Function

    Public Sub RunStringAsync(content As String) Implements IScriptEngine.RunStringAsync
        Dim tmpThread As New Thread(CType(Sub() RunString(content), ThreadStart))
        tmpThread.Name = "[系统]脚本字符串执行线程"
        tmpThread.IsBackground = True
        tmpThread.Priority = ThreadPriority.Normal
        Message.Send("[LUA]ASYNC_SCRIPT_STANDBY")
        tmpThread.Start(content)
    End Sub

    Public Sub [Set](name As String, value As Object) Implements IScriptEngine.Set
        _env(name) = value
    End Sub

    Public Function [Get](name As String) As Object Implements IScriptEngine.Get
        Return _env(name)
    End Function

    Public Sub Register(target As Field) Implements IScriptEngine.Register
        Register(target, _env("core"))
    End Sub

    '注册子Field时，父Field中为这个子Field起的名字是无用的，注册名使用的是子Field的Name
    Private Sub Register(target As Field, root As LuaTable)
        If root(target.Name) Is Nothing Then root(target.Name) = New LuaTable
        Dim table As LuaTable = root(target.Name)
        For Each e In target.Content
            If TypeOf e.Value Is Field Then
                Register(e.Value, table)
            Else
                table(e.Key) = e.Value
            End If
        Next
    End Sub
End Class
