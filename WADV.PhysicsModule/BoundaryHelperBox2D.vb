Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports FarseerPhysics
Imports FarseerPhysics.Collision.Shapes
Imports FarseerPhysics.Common
Imports FarseerPhysics.Common.Decomposition
Imports FarseerPhysics.Common.ConvexHull
Imports Microsoft.Xna.Framework

''' <summary>
''' Box2D物理世界及显示区域范围计算辅助类
''' </summary>
''' <remarks></remarks>
Public Class BoundaryConverter

    Shared Sub New()
        PixelsPerMeter = 100.0F
    End Sub

    ''' <summary>
    ''' 初始化物理世界元素显示大小
    ''' </summary>
    ''' <param name="world">要初始化的物理世界元素</param>
    ''' <remarks></remarks>
    Public Shared Sub InitialiseWorld(world As PhysicsWorld)
        If Double.IsNaN(world.Width) OrElse Double.IsNaN(world.Height) Then
            If Double.IsNaN(world.ActualWidth) OrElse Double.IsNaN(world.ActualHeight) OrElse world.ActualWidth.Equals(0.0) OrElse world.ActualHeight.Equals(0.0) Then
                world.Width = 1280.0
                world.Height = 720.0
                Debug.WriteLine("错误: 你没有给显示区域设置有效的大小，它将会被自动调整为1280(px)x720(px)")
            Else
                world.Width = world.ActualWidth
                world.Height = world.ActualHeight
                Debug.WriteLine("错误: 你没有给显示区域设置有效的大小，它将会被自动调整为" & world.Width & "(px)x" & world.Height & "(px)")
            End If
        End If
    End Sub

    ''' <summary>
    ''' 图像三角化方式
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DecomposerTypes
        Bayazit
        CDT
        Earclip
        Flipcode
        Melkman
        Giftwrap
    End Enum

    ''' <summary>
    ''' 获取或设置图形三角化方式
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Property DecomposerType() As DecomposerTypes

    Private Shared _pixelPerMeter As Single
    ''' <summary>
    ''' 获取或设置物理世界中每米所表示的显示区域像素数
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Property PixelsPerMeter As Single
        Get
            Return _pixelPerMeter
        End Get
        Set(value As Single)
            _pixelPerMeter = value
            ConvertUnits.SetDisplayUnitToSimUnitRatio(value)
        End Set
    End Property

    ''' <summary>
    ''' 根据物体和基础形状生成物体的包覆形状
    ''' </summary>
    ''' <param name="entity">目标物体</param>
    ''' <param name="shapeType">基础形状</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateShapesFromElement(entity As PhysicsSprite, shapeType As PhysicsSprite.ShapeTypes) As List(Of Shape)
        Dim shapeDefs As New List(Of Shape)()
        Select Case shapeType
            Case PhysicsSprite.ShapeTypes.Ellipse
                shapeDefs.Add(New CircleShape(ScreenToWorld(entity.Width / 2.0), 0.1F))
            Case PhysicsSprite.ShapeTypes.Rectangle
                shapeDefs.Add(New PolygonShape(PhysicsUtilities.CreateRectangle(entity.Width / 2.0, entity.Height / 2.0), 0.1F))
            Case Else
                Dim vertices As New Vertices()
                Dim pathBoundary As Shapes.Path = TryCast(entity, Canvas).Children(0)
                If pathBoundary Is Nothing Then
                    Throw New Exception("物体 " & entity.Name & " 确实存在多边形但却不是它的第一个UI元素, 你必须保证多边形是它的第一个UI元素，即便你不打算显示它")
                Else
                    Dim pathGeom As PathGeometry = pathBoundary.Data
                    If pathGeom Is Nothing Then
                        Throw New Exception("物体 " & entity.Name & " 的第一个UI元素确实是多边形, 但是它却没有定义任何几何对象, 请保证它至少是有几何对象的")
                    Else
                        For Each figure As PathFigure In pathGeom.Figures
                            Dim ptStart As Point = figure.StartPoint
                            ptStart.X = ptStart.X + (entity.Width / 2.0) + Canvas.GetLeft(pathBoundary)
                            ptStart.Y = ptStart.Y + (entity.Height / 2.0) + Canvas.GetTop(pathBoundary)
                            vertices.Add(ScreenToWorld(ptStart))
                            For Each segment As PathSegment In figure.Segments
                                If TypeOf segment Is LineSegment Then
                                    Dim lineSegment As LineSegment = segment
                                    Dim ptNext As Point = lineSegment.Point
                                    ptNext.X = ptNext.X + (entity.Width / 2.0) + Canvas.GetLeft(pathBoundary)
                                    ptNext.Y = ptNext.Y + (entity.Height / 2.0) + Canvas.GetTop(pathBoundary)
                                    vertices.Add(ScreenToWorld(ptNext))
                                ElseIf TypeOf segment Is BezierSegment Then
                                    Dim bezSegment As BezierSegment = segment
                                    Dim ptNext As Point = bezSegment.Point3
                                    ptNext.X = ptNext.X + (entity.Width / 2.0) + Canvas.GetLeft(pathBoundary)
                                    ptNext.Y = ptNext.Y + (entity.Height / 2.0) + Canvas.GetTop(pathBoundary)
                                    vertices.Add(ScreenToWorld(bezSegment.Point2))
                                End If
                            Next
                        Next
                        Dim verticesDecomposed As List(Of Vertices)
                        Select Case DecomposerType
                            Case DecomposerTypes.CDT
                                verticesDecomposed = Triangulate.ConvexPartition(vertices, TriangulationAlgorithm.Delauny)
                                Exit Select
                            Case DecomposerTypes.Earclip
                                verticesDecomposed = Triangulate.ConvexPartition(vertices, TriangulationAlgorithm.Earclip)
                                Exit Select
                            Case DecomposerTypes.Flipcode
                                verticesDecomposed = Triangulate.ConvexPartition(vertices, TriangulationAlgorithm.Flipcode)
                                Exit Select
                            Case DecomposerTypes.Melkman
                                verticesDecomposed = New List(Of Vertices)()
                                verticesDecomposed.Add(Melkman.GetConvexHull(vertices))
                                Exit Select
                            Case DecomposerTypes.Giftwrap
                                verticesDecomposed = New List(Of Vertices)()
                                verticesDecomposed.Add(GiftWrap.GetConvexHull(vertices))
                                Exit Select
                            Case Else
                                verticesDecomposed = Triangulate.ConvexPartition(vertices, TriangulationAlgorithm.Bayazit)
                                Exit Select
                        End Select
                        For Each vertexDecomposed In verticesDecomposed.Where(Function(e) e.Count > 2)
                            If vertexDecomposed.Count > Settings.MaxPolygonVertices Then
                                Dim skipNum As Double = vertexDecomposed.Count - Settings.MaxPolygonVertices
                                skipNum = Math.Round(Convert.ToDouble(vertexDecomposed.Count) / skipNum)
                                Dim i As Integer = vertexDecomposed.Count - 1
                                While i >= 0
                                    vertexDecomposed.Remove(vertexDecomposed(i))
                                    i -= CInt(skipNum)
                                End While
                            End If
                            shapeDefs.Add(New PolygonShape(vertexDecomposed, 0.1F))
                        Next
                    End If
                End If
                Exit Select
        End Select
        Return shapeDefs
    End Function

    ''' <summary>
    ''' 将显示区域长度转换为物理世界长度
    ''' </summary>
    ''' <param name="distance">要转换的长度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ScreenToWorld(distance As Single) As Single
        Return ConvertUnits.ToSimUnits(distance)
    End Function

    ''' <summary>
    ''' 将显示区域向量转换为物理世界向量
    ''' </summary>
    ''' <param name="vector">要转换的向量</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ScreenToWorld(vector As Vector2) As Vector2
        Return ConvertUnits.ToSimUnits(vector)
    End Function

    ''' <summary>
    ''' 将显示区域坐标转换为物理世界坐标
    ''' </summary>
    ''' <param name="point">要转换的坐标</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ScreenToWorld(point As Point) As Vector2
        Return ConvertUnits.ToSimUnits(point.X, point.Y)
    End Function

    ''' <summary>
    ''' 将显示区域范围转换为物理世界范围
    ''' </summary>
    ''' <param name="size">要转换的范围</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ScreenToWorld(size As Size) As Vector2
        Return ConvertUnits.ToSimUnits(size.Width, size.Height)
    End Function

    ''' <summary>
    ''' 将物理世界长度转换为显示区域长度
    ''' </summary>
    ''' <param name="distance">要转换的长度</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function WorldToScreen(distance As Single) As Single
        Return ConvertUnits.ToDisplayUnits(distance)
    End Function

    ''' <summary>
    ''' 将物理世界向量转换为显示区域向量
    ''' </summary>
    ''' <param name="vector">要转换的向量</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function WorldToScreen(vector As Vector2) As Vector2
        Return ConvertUnits.ToDisplayUnits(vector)
    End Function

    ''' <summary>
    ''' 将物理世界坐标转换为显示区域坐标
    ''' </summary>
    ''' <param name="point">要转换的坐标</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function WorldToScreen(point As Point) As Vector2
        Return ConvertUnits.ToDisplayUnits(point.X, point.Y)
    End Function

    ''' <summary>
    ''' 将物理世界范围转换为显示区域范围
    ''' </summary>
    ''' <param name="size">要转换的范围</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function WorldToScreen(size As Size) As Vector2
        Return ConvertUnits.ToDisplayUnits(size.Width, size.Height)
    End Function
End Class