Imports System.Windows.Controls

Namespace TEList

    ''' <summary>
    ''' 既存立绘列表
    ''' </summary>
    ''' <remarks></remarks>
    Public Class List
        Private Shared _imageList As New Dictionary(Of Integer, Image)
        Private Shared _id As Integer = 0

        ''' <summary>
        ''' 添加一个立绘
        ''' </summary>
        ''' <param name="target"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Add(target As Image) As Integer
            If _imageList.ContainsValue(target) Then Return (From tmpPare In _imageList Where tmpPare.Value Is target Select tmpPare.Key).FirstOrDefault
            _imageList.Add(_id, target)
            _id += 1
            Return _id - 1
        End Function

        ''' <summary>
        ''' 获取一个立绘
        ''' 当对应ID的立绘不存在时返回Nothing
        ''' </summary>
        ''' <param name="id">要获取的立绘的ID</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Item(id As Integer) As Image
            If Not _imageList.ContainsKey(id) Then Return Nothing
            Return _imageList(id)
        End Function

        ''' <summary>
        ''' 删除一个立绘
        ''' </summary>
        ''' <param name="id"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Delete(id As Integer) As Boolean
            If Not _imageList.ContainsKey(id) Then Return False
            _imageList.Remove(id)
            Return True
        End Function

    End Class

End Namespace
