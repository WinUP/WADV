Namespace RAL.Tool
    ''' <summary>
    ''' RAL辅助工具 - 画笔组件
    ''' </summary>
    Public MustInherit Class Brush : Inherits Component.Component
        Private ReadOnly _drawingList As New List(Of Sprite)

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

        Protected Friend Overrides Sub BindToScene(sprite As Sprite, scene As Scene)
            SyncLock (_drawingList)
                _drawingList.Add(sprite)
            End SyncLock
        End Sub

        Protected Friend Overrides Sub UnbindFromScene(sprite As Sprite, scene As Scene)
            SyncLock (_drawingList)
                _drawingList.Remove(sprite)
            End SyncLock
        End Sub

        Protected Friend Overrides Sub LoopOnRender()
            For Each e In _drawingList
                SyncLock _drawingParamater
                    SyncLock (e)
                        Draw(e)
                    End SyncLock
                End SyncLock
            Next
        End Sub

        Protected Friend Overrides Sub MessageOnReceiver(message As String)
            For Each e In _drawingList
                SyncLock _drawingParamater
                    SyncLock (e)
                        Draw(e)
                    End SyncLock
                End SyncLock
            Next
        End Sub

        Public MustOverride Sub Draw(target As Sprite)
    End Class
End Namespace