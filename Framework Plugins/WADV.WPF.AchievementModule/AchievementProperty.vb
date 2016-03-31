''' <summary>
''' 表示一个成就属性
''' </summary>
''' <remarks></remarks>
<Serializable> Public Structure AchievementProperty
    ''' <summary>
    ''' 属性的名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Name As String
    ''' <summary>
    ''' 属性的数据
    ''' </summary>
    ''' <remarks></remarks>
    Friend Data As Integer
    ''' <summary>
    ''' 属性的关联成就
    ''' </summary>
    ''' <remarks></remarks>
    Friend Triggle As List(Of Achievement)
End Structure
