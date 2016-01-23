Namespace RAL.Tool
    ''' <summary>
    ''' RAL辅助工具 - 画笔组件
    ''' </summary>
    Public MustInherit Class Brush : Inherits Component
        Private ReadOnly _drawingList As New Concurrent.ConcurrentBag(Of Sprite)

        Public Sub New(type As DrawingType, ParamArray param As Double())
            DrawingType = type
            DrawingParamater = param
        End Sub

        ''' <summary>
        ''' 获取画笔绘图类型
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property DrawingType As DrawingType

        ''' <summary>
        ''' 获取或设置画笔绘图参数
        ''' </summary>
        ''' <returns></returns>
        Public Property DrawingParamater As Double()

        Protected Friend Overrides Function BeforeBinding(sprite As Sprite) As Boolean
            If Not _drawingList.Contains(sprite) Then _drawingList.Add(sprite)
            Return True
        End Function

        Protected Friend Overrides Function BeforeUnbinding(sprite As Sprite, Optional isFromClear As Boolean = False) As Boolean
            If _drawingList.Contains(sprite) Then _drawingList.TryTake(sprite)
            Return True
        End Function

        Protected Friend Overrides Sub BindToScene(sprite As Sprite, scene As Scene)
            If Not _drawingList.Contains(sprite) Then _drawingList.Add(sprite)
        End Sub

        Protected Friend Overrides Sub UnbindFromScene(sprite As Sprite, scene As Scene)
            If _drawingList.Contains(sprite) Then _drawingList.TryTake(sprite)
        End Sub

        Protected Friend Overrides Function LoopOnLogic(frame As Integer) As Boolean
            For Each e In _drawingList
                SyncLock _DrawingParamater
                    SyncLock (e)
                        UpdateDrawInfo(e)
                    End SyncLock
                End SyncLock
            Next
            Return True
        End Function

        Protected Friend Overrides Sub LoopOnRender()
            SyncLock _DrawingParamater
                For Each e In _drawingList
                    SyncLock (e)
                        Draw(e)
                    End SyncLock
                Next
            End SyncLock
        End Sub

        Protected Friend Overrides Sub MessageOnReceiver(message As String)
            SyncLock _DrawingParamater
                For Each e In _drawingList
                    SyncLock (e)
                        UpdateDrawInfo(e)
                        Draw(e)
                    End SyncLock
                Next
            End SyncLock
        End Sub

        Public MustOverride Sub UpdateDrawInfo(target As Sprite)

        Public MustOverride Sub Draw(target As Sprite)
    End Class
End Namespace