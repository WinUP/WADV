Imports System.Reflection
Imports System.Threading
Imports WADV.AppCore.API
Imports NLua

Namespace AppCore.Script

    ''' <summary>
    ''' 脚本引擎核心类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ScriptCore
        Private Shared _self As ScriptCore
        Private vm As Lua

        Private Sub New()
            vm = New Lua
            vm.LoadCLRPackage()
            MessageAPI.SendSync("SCRIPT_INIT_FINISH")
        End Sub

        ''' <summary>
        ''' 获取脚本核心的唯一实例
        ''' </summary>
        Protected Friend Shared Function GetInstance() As ScriptCore
            If _self Is Nothing Then _self = New ScriptCore
            Return _self
        End Function

        ''' <summary>
        ''' 获取脚本主机实例
        ''' </summary>
        Protected Friend ReadOnly Property ScriptVM As Lua
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
        ''' 加载脚本文件
        ''' </summary>
        ''' <param name="fileName">文件路径</param>
        ''' <remarks></remarks>
        Protected Friend Sub LoadFile(fileName As String)
            MessageAPI.SendSync("SCRIPT_FILE_BEFORELOAD")
            vm.LoadFile(fileName)
            MessageAPI.SendSync("SCRIPT_FILE_AFTERLOAD")
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
        ''' 获取LUA TABLE中指定元素的值
        ''' </summary>
        ''' <param name="tableName">表名</param>
        ''' <param name="key">键名</param>
        ''' <returns>元素的值</returns>
        ''' <remarks></remarks>
        Protected Friend Function GetVariableInTable(tableName As String, key As String) As Object
            Dim tmpTable = vm.GetTable(tableName)
            If tmpTable Is Nothing Then Throw New Exception("找不到表")
            Return tmpTable.Item(key)
        End Function

    End Class

    ''' <summary>
    ''' 脚本注册类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Register

        ''' <summary>
        ''' 注册脚本接口函数
        ''' </summary>
        ''' <param name="instance">要注册的类的类型声明</param>
        ''' <param name="prefix">函数前缀</param>
        ''' <param name="toLower">是否转换函数名为小写</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub RegisterFunction(instance As Type, prefix As String, toLower As Boolean)
            Dim apiBaseName = instance.Name
            Dim apiBase = instance.Assembly.CreateInstance(instance.Namespace & "." & instance.Name)
            For Each tmpMethod In instance.GetMethods
                ScriptCore.GetInstance.ScriptVM.RegisterFunction(prefix & "." & If(toLower, tmpMethod.Name.ToLower, tmpMethod.Name), apiBase, tmpMethod)
            Next
            MessageAPI.SendSync("SCRIPT_CONTENT_ADD")
        End Sub

    End Class

End Namespace
