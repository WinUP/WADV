Imports WADV.Core.Script

Namespace API
    ''' <summary>
    ''' 脚本API
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Script
        ''' <summary>
        ''' 第二章第一合奏：注册脚本执行环境<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="target">目标虚拟机</param>
        ''' <remarks></remarks>
        Public Shared Sub Chorus80_Register(target As IScriptEngine)
            Configuration.System.ScriptEngine?.Dispose()
            Configuration.System.ScriptEngine = target
            target.Initialise()
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码<br></br>
        ''' 属性：<br></br>
        '''  异步 | NORMAL
        ''' </summary>
        ''' <param name="filePath">文件路径(Script目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub RunFileAsync(filePath As String)
            If Configuration.System.ScriptEngine Is Nothing Then Exit Sub
            Configuration.System.ScriptEngine.RunFileAsync(filePath)
        End Sub

        ''' <summary>
        ''' 执行脚本文件中的所有代码<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="filePath">文件路径(Script目录下)</param>
        ''' <remarks></remarks>
        Public Shared Function RunFile(filePath As String) As Object
            Return Configuration.System.ScriptEngine?.RunFile(filePath)
        End Function

        ''' <summary>
        ''' 执行一段字符串脚本<br></br>
        ''' 属性：<br></br>
        '''  异步 | NORMAL
        ''' </summary>
        ''' <param name="content">脚本内容</param>
        ''' <remarks></remarks>
        Public Shared Sub RunStringAsync(content As String)
            Configuration.System.ScriptEngine?.RunStringAsync(content)
        End Sub

        ''' <summary>
        ''' 执行一段字符串脚本<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="content">脚本内容</param>
        ''' <remarks></remarks>
        Public Shared Function RunString(content As String) As Object
            Return Configuration.System.ScriptEngine?.RunString(content)
        End Function

        ''' <summary>
        ''' 设置脚本全局变量的值<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <param name="value">变量内容(字符串形式)</param>
        ''' <remarks></remarks>
        Public Shared Sub [Set](name As String, value As Object)
            Configuration.System.ScriptEngine?.Set(name, value)
        End Sub

        ''' <summary>
        ''' 获取脚本全局变量的值<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="name">变量名</param>
        ''' <returns>变量内容</returns>
        ''' <remarks></remarks>
        Public Shared Function [Get](name As String) As Object
            Return Configuration.System.ScriptEngine?.Get(name)
        End Function

        ''' <summary>
        ''' 注册脚本函数<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="target">要注册的内容集合</param>
        Public Shared Sub RegisterField(target As Field)
            Configuration.System.ScriptEngine.Register(target)
        End Sub

        ''' <summary>
        ''' 获取脚本虚拟机<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function VM() As IScriptEngine
            Return Configuration.System.ScriptEngine
        End Function
    End Class
End Namespace
