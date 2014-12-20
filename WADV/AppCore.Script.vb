Imports System.Reflection
Imports System.Threading
Imports WADV.AppCore.API

Namespace AppCore.Script

    ''' <summary>
    ''' 脚本引擎核心类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ScriptCore
        Private Shared self As ScriptCore
        Private vm As LuaInterface.Lua

        Private Sub New()
            vm = New LuaInterface.Lua
            MessageAPI.SendSync("SCRIPT_INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 获取脚本核心的唯一实例
        ''' </summary>
        Protected Friend Shared Function GetInstance() As ScriptCore
            If self Is Nothing Then self = New ScriptCore
            Return self
        End Function

        ''' <summary>
        ''' 获取脚本主机实例
        ''' </summary>
        Protected Friend ReadOnly Property ScriptVM As LuaInterface.Lua
            Get
                Return vm
            End Get
        End Property

        ''' <summary>
        ''' 执行脚本文件
        ''' </summary>
        ''' <param name="fileName">文件路径</param>
        ''' <remarks></remarks>
        Protected Friend Sub RunFile(fileName As String)
            MessageAPI.SendSync("SCRIPT_FILE_BEFOREDO")
            vm.DoFile(fileName)
            MessageAPI.SendSync("SCRIPT_FILE_AFTERDO")
        End Sub

        ''' <summary>
        ''' 执行字符串脚本
        ''' </summary>
        ''' <param name="script">要执行的脚本</param>
        ''' <remarks></remarks>
        Protected Friend Sub RunStrng(script As String)
            MessageAPI.SendSync("SCRIPT_STRING_BEFOREDO")
            vm.DoString(script)
            MessageAPI.SendSync("SCRIPT_STRING_AFTERDO")
        End Sub

        ''' <summary>
        ''' 执行脚本中的函数
        ''' </summary>
        ''' <param name="functionName">函数名</param>
        ''' <param name="params">参数列表</param>
        ''' <returns>函数返回值列表</returns>
        ''' <remarks></remarks>
        Protected Friend Function RunFunction(functionName As String, params() As Object) As Object()
            Dim tmpFunction = vm.GetFunction(functionName)
            If tmpFunction Is Nothing Then Throw New MissingMethodException("当前脚本主机中不存在函数" & functionName & "，是否忘记执行包含它的文件？")
            MessageAPI.SendSync("SCRIPT_METHOD_BEFOREDO")
            Dim returnData() As Object = tmpFunction.Call(params)
            MessageAPI.SendSync("SCRIPT_METHOD_AFTERDO")
            Return returnData
        End Function

        ''' <summary>
        ''' 获取脚本变量的值
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Function GetVariable(name As String) As Object
            Return vm(name)
        End Function

        ''' <summary>
        ''' 获取字符串形式的脚本变量
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Protected Friend Function GetStringVariable(name As String) As String
            Return vm.GetString(name)
        End Function

        ''' <summary>
        ''' 获取浮点数形式的脚本变量
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Protected Friend Function GetDoubleVariable(name As String) As Double
            Return vm.GetNumber(name)
        End Function

        ''' <summary>
        ''' 获取整数形式的脚本变量
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Protected Friend Function GetIntegerVariable(name As String) As Integer
            Return CInt(GetDoubleVariable(name))
        End Function

        ''' <summary>
        ''' 获取LUA TABLE形式的脚本变量
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Protected Friend Function GetTableVariable(name As String) As LuaInterface.LuaTable
            Return vm.GetTable(name)
        End Function

        ''' <summary>
        ''' 获取LUA TABLE中指定元素的值
        ''' </summary>
        ''' <param name="tableName">表名</param>
        ''' <param name="key">键名</param>
        ''' <returns>元素的值</returns>
        ''' <remarks></remarks>
        Protected Friend Function GetVariableInTable(tableName As String, key As String) As Object
            Dim tmpTable = GetTableVariable(tableName)
            If tmpTable Is Nothing Then Throw New Exception("找不到表")
            Return tmpTable.Item(key)
        End Function

    End Class

    ''' <summary>
    ''' 脚本注册类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Register
        Private Shared registingFunctionList As New List(Of Plugin.IScript)

        ''' <summary>
        ''' 添加一个脚本接口函数
        ''' </summary>
        ''' <param name="functionContent">要添加的函数</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub AddFunction(functionContent As Plugin.IScript)
            If Not registingFunctionList.Contains(functionContent) Then
                registingFunctionList.Add(functionContent)
                MessageAPI.SendSync("SCRIPT_CONTENT_ADD")
            End If
        End Sub

        ''' <summary>
        ''' 注册所有的脚本接口函数
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub RegisterFunction()
            RegisterFunction(Assembly.GetExecutingAssembly.GetTypes, "WADV.AppCore.API", "SYS")
            For Each tmpFunction In registingFunctionList
                tmpFunction.StartRegisting(ScriptCore.GetInstance.ScriptVM)
            Next
            MessageAPI.SendSync("SCRIPT_CONTENT_INITFINISH")
        End Sub

        ''' <summary>
        ''' 使用内置命名规范注册脚本接口函数
        ''' </summary>
        ''' <param name="types">要注册的函数所在的类(将会自动过滤非类的元素)</param>
        ''' <param name="belong">要注册的函数所在的类的名称空间</param>
        ''' <param name="prefix">脚本接口函数前缀</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub RegisterFunction(types() As Type, belong As String, prefix As String)
            Dim classList = From tmpClass In types Where tmpClass.IsClass AndAlso tmpClass.Namespace = belong Select tmpClass
            Dim apiBaseName As String
            For Each tmpClass In classList
                apiBaseName = tmpClass.Name
                Dim apiBase = tmpClass.Assembly.CreateInstance(belong & "." & apiBaseName)
                Dim functionList = tmpClass.GetMethods
                For Each tmpMethod In functionList
                    ScriptCore.GetInstance.ScriptVM.RegisterFunction(String.Format(prefix & "_{0}_{1}", apiBaseName.Remove(apiBaseName.Length - 3), tmpMethod.Name), apiBase, tmpMethod)
                Next
            Next
            MessageAPI.SendSync("SCRIPT_CONTENT_ADD")
        End Sub

    End Class

End Namespace
