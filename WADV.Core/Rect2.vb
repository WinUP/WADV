''' <summary>
''' 表示一个二维矩形
''' </summary>
Public Structure Rect2
    Public X As Double
    Public Y As Double
    Public Width As Double
    Public Height As Double

    ''' <summary>
    ''' 获得一个新的二维矩形
    ''' </summary>
    ''' <param name="x">矩形左上角的X轴坐标</param>
    ''' <param name="y">矩形左上角的Y轴坐标</param>
    ''' <param name="width">矩形的宽度</param>
    ''' <param name="height">矩形的高度</param>
    Public Sub New(x As Double, y As Double, width As Double, height As Double)
        Me.X = x
        Me.Y = y
        Me.Width = width
        Me.Height = height
    End Sub

    Public Shared Operator +(a As Rect2, b As Point2) As Rect2
        Return New Rect2(a.X + b.X, a.Y + b.Y, a.Width, a.Height)
    End Operator

    Public Shared Operator +(a As Rect2, b As Vector2) As Rect2
        Return New Rect2(a.X, a.Y, a.Width + b.X, a.Height + b.Y)
    End Operator

    Public Shared Operator +(a As Rect2, b As Double) As Rect2
        Return New Rect2(a.X, a.Y, a.Width + b, a.Height + b)
    End Operator

    Public Shared Operator +(a As Rect2, b As Integer) As Rect2
        Return New Rect2(a.X, a.Y, a.Width + b, a.Height + b)
    End Operator

    Public Shared Operator -(a As Rect2, b As Point2) As Rect2
        Return New Rect2(a.X - b.X, a.Y - b.Y, a.Width, a.Height)
    End Operator

    Public Shared Operator -(a As Rect2, b As Vector2) As Rect2
        Return New Rect2(a.X, a.Y, a.Width - b.X, a.Height - b.Y)
    End Operator

    Public Shared Operator -(a As Rect2, b As Double) As Rect2
        Return New Rect2(a.X, a.Y, a.Width - b, a.Height - b)
    End Operator

    Public Shared Operator -(a As Rect2, b As Integer) As Rect2
        Return New Rect2(a.X, a.Y, a.Width - b, a.Height - b)
    End Operator

    Public Shared Operator *(a As Rect2, b As Point2) As Rect2
        Return New Rect2(a.X * b.X, a.Y * b.Y, a.Width, a.Height)
    End Operator

    Public Shared Operator *(a As Rect2, b As Vector2) As Rect2
        Return New Rect2(a.X, a.Y, a.Width * b.X, a.Height * b.Y)
    End Operator

    Public Shared Operator *(a As Rect2, b As Double) As Rect2
        Return New Rect2(a.X, a.Y, a.Width * b, a.Height * b)
    End Operator

    Public Shared Operator *(a As Rect2, b As Integer) As Rect2
        Return New Rect2(a.X, a.Y, a.Width * b, a.Height * b)
    End Operator

    Public Shared Operator /(a As Rect2, b As Point2) As Rect2
        Return New Rect2(a.X / b.X, a.Y / b.Y, a.Width, a.Height)
    End Operator

    Public Shared Operator /(a As Rect2, b As Vector2) As Rect2
        Return New Rect2(a.X, a.Y, a.Width / b.X, a.Height / b.Y)
    End Operator

    Public Shared Operator /(a As Rect2, b As Double) As Rect2
        Return New Rect2(a.X, a.Y, a.Width / b, a.Height / b)
    End Operator

    Public Shared Operator /(a As Rect2, b As Integer) As Rect2
        Return New Rect2(a.X, a.Y, a.Width / b, a.Height / b)
    End Operator

    Public Shared Operator =(a As Rect2, b As Rect2) As Boolean
        Return a.X = b.X AndAlso a.Y = b.Y AndAlso a.Width = b.Width AndAlso a.Height = b.Height
    End Operator

    Public Shared Operator =(a As Rect2, b As Point2) As Boolean
        Return a.X = b.X AndAlso a.Y = b.Y
    End Operator

    Public Shared Operator =(a As Rect2, b As Vector2) As Boolean
        Return a.Width = b.X AndAlso a.Height = b.Y
    End Operator

    Public Shared Operator =(a As Rect2, b As Double) As Boolean
        Return a.Width = b AndAlso a.Height = b
    End Operator

    Public Shared Operator =(a As Rect2, b As Integer) As Boolean
        Return a.Width = b AndAlso a.Height = b
    End Operator

    Public Shared Operator <>(a As Rect2, b As Rect2) As Boolean
        Return Not a = b
    End Operator

    Public Shared Operator <>(a As Rect2, b As Point2) As Boolean
        Return Not a = b
    End Operator
    Public Shared Operator <>(a As Rect2, b As Vector2) As Boolean
        Return Not a = b
    End Operator
    Public Shared Operator <>(a As Rect2, b As Double) As Boolean
        Return Not a = b
    End Operator
    Public Shared Operator <>(a As Rect2, b As Integer) As Boolean
        Return Not a = b
    End Operator

    Public Shared Operator >(a As Rect2, b As Rect2) As Boolean
        Return a.Width > b.Width AndAlso a.Height > b.Height
    End Operator

    Public Shared Operator >(a As Rect2, b As Point2) As Boolean
        Return a.X > b.X AndAlso a.Y > b.Y
    End Operator

    Public Shared Operator >(a As Rect2, b As Vector2) As Boolean
        Return a.Width > b.X AndAlso a.Height > b.Y
    End Operator

    Public Shared Operator >(a As Rect2, b As Double) As Boolean
        Return a.Width > b AndAlso a.Height > b
    End Operator

    Public Shared Operator >(a As Rect2, b As Integer) As Boolean
        Return a.Width > b AndAlso a.Height > b
    End Operator

    Public Shared Operator <(a As Rect2, b As Rect2) As Boolean
        Return a.Width < b.Width AndAlso a.Height < b.Height
    End Operator

    Public Shared Operator <(a As Rect2, b As Point2) As Boolean
        Return a.X < b.X AndAlso a.Y < b.Y
    End Operator

    Public Shared Operator <(a As Rect2, b As Vector2) As Boolean
        Return a.Width < b.X AndAlso a.Height < b.Y
    End Operator

    Public Shared Operator <(a As Rect2, b As Double) As Boolean
        Return a.Width < b AndAlso a.Height < b
    End Operator

    Public Shared Operator <(a As Rect2, b As Integer) As Boolean
        Return a.Width < b AndAlso a.Height < b
    End Operator

    Public Shared Operator >=(a As Rect2, b As Rect2) As Boolean
        Return a.Width >= b.Width AndAlso a.Height >= b.Height
    End Operator

    Public Shared Operator >=(a As Rect2, b As Point2) As Boolean
        Return a.X >= b.X AndAlso a.Y >= b.Y
    End Operator

    Public Shared Operator >=(a As Rect2, b As Vector2) As Boolean
        Return a.Width >= b.X AndAlso a.Height >= b.Y
    End Operator

    Public Shared Operator >=(a As Rect2, b As Double) As Boolean
        Return a.Width >= b AndAlso a.Height >= b
    End Operator

    Public Shared Operator >=(a As Rect2, b As Integer) As Boolean
        Return a.Width >= b AndAlso a.Height >= b
    End Operator

    Public Shared Operator <=(a As Rect2, b As Rect2) As Boolean
        Return a.Width <= b.Width AndAlso a.Height <= b.Height
    End Operator

    Public Shared Operator <=(a As Rect2, b As Point2) As Boolean
        Return a.X <= b.X AndAlso a.Y <= b.Y
    End Operator

    Public Shared Operator <=(a As Rect2, b As Vector2) As Boolean
        Return a.Width <= b.X AndAlso a.Height <= b.Y
    End Operator

    Public Shared Operator <=(a As Rect2, b As Double) As Boolean
        Return a.Width <= b AndAlso a.Height <= b
    End Operator

    Public Shared Operator <=(a As Rect2, b As Integer) As Boolean
        Return a.Width <= b AndAlso a.Height <= b
    End Operator

    ''' <summary>
    ''' 获取二维矩形的坐标
    ''' </summary>
    ''' <param name="target">目标矩形</param>
    ''' <returns></returns>
    Public Shared Function Point(target As Rect2) As Point2
        Return New Point2(target.X, target.Y)
    End Function

    ''' <summary>
    ''' 获取二维矩形的大小
    ''' </summary>
    ''' <param name="target">目标矩形</param>
    ''' <returns></returns>
    Public Shared Function Vector(target As Rect2) As Vector2
        Return New Vector2(target.Width, target.Height)
    End Function
End Structure
