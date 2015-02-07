Namespace API

    Public Class ObjectAPI

        Public Shared Function GetPage(name As String) As Page
            If Not PageList.List.Pages.ContainsKey(name) Then Throw New Exception("找不到目标页面")
            Return WindowAPI.GetDispatcher.Invoke(Function() Activator.CreateInstance(PageList.List.Pages(name)))
        End Function

    End Class

End Namespace
