Imports System.Windows.Controls
Imports System.Windows

Namespace Effect

    Public Interface IEffect

        Function NextState() As Boolean

        Sub Render()

        Function GetAnswer() As String

    End Interface

    Public Class Initialiser
        ''' <summary>
        ''' 待实例化的图像效果列表
        ''' </summary>
        Protected Friend Shared EffectList As Dictionary(Of String, Type)

        ''' <summary>
        ''' 读取并缓存所有图像效果
        ''' </summary>
        Protected Friend Shared Sub LoadEffect()
            EffectList = New Dictionary(Of String, Type)
            EffectList.Add("BaseEffect", GetType(BaseEffect))
            Dim basePath As String = PathAPI.GetPath(AppCore.Path.PathFunction.PathType.Resource, "ChoiceEffect\")
            For Each file As String In System.IO.Directory.GetFiles(basePath, "*.dll")
                Dim assembly = System.Reflection.Assembly.LoadFrom(file).GetTypes()
                For Each type As Type In assembly
                    If type.GetInterface("IStyle") IsNot Nothing Then
                        EffectList.Add(type.Name, type)
                    End If
                Next
            Next
        End Sub
    End Class

    Public Class BaseEffect : Implements IEffect
        Protected choices() As TextBlock
        Protected waitTime As Integer
        Protected countBlock As TextBlock
        Protected initFinished As Boolean
        Private answer As String

        Public Sub New(choices() As TextBlock, wait As Integer, Optional count As TextBlock = Nothing)
            Me.choices = choices
            waitTime = wait
            countBlock = count
            initFinished = False
            For Each choice In choices
                AddHandler choice.MouseLeftButtonDown, Sub()
                                                           answer = choice.Text
                                                           MessageAPI.SendSync("CHOICE_USER_CLICK")
                                                       End Sub
            Next
            If countBlock IsNot Nothing AndAlso waitTime > -1 Then countBlock.Text = "∞"
            MessageAPI.SendSync("CHOICE_BASEEFFECT_DECLARE")
        End Sub

        Public Function GetAnswer() As String Implements IEffect.GetAnswer
            Return answer
        End Function

        Public Function NextState() As Boolean Implements IEffect.NextState
            If Not initFinished Then Return True
            If waitTime = -1 Then Return True
            waitTime -= 1
            If waitTime >= 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Overridable Sub Render() Implements IEffect.Render
            If initFinished Then
                If countBlock IsNot Nothing AndAlso waitTime > -1 Then
                    countBlock.Text = waitTime
                Else
                    Exit Sub
                End If
            Else
                initFinished = True
                MessageAPI.SendSync("CHOICE_BASEEFFECT_SHOWFINISH")
            End If
        End Sub

    End Class

End Namespace