Imports System.Reflection

Namespace PageList

    Public Class List
        Protected Friend Shared Pages As Dictionary(Of String, Type)

        Protected Friend Shared Sub LoadPage()
            Pages = New Dictionary(Of String, Type)
            For Each tmpType In (From singleType In Assembly.GetExecutingAssembly.GetTypes Where singleType.GetInterface("System.Windows.Markup.IComponentConnector") <> Nothing Select singleType)
                Pages.Add(tmpType.Name, tmpType)
            Next
        End Sub

    End Class

End Namespace
