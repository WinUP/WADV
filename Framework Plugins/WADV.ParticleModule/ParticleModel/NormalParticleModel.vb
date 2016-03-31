Imports System.Windows

Namespace ParticleModel
    ''' <summary>
    ''' 一般粒子模型
    ''' </summary>
    ''' <remark>目前允许在Update中对Size Speed Velocity Position进行修改</remark>
    Public Class NormalParticleModel : Inherits DependencyObject
        Public Shared ReadOnly LifeProperty As DependencyProperty = DependencyProperty.Register("Life", GetType(Integer), GetType(NormalParticleModel))
        Public Shared ReadOnly SizeProperty As DependencyProperty = DependencyProperty.Register("Size", GetType(Vector), GetType(NormalParticleModel))
        Public Shared ReadOnly PositionProperty As DependencyProperty = DependencyProperty.Register("Position", GetType(Point), GetType(NormalParticleModel))
        Public Shared ReadOnly SpeedProperty As DependencyProperty = DependencyProperty.Register("Speed", GetType(Vector), GetType(NormalParticleModel))
        Public Shared ReadOnly VelocityProperty As DependencyProperty = DependencyProperty.Register("Velocity", GetType(Vector), GetType(NormalParticleModel))

        ''' <summary>
        ''' 粒子剩余生存帧数，这是一个依赖项属性
        ''' </summary>
        ''' <returns></returns>
        Public Property Life As Integer
            Get
                Return GetValue(LifeProperty)
            End Get
            Friend Set(value As Integer)
                SetValue(LifeProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' 粒子大小，这是一个依赖项属性
        ''' </summary>
        ''' <returns></returns>
        Public Property Size As Vector
            Get
                Return GetValue(SizeProperty)
            End Get
            Protected Friend Set(value As Vector)
                SetValue(SizeProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' 粒子位置，这是一个依赖项属性
        ''' </summary>
        ''' <returns></returns>
        Public Property Position As Point
            Get
                Return GetValue(PositionProperty)
            End Get
            Protected Friend Set(value As Point)
                SetValue(PositionProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' 粒子速度，这是一个依赖项属性
        ''' </summary>
        ''' <returns></returns>
        Public Property Speed As Vector
            Get
                Return GetValue(SpeedProperty)
            End Get
            Protected Friend Set(value As Vector)
                SetValue(SpeedProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' 粒子加速度，这是一个依赖项属性
        ''' </summary>
        ''' <returns></returns>
        Public Property Velocity As Vector
            Get
                Return GetValue(VelocityProperty)
            End Get
            Protected Friend Set(value As Vector)
                SetValue(VelocityProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' 更新粒子坐标
        ''' </summary>
        Friend Sub UpdatePosition()
            Position = New Point(Position.X + Speed.X * Velocity.X, Position.Y + Speed.Y * Velocity.Y)
        End Sub

        ''' <summary>
        ''' 粒子初始化
        ''' </summary>
        Public Overridable Sub Initialise()
        End Sub

        ''' <summary>
        ''' 更新粒子状态
        ''' </summary>
        ''' <return>是否需要由粒子系统进行位置更新</return>
        Public Overridable Function Update() As Boolean
            Return True
        End Function

        ''' <summary>
        ''' 销毁粒子
        ''' </summary>
        Public Overridable Sub Destroy()
        End Sub
    End Class
End Namespace