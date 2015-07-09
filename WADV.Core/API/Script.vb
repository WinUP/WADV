Namespace API
    ''' <summary>
    ''' 脚本API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Script
        ''' <summary>
        ''' 注册脚本执行环境
        ''' </summary>
        ''' <param name="target">目标虚拟机</param>
        ''' <remarks></remarks>
        Public Sub Register(target As IScriptEngine)
            Config.ScriptEngine = target
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码
        ''' 异步方法|调用线程
        ''' </summary>
        ''' <param name="filePath">文件路径(Script目录下)</param>
        ''' <remarks></remarks>
        Public Sub RunFileAsync(filePath As String)
            If Config.ScriptEngine Is Nothing Then Exit Sub
            Config.ScriptEngine.RunFileAsync(filePath)
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="filePath">文件路径(Script目录下)</param>
        ''' <remarks></remarks>
        Public Function RunFile(filePath As String) As Object
            If Config.ScriptEngine Is Nothing Then Return Nothing
            Return Config.ScriptEngine.RunFile(filePath)
        End Function

        ''' <summary>
        ''' 执行一段字符串脚本
        ''' 异步方法|调用线程
        ''' </summary>
        ''' <param name="content">脚本内容</param>
        ''' <remarks></remarks>
        Public Sub RunStringAsync(content As String)
            If Config.ScriptEngine Is Nothing Then Exit Sub
            Config.ScriptEngine.RunStringAsync(content)
        End Sub

        ''' <summary>
        ''' 执行一段字符串脚本
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="content">脚本内容</param>
        ''' <remarks></remarks>
        Public Function RunString(content As String) As Object
            If Config.ScriptEngine Is Nothing Then Return Nothing
            Return Config.ScriptEngine.RunString(content)
        End Function

        ''' <summary>
        ''' 设置脚本全局变量的值
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">变量内容(字符串形式)</param>
        ''' <remarks></remarks>
        Public Sub [Set](name As String, value As Object)
            If Config.ScriptEngine Is Nothing Then Exit Sub
            Config.ScriptEngine.Set(name, value)
        End Sub

        ''' <summary>
        ''' 获取脚本全局变量的值
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Function [Get](name As String) As Object
            If Config.ScriptEngine Is Nothing Then Return Nothing
            Return Config.ScriptEngine.Get(name)
        End Function
    End Module
End Namespace
