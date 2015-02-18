Imports System.Threading

Namespace AppCore.API

    ''' <summary>
    ''' 脚本API类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ScriptAPI

        ''' <summary>
        ''' 显示提示信息
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="content">内容</param>
        ''' <param name="title">标题</param>
        ''' <remarks></remarks>
        Public Shared Sub ShowMessageSync(content As String, title As String)
            MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Information)
            MessageAPI.SendSync("SCRIPT_MESSAGE_SHOW")
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码
        ''' 异步方法|调用线程
        ''' </summary>
        ''' <param name="fileName">文件路径(从Script目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub RunFileAsync(fileName As String)
            Dim tmpThread As New Thread(CType(Sub() ScriptCore.GetInstance.RunFile(PathAPI.GetPath(PathType.Script, fileName)), ThreadStart))
            tmpThread.Name = "脚本文件执行线程"
            tmpThread.IsBackground = True
            tmpThread.Priority = ThreadPriority.Normal
            tmpThread.Start()
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="filename">文件路径(从Script目录下开始)</param>
        ''' <remarks></remarks>
        Public Shared Sub RunFileSync(filename As String)
            ScriptCore.GetInstance.RunFile(PathAPI.GetPath(PathType.Script, filename))
        End Sub

        ''' <summary>
        ''' 执行一段字符串脚本
        ''' 异步方法|调用线程
        ''' </summary>
        ''' <param name="content">脚本代码内容</param>
        ''' <remarks></remarks>
        Public Shared Sub RunStrngAsync(content As String)
            Dim tmpThread As New Thread(CType(Sub() ScriptCore.GetInstance.RunStrng(content), ThreadStart))
            tmpThread.IsBackground = True
            tmpThread.Priority = ThreadPriority.Normal
            tmpThread.Start(content)
        End Sub

        ''' <summary>
        ''' 执行一段字符串脚本
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="content">脚本代码内容</param>
        ''' <remarks></remarks>
        Public Shared Sub RunStringSync(content As String)
            ScriptCore.GetInstance.RunStrng(content)
        End Sub

        ''' <summary>
        ''' 执行一个存在的脚本函数
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="functionName">函数名</param>
        ''' <param name="params">参数列表</param>
        ''' <returns>返回值列表</returns>
        ''' <remarks></remarks>
        Public Shared Function RunFunction(functionName As String, params() As Object) As Object()
            Return ScriptCore.GetInstance.RunFunction(functionName, params)
        End Function

        ''' <summary>
        ''' 设置脚本全局变量的值(该变量内容不是字符串)
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">变量内容(字符串形式)</param>
        ''' <remarks></remarks>
        Public Shared Sub SetSync(name As String, value As String)
            RunStringSync(String.Format("{0}={1}", name, value))
        End Sub

        ''' <summary>
        ''' 设置脚本全局变量的值(该变量内容是字符串)
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">变量内容</param>
        ''' <remarks></remarks>
        Public Shared Sub SetStringSync(name As String, value As String)
            SetSync(name, """" & value & """")
        End Sub

        ''' <summary>
        ''' 获取脚本全局变量的值
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function GetSync(name As String) As Object
            Return ScriptCore.GetInstance.GetVariable(name)
        End Function

        ''' <summary>
        ''' 获取脚本全局变量(Table类型)中某个项的值
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="tableName">表名</param>
        ''' <param name="key">键</param>
        ''' <returns>值</returns>
        ''' <remarks></remarks>
        Public Shared Function GetInTableSync(tableName As String, key As String) As Object
            Return ScriptCore.GetInstance.GetVariableInTable(tableName, key)
        End Function

        ''' <summary>
        ''' 使用预定义规则注册脚本函数
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="instance">要注册的类的类型声明</param>
        ''' <param name="prefix">函数前缀</param>
        ''' <param name="toLower">是否转换函数名为小写</param>
        Public Shared Sub RegisterSync(instance As Type, prefix As String, Optional toLower As Boolean = False)
            ScriptFunction.RegisterFunction(instance, prefix, toLower)
        End Sub

        ''' <summary>
        ''' 获取游戏脚本主机对象
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <returns>脚本主机</returns>
        ''' <remarks></remarks>
        Public Shared Function GetVm() As NLua.Lua
            Return ScriptCore.GetInstance.ScriptVm
        End Function

    End Class

End Namespace