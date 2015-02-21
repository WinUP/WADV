Imports System.Threading
Imports Neo.IronLua

Namespace AppCore.API

    ''' <summary>
    ''' 脚本API类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class ScriptAPI

        ''' <summary>
        ''' 显示提示信息
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="content">内容</param>
        ''' <param name="title">标题</param>
        ''' <remarks></remarks>
        Public Shared Sub ShowSync(content As String, title As String)
            MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Information)
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码
        ''' 异步方法|调用线程
        ''' </summary>
        ''' <param name="filePath">文件路径(从Script目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub RunFileAsync(filePath As String)
            Dim tmpThread As New Thread(CType(Sub() ScriptCore.GetInstance.RunFile(PathFunction.GetFullPath(PathType.Script, filePath)), ThreadStart))
            tmpThread.Name = "脚本文件执行线程"
            tmpThread.IsBackground = True
            tmpThread.Priority = ThreadPriority.Normal
            MessageService.GetInstance.SendMessage("[SYSTEM]ASYNC_SCRIPTFILE_STANDBY")
            tmpThread.Start()
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="filePath">文件路径(从Script目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub RunFileSync(filePath As String)
            ScriptCore.GetInstance.RunFile(PathFunction.GetFullPath(PathType.Script, filePath))
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码并返回结果
        ''' </summary>
        ''' <param name="filePath">文件路径(从Script目录下开始)</param>
        ''' <returns>执行结果</returns>
        ''' <remarks></remarks>
        Public Shared Function RunFileWithAnswer(filePath As String) As LuaResult
            Return ScriptCore.GetInstance.RunFile(PathFunction.GetFullPath(PathType.Script, filePath))
        End Function

        ''' <summary>
        ''' 执行一段字符串脚本
        ''' 异步方法|调用线程
        ''' </summary>
        ''' <param name="content">脚本内容</param>
        ''' <remarks></remarks>
        Public Shared Sub RunStringAsync(content As String)
            Dim tmpThread As New Thread(CType(Sub() ScriptCore.GetInstance.RunString(content), ThreadStart))
            tmpThread.IsBackground = True
            tmpThread.Priority = ThreadPriority.Normal
            MessageService.GetInstance.SendMessage("[SYSTEM]ASYNC_SCRIPT_STANDBY")
            tmpThread.Start(content)
        End Sub

        ''' <summary>
        ''' 执行一段字符串脚本
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="content">脚本内容</param>
        ''' <remarks></remarks>
        Public Shared Sub RunStringSync(content As String)
            ScriptCore.GetInstance.RunString(content)
        End Sub

        ''' <summary>
        ''' 执行一段字符串脚本并返回结果
        ''' </summary>
        ''' <param name="content">脚本内容</param>
        ''' <returns>执行结果</returns>
        ''' <remarks></remarks>
        Public Shared Function RunStringWithAnswer(content As String) As LuaResult
            Return ScriptCore.GetInstance.RunString(content)
        End Function

        ''' <summary>
        ''' 设置脚本全局变量的值(该变量内容不是字符串)
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">变量内容(字符串形式)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetSync(name As String, value As Object)
            ScriptCore.GetInstance.Environment(name) = value
        End Sub

        ''' <summary>
        ''' 获取脚本全局变量的值
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetSync(name As String) As Object
            Return ScriptCore.GetInstance.Environment(name)
        End Function

        ''' <summary>
        ''' 注册函数到脚本主机的指定表中
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="tableName">函数所在的表名</param>
        ''' <param name="name">函数的名称</param>
        ''' <param name="content">函数委托</param>
        ''' <param name="declareTable">是否声明或重新声明表</param>
        ''' <remarks></remarks>
        Public Shared Sub RegisterInTableSync(tableName As String, name As String, content As [Delegate], Optional declareTable As Boolean = False)
            If declareTable Then ScriptCore.GetInstance.Environment(tableName) = New LuaTable
            ScriptCore.GetInstance.Environment(tableName)(name) = content
        End Sub

        ''' <summary>
        ''' 注册函数到脚本主机
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="name">函数的名称</param>
        ''' <param name="content">函数委托</param>
        ''' <remarks></remarks>
        Public Shared Sub RegisterSync(name As String, content As [Delegate])
            ScriptCore.GetInstance.Environment(name) = content
        End Sub

        ''' <summary>
        ''' 获取游戏脚本主机对象
        ''' </summary>
        ''' <returns>脚本主机</returns>
        ''' <remarks></remarks>
        Public Shared Function GetVm() As Lua
            Return ScriptCore.GetInstance.Vm
        End Function

        ''' <summary>
        ''' 获取游戏脚本执行环境对象
        ''' </summary>
        ''' <returns>脚本执行环境</returns>
        ''' <remarks></remarks>
        Public Shared Function GetEnv() As LuaGlobal
            Return ScriptCore.GetInstance.Environment
        End Function

    End Class

End Namespace