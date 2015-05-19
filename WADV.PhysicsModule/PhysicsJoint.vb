Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Shapes

''' <summary>
''' 物理关节
''' </summary>
''' <remarks>这个类的原始代码来自 http://physicshelperxaml.codeplex.com/, 它是一个用于Windows通用应用的框架, 这里是它的WPF版</remarks>
Public Class PhysicsJoint : Inherits Canvas
    Private ReadOnly _ellDesign As Ellipse

    Public Sub New()
        Width = 8
        Height = 8
        _ellDesign = New Ellipse With {
            .Width = 8,
            .Height = 8,
            .Stroke = New SolidColorBrush(Colors.White)}
        Children.Add(_ellDesign)
    End Sub

    Private Sub PhysicsJoint_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim physCanvas As PhysicsWorld = PhysicsUtilities.FindPhysicsWorld(Me)
        physCanvas.AddPhysicsJoint(Me)
        RemoveHandler Loaded, AddressOf PhysicsJoint_Loaded
    End Sub

    ''' <summary>
    ''' 关节连接的物体A
    ''' </summary>
    Public Property BodyA() As String = String.Empty

    ''' <summary>
    ''' 关节连接的物体B
    ''' </summary>
    Public Property BodyB() As String = String.Empty

    ''' <summary>
    ''' 这个关节是否是弹性关节
    ''' </summary>
    Public Property AngleSpringEnabled() As Boolean

    ''' <summary>
    ''' 获取或设置这个关节的最低旋转角
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AngleLowerLimit() As Double = -1

    ''' <summary>
    ''' 获取或设置这个关节的最高旋转角
    ''' </summary>
    Public Property AngleUpperLimit() As Double = -1

    ''' <summary>
    ''' 关节僵硬程度，数值越大越僵硬
    ''' </summary>
    Public Property AngleSpringConstant() As Double = 0.8

    ''' <summary>
    ''' 获取或设置这个关节的碰撞组
    ''' </summary>
    Public Property CollisionGroup() As Integer

    ''' <summary>
    ''' 这个关节是否是紧密的
    ''' </summary>
    Public Property IsWeldJoint() As Boolean

    ''' <summary>
    ''' 这个关节是否具有长度
    ''' </summary>
    Public Property IsDistanceJoint() As Boolean

    ''' <summary>
    ''' 获取关节的中心点
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCenter() As Point
        Dim offSetLeft As Double = 0
        Dim offSetTop As Double = 0

        Dim parent As FrameworkElement = Me.Parent
        While parent IsNot Nothing
            Dim incLeft As Double = Convert.ToDouble(parent.GetValue(LeftProperty))
            Dim incTop As Double = Convert.ToDouble(parent.GetValue(TopProperty))
            offSetLeft += If(Double.IsNaN(incLeft), 0, incLeft)
            offSetTop += If(Double.IsNaN(incTop), 0, incTop)
            parent = TryCast(parent.Parent, FrameworkElement)
        End While
        Dim pt As New Point()
        pt.X = CDbl(GetValue(LeftProperty)) + Width / 2 + offSetLeft
        pt.Y = CDbl(GetValue(TopProperty)) + Height / 2 + offSetTop
        Return pt
    End Function
End Class
