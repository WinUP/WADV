Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.CodeDom.Compiler
Imports System.Windows
Imports Microsoft.CSharp

Namespace API
    ''' <summary>
    ''' 插件API
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Plugin
        ''' <summary>
        ''' 加载一个插件
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="filePath">插件文件路径(Plugin目录下)</param>
        ''' <remarks></remarks>
        Public Sub Add(filePath As String)
            PluginFunction.AddPlugin(filePath)
        End Sub

        ''' <summary>
        ''' 添加一个插件加载接收器
        ''' </summary>
        ''' <param name="receiver">目标接收器</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Listen(receiver As PluginInterface.IPluginLoadReceiver) As Boolean
            Return ReceiverList.PluginLoadReceiverList.Add(receiver)
        End Function

        ''' <summary>
        ''' 编译一个代码文件
        ''' </summary>
        ''' <param name="filePath">文件路径(Resource目录下)</param>
        ''' <returns>编译得到的程序集</returns>
        ''' <remarks></remarks>
        Public Function Compile(filePath As String, Optional options As String = "") As Reflection.Assembly
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
                MessageBox.Show(errorString.ToString, "编译" & filePath & "时没有通过", MessageBoxButton.OK, MessageBoxImage.Error)
                Return Nothing
            End If
            Message.Send("[SYSTEM]CODE_COMPILE")
            Return result.CompiledAssembly
        End Function

        ''' <summary>
        ''' 加载一个动态链接库
        ''' 同步方法|调用线程
        ''' </summary>
        ''' <param name="filePath">文件路径(主目录下)</param>
        ''' <returns>编译得到的程序集</returns>
        ''' <remarks></remarks>
        Public Function Load(filePath As String) As Reflection.Assembly
            Dim file = PathFunction.GetFullPath(PathType.Game, filePath)
            If Not My.Computer.FileSystem.FileExists(file) Then
                Throw New FileNotFoundException("找不到要载入的动态链接库文件")
            End If
            Message.Send("[SYSTEM]ASSEMBLE_LOAD_STANDBY")
            Return Reflection.Assembly.LoadFrom(file)
        End Function
    End Module
End Namespace