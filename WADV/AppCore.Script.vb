Imports System.Reflection
Imports System.Threading

Namespace AppCore.Script

    ''' <summary>
    ''' 脚本引擎核心类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ScriptCore : Inherits Windows.Threading.DispatcherObject
        Private Shared self As ScriptCore
        Private isBusy As Boolean
        Protected Friend ScriptVM As LuaInterface.Lua

        Private Sub New()
            ScriptVM = New LuaInterface.Lua
            isBusy = False
        End Sub

        ''' <summary>
        ''' 获取脚本核心的唯一实例
        ''' </summary>
        Protected Friend Shared Function GetInstance() As ScriptCore
            If self Is Nothing Then self = New ScriptCore
            Return self
        End Function

        ''' <summary>
        ''' 获取脚本主机当前的状态
        ''' </summary>
        Protected Friend ReadOnly Property BusyStatus As Boolean
            Get
                Return isBusy
            End Get
        End Property

        ''' <summary>
        ''' 检查脚本主机空闲状态
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CheckStatus()
            If isBusy Then Throw New InvalidOperationException("脚本主机当前正忙，这一般是由于之前的调用还没有执行完成")
            VerifyAccess()
        End Sub

        ''' <summary>
        ''' 执行脚本文件
        ''' </summary>
        ''' <param name="fileName">文件名(包括script之后的路径)</param>
        ''' <remarks></remarks>
        Protected Friend Sub RunFile(fileName As String)
            CheckStatus()
            isBusy = True
            ScriptVM.DoFile(Path.PathFunction.GetFullPath(Path.PathConfig.Script, fileName))
            isBusy = False
        End Sub

        ''' <summary>
        ''' 执行字符串脚本
        ''' </summary>
        ''' <param name="script">要执行的脚本</param>
        ''' <remarks></remarks>
        Protected Friend Sub RunStrng(script As String)
            CheckStatus()
            isBusy = True
            ScriptVM.DoString(script)
            isBusy = False
        End Sub

        ''' <summary>
        ''' 执行脚本中的函数
        ''' </summary>
        ''' <param name="functionName">函数名</param>
        ''' <param name="params">参数列表</param>
        ''' <returns>函数返回值列表</returns>
        ''' <remarks></remarks>
        Protected Friend Function RunFunction(functionName As String, params() As Object) As Object()
            CheckStatus()
            isBusy = True
            Dim tmpFunction = ScriptVM.GetFunction(functionName)
            If tmpFunction Is Nothing Then Throw New Exception("找不到函数")
            Dim returnData() As Object = tmpFunction.Call(params)
            isBusy = False
            Return returnData
        End Function

        ''' <summary>
        ''' 获取脚本变量的值
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Friend Function GetVariable(name As String) As Object
            CheckStatus()
            Return ScriptVM(name)
        End Function

        ''' <summary>
        ''' 获取字符串形式的脚本变量
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Protected Friend Function GetStringVariable(name As String) As String
            CheckStatus()
            Return ScriptVM.GetString(name)
        End Function

        ''' <summary>
        ''' 获取浮点数形式的脚本变量
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Protected Friend Function GetDoubleVariable(name As String) As Double
            CheckStatus()
            Return ScriptVM.GetNumber(name)
        End Function

        ''' <summary>
        ''' 获取整数形式的脚本变量
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Protected Friend Function GetIntegerVariable(name As String) As Integer
            CheckStatus()
            Return CInt(GetDoubleVariable(name))
        End Function

        ''' <summary>
        ''' 获取LUA表形式的脚本变量
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Protected Friend Function GetTableVariable(name As String) As LuaInterface.LuaTable
            CheckStatus()
            Return ScriptVM.GetTable(name)
        End Function

        ''' <summary>
        ''' 获取LUA表中指定元素的内容
        ''' </summary>
        ''' <param name="tableName">表名</param>
        ''' <param name="key">键名</param>
        ''' <returns>元素内容</returns>
        ''' <remarks></remarks>
        Protected Friend Function GetVariableInTable(tableName As String, key As String) As Object
            CheckStatus()
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
            RegisterFunction(Reflection.Assembly.GetExecutingAssembly.GetTypes, "WADV.AppCore.API", "SYS")
            For Each tmpFunction In registingFunctionList
                tmpFunction.StartRegisting(ScriptCore.GetInstance.ScriptVM)
            Next
        End Sub

        Protected Friend Shared Sub RegisterFunction(types() As Type, belong As String, prefix As String)
            Dim classList = From tmpClass In types Where tmpClass.IsClass AndAlso tmpClass.Namespace = belong Select tmpClass
            Dim functionList() As MethodInfo
            Dim apiBase As Object
            Dim apiBaseName As String
            For Each tmpClass In classList
                apiBaseName = tmpClass.Name
                ScriptCore.GetInstance.Dispatcher.Invoke(Sub()
                                                             apiBase = tmpClass.Assembly.CreateInstance(belong & "." & apiBaseName)
                                                             functionList = tmpClass.GetMethods
                                                             For Each tmpMethod In functionList
                                                                 ScriptCore.GetInstance.ScriptVM.RegisterFunction(String.Format(prefix & "_{0}_{1}", apiBaseName.Remove(apiBaseName.Length - 3), tmpMethod.Name), apiBase, tmpMethod)
                                                             Next
                                                         End Sub)
            Next
        End Sub

    End Class

End Namespace
