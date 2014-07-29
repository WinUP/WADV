Imports System.Windows.Controls
Imports WADV.AppCore.API
Imports WADV.ImageModule.Config
Imports WADV.ImageModule.ImageEffect

Namespace API

    Public Class ImageAPI

        Public Function ShowImage(fileName As String, duration As Integer, effectName As String, contentName As String) As Boolean
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Name = effectName AndAlso tmpClass.Namespace = "WADV.ImageModule.ImageEffect" Select tmpClass
            If classList.Count < 1 Then Return False
            Dim effect As ImageEffect.BaseEffect = Activator.CreateInstance(classList.FirstOrDefault, New Object() {fileName, duration})
            Dim content = WindowAPI.GetChildByName(Of Panel)(WindowAPI.GetWindow, contentName)
            If content Is Nothing Then Return False
            Dim loopContent As New PluginInterface.ImageLoop(effect, content)
            LoopingAPI.AddLoop(loopContent)
            LoopingAPI.WaitLoop(loopContent)
            Return True
        End Function

    End Class

End Namespace
