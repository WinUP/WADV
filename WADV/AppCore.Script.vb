Imports System.Reflection

Namespace AppCore.Script

    ''' <summary>
    ''' 脚本引擎核心类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ScriptCore

        ''' <summary>
        ''' 游戏通用的脚本解释主机
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared ScriptVM As New LuaInterface.Lua

    End Class

    ''' <summary>
    ''' 脚本注册类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Register

        Private Shared registingFunctionList As New List(Of Plugin.IScriptFunction)

        ''' <summary>
        ''' 添加一个脚本接口函数
        ''' </summary>
        ''' <param name="functionContent">要添加的函数</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub AddFunction(functionContent As Plugin.IScriptFunction)
            If Not registingFunctionList.Contains(functionContent) Then registingFunctionList.Add(functionContent)
        End Sub

        ''' <summary>
        ''' 注册所有的脚本接口函数
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Shared Sub RegisterFunction()
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.IsClass AndAlso tmpClass.Namespace = "WADV.AppCore.API" Select tmpClass
            Dim functionList() As MethodInfo
            Dim apiBase As Object
            Dim apiBaseName As String
            For Each tmpClass In classList
                apiBaseName = tmpClass.Name
                apiBase = tmpClass.Assembly.CreateInstance("WADV.AppCore.API." & apiBaseName)
                functionList = tmpClass.GetMethods
                For Each tmpMethod In functionList
                    ScriptCore.ScriptVM.RegisterFunction(String.Format("SYS_{0}_{1}", apiBaseName.Remove(apiBaseName.Length - 3), tmpMethod.Name), apiBase, tmpMethod)
                Next
            Next
            For Each tmpFunction In registingFunctionList
                tmpFunction.StartRegisting(ScriptCore.ScriptVM)
            Next
        End Sub

    End Class

    ''' <summary>
    ''' 脚本-游戏交互类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Exchanger

        ''' <summary>
        ''' 执行脚本文件
        ''' </summary>
        ''' <param name="fileName">文件名(包括script之后的路径)</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub RunFile(fileName As String)
            ScriptCore.ScriptVM.DoFile(Config.URLConfig.GetFullURI(Config.URLConfig.Script, fileName))
        End Sub

        ''' <summary>
        ''' 执行字符串脚本
        ''' </summary>
        ''' <param name="script">要执行的脚本</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub RunStrng(script As String)
            ScriptCore.ScriptVM.DoString(script)
        End Sub

        ''' <summary>
        ''' 执行脚本中的函数
        ''' </summary>
        ''' <param name="functionName">函数名</param>
        ''' <param name="params">参数列表</param>
        ''' <returns>函数返回值列表</returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function RunFunction(functionName As String, params() As Object) As Object()
            Dim tmpFunction = ScriptCore.ScriptVM.GetFunction(functionName)
            If tmpFunction Is Nothing Then Throw New Exception("找不到函数")
            Return tmpFunction.Call(params)
        End Function

        ''' <summary>
        ''' 设置脚本全局变量的值
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">值</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub SetGlobalVariable(name As String, value As String)
            RunStrng(String.Format("{0}={1}", name, value))
        End Sub

        ''' <summary>
        ''' 设置脚本全局字符串变量的值
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">值</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub SetGlobalStringVariable(name As String, value As String)
            SetGlobalVariable(name, """" & value & """")
        End Sub

        ''' <summary>
        ''' 获取脚本变量的值
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function GetVariable(name As String) As Object
            Return ScriptCore.ScriptVM(name)
        End Function

        ''' <summary>
        ''' 获取字符串形式的脚本变量
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function GetStringVariable(name As String) As String
            Return ScriptCore.ScriptVM.GetString(name)
        End Function

        ''' <summary>
        ''' 获取浮点数形式的脚本变量
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function GetDoubleVariable(name As String) As Double
            Return ScriptCore.ScriptVM.GetNumber(name)
        End Function

        ''' <summary>
        ''' 获取整数形式的脚本变量
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function GetIntegerVariable(name As String) As Integer
            Return CInt(GetDoubleVariable(name))
        End Function

        ''' <summary>
        ''' 获取LUA表形式的脚本变量
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function GetTableVariable(name As String) As LuaInterface.LuaTable
            Return ScriptCore.ScriptVM.GetTable(name)
        End Function

        ''' <summary>
        ''' 获取LUA表中指定元素的内容
        ''' </summary>
        ''' <param name="tableName">表名</param>
        ''' <param name="key">键名</param>
        ''' <returns>元素内容</returns>
        ''' <remarks></remarks>
        Protected Friend Shared Function GetVariableInTable(tableName As String, key As String) As Object
            Dim tmpTable = GetTableVariable(tableName)
            If tmpTable Is Nothing Then Throw New Exception("找不到表")
            Return tmpTable.Item(key)
        End Function

    End Class

End Namespace
