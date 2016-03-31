Public Class Extension
    Public Shared Function [Get](name As String) As Page
        If Not PageList.Pages.ContainsKey(name) Then Throw New Exception("找不到目标页面")
        Return Game.Window.Run(Function() Activator.CreateInstance(PageList.Pages(name)), True)
    End Function
End Class
