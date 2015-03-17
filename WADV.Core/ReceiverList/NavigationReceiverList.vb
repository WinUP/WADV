Imports System.Windows.Navigation
Imports WADV.Core.PluginInterface

Namespace ReceiverList

    ''' <summary>
    ''' 转场接收器列表
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class NavigationReceiverList
        Private Shared ReadOnly List As New List(Of INavigationReceiver)

        ''' <summary>
        ''' 添加一个转场接收器
        ''' </summary>
        ''' <param name="target">要添加的转场接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Add(target As INavigationReceiver)
            If Not Contains(target) Then List.Add(target)
        End Sub

        ''' <summary>
        ''' 确定指定转场接收器是否已存在
        ''' </summary>
        ''' <param name="content">要检查的转场接收器</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function Contains(content As INavigationReceiver) As Boolean
            Return List.Contains(content)
        End Function

        ''' <summary>
        ''' 删除一个转场接收器
        ''' </summary>
        ''' <param name="content">目标转场接收器</param>
        ''' <remarks></remarks>
        Friend Shared Sub Delete(content As INavigationReceiver)
            If Contains(content) Then
                List.Remove(content)
            End If
        End Sub

        ''' <summary>
        ''' 传递事件到所有已注册的转场接收器
        ''' </summary>
        ''' <param name="e">要传递的事件</param>
        ''' <remarks></remarks>
        Friend Shared Sub Boardcast(e As NavigatingCancelEventArgs)
            For Each tmpReceiver In List
                tmpReceiver.RecevingNavigate(e)
            Next
        End Sub
    End Class

End Namespace
