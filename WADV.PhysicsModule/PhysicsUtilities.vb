Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports FarseerPhysics.Collision.Shapes
Imports FarseerPhysics.Common
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Dynamics.Joints
Imports Microsoft.Xna.Framework

Friend Class PhysicsUtilities

    ''' <summary>
    ''' 根据物体寻找所属的物理世界
    ''' </summary>
    ''' <param name="element">目标物体的界面元素</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function FindPhysicsWorld(element As FrameworkElement) As PhysicsWorld
        If element IsNot Nothing Then
            If TypeOf element Is PhysicsWorld Then
                Return DirectCast(element, PhysicsWorld)
            Else
                Return FindPhysicsWorld(TryCast(VisualTreeHelper.GetParent(element), FrameworkElement))
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' 获取指定物体相对于屏幕左上角的坐标
    ''' </summary>
    ''' <param name="target">目标物体</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function GetOffsetPositionInScreen(target As PhysicsSprite) As Vector2
        Dim offset As New Vector2 With {.X = Canvas.GetLeft(target), .Y = Canvas.GetTop(target)}
        Dim parent As FrameworkElement = target.Parent
        While parent IsNot Nothing
            Dim point = Canvas.GetLeft(parent)
            offset.X += If(Double.IsNaN(point), 0.0F, point)
            point = Canvas.GetTop(parent)
            offset.Y += If(Double.IsNaN(point), 0.0F, point)
            If TypeOf parent Is PhysicsWorld Then Exit While
            parent = parent.Parent
        End While
        Return offset
    End Function

    ''' <summary>
    ''' 获取指定点距离指定线段的距离
    ''' </summary>
    ''' <param name="lineStart">目标线段的起点</param>
    ''' <param name="lineEnd">目标线段的终点</param>
    ''' <param name="point">要计算的点</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function DistanceToLine(lineStart As Point, lineEnd As Point, point As Point) As Double
        Dim xDelta As Double = lineEnd.X - lineStart.X
        Dim yDelta As Double = lineEnd.Y - lineStart.Y
        Dim u As Double = ((point.X - lineStart.X) * xDelta + (point.Y - lineStart.Y) * yDelta) / (xDelta * xDelta + yDelta * yDelta)
        Dim closestPoint As Point
        If u < 0 Then
            closestPoint = lineStart
        ElseIf u > 1 Then
            closestPoint = lineEnd
        Else
            closestPoint = New Point(lineStart.X + u * xDelta, lineStart.Y + u * yDelta)
        End If
        Return DistanceBetweenTwoPoints(closestPoint, point)
    End Function

    ''' <summary>
    ''' 将角度转为弧度
    ''' </summary>
    ''' <param name="angle">要转换的角度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function DegreesToRadians(angle As Double) As Double
        Return Math.PI * angle / 180.0
    End Function

    ''' <summary>
    ''' 将弧度转为角度
    ''' </summary>
    ''' <param name="radians">要转换的弧度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function RadiansToDegrees(radians As Double) As Double
        Return (180 / Math.PI) * radians
    End Function

    ''' <summary>
    ''' 获取两点的距离
    ''' </summary>
    ''' <param name="pt1">目标点1</param>
    ''' <param name="pt2">目标点2</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function DistanceBetweenTwoPoints(pt1 As Point, pt2 As Point) As Double
        Dim a As Double = pt2.X - pt1.X
        Dim b As Double = pt2.Y - pt1.Y
        Return Math.Sqrt(a * a + b * b)
    End Function

    ''' <summary>
    ''' 根据给定原点、角度和半径在极坐标系中计算目标点
    ''' </summary>
    ''' <param name="angle">偏转角度</param>
    ''' <param name="distance">半径</param>
    ''' <param name="ptStart">原点位置</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function GetPointFromDistanceAngle(angle As Double, distance As Double, ptStart As Point) As Point
        Dim theta As Double = angle * 0.0174532925
        Dim p As New Point()
        p.X = ptStart.X + distance * Math.Cos(theta)
        p.Y = ptStart.Y + distance * Math.Sin(theta)
        Return p
    End Function

    ''' <summary>
    ''' 根据顶点绘制路径
    ''' </summary>
    ''' <param name="body">要绘制的物体的物理对象</param>
    ''' <param name="element">要绘制的物体</param>
    ''' <param name="colorFill">要填充的颜色</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function CreatePathFromVertices(body As Body, element As PhysicsSprite, colorFill As Color) As Shapes.Shape
        Dim vertices As List(Of Fixture) = body.FixtureList
        If TypeOf vertices(0).Shape Is CircleShape Then
            Dim cs As CircleShape = vertices(0).Shape
            Dim screenSize As Vector2 = BoundaryConverter.WorldToScreen(New Vector2(cs.Radius * 2.0F, cs.Radius * 2.0F))
            Dim el As New Ellipse With {
                .Width = screenSize.X,
                .Height = screenSize.Y,
                .Stroke = New SolidColorBrush(Color.FromArgb(255, 76, 108, 76)),
                .StrokeStartLineCap = PenLineCap.Round,
                .StrokeThickness = 2,
                .Fill = New SolidColorBrush(colorFill)}
            Return el
        Else
            Dim path As New Shapes.Path With {
                .Stroke = New SolidColorBrush(Color.FromArgb(255, 76, 108, 76)),
                .StrokeThickness = 2,
                .StrokeStartLineCap = PenLineCap.Round,
                .Fill = New SolidColorBrush(colorFill)}
            Dim pathGeom As New PathGeometry(New PathFigureCollection)
            Dim halfWidth As Double = element.Width / 2.0
            Dim halfHeight As Double = element.Height / 2.0
            If halfWidth.Equals(0.0) Then halfWidth = element.ActualWidth / 2.0
            If halfHeight.Equals(0.0) Then halfHeight = element.ActualHeight / 2.0
            For Each f As Fixture In vertices.Where(Function(e) TypeOf e.Shape Is PolygonShape)
                Dim figure As New PathFigure()
                Dim p As PolygonShape = TryCast(f.Shape, PolygonShape)
                Dim ptStart As New Vector2(p.Vertices(0).X, p.Vertices(0).Y)
                ptStart = BoundaryConverter.WorldToScreen(ptStart)
                ptStart.X += halfWidth
                ptStart.Y += halfHeight
                figure.StartPoint = New Point(ptStart.X, ptStart.Y)
                figure.Segments = New PathSegmentCollection
                pathGeom.Figures.Add(figure)
                For i As Integer = 1 To p.Vertices.Count - 1
                    Dim line As New LineSegment
                    Dim pt As New Vector2(p.Vertices(i).X, p.Vertices(i).Y)
                    pt = BoundaryConverter.WorldToScreen(pt)
                    pt.X += halfWidth
                    pt.Y += halfHeight
                    line.Point = New Point(pt.X, pt.Y)
                    figure.Segments.Add(line)
                Next
            Next
            path.Data = pathGeom
            Return path
        End If
    End Function

    ''' <summary>
    ''' 重新链接元素及其子元素的故事板
    ''' </summary>
    ''' <param name="uc">要重新链接的元素</param>
    ''' <remarks></remarks>
    Friend Shared Sub RedirectStoryboardTargets(uc As FrameworkElement)
        For Each item As Object In uc.Resources
            If TypeOf item Is KeyValuePair(Of Object, Object) Then
                Dim itemValue = CType(item, KeyValuePair(Of Object, Object)).Value
                If TypeOf itemValue Is Storyboard Then
                    Dim sb As Storyboard = itemValue
                    For Each tl As Timeline In sb.Children
                        Dim target As String = tl.GetValue(Storyboard.TargetNameProperty).ToString()
                        Dim targetElement As UIElement = TryCast(uc.FindName(target), UIElement)
                        If targetElement IsNot Nothing Then Storyboard.SetTarget(tl, targetElement)
                    Next
                End If
            End If
        Next
        For i = 0 To VisualTreeHelper.GetChildrenCount(uc) - 1
            Dim child As DependencyObject = VisualTreeHelper.GetChild(uc, i)
            Dim dependencyObject = TryCast(child, FrameworkElement)
            If (dependencyObject IsNot Nothing) Then RedirectStoryboardTargets(dependencyObject)
        Next
    End Sub

    ''' <summary>
    ''' 获取两点间的偏转角度
    ''' </summary>
    ''' <param name="pt1">目标点1</param>
    ''' <param name="pt2">目标点2</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function GetAngleBetween(pt1 As Point, pt2 As Point) As Double
        Dim retval As Double = 57.2727272727273 * Math.Atan2(pt2.X - pt1.X, pt2.Y - pt1.Y)
        Return retval
    End Function

    ''' <summary>
    ''' 一般化目标角度
    ''' </summary>
    ''' <param name="angle">要处理的角度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function NormalizeAngle(angle As Double) As Double
        If angle < 0 OrElse angle > Math.PI * 2 Then
            Return Math.Abs((Math.PI * 2) - Math.Abs(angle))
        Else
            Return angle
        End If
    End Function

    ''' <summary>
    ''' 在物理世界中寻找指定关节
    ''' </summary>
    ''' <param name="cnvGame">目标物理世界</param>
    ''' <param name="body1">关节一端的物体</param>
    ''' <param name="body2">关节另一端的物体</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function FindJoint(cnvGame As PhysicsWorld, body1 As Body, body2 As Body) As List(Of Joint)
        Dim joint As New List(Of Joint)()
        For Each x As Joint In cnvGame.Simulator.JointList.Where(Function(e) (e.BodyA Is body1 AndAlso e.BodyB Is body2) OrElse (e.BodyB Is body1 AndAlso e.BodyA Is body2))
            joint.Add(x)
        Next
        Return joint
    End Function

    ''' <summary>
    ''' 判断目标点是否在指定矩形内
    ''' </summary>
    ''' <param name="rect">目标矩形</param>
    ''' <param name="pt">要判断的点</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function IsPointInside(rect As Rect, pt As Vector2) As Boolean
        Return (pt.X > rect.Left AndAlso pt.X < rect.Right AndAlso pt.Y > rect.Top AndAlso pt.Y < rect.Bottom)
    End Function

    ''' <summary>
    ''' 根据长宽创建矩形
    ''' </summary>
    ''' <param name="halfX">长度的一半</param>
    ''' <param name="halfY">宽度的一半</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function CreateRectangle(halfX As Single, halfY As Single) As Vertices
        Dim worldX = BoundaryConverter.ScreenToWorld(halfX)
        Dim worldY = BoundaryConverter.ScreenToWorld(halfY)
        Dim vertices As New Vertices(3)
        vertices.Add(New Vector2(-worldX, -worldY))
        vertices.Add(New Vector2(worldX, -worldY))
        vertices.Add(New Vector2(worldX, worldY))
        vertices.Add(New Vector2(-worldX, worldY))
        Return vertices
    End Function

    ''' <summary>
    ''' 根据长宽、中心和旋转角度创建矩形
    ''' </summary>
    ''' <param name="halfX">长度的一半</param>
    ''' <param name="halfY">宽度的一半</param>
    ''' <param name="center">矩形中心</param>
    ''' <param name="angle">旋转角度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function CreateRectangle(halfX As Single, halfY As Single, center As Vector2, angle As Single) As Vertices
        Dim vertices As Vertices = CreateRectangle(halfX, halfY)
        Dim r As New FarseerPhysics.Common.Mat22
        Dim c = Math.Cos(angle), s = Math.Sin(angle)
        r.Set(New Vector2(c, s), New Vector2(-s, c))
        For i As Integer = 0 To 3
            vertices(i) = New Vector2(center.X + r.ex.X * vertices(i).X + r.ey.X * vertices(i).Y, center.Y + r.ex.Y * vertices(i).X + r.ey.Y * vertices(i).Y)
        Next
        Return vertices
    End Function

    ''' <summary>
    ''' 为元素及其所有子元素分配唯一名称
    ''' </summary>
    ''' <param name="uc">要分配的元素们的顶级元素</param>
    ''' <param name="ucOffset">要用于后缀的GUID</param>
    ''' <param name="collisionGroup">这些元素对应物体的碰撞组</param>
    ''' <param name="spriteList">将要盛放这些元素对应物体的列表</param>
    ''' <remarks></remarks>
    Friend Shared Sub EnsureUniqueNames(uc As DependencyObject, ucOffset As Guid, collisionGroup As Integer, spriteList As List(Of PhysicsSprite))
        For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(uc) - 1
            Dim thisChild As DependencyObject = VisualTreeHelper.GetChild(uc, i)
            If TypeOf thisChild Is PhysicsSprite Then
                Dim spr As PhysicsSprite = thisChild
                spr.Name = spr.Name + "_" & ucOffset.ToString()
                spr.InternalName = spr.Name
                If collisionGroup > 0 AndAlso spr.CollisionGroup > 0 Then spr.CollisionGroup = collisionGroup
                spriteList.Add(spr)
            ElseIf TypeOf thisChild Is PhysicsJoint Then
                Dim joint As PhysicsJoint = thisChild
                joint.BodyA = joint.BodyA + "_" & ucOffset.ToString()
                joint.BodyB = joint.BodyB + "_" & ucOffset.ToString()
                If collisionGroup > 0 AndAlso joint.CollisionGroup > 0 Then joint.CollisionGroup = collisionGroup
            ElseIf TypeOf thisChild Is FrameworkElement Then
                Dim child As FrameworkElement = thisChild
                If child.Name IsNot Nothing AndAlso child.Name <> String.Empty Then child.Name = child.Name + "_" & ucOffset.ToString()
            End If
            If TypeOf thisChild Is FrameworkElement Then
                For Each x In TryCast(thisChild, FrameworkElement).Resources
                    If TypeOf x Is KeyValuePair(Of Object, Object) Then
                        Dim storyboard = TryCast(CType(x, KeyValuePair(Of Object, Object)).Value, Storyboard)
                        If (storyboard IsNot Nothing) Then
                            storyboard.SetValue(Canvas.NameProperty, Convert.ToString(storyboard.GetValue(Canvas.NameProperty)) & "_" & ucOffset.ToString())
                        End If

                    End If
                Next
            End If
            EnsureUniqueNames(thisChild, ucOffset, collisionGroup, spriteList)
        Next
    End Sub

    ''' <summary>
    ''' 为元素分配唯一名称
    ''' </summary>
    ''' <param name="cnv">元素所在的容器</param>
    ''' <param name="sprToAdd">要分配名称的元素</param>
    ''' <param name="name">元素的理想名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function EnsureUniqueName(cnv As Canvas, sprToAdd As PhysicsSprite, name As String) As String
        Dim suffix As Integer = 1
        Dim thisName As String = name
        Dim possibleMatch = cnv.FindName(name)
        While possibleMatch IsNot Nothing AndAlso possibleMatch IsNot sprToAdd
            name = thisName & "_" & suffix.ToString()
            suffix += 1
            possibleMatch = cnv.FindName(name)
        End While
        Return name
    End Function

End Class
