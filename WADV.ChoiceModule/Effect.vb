Imports System.Reflection
Imports System.Windows.Controls
Imports System.Windows

Namespace Effect

    Public Interface IShowEffect

        Sub Render()

        Sub Wait()

    End Interface

    Public Interface IHideEffect

        Sub Render()

        Sub Wait()

    End Interface

    Public Interface IProgressEffect

        Function Logic() As Boolean

        Sub Render()

        Function GetAnswer() As String

    End Interface

    Friend NotInheritable Class Initialiser
        ''' <summary>
        ''' 待实例化的图像效果列表
        ''' </summary>
        Friend Shared ShowEffectList, HideEffectList, ProgressEffectList As Dictionary(Of String, Type)

        ''' <summary>
        ''' 读取并缓存所有图像效果
        ''' </summary>
        Friend Shared Sub LoadEffect()
            ShowEffectList = New Dictionary(Of String, Type)
            HideEffectList = New Dictionary(Of String, Type)
            ProgressEffectList = New Dictionary(Of String, Type)
            ShowEffectList.Add("BaseShow", GetType(BaseShow))
            HideEffectList.Add("BaseHide", GetType(BaseHide))
            ProgressEffectList.Add("BaseProgress", GetType(BaseProgress))
            Dim basePath As String = PathAPI.GetPath(PathType.Resource, "ChoiceEffect\")
            For Each tmpType In From assemble In (IO.Directory.GetFiles(basePath, "*.dll").Select(Function(file) Assembly.LoadFrom(file)))
                                Select types = assemble.GetTypes
                                From tmpType1 In types Select tmpType1
                If tmpType.GetInterface("IShowEffect") IsNot Nothing Then ShowEffectList.Add(tmpType.Name, tmpType)
                If tmpType.GetInterface("IHideEffect") IsNot Nothing Then HideEffectList.Add(tmpType.Name, tmpType)
                If tmpType.GetInterface("IProgressEffect") IsNot Nothing Then ProgressEffectList.Add(tmpType.Name, tmpType)
            Next
            MessageAPI.SendSync("[CHOICE]INIT_EFFECT_FINISH")
        End Sub

    End Class

    Public Class BaseShow : Implements IShowEffect
        Protected ReadOnly Choices() As Button
        Protected IsOver As Boolean

        Public Sub New(choices() As Button)
            Me.Choices = choices
        End Sub

        Public Overridable Sub Render() Implements IShowEffect.Render
            IsOver = True
            SendMessage()
        End Sub

        Public Overridable Sub Wait() Implements IShowEffect.Wait
            While True
                MessageAPI.WaitSync("[CHOICE]SHOW_FINISH")
                If IsOver Then Exit While
            End While
        End Sub

        Protected Sub SendMessage()
            MessageAPI.SendSync("[CHOICE]SHOW_FINISH")
        End Sub

    End Class

    Public Class BaseHide : Implements IHideEffect
        Protected ReadOnly Choices() As Button
        Protected IsOver As Boolean

        Public Sub New(choices() As Button)
            Me.Choices = choices
        End Sub

        Public Overridable Sub Render() Implements IHideEffect.Render
            For Each choice In Choices
                choice.Visibility = Visibility.Hidden
            Next
            IsOver = True
            SendMessage()
        End Sub

        Public Overridable Sub Wait() Implements IHideEffect.Wait
            While True
                MessageAPI.WaitSync("[CHOICE]HIDE_FINISH")
                If IsOver Then Exit While
            End While
        End Sub

        Protected Sub SendMessage()
            MessageAPI.SendSync("[CHOICE]HIDE_FINISH")
        End Sub

    End Class

    Public Class BaseProgress : Implements IProgressEffect
        Protected ReadOnly Choices() As Button
        Protected WaitFrame As Integer
        Private _answer As String

        Public Sub New(choices() As Button, waitFrame As Integer)
            Me.Choices = choices
            Me.WaitFrame = waitFrame
            _answer = ""
            choices(0).Dispatcher.Invoke(Sub()
                                             For Each choice In choices
                                                 AddHandler choice.Click, AddressOf Button_Click
                                             Next
                                         End Sub)
        End Sub

        Public Overridable Function Logic() As Boolean Implements IProgressEffect.Logic
            If WaitFrame > 0 Then WaitFrame -= 1
            If WaitFrame = 0 OrElse _answer <> "" Then Return False
            Return True
        End Function

        Public Overridable Sub Render() Implements IProgressEffect.Render
        End Sub

        Public Function GetAnswer() As String Implements IProgressEffect.GetAnswer
            Return _answer
        End Function

        Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
            _answer = TryCast(sender, Button).Content
            MessageAPI.SendSync("[CHOICE]USER_CLICKED")
        End Sub

    End Class

End Namespace