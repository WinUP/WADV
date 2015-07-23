''' <summary>
''' 脚本引擎接口
''' </summary>
Public Interface IScriptEngine

    ''' <summary>
    ''' 初始化脚本引擎
    ''' </summary>
    Sub Initialise()
    ''' <summary>
    ''' 运行一个脚本文件
    ''' </summary>
    ''' <param name="filePath"></param>
    Sub RunFileAsync(filePath As String)
    ''' <summary>
    ''' 运行一个脚本文件
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    Function RunFile(filePath As String) As Object
    ''' <summary>
    ''' 运行一串脚本代码
    ''' </summary>
    ''' <param name="content"></param>
    Sub RunStringAsync(content As String)
    ''' <summary>
    ''' 运行一串脚本代码
    ''' </summary>
    ''' <param name="content"></param>
    ''' <returns></returns>
    Function RunString(content As String) As Object
    ''' <summary>
    ''' 设置脚本全局变量的值
    ''' </summary>
    ''' <param name="name">变量名</param>
    ''' <param name="value">目标值</param>
    Sub [Set](name As String, value As Object)
    ''' <summary>
    ''' 获取脚本全局变量的值
    ''' </summary>
    ''' <param name="name">变量名</param>
    ''' <returns></returns>
    Function [Get](name As String) As Object

    ''' <summary>
    ''' 释放脚本引擎的资源
    ''' </summary>
    Sub Dispose()
End Interface