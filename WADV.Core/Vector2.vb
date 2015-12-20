''' <summary>
''' 表示一个二维向量
''' </summary>
Public Structure Vector2
    ''' <summary>
    ''' 向量的X轴分量
    ''' </summary>
    Public X As Double
    ''' <summary>
    ''' 向量的Y轴分量
    ''' </summary>
    Public Y As Double

    ''' <summary>
    ''' 获得一个新的二维向量
    ''' </summary>
    ''' <param name="x">向量的X轴分量</param>
    ''' <param name="y">向量的Y轴分量</param>
    Public Sub New(x As Double, y As Double)
        Me.X = x
        Me.Y = y
    End Sub

    Public Shared Operator +(a As Vector2, b As Vector2) As Vector2
        Return New Vector2(a.X + b.X, a.Y + b.Y)
    End Operator

    Public Shared Operator +(a As Vector2, b As Double) As Vector2
        Return New Vector2(a.X + b, a.Y + b)
    End Operator

    Public Shared Operator +(a As Vector2, b As Integer) As Vector2
        Return New Vector2(a.X + b, a.Y + b)
    End Operator

    Public Shared Operator +(a As Vector2, b As Point2) As Vector2
        Return New Vector2(a.X + b.X, a.Y + b.Y)
    End Operator

    Public Shared Operator -(a As Vector2, b As Vector2) As Vector2
        Return New Vector2(a.X - b.X, a.Y - b.Y)
    End Operator

    Public Shared Operator -(a As Vector2, b As Double) As Vector2
        Return New Vector2(a.X - b, a.Y - b)
    End Operator

    Public Shared Operator -(a As Vector2, b As Integer) As Vector2
        Return New Vector2(a.X - b, a.Y - b)
    End Operator

    Public Shared Operator -(a As Vector2, b As Point2) As Vector2
        Return New Vector2(a.X - b.X, a.Y - b.Y)
    End Operator

    Public Shared Operator *(a As Vector2, b As Vector2) As Double
        Return a.X * b.X + a.Y * b.Y
    End Operator

    Public Shared Operator *(a As Vector2, b As Double) As Vector2
        Return New Vector2(a.X * b, a.Y * b)
    End Operator

    Public Shared Operator *(a As Vector2, b As Integer) As Vector2
        Return New Vector2(a.X * b, a.Y * b)
    End Operator

    Public Shared Operator *(a As Vector2, b As Point2) As Vector2
        Return New Vector2(a.X * b.X, a.Y * b.X)
    End Operator

    Public Shared Operator /(a As Vector2, b As Double) As Vector2
        Return New Vector2(a.X / b, a.Y / b)
    End Operator

    Public Shared Operator /(a As Vector2, b As Integer) As Vector2
        Return New Vector2(a.X / b, a.Y / b)
    End Operator

    Public Shared Operator /(a As Vector2, b As Point2) As Vector2
        Return New Vector2(a.X / b.X, a.Y / b.X)
    End Operator

    Public Shared Operator =(a As Vector2, b As Vector2) As Boolean
        Return a.X = b.X AndAlso a.Y = b.Y
    End Operator

    Public Shared Operator =(a As Vector2, b As Double) As Boolean
        Return [Mod](a) = b
    End Operator

    Public Shared Operator <>(a As Vector2, b As Vector2) As Boolean
        Return Not a = b
    End Operator

    Public Shared Operator <>(a As Vector2, b As Double) As Boolean
        Return Not a = b
    End Operator

    Public Shared Operator >(a As Vector2, b As Vector2) As Boolean
        Return (a.X * a.X + a.Y * a.Y) > (b.X * b.X + b.Y * b.Y)
    End Operator

    Public Shared Operator <(a As Vector2, b As Vector2) As Boolean
        Return (a.X * a.X + a.Y * a.Y) < (b.X * b.X + b.Y * b.Y)
    End Operator

    Public Shared Operator >=(a As Vector2, b As Vector2) As Boolean
        Return (a.X * a.X + a.Y * a.Y) >= (b.X * b.X + b.Y * b.Y)
    End Operator

    Public Shared Operator <=(a As Vector2, b As Vector2) As Boolean
        Return (a.X * a.X + a.Y * a.Y) <= (b.X * b.X + b.Y * b.Y)
    End Operator

    Public Shared Widening Operator CType(v As Integer) As Vector2
        Return New Vector2(v, v)
    End Operator

    Public Shared Widening Operator CType(v As Double) As Vector2
        Return New Vector2(v, v)
    End Operator

    ''' <summary>
    ''' 求取二维向量的模
    ''' </summary>
    ''' <param name="target">要使用的二维向量</param>
    ''' <returns></returns>
    Public Shared Function [Mod](target As Vector2) As Double
        Return Math.Sqrt(Mod2(target))
    End Function

    ''' <summary>
    ''' 求取二维向量的模的平方
    ''' </summary>
    ''' <param name="target">要使用的二维向量</param>
    ''' <returns></returns>
    Public Shared Function Mod2(target As Vector2) As Double
        Return target.X * target.X + target.Y * target.Y
    End Function

    ''' <summary>
    ''' 零向量
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property Zero As Vector2 = New Vector2 With {.X = 0.0, .Y = 0.0}

    ''' <summary>
    ''' 单位向量
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property Unit As Vector2 = New Vector2 With {.X = 1.0, .Y = 1.0}
End Structure