Imports WADV.AppCore
Imports System.Reflection
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace PluginInterface

    Public Class Initialise : Implements Plugin.IInitialise

        Public Function StartInitialising() As Boolean Implements Plugin.IInitialise.StartInitialising
            Config.ModuleConfig.ReadConfigFile()
            Return True
        End Function

    End Class

    Public Class Script : Implements Plugin.IScriptFunction

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements AppCore.Plugin.IScriptFunction.StartRegisting
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.IsClass AndAlso tmpClass.Namespace = "WADV.ImageModule.API" Select tmpClass
            Dim functionList() As MethodInfo
            Dim apiBase As Object
            Dim apiBaseName As String
            For Each tmpClass In classList
                apiBaseName = tmpClass.Name
                apiBase = tmpClass.Assembly.CreateInstance("WADV.ImageModule.API." & apiBaseName)
                functionList = tmpClass.GetMethods
                For Each tmpMethod In functionList
                    ScriptVM.RegisterFunction(String.Format("IM_{0}_{1}", apiBaseName.Remove(apiBaseName.Length - 3), tmpMethod.Name), apiBase, tmpMethod)
                Next
            Next
        End Sub
    End Class

    Public Class CustomizedLoop : Implements Plugin.ICustomizedLoop
        Private effect As Effect.ImageEffect
        Private content As Panel
        Private brush As ImageBrush

        Public Sub New(effect As Effect.ImageEffect, content As Panel)
            Me.effect = effect
            Me.content = content
            content.Dispatcher.Invoke(Sub()
                                          brush = New ImageBrush
                                          content.Background = brush
                                      End Sub)
        End Sub

        Public Function StartLooping() As Boolean Implements Plugin.ICustomizedLoop.StartLooping
            Dim image = effect.GetNextImageState
            content.Dispatcher.Invoke(Sub() brush.ImageSource = image)
            GC.Collect()
            If effect.IsEffectComplete Then Return False
            Return True
        End Function

    End Class

End Namespace
