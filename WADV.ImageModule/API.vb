Imports System.Windows.Controls
Imports WADV.AppCore.API
Imports WADV.ImageModule.Config
Imports WADV.ImageModule.ImageEffect

Namespace API

    Public Class ImageAPI

        Public Shared Function Show(fileName As String, effectName As String, duration As Integer, contentName As String) As Boolean
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

    Public Class TachieAPI

        Public Shared Function ShowRegular(fileName As String, effectName As String, duration As Integer, ease As Boolean, x As Double, y As Double, z As Double, width As Double, height As Double, effectParam() As Object) As Integer
            Dim id As Integer = Config.TachieConfig.AddTachie(fileName, x, y, width, height, z)
            Dim image = Config.TachieConfig.GetTachie(id)
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Name = effectName AndAlso tmpClass.Namespace = "WADV.ImageModule.TachieEffect" Select tmpClass
            If classList.Count < 1 Then Return False
            Dim effect As TachieEffect.BaseEffect = Activator.CreateInstance(classList.FirstOrDefault, New Object() {image, duration, ease, effectParam})
            Dim loopContent As New PluginInterface.TachieLoop(effect)
            LoopingAPI.AddLoop(loopContent)
            LoopingAPI.WaitLoop(loopContent)
            Return id
        End Function

        Public Shared Function Show(fileName As String, effectName As String, duration As Integer, ease As Boolean, x As Double, y As Double, z As Double, width As Double, height As Double, effectParam As LuaInterface.LuaTable) As Integer
            Dim params As New List(Of Object)
            For Each tmpItem In effectParam.Values
                params.Add(tmpItem)
            Next
            Return ShowRegular(fileName, effectName, duration, ease, x, y, z, width, height, params.ToArray)
        End Function

        Public Shared Sub Change(id As Integer, fileName As String, width As Double, height As Double)
            Config.TachieConfig.ChangeTachie(id, fileName, width, height)
        End Sub

        Public Shared Sub EffectRegular(id As Integer, effectName As String, duration As Integer, ease As Boolean, effectParam() As Object)
            Dim image = Config.TachieConfig.GetTachie(id)
            If image Is Nothing Then Exit Sub
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Name = effectName AndAlso tmpClass.Namespace = "WADV.ImageModule.TachieEffect" Select tmpClass
            If classList.Count < 1 Then Exit Sub
            Dim effect As TachieEffect.BaseEffect = Activator.CreateInstance(classList.FirstOrDefault, New Object() {image, duration, ease, effectParam})
            Dim loopContent As New PluginInterface.TachieLoop(effect)
            LoopingAPI.AddLoop(loopContent)
            LoopingAPI.WaitLoop(loopContent)
        End Sub

        Public Shared Sub EffectNowRegular(id As Integer, effectName As String, duration As Integer, ease As Boolean, effectParam() As Object)
            Dim image = Config.TachieConfig.GetTachie(id)
            If image Is Nothing Then Exit Sub
            Dim classList = From tmpClass In Reflection.Assembly.GetExecutingAssembly.GetTypes Where tmpClass.Name = effectName AndAlso tmpClass.Namespace = "WADV.ImageModule.TachieEffect" Select tmpClass
            If classList.Count < 1 Then Exit Sub
            Dim effect As TachieEffect.BaseEffect = Activator.CreateInstance(classList.FirstOrDefault, New Object() {image, duration, ease, effectParam})
            Dim loopContent As New PluginInterface.TachieLoop(effect)
            LoopingAPI.AddLoop(loopContent)
        End Sub

        Public Shared Sub Effect(id As Integer, effectName As String, duration As Integer, ease As Boolean, effectParam As LuaInterface.LuaTable)
            Dim params As New List(Of Object)
            For Each tmpItem In effectParam.Values
                params.Add(tmpItem)
            Next
            EffectRegular(id, effectName, duration, ease, params.ToArray)
        End Sub

        Public Shared Sub EffectNow(id As Integer, effectName As String, duration As Integer, ease As Boolean, effectParam As LuaInterface.LuaTable)
            Dim params As New List(Of Object)
            For Each tmpItem In effectParam.Values
                params.Add(tmpItem)
            Next
            EffectNowRegular(id, effectName, duration, ease, params.ToArray)
        End Sub

        Public Shared Sub Delete(id As Integer)
            Config.TachieConfig.DeleteTachie(id)
        End Sub

    End Class

End Namespace
