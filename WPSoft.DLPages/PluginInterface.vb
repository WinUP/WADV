Imports System.Reflection
Imports WADV.AppCore.Plugin

Namespace PluginInterface

    Public Class Initlizer : Implements IInitialise

        Public Function Initialising() As Boolean Implements WADV.AppCore.Plugin.IInitialise.Initialising
            PageList.List.LoadPage()
            ScriptAPI.RunStringSync("api_pages={}")
            For Each tmpApiClass In (From tmpClass In Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Namespace = "WPSoft.DLPages.API" AndAlso tmpClass.IsClass AndAlso tmpClass.Name.LastIndexOf("API", StringComparison.Ordinal) = tmpClass.Name.Length - 3 Select tmpClass)
                Dim registerName = tmpApiClass.Name.Substring(0, tmpApiClass.Name.Length - 3).ToLower()
                ScriptAPI.RunStringSync("api_pages." + registerName + "={}")
                ScriptAPI.RegisterSync(tmpApiClass, "api_pages." + registerName)
            Next
            Return True
        End Function

    End Class

End Namespace
