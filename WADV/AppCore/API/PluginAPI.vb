Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.CodeDom.Compiler
Imports Microsoft.CSharp

Namespace AppCore.API

    ''' <summary>
    ''' 插件API类
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class PluginAPI

        ''' <summary>
        ''' 加载一个插件
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="fileName">插件文件路径(从Plugin目录下开始)</param>
        ''' <returns>是否加载成功</returns>
        ''' <remarks></remarks>
        Public Shared Function Add(fileName As String) As Boolean
            Return PluginFunction.AddPlugin(PathAPI.GetPath(PathType.Plugin, fileName))
        End Function

        ''' <summary>
        ''' 编译一个代码文件
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="fileName">文件路径(从Resource目录下开始)</param>
        ''' <returns>编译得到的程序集</returns>
        ''' <remarks></remarks>
        Public Shared Function Compile(fileName As String, Optional options As String = "") As Reflection.Assembly
            Dim codeProvider As CodeDomProvider
            Dim codeFile = New FileInfo(PathAPI.GetPath(PathType.Resource, fileName))
            If codeFile.Extension.ToLower = ".vb" Then
                codeProvider = New VBCodeProvider
            ElseIf codeFile.Extension.ToLower = ".cs" Then
                codeProvider = New CSharpCodeProvider
            Else
                Throw New FileFormatException("目前只能编译VB.NET和CSharp的代码文件，并且目标.NET Framework版本不能高于4.0")
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
                param.ReferencedAssemblies.Add(PathAPI.GetPath(PathType.Plugin, gameAssemblies.InnerXml))
            Next
            For Each ownAssemblies As XmlNode In asList.SelectNodes("/assemblies/own")
                param.ReferencedAssemblies.Add(PathAPI.GetPath(PathType.Game, ownAssemblies.InnerXml))
            Next
            param.CompilerOptions = options
            Dim result = codeProvider.CompileAssemblyFromFile(param, codeFile.FullName)
            If result.Errors.HasErrors Then
                Dim errorString As New StringBuilder
                For Each tmpError As CompilerError In result.Errors
                    errorString.Append(Environment.NewLine & tmpError.ErrorText)
                Next
                MessageBox.Show("编译" & fileName & "时没有通过：" & Environment.NewLine & errorString.ToString, "错误", MessageBoxButton.OK, MessageBoxImage.Error)
                Return Nothing
            End If
            MessageAPI.SendSync("[SYSTEM]CODE_COMPILE")
            Return result.CompiledAssembly
        End Function

        ''' <summary>
        ''' 加载一个动态链接库
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="fileName">文件路径(从游戏主目录下开始)</param>
        ''' <returns>编译得到的程序集</returns>
        ''' <remarks></remarks>
        Public Shared Function Load(fileName As String) As Reflection.Assembly
            Dim filePath = PathAPI.GetPath(PathType.Game, fileName)
            If Not My.Computer.FileSystem.FileExists(filePath) Then
                Throw New FileNotFoundException("找不到要载入的动态链接库文件")
            End If
            MessageAPI.SendSync("[SYSTEM]ASSEMBLE_LOAD_STANDBY")
            Return Reflection.Assembly.LoadFrom(filePath)
        End Function

    End Class

End Namespace