Imports System.Collections.Generic
Imports System.Linq
Imports FarseerPhysics.Controllers
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Dynamics.Contacts
Imports FarseerPhysics.Dynamics.Joints
Imports Microsoft.Xna.Framework
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media

Public Class PhysicsWorld : Inherits Canvas : Implements IDisposable
    Private _scaleParent As FrameworkElement

#Region "属性们"

    Private ReadOnly _simulator As World
    ''' <summary>
    ''' 获取这个物理世界的模拟器
    ''' </summary>
    Public ReadOnly Property Simulator As World
        Get
            Return _simulator
        End Get
    End Property

    Private ReadOnly _physicsObjects As Dictionary(Of String, PhysicsSprite)
    ''' <summary>
    ''' 获取当前世界中所有的物理对象
    ''' </summary>
    Public ReadOnly Property PhysicsObjects() As Dictionary(Of String, PhysicsSprite)
        Get
            Return _physicsObjects
        End Get
    End Property

    ''' <summary>
    ''' 是否暂停物理模拟
    ''' </summary>
    Public Property PauseSimulation() As Boolean

    ''' <summary>
    ''' 获取或设置这个物理世界的横向重力
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GravityHorizontal() As Single
        Get
            Return Simulator.Gravity.X
        End Get
        Set(value As Single)
            Simulator.Gravity.X = value
        End Set
    End Property

    ''' <summary>
    ''' 获取或设置这个物理世界的纵向重力
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GravityVertical() As Single
        Get
            Return Simulator.Gravity.Y
        End Get
        Set(value As Single)
            Simulator.Gravity.Y = value
        End Set
    End Property

    Private _showFixtures As Boolean = False
    ''' <summary>
    ''' 是否显示物体的关联装置
    ''' </summary>
    Public Property ShowFixtures() As Boolean
        Get
            Return _showFixtures
        End Get
        Set(value As Boolean)
            _showFixtures = value
            PhysicsObjects.Values.ToList.ForEach(Sub(e) e.ShowFixtures = value)
        End Set
    End Property

    Private _allowStaticObjectManipulation As Boolean = False
    ''' <summary>
    ''' 是否允许这个世界中的物体在静态状态下也响应用户操作事件
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllowManipulationWhenStatic As Boolean
        Get
            Return _allowStaticObjectManipulation
        End Get
        Set(value As Boolean)
            _allowStaticObjectManipulation = value
            PhysicsObjects.Values.ToList.ForEach(Sub(e) e.AllowManipulationWhenStatic = value)
        End Set
    End Property
#End Region

#Region "用户操作处理"

    Private _isManipulationStarted As Boolean
    Private _mouseManipulationTarget As PhysicsSprite

    Friend Sub RegisterMouseManipulation(target As PhysicsSprite)
        If _mouseManipulationTarget IsNot Nothing Then _mouseManipulationTarget.FinishManiupulation()
        _mouseManipulationTarget = target
    End Sub

    Private _manipulationEnabled As Boolean = False
    ''' <summary>
    ''' 是否启用这个世界中物体的操作响应
    ''' 仅当物理世界启用操作相应且对应物体也启用操作相应时用户操作才会被相应
    ''' </summary>
    Public Property ManipulationEnabled() As Boolean
        Get
            Return _manipulationEnabled
        End Get
        Set(value As Boolean)
            If value <> _manipulationEnabled Then
                If value Then
                    AddHandler MouseMove, AddressOf Mouse_Move
                    AddHandler MouseLeftButtonDown, AddressOf Mouse_Click
                Else
                    RemoveHandler MouseMove, AddressOf Mouse_Move
                    RemoveHandler MouseLeftButtonDown, AddressOf Mouse_Click
                End If
            End If
            _manipulationEnabled = value
        End Set
    End Property

    Private Sub Mouse_Click(sender As Object, e As Input.MouseButtonEventArgs)
        If _mouseManipulationTarget Is Nothing Then Exit Sub
        If _isManipulationStarted Then
            _mouseManipulationTarget.FinishManiupulation()
            _mouseManipulationTarget = Nothing
            _isManipulationStarted = False
        Else
            _mouseManipulationTarget.AccessManipulation(_mouseManipulationTarget, e.GetPosition(_mouseManipulationTarget))
            _isManipulationStarted = True
        End If
    End Sub

    Private Sub Mouse_Move(sender As Object, e As Input.MouseEventArgs)
        If _isManipulationStarted Then
            _mouseManipulationTarget.ContinueManipulation(_mouseManipulationTarget, e.GetPosition(_mouseManipulationTarget))
        End If
    End Sub
