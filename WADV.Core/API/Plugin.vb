Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.CodeDom.Compiler
Imports System.Windows
Imports Microsoft.CSharp
Imports WADV.Core.GameSystem

Namespace API
    ''' <summary>
    ''' 插件API
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Plugin
        ''' <summary>
        ''' 加载一个插件<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL | PLUGIN_ADD
        ''' </summary>
        ''' <param name="filePath">插件文件路径(Plugin目录下)</param>
        ''' <remarks></remarks>
        Public Shared Sub Add(filePath As String)
            PluginFunction.AddPlugin(filePath)
        End Sub

        ''' <summary>
        ''' 添加一个插件加载接收器<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL
        ''' </summary>
        ''' <param name="receiver">目标接收器</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Listen(receiver As PluginInterface.IPluginLoadReceiver) As Boolean
            Return ReceiverList.PluginLoadReceiverList.Add(receiver)
        End Function

        ''' <summary>
        ''' 编译一个代码文件<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL | CODE_COMPILE<br></br>
        ''' 异常:<br></br>
        ''' CompileTargetFormatIllegalException<br></br>
        ''' 其他编译过程中出现的异常（这部分异常不会抛出但会显示提示信息）
        ''' </summary>
        ''' <param name="filePath">文件路径(Resource目录下)</param>
        ''' <returns>编译得到的程序集</returns>
        ''' <remarks></remarks>
        Public Shared Function Compile(filePath As String, Optional options As String = "") As Reflection.Assembly
            Dim codeProvider As CodeDomProvider
            Dim codeFile = New FileInfo(PathFunction.GetFullPath(PathType.Resource, filePath))
            If codeFile.Extension.ToLower = ".vb" Then
                codeProvider = New VBCodeProvider
            ElseIf codeFile.Extension.ToLower = ".cs" Then
                codeProvider = New CSharpCodeProvider
            Else
                Throw New Exception.CompileTargetFormatIllegalException
            End If
            Dim param As New CompilerParameters
            param.GenerateExecutable = False
            param.GenerateInMemory = True
            param.IncludeDebugInformation = False
            Dim asList As New XmlDocument
            asList.Load(codeFile.FullName & ".xml")
            For Each clrAssemblies As XmlNode In asList.SelectNodes("/assemblies/clr")
                param.ReferencedAssemblies.Add(clrAssemblies.InnerXml)
            Next
            For Each gameAssemblies As XmlNode In asList.SelectNodes("/assemblies/game")
                param.ReferencedAssemblies.Add(Path.Combine(PathType.Plugin, gameAssemblies.InnerXml))
            Next
            For Each ownAssemblies As XmlNode In asList.SelectNodes("/assemblies/own")
                param.ReferencedAssemblies.Add(Path.Combine(PathType.Game, ownAssemblies.InnerXml))
            Next
            param.CompilerOptions = options
            Dim result = codeProvider.CompileAssemblyFromFile(param, codeFile.FullName)
            If result.Errors.HasErrors Then
                Dim errorString As New StringBuilder
                For Each tmpError As CompilerError In result.Errors
                    errorString.Append(Environment.NewLine & tmpError.ErrorText)
                Next
                Throw New Exception.CompileFailedException(filePath)
            End If
            Message.Send("[SYSTEM]CODE_COMPILE")
            Return result.CompiledAssembly
        End Function

        ''' <summary>
        ''' 加载一个动态链接库<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL | ASSEMBLE_LOAD_STANDBY/ASSEMBLE_LOAD_FINISH<br></br>
        ''' 异常：<br></br>
        '''  FileNotFoundException
        ''' </summary>
        ''' <param name="filePath">文件路径(主目录下)</param>
        ''' <returns>编译得到的程序集</returns>
        ''' <remarks></remarks>
        Public Shared Function Load(filePath As String) As Reflection.Assembly
            Dim file = PathFunction.GetFullPath(PathType.Game, filePath)
            If Not My.Computer.FileSystem.FileExists(file) Then
                Throw New FileNotFoundException("找不到要载入的动态链接库文件")
            End If
            Message.Send("[SYSTEM]ASSEMBLE_LOAD_STANDBY")
            Dim target = Reflection.Assembly.LoadFrom(file)
            Message.Send("[SYSTEM]ASSEMBLE_LOAD_FINISH")
            Return target
        End Function

        ''' <summary>
        ''' 根据类型完整名称获取类型实例<br></br>
        ''' 属性：<br></br>
        '''  同步 | NORMAL<br></br>
        ''' 异常：<br></br>
        '''  任何System.Type.GetType()可能引发的异常
        ''' </summary>
        ''' <param name="name">类型的完整名称</param>
        ''' <param name="param">要获得实例所需的参数</param>
        ''' <returns></returns>
        Public Shared Function Create(name As String, ParamArray param As Object()) As Object
            Return Activator.CreateInstance(Type.GetType(name, True, False), param)
        End Function
    End Class
End Namespace