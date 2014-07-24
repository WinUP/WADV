﻿Imports System.Windows.Controls
Imports WADV.AppCore.API
Imports WADV.ImageModule.Config
Imports WADV.ImageModule.Effect

Namespace API

    Public Class ImageAPI

        Public Function ShowImage(fileName As String, effectName As String, duration As Integer, contentName As String) As Boolean
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Name = effectName AndAlso tmpClass.Namespace = "WADV.ImageModule.Effect" Select tmpClass
            If classList.Count < 1 Then Return False
            Dim effect As ImageEffect = Activator.CreateInstance(classList.FirstOrDefault, New Object() {fileName, duration})
            Dim content = ResourceAPI.GetChildByName(Of Panel)(WindowAPI.GetWindow, contentName)
            If content Is Nothing Then Return False
            Dim loopContent As New PluginInterface.CustomizedLoop(effect, content)
            LoopAPI.AddCustomizedLoop(loopContent)
            LoopAPI.WaitCustomizedLoop(loopContent)
            Return True
        End Function

        Public Function ShowImageNow(fileName As String, effectName As String, duration As Integer, contentName As String) As Boolean
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Name = effectName AndAlso tmpClass.Namespace = "WADV.ImageModule.Effect" Select tmpClass
            If classList.Count < 1 Then Return False
            Dim effect As ImageEffect = Activator.CreateInstance(classList.FirstOrDefault, New Object() {fileName, duration})
            Dim content = ResourceAPI.GetChildByName(Of Panel)(WindowAPI.GetMainGrid, contentName)
            If content Is Nothing Then Return False
            Dim loopContent As New PluginInterface.CustomizedLoop(effect, content)
            LoopAPI.AddCustomizedLoop(loopContent)
            Return True
        End Function

    End Class

End Namespace
