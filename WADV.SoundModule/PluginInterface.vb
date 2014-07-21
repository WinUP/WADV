Imports WADV.AppCore
Imports System.Reflection

Namespace PluginInterface

    Public Class Initialise : Implements AppCore.Plugin.IInitialise

        Public Function StartInitialising() As Boolean Implements Plugin.IInitialise.StartInitialising
            Config.SoundConfig.ReadConfigFile()
            Return True
        End Function

    End Class

    Public Class Script : Implements Plugin.IScriptFunction

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements Plugin.IScriptFunction.StartRegisting
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.IsClass AndAlso tmpClass.Namespace = "WADV.SoundModule.API" Select tmpClass
            Dim functionList() As MethodInfo
            Dim apiBase As Object
            Dim apiBaseName As String
            For Each tmpClass In classList
                apiBaseName = tmpClass.Name
                apiBase = tmpClass.Assembly.CreateInstance("WADV.SoundModule.API." & apiBaseName)
                functionList = tmpClass.GetMethods
                For Each tmpMethod In functionList
                    ScriptVM.RegisterFunction(String.Format("SM_{0}_{1}", apiBaseName.Remove(apiBaseName.Length - 3), tmpMethod.Name), apiBase, tmpMethod)
                Next
            Next
        End Sub

    End Class

    Public Class LoopContent : Implements AppCore.Plugin.ILoop

        Public Sub StartLooping() Implements AppCore.Plugin.ILoop.StartLooping
            For Each soundContent In Config.SoundList.GetList
                soundContent.Content.Dispatcher.Invoke(Sub()
                                                           If soundContent.Content.Position = soundContent.Content.NaturalDuration.TimeSpan Then
                                                               If (Not soundContent.Cycle) OrElse (soundContent.Cycle AndAlso soundContent.CycleCount = 0) Then
                                                                   Config.SoundList.DeleteSound(soundContent)
                                                               End If
                                                               If soundContent.Cycle AndAlso soundContent.CycleCount = -1 Then
                                                                   soundContent.Content.Position = New TimeSpan(0)
                                                                   soundContent.Content.Play()
                                                               End If
                                                               soundContent.CycleCount -= 1
                                                               soundContent.Content.Position = New TimeSpan(0)
                                                               soundContent.Content.Play()
                                                           End If
                                                       End Sub)
            Next
        End Sub

    End Class

End Namespace