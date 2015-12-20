''' <summary>
''' 表示一个二维点
''' </summary>
Public Structure Point2
    ''' <summary>
    ''' 点的X轴坐标
    ''' </summary>
    Public X As Double
    ''' <summary>
    ''' 点的Y轴坐标
    ''' </summary>
    Public Y As Double

    ''' <summary>
    ''' 获得一个新的二维点
    ''' </summary>
    ''' <param name="x">点的X轴坐标</param>
    ''' <param name="y">点的Y轴坐标</param>
    Public Sub New(x As Double, y As Double)
        Me.X = x
        Me.Y = y
    End Sub

    Public Shared Operator +(a As Point2, b As Point2) As Point2
        Return New Point2(a.X + b.X, a.Y + b.Y)
    End Operator

    Public Shared Operator +(a As Point2, b As Double) As Point2
        Return New Point2(a.X + b, a.Y + b)
    End Operator

    Public Shared Operator +(a As Point2, b As Integer) As Point2
        Return New Point2(a.X + b, a.Y + b)
    End Operator

    Public Shared Operator +(a As Point2, b As Vector2) As Point2
        Return New Point2(a.X + b.X, a.Y + b.Y)
    End Operator

    Public Shared Operator -(a As Point2, b As Point2) As Point2
        Return New Point2(a.X - b.X, a.Y - b.Y)
    End Operator

    Public Shared Operator -(a As Point2, b As Double) As Point2
        Return New Point2(a.X - b, a.Y - b)
    End Operator

    Public Shared Operator -(a As Point2, b As Integer) As Point2
        Return New Point2(a.X - b, a.Y - b)
    End Operator

    Public Shared Operator -(a As Point2, b As Vector2) As Point2
        Return New Point2(a.X - b.X, a.Y - b.Y)
    End Operator

    Public Shared Operator *(a As Point2, b As Point2) As Point2
        Return New Point2(a.X * b.X, a.Y * b.Y)
    End Operator

    Public Shared Operator *(a As Point2, b As Double) As Point2
        Return New Point2(a.X * b, a.Y * b)
    End Operator

    Public Shared Operator *(a As Point2, b As Integer) As Point2
        Return New Point2(a.X * b, a.Y * b)
    End Operator

    Public Shared Operator *(a As Point2, b As Vector2) As Point2
        Return New Point2(a.X * b.X, a.Y * b.Y)
    End Operator

    Public Shared Operator /(a As Point2, b As Point2) As Point2
        Return New Point2(a.X / b.X, a.Y / b.Y)
    End Operator

    Public Shared Operator /(a As Point2, b As Double) As Point2
        Return New Point2(a.X / b, a.Y / b)
    End Operator

    Public Shared Operator /(a As Point2, b As Integer) As Point2
        Return New Point2(a.X / b, a.Y / b)
    End Operator

    Public Shared Operator /(a As Point2, b As Vector2) As Point2
        Return New Point2(a.X / b.X, a.Y / b.Y)
    End Operator

    Public Shared Operator =(a As Point2, b As Point2) As Boolean
        Return a.X = b.X AndAlso a.Y = b.Y
    End Operator

    Public Shared Operator <>(a As Point2, b As Point2) As Boolean
        Return Not a = b
    End Operator

    Public Shared Operator >(a As Point2, b As Point2) As Boolean
        Return (a.X * a.X + a.Y * a.Y) > (b.X * b.X + b.Y * b.Y)
    End Operator

    Public Shared Operator <(a As Point2, b As Point2) As Boolean
        Return (a.X * a.X + a.Y * a.Y) < (b.X * b.X + b.Y * b.Y)
    End Operator

    Public Shared Operator >=(a As Point2, b As Point2) As Boolean
        Return (a.X * a.X + a.Y * a.Y) >= (b.X * b.X + b.Y * b.Y)
    End Operator

    Public Shared Operator <=(a As Point2, b As Point2) As Boolean
        Return (a.X * a.X + a.Y * a.Y) <= (b.X * b.X + b.Y * b.Y)
    End Operator

    Public Shared Widening Operator CType(v As Integer) As Point2
        Return New Point2(v, v)
    End Operator

    Public Shared Widening Operator CType(v As Double) As Point2
        Return New Point2(v, v)
    End Operator

    ''' <summary>
    ''' 确定两个二维点的X轴坐标是否相同
    ''' </summary>
    ''' <param name="a">第一个二维点</param>
    ''' <param name="b">第二个二维点</param>
    ''' <returns></returns>
    Public Shared Function EqualX(a As Point2, b As Point2) As Boolean
        Return a.X = b.X
    End Function

    ''' <summary>
    ''' 确定两个二维点的Y轴坐标是否相同
    ''' </summary>
    ''' <param name="a">第一个二维点</param>
    ''' <param name="b">第二个二维点</param>
    ''' <returns></returns>
    Public Shared Function EqualY(a As Point2, b As Point2) As Boolean
        Return a.Y = b.Y
    End Function

    ''' <summary>
    ''' 零点
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property Zero As Point2 = New Point2 With {.X = 0.0, .Y = 0.0}
End Structure