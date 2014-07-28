Imports System.Windows.Controls
Imports WADV.AppCore.API
Imports WADV.ImageModule.Config
Imports WADV.ImageModule.Effect

Namespace API

    Public Class ImageAPI

        Public Function ShowImage(fileName As String, duration As Integer, effectName As String, contentName As String) As Boolean
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Name = effectName AndAlso tmpClass.Namespace = "WADV.ImageModule.Effect" Select tmpClass
            If classList.Count < 1 Then Return False
            Dim effect As ImageEffect = Activator.CreateInstance(classList.FirstOrDefault, New Object() {fileName, duration})
            Dim content = WindowAPI.GetChildByName(Of Panel)(WindowAPI.GetWindow, contentName)
            If content Is Nothing Then Return False
            Dim loopContent As New PluginInterface.CustomizedLoop(effect, content)
            System.Diagnostics.Debug.WriteLine(DateTime.Now.Second)
            LoopingAPI.AddLoop(loopContent)
            LoopingAPI.WaitLoop(loopContent)
            System.Diagnostics.Debug.WriteLine(DateTime.Now.Second)
            Return True
        End Function

    End Class

End Namespace
