Imports WADV
Imports System.Reflection

Namespace PluginInterface

    Public Class Script : Implements AppCore.Plugin.IScriptFunction

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements AppCore.Plugin.IScriptFunction.StartRegisting
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.IsClass AndAlso tmpClass.Namespace = "WPSoft.AVGCore.API" Select tmpClass
            Dim functionList() As MethodInfo
            Dim apiBase As Object
            Dim apiBaseName As String
            For Each tmpClass In classList
                apiBaseName = tmpClass.Name
                apiBase = tmpClass.Assembly.CreateInstance("WPSoft.AVGCore.API." & apiBaseName)
                functionList = tmpClass.GetMethods
                For Each tmpMethod In functionList
                    ScriptVM.RegisterFunction(String.Format("AVG_{0}_{1}", apiBaseName.Remove(apiBaseName.Length - 3), tmpMethod.Name), apiBase, tmpMethod)
                Next
            Next
        End Sub

    End Class

    Public Class Initialise : Implements AppCore.Plugin.IInitialise

        Public Function StartInitialising() As Boolean Implements AppCore.Plugin.IInitialise.StartInitialising
            AppCore.API.WindowAPI.LoadElement(AppCore.API.WindowAPI.GetMainGrid, "main1.xaml", Nothing)
            AppCore.API.WindowAPI.LoadElement(AppCore.API.WindowAPI.GetMainGrid, "main2.xaml", Nothing)
            AppCore.API.WindowAPI.ChangeWindowResizeMod(False, Nothing)
            AppCore.API.WindowAPI.ChangeBackgroundColorByHex("#000000", Nothing)
            AppCore.API.WindowAPI.ChangeWindowIcon("icon.ico", Nothing)
            Return True
        End Function

    End Class

End Namespace