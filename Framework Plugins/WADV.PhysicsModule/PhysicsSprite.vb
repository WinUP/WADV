Imports System.Collections.Generic
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Dynamics.Contacts
Imports FarseerPhysics.Dynamics.Joints
Imports FarseerPhysics.Factories
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Shapes
Imports Microsoft.Xna.Framework

''' <summary>
''' 带有物理模拟功能的精灵
''' </summary>
''' <remarks>这个类的原始代码来自 http://physicshelperxaml.codeplex.com/ </remarks>
Public Class PhysicsSprite : Inherits Canvas : Implements IDisposable
    Private _positionBeforeManipulation As Vector2
    Private _manipulationJoint As FixedMouseJoint
    Private _manipulationStartPoint As Point
    Private _manipulationOriginalAngularDamping As Single
    Private _isManipulationWithStaticBody As Boolean = False
    Private _isManipulationWithoutPhysicsBody As Boolean = False

    ''' <summary>
    ''' 物体形状
    ''' </summary>
    Public Enum ShapeTypes
        Rectangle
        Ellipse
        Polygon
    End Enum

#Region "属性们"

    ''' <summary>
    ''' 获取或设置这个物体的调试显示路径
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property PathDebug() As Shape

    ''' <summary>
    ''' 获取或设置这个物体休眠时的调试路径
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property PathDebugResting() As Shape

    Private _world As PhysicsWorld
    ''' <summary>
    ''' 获取这个物体所在的物理世界
    ''' </summary>
    Public ReadOnly Property World() As PhysicsWorld
        Get
            Return _world
        End Get
    End Property

    Private _frictionCoefficient As Single = 0.4F
    ''' <summary>
    ''' 获取或设置这个物体的摩擦系数
    ''' </summary>
    Public Property FrictionCoefficient() As Single
        Get
            Return _frictionCoefficient
        End Get
        Set(value As Single)
            _frictionCoefficient = value
            If BodyObject IsNot Nothing Then BodyObject.FixtureList.ForEach(Sub(e) e.Friction = value)
        End Set
    End Property

    Private _collisionEventDisable As Boolean = False
    ''' <summary>
    ''' 是否置空这个物体的碰撞检测事件
    ''' </summary>
    Public Property CollisionEventDisable() As Boolean
        Get
            Return _collisionEventDisable
        End Get
        Set(value As Boolean)
            _collisionEventDisable = value
            If BodyObject IsNot Nothing Then BodyObject.FixtureList.ForEach(Sub(e) e.OnCollision = If(_collisionEventDisable, Nothing, New OnCollisionEventHandler(AddressOf HandleOnCollision)))
        End Set
    End Property

    Private _isStatic As Boolean
    ''' <summary>
    ''' 获取或设置这个物体的可动状态
    ''' </summary>
    Public Property IsStatic() As Boolean
        Get
            Return _isStatic
        End Get
        Set(value As Boolean)
            _isStatic = value
            If BodyObject IsNot Nothing Then BodyObject.IsStatic = value
        End Set
    End Property

    ''' <summary>
    ''' 是否允许这个物体在静态状态下也响应用户操作事件
    ''' </summary>
    Public Property AllowManipulationWhenStatic() As Boolean

    Private _isSensor As Boolean
    ''' <summary>
    ''' 是否自行处理这个物体碰撞后的状态
    ''' </summary>
    Public Property IsSensor() As Boolean
        Get
            Return _isSensor
        End Get
        Set(value As Boolean)
            _isSensor = value
            If BodyObject IsNot Nothing Then BodyObject.IsSensor = value
        End Set
    End Property

    Private _isCanvasEnabled As Boolean = True
    ''' <summary>
    ''' 是否启用这个物体
    ''' </summary>
    Public Property Enabled() As Boolean
        Get
            Return _isCanvasEnabled
        End Get
        Set(value As Boolean)
            _isCanvasEnabled = value
            If _isCanvasEnabled Then
                If BodyObject IsNot Nothing Then BodyObject.Enabled = True
                Visibility = Visibility.Visible
            Else
                If BodyObject IsNot Nothing Then BodyObject.Enabled = False
                Visibility = Visibility.Collapsed
                RaiseEvent PhysicsDisabled(Me)
            End If
        End Set
    End Property

    Private _isBodyEnabled As Boolean = True
    ''' <summary>
    ''' 是否启用这个物体的物理运算
    ''' </summary>
    Public Property EnablePhysics() As Boolean
        Get
            Return _isBodyEnabled
        End Get
        Set(value As Boolean)
            _isBodyEnabled = value
            If BodyObject IsNot Nothing Then BodyObject.Enabled = value
        End Set
    End Property

    Private _shapeType As ShapeTypes
    ''' <summary>
    ''' 获取或设置这个物体的形状
    ''' </summary>
    ''' <value>物体的形状，必须是字符串"Rectangle"或"Ellipse"或"Polygon"]</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShapeType() As String
        Get
            Return _shapeType.ToString()
        End Get
        Set(value As String)
            _shapeType = CType([Enum].Parse(GetType(ShapeTypes), value, True), ShapeTypes)
        End Set
    End Property

    Private _isBullet As Boolean
    ''' <summary>
    ''' 是否标志此物体为高速移动物体
    ''' </summary>
    Public Property IsBullet() As Boolean
        Get
            Return _isBullet
        End Get
        Set(value As Boolean)
            _isBullet = value
            If BodyObject IsNot Nothing Then BodyObject.IsBullet = value
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置这个物体的Body对象
    ''' </summary>
    Public Property BodyObject As Body

    Private _collisionGroup As Short = 0
    ''' <summary>
    ''' 获取或设置这个物体的碰撞序列
    ''' </summary>
    Public Property CollisionGroup() As Short
        Get
            Return _collisionGroup
        End Get
        Set(value As Short)
            _collisionGroup = Convert.ToInt16(value)
            If BodyObject IsNot Nothing Then BodyObject.FixtureList.ForEach(Sub(e) e.CollisionGroup = _collisionGroup)
        End Set
    End Property

    Private _causesCollisions As Boolean = True
    ''' <summary>
    ''' 这个物体是否引发碰撞
    ''' </summary>
    Public Property CausesCollisions() As Boolean
        Get
            Return _causesCollisions
        End Get
        Set(value As Boolean)
            _causesCollisions = value
            If BodyObject IsNot Nothing Then BodyObject.FixtureList.ForEach(Sub(e) e.CollidesWith = If(_causesCollisions, Category.All, Category.None))
        End Set
    End Property

    Private _showFixtures As Boolean
    ''' <summary>
    ''' 是否显示这个物体的关联装置
    ''' </summary>
    Public Property ShowFixtures() As Boolean
        Get
            Return _showFixtures
        End Get
        Set(value As Boolean)
            _showFixtures = value
            If _showFixtures Then
                PathDebug = PhysicsUtilities.CreatePathFromVertices(BodyObject, Me, Color.FromArgb(192, 92, 192, 92))
                PathDebugResting = PhysicsUtilities.CreatePathFromVertices(BodyObject, Me, Color.FromArgb(192, 192, 92, 92))
                Children.Add(PathDebug)
                Children.Add(PathDebugResting)
            Else
                If PathDebug IsNot Nothing Then
                    Children.Remove(PathDebug)
                    PathDebug = Nothing
                End If
                If PathDebugResting IsNot Nothing Then
                    Children.Remove(PathDebugResting)
                    PathDebugResting = Nothing
                End If
            End If
        End Set
    End Property

    Private _restitutionCoefficient As Single
    ''' <summary>
    ''' 获取或设置这个物体的弹性力度
    ''' </summary>
    Public Property RestitutionCoefficient() As Single
        Get
            Return _restitutionCoefficient
        End Get
        Set(value As Single)
            _restitutionCoefficient = value
            If BodyObject IsNot Nothing Then BodyObject.FixtureList.ForEach(Sub(e) e.Restitution = value)
        End Set
    End Property

    Private _angularDamping As Single
    ''' <summary>
    ''' 获取或设置这个物体的旋转阻力
    ''' </summary>
    Public Property AngularDamping() As Single
        Get
            Return _angularDamping
        End Get
        Set(value As Single)
            _angularDamping = value
            If BodyObject IsNot Nothing Then BodyObject.AngularDamping = value
        End Set
    End Property

    Private _linerDamping As Single
    ''' <summary>
    ''' 获取或设置这个物体的移动阻力
    ''' </summary>
    Public Property LinearDamping() As Single
        Get
            Return _linerDamping
        End Get
        Set(value As Single)
            _linerDamping = value
            If BodyObject IsNot Nothing Then BodyObject.LinearDamping = value
        End Set
    End Property

    Private _internalName As String
    ''' <summary>
    ''' 获取或设置这个物体的内部名称
    ''' </summary>
    Public Property InternalName() As String
        Get
            Return _internalName
        End Get
        Set(value As String)
            _internalName = value
            If BodyObject IsNot Nothing Then
                BodyObject.FixtureList.ForEach(Sub(e) e.UserData = _internalName)
                BodyObject.UserData = _internalName
            End If
        End Set
    End Property

    Private _mass As Single = -1
    ''' <summary>
    ''' 获取或设置这个物体的质量
    ''' </summary>
    Public Property Mass() As Single
        Get
            Return _mass
        End Get
        Set(value As Single)
            _mass = value
            If BodyObject IsNot Nothing Then BodyObject.Mass = value
        End Set
    End Property

    Private _position As Vector2 = Vector2.Zero
    ''' <summary>
    ''' 获取或设置这个物体的显示坐标
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Position() As Vector2
        Get
            Return _position
        End Get
        Set(value As Vector2)
            _position = value
            If BodyObject IsNot Nothing Then
                Dim point = _position
                point.X += Width / 2.0
                point.Y += Height / 2.0
                BodyObject.Position = BoundaryConverter.ScreenToWorld(point)
                BodyObject.AngularVelocity = 0.0F
                BodyObject.LinearVelocity = Vector2.Zero
            Else
                SetLeft(Me, _position.X)
                SetTop(Me, _position.Y)
            End If
        End Set
    End Property

    Private _rotation As Single
    ''' <summary>
    ''' 获取或设置这个物体的旋转角度
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Rotation() As Double
        Get
            Return Utilities.GetTransformFromGroup(Of RotateTransform)(RenderTransform).Angle
        End Get
        Set(value As Double)
            _rotation = value
            If BodyObject IsNot Nothing Then BodyObject.Rotation = PhysicsUtilities.DegreesToRadians(_rotation)
            If Not TypeOf RenderTransform Is TransformGroup Then RenderTransform = Utilities.GenerateTransformGroup
            Utilities.GetTransformFromGroup(Of RotateTransform)(RenderTransform).Angle = value
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置这个物体作为用户控件的一部分时所对应的用户控件
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserControlParent As UserControl
#End Region

#Region "用户操作处理"

    Private _manipulationEnabled As Boolean
    ''' <summary>
    ''' 是否启用这个物体的操作相应
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ManipulationEnabled As Boolean
        Get
            Return _manipulationEnabled
        End Get
        Set(value As Boolean)
            If _world IsNot Nothing AndAlso Not _world.ManipulationEnabled Then value = False
            If value Then
                AddHandler ManipulationStarted, AddressOf Manipulation_Started
                AddHandler ManipulationDelta, AddressOf Manipulation_Delta
                AddHandler ManipulationCompleted, AddressOf Manipulation_Completed
                AddHandler MouseLeftButtonDown, AddressOf Mouse_Click
            Else
                RemoveHandler ManipulationStarted, AddressOf Manipulation_Started
                RemoveHandler ManipulationDelta, AddressOf Manipulation_Delta
                RemoveHandler ManipulationCompleted, AddressOf Manipulation_Completed
                RemoveHandler MouseLeftButtonDown, AddressOf Mouse_Click
            End If
            _manipulationEnabled = value
            IsManipulationEnabled = value
        End Set
    End Property

    ''' <summary>
    ''' 用户操作开始
    ''' </summary>
    Public Sub AccessManipulation(sender As UIElement, target As Point)
        _manipulationStartPoint = target
        Dim optionPoint = TransformToCanvas(sender, target)
        _positionBeforeManipulation = Position
        '操作时Body还没有被指定
        If IsManipulationEnabled AndAlso BodyObject Is Nothing Then
            _isManipulationWithoutPhysicsBody = True
            RaiseEvent PhysicsManipulationStarted(Me, Position)
            Exit Sub
        End If
        '操作的是静态物体
        If IsManipulationEnabled AndAlso BodyObject.IsStatic AndAlso AllowManipulationWhenStatic AndAlso BodyObject.Enabled Then
            _manipulationOriginalAngularDamping = BodyObject.AngularDamping
            BodyObject.AngularDamping = Single.MaxValue
            _isManipulationWithStaticBody = True
            BodyObject.IsStatic = False
        End If
        '操作的是一般物体
        If IsManipulationEnabled AndAlso BodyObject.IsStatic = False AndAlso BodyObject.Enabled Then
            Dim point As Vector2 = BoundaryConverter.ScreenToWorld(New Vector2(optionPoint.X, optionPoint.Y))
            _manipulationJoint = New FixedMouseJoint(BodyObject, point)
            _manipulationJoint.MaxForce = 100.0F * BodyObject.Mass
            _world.Simulator.AddJoint(_manipulationJoint)
            If Not BodyObject.Awake Then BodyObject.Awake = True
            _manipulationJoint.Enabled = True
            RaiseEvent PhysicsManipulationStarted(Me, Position)
        End If
    End Sub

    ''' <summary>
    ''' 更新用户操作
    ''' </summary>
    Public Sub ContinueManipulation(sender As UIElement, target As Point)
        Dim pt = TransformToCanvas(sender, target)
        If _isManipulationWithoutPhysicsBody Then
            SetLeft(Me, pt.X - _manipulationStartPoint.X)
            SetTop(Me, pt.Y - _manipulationStartPoint.Y)
        ElseIf _manipulationJoint IsNot Nothing Then
            _manipulationJoint.WorldAnchorB = BoundaryConverter.ScreenToWorld(New Vector2(pt.X, pt.Y))
        End If
    End Sub

    ''' <summary>
    ''' 结束用户操作
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub FinishManiupulation()
        If _isManipulationWithoutPhysicsBody Then '操作时Body还没有被指定
            _isManipulationWithoutPhysicsBody = False
        ElseIf _manipulationJoint IsNot Nothing Then '其他情况
            _manipulationJoint.Enabled = False
            _world.Simulator.RemoveJoint(_manipulationJoint)
            _manipulationJoint = Nothing
            '操作的是静态物体
            If AllowManipulationWhenStatic AndAlso _isManipulationWithStaticBody Then
                BodyObject.AngularDamping = _manipulationOriginalAngularDamping
                BodyObject.IsStatic = True
                _isManipulationWithStaticBody = False
            End If
        End If
        _manipulationStartPoint = Nothing
        RaiseEvent PhysicsManipulationCompleted(Me, _positionBeforeManipulation)
    End Sub

    ''' <summary>
    ''' 触摸操作开始
    ''' </summary>
    Private Sub Manipulation_Started(sender As Object, e As ManipulationStartedEventArgs)
        AccessManipulation(sender, e.ManipulationOrigin)
    End Sub

    ''' <summary>
    ''' 触摸操作进行中
    ''' </summary>
    Private Sub Manipulation_Delta(sender As Object, e As ManipulationDeltaEventArgs)
        ContinueManipulation(sender, e.ManipulationOrigin)
    End Sub

    ''' <summary>
    ''' 触摸操作结束
    ''' </summary>
    Private Sub Manipulation_Completed(sender As Object, e As ManipulationCompletedEventArgs)
        FinishManiupulation()
    End Sub

    Private Sub Mouse_Click(sender As Object, e As MouseButtonEventArgs)
        If _manipulationStartPoint = Nothing Then _world.RegisterMouseManipulation(Me)
    End Sub
