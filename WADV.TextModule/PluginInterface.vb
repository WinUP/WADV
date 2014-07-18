Imports WADV.AppCore
Imports System.Reflection
Imports System.Windows

Namespace PluginInterface

    Public Class Initialise : Implements Plugin.IInitialise

        Public Function StartInitialising() As Boolean Implements Plugin.IInitialise.StartInitialising
            Config.ModuleConfig.ReadConfigFile()
            Config.ModuleConfig.Ellipsis = False
            Config.ModuleConfig.Fast = False
            Return True
        End Function

    End Class

    Public Class Script : Implements Plugin.IScriptFunction

        Public Sub StartRegisting(ScriptVM As LuaInterface.Lua) Implements Plugin.IScriptFunction.StartRegisting
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.IsClass AndAlso tmpClass.Namespace = "WADV.TextModule.API" Select tmpClass
            Dim functionList() As MethodInfo
            Dim apiBase As Object
            Dim apiBaseName As String
            For Each tmpClass In classList
                apiBaseName = tmpClass.Name
                apiBase = tmpClass.Assembly.CreateInstance("WADV.TextModule.API." & apiBaseName)
                functionList = tmpClass.GetMethods
                For Each tmpMethod In functionList
                    ScriptVM.RegisterFunction(String.Format("TM_{0}_{1}", apiBaseName.Remove(apiBaseName.Length - 3), tmpMethod.Name), apiBase, tmpMethod)
                Next
            Next
        End Sub

    End Class

    Public Class CustomizedLoop : Implements Plugin.ICustomizedLoop

        Private effect As TextEffect.StandardEffect
        Private waitingCount As Integer = 0
        Private Delegate Sub UpdateUI(text As String)

        Private Sub UpdateUI1(text As String)
            Config.UIConfig.TextArea.Text = text
        End Sub


        Public Sub New(effect As TextEffect.StandardEffect)
            Me.effect = effect
        End Sub

        Public Function StartLooping() As Boolean Implements AppCore.Plugin.ICustomizedLoop.StartLooping
            Dim text As String = ""
            '快进状态
            If Config.ModuleConfig.Fast Then
                text = effect.GetNextString
                While Not effect.IsSentenceReadOver
                    text = effect.GetNextString
                End While
                AppCore.API.WindowAPI.GetWindowDispatcher.BeginInvoke(New UpdateUI(AddressOf UpdateUI1), text)
                If effect.IsReadOver Then Return False
                Return True
            End If
            '自动状态
            If Config.ModuleConfig.Auto Then
                If waitingCount > 0 Then
                    waitingCount -= 1
                    Return True
                End If
                text = effect.GetNextString
                AppCore.API.WindowAPI.GetWindowDispatcher.BeginInvoke(New UpdateUI(AddressOf UpdateUI1), text)
                If effect.IsReadOver Then Return False
                If effect.IsSentenceReadOver Then
                    waitingCount = Config.ModuleConfig.SetenceFrame
                    Return True
                End If
                waitingCount = Config.ModuleConfig.WordFrame
                Return True
            End If
            '手动状态
            If Not Config.ModuleConfig.Ellipsis AndAlso effect.IsSentenceReadOver AndAlso Not effect.IsReadOver Then Return True
            If effect.IsSentenceReadOver Then Config.ModuleConfig.Ellipsis = False
            text = effect.GetNextString
            If Config.ModuleConfig.Ellipsis Then
                While Not effect.IsSentenceReadOver
                    text = effect.GetNextString
                End While
            End If
            Config.ModuleConfig.Ellipsis = False
            AppCore.API.WindowAPI.GetWindowDispatcher.BeginInvoke(New UpdateUI(AddressOf UpdateUI1), text)
            If effect.IsReadOver Then Return False
            Return True
        End Function

    End Class

End Namespace