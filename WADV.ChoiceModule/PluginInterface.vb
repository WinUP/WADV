Imports System.Reflection

Namespace PluginInterface

    Public Class Script : Implements AppCore.Plugin.IScriptFunction

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements AppCore.Plugin.IScriptFunction.StartRegisting
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.IsClass AndAlso tmpClass.Namespace = "WADV.ChoiceModule.API" Select tmpClass
            Dim functionList() As MethodInfo
            Dim apiBase As Object
            Dim apiBaseName As String
            For Each tmpClass In classList
                apiBaseName = tmpClass.Name
                apiBase = tmpClass.Assembly.CreateInstance("WADV.ChoiceModule.API." & apiBaseName)
                functionList = tmpClass.GetMethods
                For Each tmpMethod In functionList
                    ScriptVM.RegisterFunction(String.Format("CM_{0}_{1}", apiBaseName.Remove(apiBaseName.Length - 3), tmpMethod.Name), apiBase, tmpMethod)
                Next
            Next
        End Sub

    End Class

    Public Class CustomizedLoop : Implements AppCore.Plugin.ICustomizedLoop

        Private Style As Effect.StandardEffect

        Public Sub New(style As Effect.StandardEffect)
            Me.Style = style
        End Sub

        Public Function StartLooping() As Boolean Implements AppCore.Plugin.ICustomizedLoop.StartLooping
            Dim returnData = Style.GetNextUIStyle
            If Config.DataConfig.Choice <> "" Then Return False
            If Not returnData AndAlso Config.DataConfig.Choice = "" Then Return False
            Return True
        End Function

    End Class

End Namespace