#End Region

#Region "事件处理"

    ''' <summary>
    ''' 碰撞事件
    ''' </summary>
    Private Function HandleOnCollision(fixtureA As Fixture, fixtureB As Fixture, contact As Contact) As Boolean
        If fixtureA.Body Is BodyObject Then
            RaiseEvent FixtureCollision(fixtureA, fixtureB, contact)
            If Config.SpriteCollisionHandle.ContainsKey(Me) Then Config.SpriteCollisionHandle(Me).Invoke(fixtureA, fixtureB, contact)
            CollisionStore.AddCollision(Me, _world.PhysicsObjects(Convert.ToString(fixtureB.UserData)), contact)
        End If
        Return True
    End Function
#End Region

#Region "公开事件"

    ''' <summary>
    ''' 物体发生碰撞
    ''' </summary>
    Public Event FixtureCollision As Action(Of Fixture, Fixture, Contact)

    ''' <summary>
    ''' 鼠标拖拽开始
    ''' </summary>
    Public Event PhysicsManipulationStarted As Action(Of PhysicsSprite, Vector2)

    ''' <summary>
    ''' 鼠标拖拽结束
    ''' </summary>
    Public Event PhysicsManipulationCompleted As Action(Of PhysicsSprite, Vector2)

    ''' <summary>
    ''' 对象被销毁
    ''' </summary>
    Public Event PhysicsDisabled As Action(Of PhysicsSprite)
#End Region

    ''' <summary>
    ''' 物体第一次被显示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PhysicsSprite_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        CacheMode = New BitmapCache()
        If Not TypeOf RenderTransform Is TransformGroup Then RenderTransform = Utilities.GenerateTransformGroup
        RenderTransformOrigin = New Point(0.5, 0.5)
        If _world Is Nothing Then
            _world = PhysicsUtilities.FindPhysicsWorld(Me)
            If _world Is Nothing Then Throw New Exception("找不到可用的物理世界，请确保它的父级元素中存在物理世界元素")
        End If
        Dim shapes As List(Of FarseerPhysics.Collision.Shapes.Shape)
        Try
            shapes = BoundaryConverter.CreateShapesFromElement(Me, _shapeType)
        Catch ex As Exception
            shapes = BoundaryConverter.CreateShapesFromElement(Me, ShapeTypes.Rectangle)
        End Try
        BodyObject = BodyFactory.CreateBody(_world.Simulator)
        BodyObject.Awake = False
        BodyObject.BodyType = BodyType.Dynamic
        shapes.ForEach(Sub(e1) BodyObject.CreateFixture(e1))
        '匹配现有旋转角度
        Dim angle = Utilities.GetTransformFromGroup(Of RotateTransform)(RenderTransform).Angle
        Rotation = If(angle.Equals(0.0), 0.0, angle)
        '匹配现有质量
        If _mass.Equals(-1.0F) Then
            _mass = BodyObject.Mass
        Else
            Mass = _mass
        End If
        '匹配现有位置
        Position = PhysicsUtilities.GetOffsetPositionInScreen(Me)
        '同步Body和Canvas的属性数据，因为这个事件发生前部分属性可能已更改
        FrictionCoefficient = _frictionCoefficient
        CollisionEventDisable = _collisionEventDisable
        IsStatic = _isStatic
        IsSensor = _isSensor
        Enabled = _isCanvasEnabled
        EnablePhysics = _isBodyEnabled
        IsBullet = _isBullet
        ShowFixtures = _world.ShowFixtures
        InternalName = Name
        CollisionGroup = _collisionGroup
        CausesCollisions = _causesCollisions
        RestitutionCoefficient = _restitutionCoefficient
        AngularDamping = _angularDamping
        LinearDamping = _linerDamping
        ManipulationEnabled = _manipulationEnabled
        '更新物体显示
        Update()
        _world.AddPhysicsSprite(Me, True)
        BodyObject.Awake = True
    End Sub

    ''' <summary>
    ''' 将指定点的坐标变换为相对物理世界元素的坐标
    ''' </summary>
    ''' <param name="elem">点所在的元素</param>
    ''' <param name="pt">要变换的点</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TransformToCanvas(elem As UIElement, pt As Point) As Point
        Return elem.TransformToVisual(_world).Transform(pt)
    End Function

    ''' <summary>
    ''' 更新这个物体的显示状态
    ''' </summary>
    Public Overridable Sub Update()
        If Single.IsNaN(BodyObject.Position.X) OrElse Single.IsNaN(BodyObject.Position.Y) Then Exit Sub
        '更新显示位置
        Dim pos As Vector2 = BoundaryConverter.WorldToScreen(BodyObject.Position)
        pos.X -= Width / 2.0
        pos.Y -= Height / 2.0
        If Not _position.X.Equals(pos.X) Then
            _position.X = pos.X
            SetLeft(Me, _position.X)
        End If
        If Not _position.Y.Equals(pos.Y) Then
            _position.Y = pos.Y
            SetTop(Me, _position.Y)
        End If
        '更新旋转角度
        If Math.Abs(PhysicsUtilities.DegreesToRadians(_rotation) - BodyObject.Rotation) > 0.017F Then
            _rotation = PhysicsUtilities.RadiansToDegrees(BodyObject.Rotation)
            Utilities.GetTransformFromGroup(Of RotateTransform)(RenderTransform).Angle = _rotation
        End If
        '更新调试显示
        If _showFixtures Then
            If BodyObject.Awake Then
                PathDebug.Opacity = 1
                PathDebugResting.Opacity = 0
            Else
                PathDebug.Opacity = 0
                PathDebugResting.Opacity = 1
            End If
        End If
    End Sub

    ''' <summary>
    ''' 立即释放这个物体所持有的资源
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Dispose() Implements IDisposable.Dispose
        Enabled = False
        IsEnabled = False
        RemoveHandler ManipulationStarted, AddressOf Manipulation_Started
        RemoveHandler ManipulationDelta, AddressOf Manipulation_Delta
        RemoveHandler ManipulationCompleted, AddressOf Manipulation_Completed
        RemoveCollisionHandler()
    End Sub

    ''' <summary>
    ''' 移除这个物体的碰撞检测事件
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RemoveCollisionHandler()
        BodyObject.FixtureList.ForEach(Sub(e) e.OnCollision = Nothing)
    End Sub
End Class