#End Region

#Region "事件"

    ''' <summary>
    ''' 物理世界发生一次更新
    ''' </summary>
    Public Event PhysicsTimeStep As Action(Of Object)

    ''' <summary>
    ''' 物理世界中的物体发生一次碰撞
    ''' </summary>
    Public Event PhysicsCollision As Action(Of PhysicsSprite, PhysicsSprite, Contact)
#End Region

    Public Sub New(gravityHorizontal As Single, gravityVertical As Single)
        _simulator = New World(New Vector2(gravityHorizontal, gravityVertical))
        _physicsObjects = New Dictionary(Of String, PhysicsSprite)
    End Sub

    Public Sub New()
        Me.New(0.0F, 9.8F)
    End Sub

    Private Sub PhysicsWorld_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        RenderTransform = Utilities.GenerateTransformGroup
        BoundaryConverter.InitialiseWorld(Me)
    End Sub

    ''' <summary>
    ''' 将这个物理世界元素调整到对应元素大小，并始终保持与其大小一致
    ''' </summary>
    ''' <param name="target"></param>
    Public Sub ScaleToObject(target As FrameworkElement)
        _scaleParent = target
        AddHandler _scaleParent.SizeChanged, AddressOf Parent_SizeChanged
        Parent_SizeChanged(Me, New EventArgs)
    End Sub

    ''' <summary>
    ''' 当ScaleToObject元素的大小发生变化时自动调整显示区域大小
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Parent_SizeChanged(sender As Object, e As SizeChangedEventArgs)
        Dim parentContainer As FrameworkElement = sender
        Dim scaleX As Double = parentContainer.ActualWidth / Width
        Dim scaleY As Double = parentContainer.ActualHeight / Height
        If scaleX > scaleY Then
            scaleX = scaleY
        Else
            scaleY = scaleX
        End If
        Dim transform = Utilities.GetTransformFromGroup(Of ScaleTransform)(RenderTransform)
        transform.ScaleX = scaleX
        transform.ScaleY = scaleY
        Utilities.GetTransformFromGroup(Of TranslateTransform)(RenderTransform).X = (parentContainer.ActualWidth - Width * scaleX) / 2
        UpdateLayout()
    End Sub

    ''' <summary>
    ''' 唤醒这个物理世界中的所有物体
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub WakeUpAllSprites()
        PhysicsObjects.Values.ToList.ForEach(Sub(e) e.BodyObject.Awake = True)
    End Sub

    ''' <summary>
    ''' 更新物理世界显示状态
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateRender()
        If PauseSimulation Then Return
        PhysicsObjects.Values.Where(Function(e) e.ShowFixtures OrElse e.BodyObject.Awake).ToList.ForEach(Sub(e) e.Update())
    End Sub

    ''' <summary>
    ''' 更新物理世界逻辑状态
    ''' </summary>
    ''' <param name="stepTime">要步进的时间(秒)</param>
    ''' <remarks></remarks>
    Public Sub UpdateStatus(stepTime As Single)
        If PauseSimulation Then Return
        Simulator.Step(stepTime)
        CollisionStore.Collisions.ForEach(Sub(e)
                                              RaiseEvent PhysicsCollision(e.SpriteA, e.SpriteB, e.Contact)
                                              If Config.WorldCollisionHandle IsNot Nothing Then Config.WorldCollisionHandle.Invoke(e.SpriteA, e.SpriteB, e.Contact)
                                          End Sub)
        CollisionStore.Collisions.Clear()
        RaiseEvent PhysicsTimeStep(Me)
    End Sub

    ''' <summary>
    ''' 添加一个物体到物理世界中
    ''' </summary>
    ''' <param name="element">要添加的物体</param>
    ''' <param name="enablePhysics">是否启用物理模拟，此项无法在之后修改</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddPhysicsSprite(element As PhysicsSprite, enablePhysics As Boolean) As PhysicsSprite
        If enablePhysics Then
            Dim thisName = element.Name
            element.SetValue(TagProperty, thisName)
            If element.Parent IsNot Nothing Then TryCast(element.Parent, Panel).Children.Remove(element)
            thisName = PhysicsUtilities.EnsureUniqueName(Me, element, thisName)
            element.Name = thisName
            PhysicsObjects.Add(thisName, element)
            element.Update()
        End If
        Children.Add(element)
        element.IsManipulationEnabled = ManipulationEnabled
        Return element
    End Function

    ''' <summary>
    ''' 添加一个关节到物理世界
    ''' </summary>
    ''' <param name="joint"></param>
    Friend Sub AddPhysicsJoint(joint As PhysicsJoint)
        Dim bodyOne As String = joint.BodyA
        Dim bodyTwo As String = joint.BodyB
        Dim collisionGroup As Short = Convert.ToInt16(joint.CollisionGroup)
        Dim isAngleSpringEnabled As Boolean = joint.AngleSpringEnabled
        Dim springConstant As Single = CSng(joint.AngleSpringConstant)
        Dim angleLowerLimit As Single = CSng(joint.AngleLowerLimit)
        Dim angleUpperLimit As Single = CSng(joint.AngleUpperLimit)
        Dim center As Point = joint.GetCenter()
        Dim ptCollisionCenter As New Vector2(CSng(center.X), CSng(center.Y))
        If Not PhysicsObjects.ContainsKey(bodyOne) Then
            Throw New Exception("找不到关节所连接的物体 " & bodyOne & ", 是否忘记添加PhysicsObjectBehavior?")
        End If
        If Not PhysicsObjects.ContainsKey(bodyTwo) Then
            Throw New Exception("找不到关节所连接的物体 " & bodyTwo & ", 是否忘记添加PhysicsObjectBehavior?")
        End If
        Dim body1 As Body = PhysicsObjects(bodyOne).BodyObject
        Dim body2 As Body = PhysicsObjects(bodyTwo).BodyObject
        Dim ptCollisionCenterA As Vector2 = BoundaryConverter.ScreenToWorld(ptCollisionCenter)
        Dim ptCollisionCenterB As Vector2 = BoundaryConverter.ScreenToWorld(ptCollisionCenter)
        ptCollisionCenterA -= body1.Position
        ptCollisionCenterB -= body2.Position
        If joint.IsWeldJoint Then
            Simulator.AddJoint(New WeldJoint(body1, body2, ptCollisionCenterA, ptCollisionCenterB))
        ElseIf joint.IsDistanceJoint Then
            Simulator.AddJoint(New DistanceJoint(body1, body2, ptCollisionCenterA, ptCollisionCenterB))
        Else
            Dim revoluteJoint As New RevoluteJoint(body1, body2, ptCollisionCenterA, ptCollisionCenterB)
            Simulator.AddJoint(revoluteJoint)
            If isAngleSpringEnabled Then
                Simulator.AddJoint(New AngleJoint(body1, body2) With {.TargetAngle = 0, .Softness = springConstant})
            End If
            If angleUpperLimit.Equals(-1.0) AndAlso angleLowerLimit.Equals(-1.0) Then
                revoluteJoint.LimitEnabled = True
                revoluteJoint.LowerLimit = PhysicsUtilities.DegreesToRadians(angleLowerLimit)
                revoluteJoint.UpperLimit = PhysicsUtilities.DegreesToRadians(angleUpperLimit)
            End If
        End If
        If collisionGroup > 0 Then
            PhysicsObjects(bodyOne).BodyObject.FixtureList.ForEach(Sub(e) e.CollisionGroup = collisionGroup)
            PhysicsObjects(bodyTwo).BodyObject.FixtureList.ForEach(Sub(e) e.CollisionGroup = collisionGroup)
        End If
        joint.Visibility = Visibility.Collapsed
    End Sub

    ''' <summary>
    ''' 添加用户控件并作为不检测碰撞的物理对象使用
    ''' </summary>
    ''' <param name="uc">要添加的用户控件，它至少包含一个物理对象</param>
    ''' <param name="left">与显示区域左上角的横向距离</param>
    ''' <param name="top">与显示区域左上角的纵向距离</param>
    Public Sub AddPhysicsUserControl(uc As UserControl, left As Double, top As Double)
        AddPhysicsUserControl(uc, left, top, -1)
    End Sub

    ''' <summary>
    ''' 添加用户控件并作为物理对象使用
    ''' </summary>
    ''' <param name="uc">要添加的用户控件，它至少包含一个物理对象</param>
    ''' <param name="left">与显示区域左上角的横向距离</param>
    ''' <param name="top">与显示区域左上角的纵向距离</param>
    ''' <param name="collisionGroup">这个用户控件所包含的所有物理对象所属的碰撞组</param>
    Public Function AddPhysicsUserControl(uc As UserControl, left As Double, top As Double, collisionGroup As Integer) As List(Of PhysicsSprite)
        Dim spriteList As New List(Of PhysicsSprite)()
        SetLeft(uc, left)
        SetTop(uc, top)
        PhysicsUtilities.RedirectStoryboardTargets(uc)
        PhysicsUtilities.EnsureUniqueNames(uc, Guid.NewGuid(), collisionGroup, spriteList)
        Children.Add(uc)
        spriteList.ForEach(Sub(e) e.UserControlParent = uc)
        Return spriteList
    End Function

    ''' <summary>
    ''' 删除一个物理对象
    ''' </summary>
    ''' <param name="sprite">要删除的对象的名称</param>
    Public Sub DeletePhysicsObject(sprite As String)
        If PhysicsObjects.ContainsKey(sprite) Then
            Dim spr As PhysicsSprite = PhysicsObjects(sprite)
            DeleteObject(spr.BodyObject)
            Children.Remove(spr)
            PhysicsObjects.Remove(sprite)
            spr.Dispose()
        End If
    End Sub

    ''' <summary>
    ''' 立即释放这个物理世界所持有的资源
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Dispose() Implements IDisposable.Dispose
        For i = PhysicsObjects.Values.Count - 1 To 0 Step -1
            Dim spr As PhysicsSprite = PhysicsObjects.Values.Last()
            DeletePhysicsObject(spr.Name)
        Next
        RemoveHandler _scaleParent.SizeChanged, AddressOf Parent_SizeChanged
    End Sub

    ''' <summary>
    ''' 从物理世界模拟器中删除物体
    ''' </summary>
    ''' <param name="target">要删除的物体</param>
    ''' <remarks></remarks>
    Private Sub DeleteObject(target As Object)
        If TypeOf target Is Body Then
            Simulator.RemoveBody(TryCast(target, Body))
        ElseIf TypeOf target Is Controller Then
            Simulator.RemoveController(TryCast(target, Controller))
        ElseIf TypeOf target Is Joint Then
            Simulator.RemoveJoint(TryCast(target, Joint))
        Else
            Throw New Exception("你无法从物理世界中删除不是Body，不是Controller，也不是Joint的对象")
        End If
    End Sub
End Class