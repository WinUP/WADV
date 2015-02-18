Imports NLua
Imports WADV.AppCore.API

Namespace AppCore

    ''' <summary>
    ''' 脚本引擎核心类
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class ScriptCore
        Private Shared _self As ScriptCore
        Private ReadOnly _vm As Lua
        Private ReadOnly _messanger As MessageService

        Private Sub New()
            _vm = New Lua
            _vm.LoadCLRPackage()
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
        Friend ReadOnly Property ScriptVm As Lua
            Get
                Return _vm
            End Get
        End Property

        ''' <summary>
        ''' 执行脚本文件
        ''' </summary>
        ''' <param name="fileName">文件路径</param>
        ''' <remarks></remarks>
        Friend Sub RunFile(fileName As String)
            _messanger.SendMessage("[SYSTEM]SCRIPTFILE_DO_BEFORE")
            _vm.DoFile(fileName)
            _messanger.SendMessage("[SYSTEM]SCRIPTFILE_DO_AFTER")
        End Sub

        ''' <summary>
        ''' 执行字符串脚本
        ''' </summary>
        ''' <param name="script">要执行的脚本</param>
        ''' <remarks></remarks>
        Friend Sub RunStrng(script As String)
            _messanger.SendMessage("[SYSTEM]SCRIPT_DO_BEFORE")
            _vm.DoString(script)
            _messanger.SendMessage("[SYSTEM]SCRIPT_DO_AFTER")
        End Sub

        ''' <summary>
        ''' 加载脚本文件
        ''' </summary>
        ''' <param name="fileName">文件路径</param>
        ''' <remarks></remarks>
        Friend Sub LoadFile(fileName As String)
            _messanger.SendMessage("[SYSTEM]SCRIPTFILE_LOAD_BEFORE")
            _vm.LoadFile(fileName)
            _messanger.SendMessage("[SYSTEM]SCRIPTFILE_LOAD_AFTER")
        End Sub

        ''' <summary>
        ''' 执行脚本中的函数
        ''' </summary>
        ''' <param name="functionName">函数名</param>
        ''' <param name="params">参数列表</param>
        ''' <returns>函数返回值列表</returns>
        ''' <remarks></remarks>
        Friend Function RunFunction(functionName As String, params() As Object) As Object()
            Dim tmpFunction = _vm.GetFunction(functionName)
            If tmpFunction Is Nothing Then Throw New MissingMethodException("当前脚本主机中不存在函数" & functionName & "，是否忘记执行包含它的文件？")
            _messanger.SendMessage("[SYSTEM]SCRIPTMETHOD_DO_BEFORE")
            Dim returnData() As Object = tmpFunction.Call(params)
            _messanger.SendMessage("[SYSTEM]SCRIPTMETHOD_DO_AFTER")
            Return returnData
        End Function

        ''' <summary>
        ''' 获取脚本变量的值
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function GetVariable(name As String) As Object
            Return _vm(name)
        End Function

        ''' <summary>
        ''' 获取LUA TABLE中指定元素的值
        ''' </summary>
        ''' <param name="tableName">表名</param>
        ''' <param name="key">键名</param>
        ''' <returns>元素的值</returns>
        ''' <remarks></remarks>
        Friend Function GetVariableInTable(tableName As String, key As String) As Object
            Dim tmpTable = _vm.GetTable(tableName)
            If tmpTable Is Nothing Then Throw New MissingMemberException("找不到表")
            Return tmpTable.Item(key)
        End Function

    End Class

    ''' <summary>
    ''' 脚本引擎辅助类
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class ScriptFunction

        ''' <summary>
        ''' 注册脚本接口函数
        ''' </summary>
        ''' <param name="instance">要注册的类的类型声明</param>
        ''' <param name="prefix">函数前缀</param>
        ''' <param name="toLower">是否转换函数名为小写</param>
        ''' <remarks></remarks>
        Protected Friend Shared Sub RegisterFunction(instance As Type, prefix As String, toLower As Boolean)
            Dim apiBase = instance.Assembly.CreateInstance(instance.Namespace & "." & instance.Name)
            For Each tmpMethod In instance.GetMethods
                ScriptCore.GetInstance.ScriptVm.RegisterFunction(prefix & "." & If(toLower, tmpMethod.Name.ToLower, tmpMethod.Name), apiBase, tmpMethod)
            Next
            MessageAPI.SendSync("[SYSTEM]SCRIPT_CONTENT_ADD")
        End Sub

    End Class

End Namespace
